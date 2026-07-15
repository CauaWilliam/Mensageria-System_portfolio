using FluentValidation;

namespace Mensageria.Application.UseCase.Auth.Login.Dto;

public class LoginDtoValidator: AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(l => l.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty();
    }
}