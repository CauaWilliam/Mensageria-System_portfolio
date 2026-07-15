using FluentValidation;
using Mensageria.Application.Common.Events.Interfaces;
using Mensageria.Application.UseCase.Auth.Login;
using Mensageria.Application.UseCase.Auth.Login.Dto;
using Mensageria.Application.UseCase.Message;
using Mensageria.Application.UseCase.Message.Dto.CreateDto;
using Mensageria.Application.UseCase.Message.Dto.UpdateDto;
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
        services.AddScoped<UpdateMessageUseCase>();
        services.AddScoped<IValidator<UpdateMessageDto>,  UpdateMessageDtoValidator>();
        services.AddScoped<IValidator<CreateMessageDto>, CreateMessageDtoValidator>();
        services.AddScoped<LoginUseCase>();
        services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
        return services;
    }
}