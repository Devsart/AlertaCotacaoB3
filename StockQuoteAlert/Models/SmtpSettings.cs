using System.Collections.Generic;

namespace StockQuoteAlert.Models
{
    public sealed class SmtpSettings
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public List<string> MailList { get; set; }
    }
}
