using FluentValidation;
using Mensageria.Application.UseCase.Message;
using Mensageria.Application.UseCase.Message.Dto.CreateDto;
using Mensageria.Application.UseCase.User;
using Mensageria.Application.UseCase.User.Dto.CreateDto;
using Microsoft.Extensions.DependencyInjection;

namespace Mensageria.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddScoped<CreateUserUseCase>();
        services.AddScoped<IValidator<CreateUserDto>, CreateUserRequestDtoValidator>();
        services.AddScoped<CreateMessageUseCase>();
        services.AddScoped<IValidator<CreateMessageDto>, CreateMessageDtoValidator>();
        return services;
    }
}