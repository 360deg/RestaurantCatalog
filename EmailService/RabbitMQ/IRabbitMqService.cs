namespace EmailService.RabbitMQ;

public interface IRabbitMqService
{
    void SendMessage(object obj);
    void SendMessage(string message);
}
