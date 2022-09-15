using System;
using System.Collections.Generic;

namespace StockQuoteAlert.Models
{
    public class MailRequest
    {
        public List<string> MailToList { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
