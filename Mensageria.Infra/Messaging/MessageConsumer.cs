using System.Text;
using System.Text.Json;
using Mensageria.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Mensageria.Application.Common.Events;

public class MessageConsumer : BackgroundService
{
private readonly ILogger<MessageConsumer> _logger;
    private readonly IServiceProvider _serviceProvider;
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly IConfiguration _configuration;
    private const string QueueName = "messages-to-send";


    public MessageConsumer(ILogger<MessageConsumer> logger, IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMqSettings:HostName"],
            UserName = _configuration["RabbitMqSettings:UserName"],
            Password = _configuration["RabbitMqSettings:Password"],
            Port = int.Parse(_configuration["RabbitMqSettings:Port"])
        };

        _connection = await factory.CreateConnectionAsync(stoppingToken);
        _channel = await _connection.CreateChannelAsync();

        await _channel.QueueDeclareAsync(
            queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: stoppingToken
        );

        _logger.LogInformation("🚀 [Worker] Escutando a fila '{Queue}'...", QueueName);

        var consumer = new AsyncEventingBasicConsumer(_channel);
         
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var messageJson = Encoding.UTF8.GetString(body);
                
            try
            {
                var messageEvent = JsonSerializer.Deserialize<CreateMessageEvent>(messageJson);
                if(messageEvent != null)
                {
                    _logger.LogInformation("Mensagem recebida: {MessageId}", messageEvent.MessageId);
                    using(var scope = _serviceProvider.CreateScope())
                    {   
                        var messageRepository = scope.ServiceProvider.GetRequiredService<IMessageRepository>();
                        var message = await messageRepository.FindByIdAsync(messageEvent.MessageId);

                        if(message != null)
                        {
                            _logger.LogInformation("⏳ [Worker] Mensagem {Id} está agendada para o futuro ({Time}). Devolvendo para a fila.", message.Id, message.SentAt);
                            await Task.Delay(10000, stoppingToken);
                            await _channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true, cancellationToken: stoppingToken);
                            return;
                        }

                        message.Status = Domain.Entity.MessageStatus.Sent;
                        message.SentAt = DateTime.UtcNow.ToString();

                        await messageRepository.UpdateAsync(message);

                        await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
                    }
                }
                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar a mensagem: {MessageJson}", messageJson);
                await _channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true, cancellationToken: stoppingToken);
            }
        };

        await _channel.BasicConsumeAsync(
            queue: QueueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken
        );

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Parando o consumidor de mensagens...");
        if(_channel is not null) await _channel.DisposeAsync();
        if(_connection is not null) await _connection.DisposeAsync();

        await base.StopAsync(cancellationToken);
    }
}