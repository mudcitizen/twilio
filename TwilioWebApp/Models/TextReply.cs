using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwilioWebApp.Models
{
    public class TextReply
    {
        protected String _Id;
        protected String _FromPhone;

        public String Id { get { return _Id; } set { _Id = value.Trim(); } }
        public String FromPhone { get { return _FromPhone; } set { _FromPhone = value.Trim(); } }
        public String Message { get; set; }
        public DateTime Received { get; set; }

        public String ProviderId { get; set; }

    }
}