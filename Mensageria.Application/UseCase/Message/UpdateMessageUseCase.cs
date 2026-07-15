using FluentValidation;
using Mensageria.Application.UseCase.Message.Dto.UpdateDto;
using Mensageria.Domain.Interfaces.Repositories;

namespace Mensageria.Application.UseCase.Message;

public class UpdateMessageUseCase(
        IMessageRepository repository,
        IValidator<UpdateMessageDto> validator
        )
{
    public async Task ExecuteAsync(string messageId, UpdateMessageDto data, string userId)
    {
        var validateResult = await validator.ValidateAsync(data);
        if (!validateResult.IsValid)
        {
            throw new Exception(validateResult.Errors.First().ErrorMessage);
        }
        
        var message = await repository.FindByIdAsync(messageId);
        if (message == null)
        {
            throw new Exception("Mensagem não encontrada ou você não tem permissão.");
        }
        
        message.UpdateContent(data.Content);
        await repository.UpdateAsync(message);
    }
}