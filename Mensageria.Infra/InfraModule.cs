using Mensageria.Domain.Interfaces.Repositories;
using Mensageria.Infra.db.Context;
using Mensageria.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mensageria.Infra;

public static class InfraModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(p => p.UseNpgsql(connectionString));
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}