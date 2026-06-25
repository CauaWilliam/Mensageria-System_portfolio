using FluentValidation;
using Mensageria.Application.UseCase.User;
using Mensageria.Application.UseCase.User.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace Mensageria.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddScoped<CreateUserUseCase>();
        services.AddScoped<IValidator<CreateUserRequestDto>, CreateUserRequestDtoValidator>();
        return services;
    }
}