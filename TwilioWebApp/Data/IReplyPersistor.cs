﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwilioWebApp.Models;

namespace TwilioWebApp.Data
{
    interface IReplyPersistor
    {
        void Put(TextReply reply);
    }
}