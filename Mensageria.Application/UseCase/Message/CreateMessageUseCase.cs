using FluentValidation;
using Mensageria.Application.UseCase.Message.Dto.CreateDto;
using Mensageria.Domain.Entity;
using Mensageria.Domain.Interfaces.Repositories;
using Mensageria.Domain.Interfaces.Share;

namespace Mensageria.Application.UseCase.Message;

public class CreateMessageUseCase (
    IMessageRepository messageRepository,
    IValidator<CreateMessageDto> validator,
    ISnowFlakeGenerator snowFlakeGenerator
    )
{
    public async Task<MessageEntity> Execute(CreateMessageDto request)
    {
        var validateResult = await validator.ValidateAsync(request);
        if (!validateResult.IsValid)
        {
            throw new Exception(validateResult.Errors.First().ErrorMessage);
        }

        var messageId = snowFlakeGenerator.GenerateId();

        List<MessageRecipientEntity> recipients = request.Emails.Select(email => new MessageRecipientEntity
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
            Content = request.Content,
            Recipients = recipients,
            UserId = "243b4fce-51f7-4f82-8a05-a6d1ed445799",
            CreatedAt = DateTime.UtcNow.ToString("O"),
            UpdatedAt = DateTime.UtcNow.ToString("O"),
            SentAt = null,
            Status = MessageStatus.Pending
        };
        
        
        return await messageRepository.CreateAsync(response);
    }
}