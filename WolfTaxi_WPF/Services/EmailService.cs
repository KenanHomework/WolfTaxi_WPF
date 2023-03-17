using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using WolfTaxi_WPF.EmailDesigns;

namespace WolfTaxi_WPF.Services
{
    public abstract class EmailService
    {
        public static void Send(string ToAdress, string subject, string body, string displayName = null)
        {
            MailMessage message = new MailMessage();
            message.To.Add(ToAdress);
            message.From = new MailAddress("wolftaxi.team@gmail.com", displayName);
            message.Subject = subject;
            message.Body = body;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Credentials = new NetworkCredential("wolftaxi.team@gmail.com", "qxnpvlnncwkvxzrl");
            smtpClient.Send(message);
        }

        public static void SendSecurityCode(string ToAdress, string subject, string code, string displayName = null)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("your_email_address@gmail.com");
            mail.To.Add(ToAdress);
            mail.Subject = "Email Verification";

            mail.IsBodyHtml = true;
            string htmlBody;

            htmlBody = EmailDesing.SecurityCode(code);

            mail.Body = htmlBody;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("wolftaxi.team@gmail.com", "qxnpvlnncwkvxzrl");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}
