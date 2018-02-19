using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SampleApp.Models;

namespace SampleApp.Controllers
{
    public class CheckoutController : Controller
    {
        private const string JudoId = "<YOUR JUDO ID>";
        private const string Token = "<YOUR TOKEN>";
        private const string Secret = "<YOUR SECRET>";
        private const string ApiVersion = "5.5.0";

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
            if (result.Response is PaymentReceiptModel)
            {
                return View("ConfirmTransaction", result.Response.ReceiptId.ToString());
            }
            else if (result.Response is PaymentRequiresThreeDSecureModel)
            {
                return View("ThreeDSecure", result.Response as PaymentRequiresThreeDSecureModel);
            }
            return Error();
        }

        [HttpGet]
        public IActionResult ConfirmTransaction(string receiptId)
        {
            return View(receiptId);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult CallbackThreeDSecure([FromBody] string PaRes, [FromBody] string MD)
        {
            var client = JudoPaymentsFactory.Create(JudoPayDotNet.Enums.JudoEnvironment.Sandbox, Token, Secret);
            new ThreeDResultModel
            {
                Md = MD,
                PaRes = PaRes
            };
            client.ThreeDs.Complete3DSecure()
        }

        private async Task<IResult<ITransactionResult>> Transaction(PaymentModel model)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format($"{Token}:{Secret}"))));
            client.DefaultRequestHeaders.Add("Api-Version", ApiVersion);

            var parameters = new Dictionary<string, object>
            {
                { "yourConsumerReference", Guid.NewGuid().ToString() },
                { "yourPaymentReference", Guid.NewGuid().ToString() },
                { "judoId", JudoId },
                { "amount", 1.01 },
                { "oneUseToken", model.OneUseToken },
                { "expiryDate", "1220" },
                { "cv2", "452" },
                {"cardAddress", new CardAddressModel
                                {
                                    PostCode = model.Postcode
                                }
                }
            };
            var payload = JsonConvert.SerializeObject(parameters);
            var stringContent = new StringContent(payload, Encoding.UTF8, "application/json");

            var restResponse = await client.PostAsync(new Uri("https://gw1.judopay-sandbox.com/transactions/preauths"), stringContent);
            var content = await restResponse.Content.ReadAsStringAsync();
            if (content.Contains("receiptid"))
            {
                return JsonConvert.DeserializeObject<Result<PaymentReceiptModel>>(content);
            }

            if (content.Contains("AcsUrl"))
            {
                return JsonConvert.DeserializeObject<Result<PaymentRequiresThreeDSecureModel>>(content);
            }

            throw new Exception($"Unexpected response in return: {content}");
        }
    }
}

public class PaymentModel
{
    public string OneUseToken { get; set; }
    public string Postcode { get; set; }
}
