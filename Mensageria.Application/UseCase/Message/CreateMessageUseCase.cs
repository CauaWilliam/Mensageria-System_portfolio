using FluentValidation;
using Mensageria.Application.Common.Events;
using Mensageria.Application.UseCase.Message.Dto.CreateDto;
using Mensageria.Domain.Entity;
using Mensageria.Domain.Interfaces.Repositories;
using Mensageria.Domain.Interfaces.Share;
using Mensageria.Application.Common.Events.Interfaces;

namespace Mensageria.Application.UseCase.Message;

public class CreateMessageUseCase (
    IMessageRepository messageRepository,
    IValidator<CreateMessageDto> validator,
    ISnowFlakeGenerator snowFlakeGenerator,
    IIntegrationEventPublisher eventPublisher
    )
{
    public async Task<MessageEntity> Execute(CreateMessageDto data, string  userId)
    {
        var validateResult = await validator.ValidateAsync(data);
        if (!validateResult.IsValid)
        {
            throw new Exception(validateResult.Errors.First().ErrorMessage);
        }

        var messageId = snowFlakeGenerator.GenerateId();

        List<MessageRecipientEntity> recipients = data.Emails.Select(email => new MessageRecipientEntity
        {
            Id = snowFlakeGenerator.GenerateId(),
            MessageId = messageId,
            Email = email,
            Sent = false,
            CreatedAt = DateTime.UtcNow.ToString("O"),
            UpdatedAt = DateTime.UtcNow.ToString("O")
        }).ToList();


        var response = new MessageEntity
        {
            Id = messageId,
            Content = data.Content,
            Recipients = recipients,
            UserId = userId,
            CreatedAt = DateTime.UtcNow.ToString("O"),
            UpdatedAt = DateTime.UtcNow.ToString("O"),
            SentAt = data.SentAt,
            Status = MessageStatus.Pending
        };
        
        var createdMessage = await messageRepository.CreateAsync(response);

        var MessageCreatedEvent = new CreateMessageEvent(messageId);
        await eventPublisher.PublishAsync("messages-to-send", MessageCreatedEvent);
        return createdMessage;
    }
}