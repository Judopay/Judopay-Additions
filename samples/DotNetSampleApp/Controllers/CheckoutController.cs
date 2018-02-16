using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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
            return View("ConfirmTransaction", result.ReceiptId.ToString());
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

        private async Task<PaymentReceiptModel> Transaction(PaymentModel model)
        {
            string judoId = _judoOptions.Value.JudoId;
            string token = _judoOptions.Value.ApiToken;
            string secret = _judoOptions.Value.ApiSecret;
            string apiVersion = _judoOptions.Value.ApiVersion;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format($"{token}:{secret}"))));
            client.DefaultRequestHeaders.Add("Api-Version", apiVersion);

            var parameters = new Dictionary<string, object>
            {
                { "yourConsumerReference", Guid.NewGuid().ToString() },
                { "yourPaymentReference", Guid.NewGuid().ToString() },
                { "judoId", judoId },
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

            var restResponse = await client.PostAsync(new Uri(_judoOptions.Value.ApiUrl + "/transactions/preauths"), stringContent);
            var readAsStringAsync = await restResponse.Content.ReadAsStringAsync();

            _logger.LogWarning($"Reply from partner api {readAsStringAsync} when transacting to {_judoOptions.Value.ApiUrl}/transactions/preauths");
            _logger.LogWarning($"Status Code of reply: {restResponse.StatusCode}, reason code: {restResponse.ReasonPhrase}");
            return JsonConvert.DeserializeObject<PaymentReceiptModel>(readAsStringAsync);
        }
    }
}

public class PaymentModel
{
    public string OneUseToken { get; set; }
    public string Postcode { get; set; }
}
