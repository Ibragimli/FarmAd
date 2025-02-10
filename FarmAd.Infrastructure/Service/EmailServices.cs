using FarmAd.Application.Abstractions.Services;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Text;
using MailKit.Security;
using MimeKit.Text;

namespace FarmAd.Persistence.Service
{
    public class EmailServices : IEmailServices
    {

        public void Send(string to, string subject, string html)
        {

            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("idagrouptester@yandex.ru"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.yandex.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("idagrouptester@yandex.ru", "idagroup123");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
