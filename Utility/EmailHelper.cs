using HareKrishnaNamaHattaWebApp.Data;
using HareKrishnaNamaHattaWebApp.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HareKrishnaNamaHattaWebApp.Utility
{
    public static class EmailHelper
    {        
        public static async Task SendEmailAsync(string toEmail, string subject, string body, string smtpUser, string smtpPassWord, string smtpHost, int smtpPort, string fromEmail)
        {
            //Need to remove later
            toEmail = "sheelaswathi2013@gmail.com";


            var message = new MailMessage();
            message.To.Add(toEmail);
            message.From = new MailAddress(fromEmail, "Hare Krishna Temple");
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var client = new SmtpClient(smtpHost, smtpPort))
            {
                client.Credentials = new NetworkCredential(smtpUser, smtpPassWord);
                client.EnableSsl = true;
                await client.SendMailAsync(message);
            }
        }



    }
}
