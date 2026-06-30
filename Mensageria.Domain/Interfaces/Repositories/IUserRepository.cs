using Mensageria.Domain.Entity;

namespace Mensageria.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<string> CreateAsync(UserEntity user);
    Task<UserEntity?> FindByEmailAsync(string email);
}