using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using StockQuoteAlert.Models;

namespace StockQuoteAlert.Controllers
{
    public class MailHandler
    {
        public static SmtpClient GetClient(SmtpSettings settings)
        {
            SmtpClient smtp = new();
            smtp.Host = settings.Host;
            smtp.Port = settings.Port;

            smtp.Credentials = new NetworkCredential(
                settings.Mail, settings.Password);
            smtp.EnableSsl = true;
            return smtp;
        }
        public static List<string> GetMailList(SmtpSettings settings)
        {
            List<string> list = settings.MailList;
            return list;
        }
        public static string GetMailFrom(SmtpSettings settings)
        {
            string from = settings.Mail;
            return from;
        }
        public static void SendMail(SmtpClient smtp, List<string> mailList, string from, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            foreach (var address in mailList)
            {
                try
                {
                    mail.To.Add(address);
                }
                catch(Exception e)
                {
                    Console.WriteLine("String fornecida não possui um formato de e-mail válido. Verifique as configurações.", e.Message );
                }
            }

            mail.Subject = subject;

            mail.Body = body;

            Console.WriteLine("Sending email...");
            smtp.Send(mail);
            Console.WriteLine("E-mail sent!");
        }
    }

}
