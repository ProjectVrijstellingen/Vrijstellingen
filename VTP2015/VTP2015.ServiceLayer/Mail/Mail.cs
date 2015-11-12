namespace VTP2015.ServiceLayer.Mail
{
    public class Mail : IMail
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
    }
}
