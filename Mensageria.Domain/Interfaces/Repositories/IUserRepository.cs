using Mensageria.Domain.Entity;

namespace Mensageria.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<string> CadastrarAsync(UserEntity user);
    Task<UserEntity?> FindByEmailAsync(string email);
}