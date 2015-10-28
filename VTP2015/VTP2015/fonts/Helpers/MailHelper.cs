using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace VTP2015.Helpers
{
    public class MailHelper
    {
        readonly SmtpClient _smtpClient = new SmtpClient();
        public MailHelper()
        {
            _smtpClient.EnableSsl = true;
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        }
        

        public void sendEmail(string sender, string receiver, string bodyText)
        {
            MailMessage mail = new MailMessage();
            mail.Subject = "Vrijstellingen platform dringende aanvraag!";
            mail.To.Add(mail.From);
            //todo replace code line above with code line underneath for live version
            //mail.To.Add(new MailAddress(receiver));
            mail.Body = bodyText;
            _smtpClient.Send(mail);
        }




    }
}