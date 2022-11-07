namespace EmailService.Mailer;

public interface IMailerService
{
    Task SendEmailAsync(string message);
}
