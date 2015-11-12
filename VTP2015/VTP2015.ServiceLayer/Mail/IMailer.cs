namespace VTP2015.ServiceLayer.Mail
{
    public interface IMailer
    {
        void SendMail(IMail mail);
        IMail ProduceMail();
    }
}
