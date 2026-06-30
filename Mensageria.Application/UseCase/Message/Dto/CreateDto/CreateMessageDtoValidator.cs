using FluentValidation;

namespace Mensageria.Application.UseCase.Message.Dto.CreateDto;

public class CreateMessageDtoValidator: AbstractValidator<CreateMessageDto>
{
    public CreateMessageDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotNull()
            .WithMessage("Message is required");
        
        RuleFor(x => x.Emails)
            .NotNull()
            .WithMessage("É preciso pelo menos um email");
        
    }
}