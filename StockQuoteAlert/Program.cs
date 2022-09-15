using System;
using System.Collections.Generic;
using System.Net.Mail;
using StockQuoteAlert.Models;
using Microsoft.Extensions.Configuration;
using StockQuoteAlert.Controllers;
using System.Net.Http;


namespace StockQuoteAlert
{
    class Program
    {
        static void Main(string[] args)
        {
            Quote quote = VerificaArgs(args);

            bool alertaVendaAtivo = true;
            bool alertaCompraAtivo = true;
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json")
            .AddEnvironmentVariables()
            .Build();
            SmtpSettings settings = config.GetRequiredSection("SmtpSettings").Get<SmtpSettings>();
            settings.Password = config[settings.Password];
            List<string> mailList = MailHandler.GetMailList(settings);
            string mailFrom = MailHandler.GetMailFrom(settings);
            SmtpClient smtp = MailHandler.GetClient(settings);

            string API_URL = "https://brapi.dev/api/quote/";

            HttpClient client = QuoteHandler.GetClient(API_URL);

            QuoteHandler.VerificaPreco(client, quote, alertaVendaAtivo, alertaCompraAtivo, smtp, mailList, mailFrom);


        }
        static Quote VerificaArgs(string[] args)
        {
            if (args.Length == 3 && float.TryParse(args[1], out float uv) == true && float.TryParse(args[2], out float lv) == true)
            {
                return new Quote(args[0], uv, lv);
            }
            else
            {
                throw new ArgumentNullException("Por favor, insira os argumentos de linha de comando no formato adequado. Ex.: PETR4 32,18 31,14");
            }
        }
    }
}
