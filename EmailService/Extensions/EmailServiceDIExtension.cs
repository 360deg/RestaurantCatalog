using EmailService.Mailer;
using EmailService.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;

namespace EmailService.Extensions;

public static class EmailServiceDIExtension
{
    public static void AddEmailServiceDI(this IServiceCollection service)
    {
        service.AddTransient<IRabbitMqService, RabbitMqService>();
        service.AddTransient<IMailerService, MailerService>();

        service.AddHostedService<RabbitMqListener>();
    }
}
