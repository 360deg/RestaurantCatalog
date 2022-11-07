namespace EmailService.Configurations;

public class MailSettings
{
    public const string SectionName = "MailSettings";
    
    public string Host { get; set; }
    public int Port { get; set; }
    public string Mail { get; set; }
    public string Password { get; set; }
}
