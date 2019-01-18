using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010;
using Twilio.AspNet.Mvc;
using Twilio.Rest.Api.V2010.Account;

namespace TwilioWebApp.Controllers
{
    public class HomeController : TwilioController
    {
        public ActionResult Index()
        {
            String acctSid = ConfigurationManager.AppSettings["TwilioAccountSID"];
            String authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];

            TwilioClient.Init(acctSid, authToken);

            String fromPhoneNumber = ConfigurationManager.AppSettings["TwilioPhoneNumber"];
            String toPhoneNumber = ConfigurationManager.AppSettings["MyPhoneNumber"];

            string messageBody = "What's up @ " + DateTime.Now.ToShortTimeString();
            var message = MessageResource.Create(body: messageBody,
                from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
                to: new Twilio.Types.PhoneNumber(toPhoneNumber));

            return Content(message.Sid);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}