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
using Newtonsoft.Json;
using SampleApp.Models;

namespace SampleApp.Controllers
{
    public class CheckoutController : Controller
    {
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
            const string judoId = "<YOUR JUDO ID>";
            const string token = "<YOUR TOKEN>";
            const string secret = "<YOUR SECRET>";
            const string apiVersion = "5.5.0";

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

            var restResponse = await client.PostAsync(new Uri("https://gw1.judopay-sandbox.com/transactions/preauths"), stringContent);
            var readAsStringAsync = await restResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<PaymentReceiptModel>(readAsStringAsync);
        }
    }
}

public class PaymentModel
{
    public string OneUseToken { get; set; }
    public string Postcode { get; set; }
}
