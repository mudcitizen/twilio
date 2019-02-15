using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwilioWebApp.Models
{
    public class TextRequest
    {
        protected String _Id;
        protected String _ToPhone;

        public String Id { get { return _Id; } set { _Id = value.Trim(); } }
        public String ToPhone { get { return _ToPhone; }  set { _ToPhone = value.Trim(); } }
        public String Body { get; set; }
        public DateTime RequestTime { get; set; }

        public DateTime? ResponseTime { get; set; }

        public String Response { get; set; }



    }
}