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

                String fromPhoneNumber = ConfigurationManager.AppSettings["TwilioPhoneNumber"];
                IRequestPersistor reqHandler = new DbRequestPersistor();
                TextRequest request = reqHandler.Get(id);

                var message = MessageResource.Create(body: request.Body,
                    from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
                    to: new Twilio.Types.PhoneNumber(request.ToPhone));

                request.ResponseTime = DateTime.Now;
                request.Response = message.Sid;
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
        
            TextReply reply = new TextReply();
            reply.Id = Guid.NewGuid().ToString();
            reply.FromPhone = formData["From"].ToString();
            reply.Received = DateTime.Now;
            reply.Message = formData["Body"];
            reply.ProviderId = formData["MessageSid"];

            new DbReplyPersistor().Put(reply);

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