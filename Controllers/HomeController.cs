using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2_Factor_Authentication_with_OTP_Token.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult Login()
        {
            var username = Request["username"];
            var password = Request["password"];

            if (username == "test" && password=="test")
            {
                var request = (HttpWebRequest)WebRequest.Create("https://rest.nexmo.com/sms/json");

                var secret = "TEST_SECRET";

                var postData = "api_key=5c4147872ce1081f1799879aea7011cc-7bbbcb78-d60b2e44";
                postData += "&api_secret=f96ddf6b57ff9765e94702ca5f0193a2-7bbbcb78-688cb712";
                postData += "&to=184.173.153.194";
                postData += "&from=\"\"NEXMO\"\"";
                postData += "&text=\"" + secret + "\"";
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                ViewBag.Message = responseString;
            }

            else
            {
                ViewBag.Message = "Wrong Credentials";
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}