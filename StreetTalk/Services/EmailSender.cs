using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace StreetTalk.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration Configuration;

        public EmailSender(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            Console.WriteLine("SendEmailAsync");
            await Execute(subject, message, email);
        }

        private async Task Execute(string subject, string message, string email)
        {
            var key = Configuration.GetSection("SendGrid").GetValue<string>("Key");
            var name = Configuration.GetSection("SendGrid").GetValue<string>("User");
            
            Console.WriteLine($"key:{key} from:{name} to:{email}");
            
            var client = new SendGridClient(key);
            
            var msg = new SendGridMessage
            {
                From = new EmailAddress("streettalk.buurtapp@gmail.com", name),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            
            msg.AddTo(new EmailAddress(email));
            msg.SetClickTracking(false, false);

            await client.SendEmailAsync(msg);
        }
    }
}
