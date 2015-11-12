using System.Net;
using System.Net.Mail;

namespace VTP2015.ServiceLayer.Mail
{
    public class Mailer : IMailer
    {
        private readonly SmtpClient _client;

        public Mailer()
        {
            _client = new SmtpClient
            {
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtpmailer.howest.be",
                Credentials = new NetworkCredential("", "")
            };
        }


        public void SendMail(IMail mail)
        {
            var mailMessage = new MailMessage("dieter.jakobs@student.howest.be", "dieter.jakobs@student.howest.be")
            {
                Body = mail.Body,
                Subject = "qsfqsdfqsdf"
            };

            _client.Send(mailMessage);
        }

        public IMail ProduceMail()
        {
            return new Mail();
        }
    }
}
