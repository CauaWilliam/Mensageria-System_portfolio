using FluentValidation;

namespace Mensageria.Application.UseCase.User.Dto;

public class CreateUserRequestDtoValidator: AbstractValidator<CreateUserRequestDto>
{
    public CreateUserRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatorio")
            .NotNull()
            .MaximumLength(50);
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail informado não é válido.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("A confirmação de senha é obrigatória.")
            .Equal(x => x.Password).WithMessage("As senhas informadas não coincidem.");
    }
}