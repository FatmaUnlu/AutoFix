using AutoFix.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AutoFix.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string SenderMail => _configuration.GetSection("EmailOptions:SenderMail").Value;
        public string Password => _configuration.GetSection("EmailOptions:Password").Value;
        public string Smtp => _configuration.GetSection("EmailOptions:Smtp").Value;
        public int SmtpPort => Convert.ToInt32(_configuration.GetSection("EmailOptions:SmtpPort").Value);

        public async Task SendAsyc(EmailMessage message)
        {
            var mail = new MailMessage { From = new MailAddress(this.SenderMail) };

            foreach (var c in message.Contacts)
            {
                mail.To.Add(c);

            }
            if (message.Cc != null && message.Cc.Length > 0)
            {
                foreach (var cc in message.Cc)
                {
                    mail.CC.Add(new MailAddress(cc));

                }
            }
            if (message.Bcc != null && message.Bcc.Length > 0)
            {
                foreach (var bcc in message.Bcc)
                {
                    mail.Bcc.Add(new MailAddress(bcc));

                }
            }

            mail.Subject = message.Subject;
            mail.Body = message.Body;

            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.UTF8;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.HeadersEncoding = Encoding.UTF8;

            var smptClient = new SmtpClient(this.Smtp, this.SmtpPort)
            {
                Credentials = new NetworkCredential(this.SenderMail, this.Password),
                EnableSsl = true
            };
            await smptClient.SendMailAsync(mail);



        }
    }
}
