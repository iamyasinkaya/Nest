namespace Nest.Domain;

public interface IMessageQueueService
{
    Task SendMessageAsync<T>(T message);
}