using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet;
using JudoPayDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SampleApp.Models;

namespace SampleApp.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IOptions<JudoConfiguration> _judoOptions;
        private ILogger<CheckoutController> _logger;

        public CheckoutController(IOptions<JudoConfiguration> judoOptions, ILogger<CheckoutController> logger)
        {
            _logger = logger;
            _judoOptions = judoOptions;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Pay()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Pay(PaymentModel model)
        {
            var result = await Transaction(model);
            if (result == null)
            {
                _logger.LogDebug("An Error occured.");
                return Error();
            }

            if (result is PaymentReceiptModel)
            {
                return View("ConfirmTransaction", result.ReceiptId.ToString());
            }

            if (result is PaymentRequiresThreeDSecureModel requiresThreeDSecureModel)
            {
                RecordTransaction(requiresThreeDSecureModel);
                return RedirectToAction("ThreeDSecure", requiresThreeDSecureModel);
            }
            
            return Error();
        }


        [HttpGet]
        [Route("Checkout/ConfirmTransaction/{receiptId?}")]
        public IActionResult ConfirmTransaction(string receiptId)
        {
            return View("ConfirmTransaction", receiptId);
        }

        public IActionResult Error()
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<ITransactionResult> Transaction(PaymentModel model)
        {
            var client = new HttpClient();
            var judocConfig = _judoOptions.Value;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format($"{judocConfig.ApiToken}:{judocConfig.ApiSecret}"))));
            client.DefaultRequestHeaders.Add("Api-Version", judocConfig.ApiVersion);

            var parameters = new Dictionary<string, object>
            {
                { "yourConsumerReference", Guid.NewGuid().ToString() },
                { "yourPaymentReference", Guid.NewGuid().ToString() },
                { "judoId", judocConfig.JudoId },
                { "amount", 1.01 },
                { "oneUseToken", model.OneUseToken },
                { "cardAddress", new CardAddressModel
                    {
                        PostCode = model.Postcode
                    }
                }
            };

            var payload = JsonConvert.SerializeObject(parameters);
            var stringContent = new StringContent(payload, Encoding.UTF8, "application/json");

            var restResponse = await client.PostAsync(new Uri(judocConfig.ApiUrl + "/transactions/preauths"), stringContent);
            var content = await restResponse.Content.ReadAsStringAsync();

            _logger.LogDebug($"Reply from partner api {content} when transacting to {judocConfig.ApiUrl}/transactions/preauths");
            if (content.ToLower().Contains("requires 3d secure"))
            {
                return JsonConvert.DeserializeObject<PaymentRequiresThreeDSecureModel>(content);
            }

            return JsonConvert.DeserializeObject<PaymentReceiptModel>(content); 
        }

        /*******************/
        /* Methods for 3DS */
        /*******************/
        [HttpGet]
        public IActionResult ThreeDSecure(PaymentRequiresThreeDSecureModel threeDSecureModel)
        {
            return View(threeDSecureModel);
        }

        [HttpGet]
        public IActionResult Acs(PaymentRequiresThreeDSecureModel threeDSecureModel)
        {
            ViewData["TermUrl"] = "http://localhost:5050/Checkout/CallbackThreeDSecure";
            return View(threeDSecureModel);
        }

        public async Task<IActionResult> CallbackThreeDSecure([FromForm] string PaRes, [FromForm] string MD)
        {
            var client = JudoPaymentsFactory.Create(_judoOptions.Value.ApiToken, _judoOptions.Value.ApiSecret, "http://localhost/partnerapi");
            var threeDResultModel = new ThreeDResultModel
            {
                Md = MD,
                PaRes = PaRes
            };

            var receiptId = RetrieveReceiptId(MD);
            var complete3DSecure = await client.ThreeDs.Complete3DSecure(receiptId, threeDResultModel);

            if (complete3DSecure.HasError)
            {
                _logger.LogDebug($"An Error occured - judopay error was: {complete3DSecure.Error.Message}");
                return Error();
            }

            return View("CallbackThreeDSecure", complete3DSecure.Response.ReceiptId.ToString());
        }

        /*****************************************/
        /* Should be stored in a persistent store*/
        /*****************************************/
        private static Dictionary<string, long> MdReceiptIdStore { get; set; } = new Dictionary<string, long>();
        private void RecordTransaction(PaymentRequiresThreeDSecureModel requiresThreeDSecureModel)
        {
            MdReceiptIdStore.Add(requiresThreeDSecureModel.Md, requiresThreeDSecureModel.ReceiptId);
        }
        private long RetrieveReceiptId(string md)
        {
            return MdReceiptIdStore[md];
        }
    }
}

public class PaymentModel
{
    public string OneUseToken { get; set; }
    public string Postcode { get; set; }
}
