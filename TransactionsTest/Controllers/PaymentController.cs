using Microsoft.AspNetCore.Mvc;
using TransactionsTest.Models;
using System.IO;
using TransactionsTest.Services;
using System.Text.Json;
using System.Text;
using System.Collections.Generic;
using System.Xml;
using Newtonsoft.Json;

namespace TransactionsTest.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public PaymentController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            Dictionary<string, string> cardHolderInfo = new Dictionary<string, string>();
            cardHolderInfo.Add("email", "user@sample.com");
            cardHolderInfo.Add("cardholderName", "CARDHOLDER NAME");
            string json = JsonConvert.SerializeObject(cardHolderInfo, Newtonsoft.Json.Formatting.Indented);

            var encodedUserData = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            var r = webHostEnvironment.ContentRootPath + "\\wwwroot\\" + "V5402167_20240711_T_private.key";

            AuthorizationPaymentModel apm =
                new AuthorizationPaymentModel("100", r);

            VerifyCredentialsRequestService verifyCredentialsRequestService = new VerifyCredentialsRequestService(apm);
            var res = verifyCredentialsRequestService.SignMessage();
            string signatureHex = BitConverter.ToString(res).Replace("-", "").ToUpper();
            apm.P_SIGN = signatureHex;
            apm.M_INFO = encodedUserData;
           
            return View("PaymentForm",apm );
        }
        public IActionResult ServerResponse()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult VerifyPayment(CardInputModel model)
        //{
        //    Dictionary<string,string> cardHolderInfo=new Dictionary<string,string>();
        //    cardHolderInfo.Add("email", "user@sample.com");
        //    cardHolderInfo.Add("cardholderName", model.Name);
        //    string json = JsonConvert.SerializeObject(cardHolderInfo,Newtonsoft.Json.Formatting.Indented);

        //    var encodedUserData= System.Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

        //    var r = webHostEnvironment.ContentRootPath + "\\wwwroot\\" + "V5402167_20240711_T_private.key";

        //    AuthorizationPaymentModel apm =
        //        new AuthorizationPaymentModel("100", encodedUserData, r);

        //  VerifyCredentialsRequestService verifyCredentialsRequestService=new VerifyCredentialsRequestService(apm);
        //   var res= verifyCredentialsRequestService.SignMessage();
        //    string signatureHex = BitConverter.ToString(res).Replace("-", "").ToUpper();
        //    apm.P_SIGN = signatureHex ;
        //    HttpClient client = new HttpClient();

        //    Dictionary<string, string> data = new Dictionary<string, string>();
        //    data.Add("TERMINAL", apm.TERMINAL);
        //    data.Add("TRTYPE", apm.TRTYPE);
        //    data.Add("AMOUNT", apm.AMOUNT);
        //    data.Add("CURRENCY", apm.CURRENCY);
        //    data.Add("ORDER", apm.ORDER);
        //    data.Add("DESC", apm.DESC);
        //    data.Add("MERCHANT", apm.MERCHANT);
        //    data.Add("MERCH_NAME", apm.MERCH_NAME);
        //    data.Add("AD.CUST_BOR_ORDER_ID", apm.AD_CUST_BOR_ORDER_ID);
        //    data.Add("TIMESTAMP", apm.TIMESTAMP);
        //    data.Add("M_INFO", apm.M_INFO);
        //    data.Add("NONCE", apm.NONCE);
        //    data.Add("P_SIGN", apm.P_SIGN);

        //    FormUrlEncodedContent content = new FormUrlEncodedContent(data);
        //    var result= client.PostAsJsonAsync(verifyCredentialsRequestService._gate, data).Result;
        //   var contentResult= result.Content.ReadAsStringAsync().Result;
          
        //    return View();
        //}
    }
}
