using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwilioWebApp.Models
{
    public class TextMessage
    {
        protected String _Id;
        protected String _FromPhone;
        protected String _ToPhone;

        public String Id { get { return _Id; } set { _Id = value.Trim(); } }
        public String FromPhone { get { return _FromPhone; } set { _FromPhone = value.Trim(); } }
        public String ToPhone { get { return _ToPhone; } set { _ToPhone = value.Trim(); } }
        public String Message { get; set; }
        public DateTime When { get; set; }
        public String ProviderId { get; set; }
        public String Direction { get; set; }
    }
}