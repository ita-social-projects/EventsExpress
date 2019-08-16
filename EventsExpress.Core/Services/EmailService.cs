using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EventsExpress.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration iConfig)
        {
            _configuration = iConfig;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var from = _configuration.GetValue<string>("EmailSenderOptions:GoogleAccount");
            var pass = _configuration.GetValue<string>("EmailSenderOptions:Password");

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(@from, pass),
                EnableSsl = true
            };
            var mail = new MailMessage(@from, email)
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            return client.SendMailAsync(mail);
        }

        public Task SendEmailAsync(EmailDTO emailDTO)
        {
            var client = new SmtpClient
            {
                Port = 2525,
                Host = "localhost"
            };
            var email = new MailMessage()
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
