using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.Services
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var from = "q.u.i.c.k.sender.r.r@gmail.com";
            var pass = "quicksender123";
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(from, pass);
            client.EnableSsl = true;
            var mail = new MailMessage(from, email);
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;
            return client.SendMailAsync(mail);
        }

        public Task SendEmailAsync(EmailDTO emailDTO)
        {
            SmtpClient client = new SmtpClient
            {
                Port = 2525,
                Host = "localhost"
            };
            MailMessage email = new MailMessage()
            {
                From = new MailAddress(emailDTO.SenderEmail),
                Body = emailDTO.MessageText,
                IsBodyHtml = true,
                Sender = new MailAddress(emailDTO.SenderEmail),
            };
            email.To.Add(new MailAddress(emailDTO.RecepientEmail));
            return client.SendMailAsync(email);
        }

    }
}
