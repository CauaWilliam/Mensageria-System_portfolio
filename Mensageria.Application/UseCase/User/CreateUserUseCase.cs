using FluentValidation;
using Mensageria.Application.UseCase.User.Dto.CreateDto;
using Mensageria.Domain.Entity;
using Mensageria.Domain.Interfaces.Repositories;

namespace Mensageria.Application.UseCase.User;

public class CreateUserUseCase(
    IUserRepository userRepository,
    IValidator<CreateUserDto> validator )
{
    public async Task<string> Execute(CreateUserDto request)
    {
        var validateResult = await validator.ValidateAsync(request);
        if (!validateResult.IsValid)
        {
            throw new Exception(validateResult.Errors.First().ErrorMessage);
        }
        
        if (request.Password != request.ConfirmPassword)
        {
            throw new Exception("As senhas não são iguais");
        }
        
        var userExist = await userRepository.FindByEmailAsync(request.Email);
        if (userExist != null)
        {
            throw new Exception("Este e-mail já está cadastrado.");
        }
        
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var response = new UserEntity
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            Email = request.Email,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow.ToString("O"),
            UpdatedAt = DateTime.UtcNow.ToString("O")
        };
        return await userRepository.CreateAsync(response);
    }
}