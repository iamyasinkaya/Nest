using Microsoft.Extensions.Options;
using Nest.Domain;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Nest.Infrastructure;

public class RabbitMqService : IMessageQueueService
{
    private readonly RabbitMQSettings _rabbitMqSettings;

    public RabbitMqService(IOptions<RabbitMQSettings> rabbitMqSettings)
    {
        _rabbitMqSettings = rabbitMqSettings.Value;
    }

    public async Task SendMessageAsync<T>(T message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _rabbitMqSettings.HostName,
            UserName = _rabbitMqSettings.UserName,
            Password = _rabbitMqSettings.Password
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: _rabbitMqSettings.QueueName,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var jsonMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);

        channel.BasicPublish(exchange: "",
                             routingKey: _rabbitMqSettings.QueueName,
                             basicProperties: null,
                             body: body);

        await Task.CompletedTask;
    }
}