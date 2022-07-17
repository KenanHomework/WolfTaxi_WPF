using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace EyeTaxi_WPF.Services
{
    public abstract class EmailService
    {
        public static void Send(string ToAdress, string subject, string body, string displayName = null)
        {
            MailMessage message = new MailMessage();
            message.To.Add(ToAdress);
            message.From = new MailAddress("eyetaxi.team@gmail.com", displayName);
            message.Subject = subject;
            message.Body = body;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Credentials = new NetworkCredential("eyetaxi.team@gmail.com", "ynkipwgyattyuixm");
            smtpClient.Send(message);
        }
    }
}
