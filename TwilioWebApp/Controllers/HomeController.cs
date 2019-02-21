using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;

using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010;
using Twilio.AspNet.Mvc;
using Twilio.Rest.Api.V2010.Account;
using TwilioWebApp.Data;
using TwilioWebApp.Models;
using Twilio.TwiML;

namespace TwilioWebApp.Controllers
{
    public class HomeController : TwilioController
    {

        public ActionResult Index()
        {
            return View();
        }
        // localhost:56660/home/SendText/_5E710IX21
        public ActionResult SendText(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return Content("No requestId provided");
            }
            else
            {

                id = id.Trim();
                String acctSid = ConfigurationManager.AppSettings["TwilioAccountSID"];
                String authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];

                TwilioClient.Init(acctSid, authToken);

                ITextMessagePersistor reqHandler = new DbTextMessagePersistor();
                TextMessage request = reqHandler.Get(id);
                request.Direction = Constants.Direction.Outbound;

                if (String.IsNullOrEmpty(request.FromPhone))
                    request.FromPhone = ConfigurationManager.AppSettings["TwilioPhoneNumber"];

                var message = MessageResource.Create(body: request.Message,
                    from: new Twilio.Types.PhoneNumber(request.FromPhone),
                    to: new Twilio.Types.PhoneNumber(request.ToPhone));

                request.When = DateTime.Now;
                request.ProviderId = message.Sid;

                reqHandler.Put(request);

                return Content(message.ToString());
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [HttpPost]
        public TwiMLResult ReceiveText()
        {
            NameValueCollection formData = this.Request.Form;
        
            TextMessage reply = new TextMessage();
            reply.Id = Guid.NewGuid().ToString();
            reply.FromPhone = formData["From"].ToString();
            reply.ToPhone = formData["To"].ToString();
            reply.When = DateTime.Now;
            reply.Message = formData["Body"];
            reply.ProviderId = formData["MessageSid"];
            reply.Direction = Constants.Direction.Inbound;

            new DbTextMessagePersistor().Put(reply);

            var messagingResponse = new MessagingResponse();
            messagingResponse.Message("10-4 Good buddy");

            return TwiML(messagingResponse);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}