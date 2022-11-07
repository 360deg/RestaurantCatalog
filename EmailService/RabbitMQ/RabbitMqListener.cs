using System.Text;
using EmailService.Configurations;
using EmailService.Mailer;
using RabbitMQ.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;

namespace EmailService.RabbitMQ;

public class RabbitMqListener : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IMailerService _mailerService;
    private readonly RabbitMqSettings _rabbitMqSettings;
    
    public RabbitMqListener(IMailerService mailerService, IOptions<RabbitMqSettings> rabbitMqSettings)
    {
        _rabbitMqSettings = rabbitMqSettings.Value;
        var factory = new ConnectionFactory { HostName = _rabbitMqSettings.HostName };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _mailerService = mailerService;
        _channel.QueueDeclare(queue: _rabbitMqSettings.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
			
            _mailerService.SendEmailAsync(content);

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(_rabbitMqSettings.QueueName, false, consumer);

        return Task.CompletedTask;
    }
	
    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}