using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendingEmail
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var sender = new SmtpSender(() => new SmtpClient(host: "localhost")
            {
                EnableSsl=false,
                DeliveryMethod=SmtpDeliveryMethod.Network,
                Port=25
                //DeliveryMethod=SmtpDeliveryMethod.SpecifiedPickupDirectory,
                //PickupDirectoryLocation= @"C:\Users\asus\source\repos\SendingEmail\Sending"
            });

            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine(value: "Dear @Model.FirstName,");
            stringBuilder.AppendLine(value: "<p> Thanks for purchasing @Model.ProductName. We hope you enjoy it </p> ");
            stringBuilder.AppendLine(value: " - Kadir Aztekin");

             Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            Email.DefaultSender = sender;
            var email = await Email
                .From(emailAddress: "aztekadir@outlook.com")
                .To(emailAddress: "aztekadir@outlook.com", name: "Kadir")
                .Subject(subject: "Thanks")
                .UsingTemplate(stringBuilder.ToString(), new{ FirstName="Kadir",ProductName="Aztekin Academy"});
                //.Body(body: "Thanks for buying out product.")
                .SendAsync();
            
        }
    }
}
