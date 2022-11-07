using System.Text;
using System.Text.Json;
using EmailService.Configurations;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace EmailService.RabbitMQ;

public class RabbitMqService : IRabbitMqService
{
    private readonly RabbitMqSettings _rabbitMqSettings;

    public RabbitMqService(IOptions<RabbitMqSettings> rabbitMqSettings)
    {
        _rabbitMqSettings = rabbitMqSettings.Value;
    }
    
    public void SendMessage(object obj)
    {
        var message = JsonSerializer.Serialize(obj);
        SendMessage(message);
    }

    public void SendMessage(string message)
    {
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqSettings.HostName
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        
        channel.QueueDeclare(queue: _rabbitMqSettings.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
            routingKey: _rabbitMqSettings.QueueName,
            basicProperties: null,
            body: body);
    }
}
