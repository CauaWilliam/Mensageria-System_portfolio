using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Mensageria.Application.Common.Events.Interfaces;

namespace Mensageria.Infrastructure.Messaging;

public class RabbitMqEventPublisher : IIntegrationEventPublisher, IAsyncDisposable
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public RabbitMqEventPublisher(IConfiguration configuration)
    {
        var factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMqSettings:HostName"] ?? "localhost",
            UserName = configuration["RabbitMqSettings:UserName"] ?? "guest",
            Password = configuration["RabbitMqSettings:Password"] ?? "guest",
            Port = int.Parse(configuration["RabbitMqSettings:Port"] ?? "5672")
        };

        _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();

        _channel.QueueDeclareAsync(
            queue: "messages-to-send",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        ).GetAwaiter().GetResult();
    }

    public async Task PublishAsync<T>(string queueName, T @event)
    {
        var json = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(json);

        var properties = new BasicProperties(); 

        await _channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: queueName,
            mandatory: false,
            basicProperties: properties, 
            body: body
        );
    }

    public async ValueTask DisposeAsync()
    {
        if (_channel is not null)
            await _channel.DisposeAsync();

        if (_connection is not null)
            await _connection.DisposeAsync();
    }
}