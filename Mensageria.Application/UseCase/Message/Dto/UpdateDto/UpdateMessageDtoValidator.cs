using FluentValidation;

namespace Mensageria.Application.UseCase.Message.Dto.UpdateDto;

public class UpdateMessageDtoValidator : AbstractValidator<UpdateMessageDto>
{
    public UpdateMessageDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotNull()
            .NotEmpty();
    }
}