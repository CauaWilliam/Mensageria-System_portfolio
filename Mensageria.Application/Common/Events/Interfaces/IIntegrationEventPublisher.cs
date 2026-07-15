namespace Mensageria.Application.Common.Events.Interfaces;
public interface IIntegrationEventPublisher
{
    Task PublishAsync<T>(string queueName, T @event);
}