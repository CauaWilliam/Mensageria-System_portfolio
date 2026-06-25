using Mensageria.Domain.Entity;
using Mensageria.Domain.Interfaces.Repositories;
using Mensageria.Infra.db.Context;
using Microsoft.EntityFrameworkCore;

namespace Mensageria.Infra.Repositories;

public class UserRepository(AppDbContext dbContext): IUserRepository
{   
    
    public async Task<string> CadastrarAsync(UserEntity user)
    {
        var response = await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
        return response.Entity.Id!;
    }

    public async Task<UserEntity?> FindByEmailAsync(string email)
    {
        var response = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        return response;
    }
}