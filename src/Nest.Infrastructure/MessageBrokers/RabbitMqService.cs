using Microsoft.Extensions.Options;
using Nest.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Nest.Infrastructure
{
    public class RabbitMQService : IRabbitMQService, IDisposable
    {
        private readonly RabbitMQSettings _settings;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQService(IOptions<RabbitMQSettings> options)
        {
            _settings = options.Value;
            var factory = new ConnectionFactory()
            {
                HostName = _settings.HostName,
                UserName = _settings.UserName,
                Password = _settings.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            
            foreach (var queueName in _settings.Queues)
            {
                DeclareQueue(queueName);
            }
        }

        private void DeclareQueue(string queueName)
        {
            try
            {
                _channel.QueueDeclarePassive(queueName);
            }
            catch (Exception)
            {
                
                _channel.QueueDeclare(queue: queueName,
                                      durable: true,
                                      exclusive: false,
                                      autoDelete: false,
                                      arguments: null);
            }
        }

        public void Publish<T>(string queueName, T message)
        {
            if (!_settings.Queues.Contains(queueName))
            {
                throw new ArgumentException($"Queue '{queueName}' is not configured in appsettings.");
            }

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            _channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: properties,
                                 body: body);

            Console.WriteLine($" [x] Sent to {queueName}: {message}");
        }

        public async Task<Report> GetReportAsync(string queueName, Guid reportId, TimeSpan timeout)
        {
            if (!_settings.Queues.Contains(queueName))
            {
                throw new ArgumentException($"Queue '{queueName}' is not configured in appsettings.");
            }

            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(_channel);
            var tcs = new TaskCompletionSource<Report>();

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<Report>(Encoding.UTF8.GetString(body));

                if (message.Id == reportId)
                {
                    tcs.TrySetResult(message);
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                else
                {
                    _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                }
            };

            string consumerTag = _channel.BasicConsume(queue: queueName,
                                                      autoAck: false,
                                                      consumer: consumer);

            try
            {
                using var cts = new CancellationTokenSource(timeout);
                var completedTask = await Task.WhenAny(tcs.Task, Task.Delay(Timeout.Infinite, cts.Token));

                if (completedTask == tcs.Task)
                {
                    return await tcs.Task;
                }
                else
                {
                    throw new TimeoutException("Report retrieval timed out.");
                }
            }
            finally
            {
                try
                {
                    _channel.BasicCancel(consumerTag);
                }
                catch (Exception ex)
                {
                    /
                    Console.WriteLine($"Error while canceling consumer: {ex.Message}");
                }
            }
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}