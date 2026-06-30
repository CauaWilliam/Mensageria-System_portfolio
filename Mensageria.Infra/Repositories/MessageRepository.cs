using Mensageria.Domain.Entity;
using Mensageria.Domain.Interfaces.Repositories;
using Mensageria.Infra.db.Context;
using Microsoft.EntityFrameworkCore;

namespace Mensageria.Infra.Repositories;

public class MessageRepository(AppDbContext dbContext): IMessageRepository
{
    public async Task<MessageEntity> CreateAsync(MessageEntity message)
    {
        var response = await dbContext.Messages.AddAsync(message);
        await dbContext.SaveChangesAsync();
        return response.Entity;
    }

    public async Task<MessageEntity?> FindByIdAsync(string id)
    {
        var response = await dbContext.Messages.FirstOrDefaultAsync(m => m.Id == id);
        return response;
    }
}