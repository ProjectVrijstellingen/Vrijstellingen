namespace VTP2015.ServiceLayer.Mail
{
    public interface IMail
    {
        string From { get; set; }
        string To { get; set; }
        string Body { get; set; }
    }
}
