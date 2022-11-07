namespace EmailService.Configurations;

public class RabbitMqSettings
{
    public const string SectionName = "RabbitMqSettings";
    
    public string HostName { get; set; }
    public string QueueName { get; set; }
}
