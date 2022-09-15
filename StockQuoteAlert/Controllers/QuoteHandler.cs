using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using StockQuoteAlert.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace StockQuoteAlert.Controllers
{
    public class QuoteHandler
    {
        public static HttpClient GetClient(string url)
        {
            HttpClient client = new();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public static void VerificaPreco(HttpClient client,Quote quote, bool alertaVendaAtivo, bool alertaCompraAtivo, SmtpClient smtp, List<string> mailList, string mailFrom)
        {
            HttpResponseMessage response = client.GetAsync(quote.Name).Result;
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var jsonString = response.Content.ReadAsStringAsync().Result;
                var json = JObject.Parse(jsonString);
                var result = json["results"];
                float preco = result[0].Value<float>("regularMarketPrice");
                if (preco >= quote.UpperValue && alertaVendaAtivo == true)
                {
                    alertaVendaAtivo = false;
                    alertaCompraAtivo = true;
                    MailHandler.SendMail(smtp, mailList, mailFrom, $@"Venda {quote.Name} agora mesmo!", $@"O preço da ação {quote.Name} subiu e atingiu o limite superior de R${quote.UpperValue}. Venda agora mesmo e garanta seu lucro!");
                    VerificaPreco(client, quote, alertaVendaAtivo, alertaCompraAtivo, smtp, mailList, mailFrom);
                }
                else if (preco <= quote.LowerValue && alertaCompraAtivo == true)
                {
                    alertaVendaAtivo = true;
                    alertaCompraAtivo = false;
                    MailHandler.SendMail(smtp, mailList, mailFrom, $@"Compre {quote.Name} agora mesmo!", $@"O preço da ação {quote.Name} caiu e atingiu o limite inferior de R${quote.LowerValue}. Compre agora mesmo e garanta sua posição!");
                    VerificaPreco(client, quote, alertaVendaAtivo, alertaCompraAtivo, smtp, mailList, mailFrom);
                }
                VerificaPreco(client, quote, alertaVendaAtivo, alertaCompraAtivo, smtp, mailList, mailFrom);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                throw new HttpListenerException((int)response.StatusCode, "Falha ao requisitar API. favor verificar endereço e parâmetros.");
            }

        }




    }
}
