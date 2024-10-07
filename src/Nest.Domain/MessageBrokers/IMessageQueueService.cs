namespace Nest.Domain;

public interface IRabbitMQService
{
    void Publish<T>(string queueName, T message);
    Task<Report> GetReportAsync(string queueName, Guid reportId, TimeSpan timeout);
}