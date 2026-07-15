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

    public async Task UpdateAsync(MessageEntity message)
    {
        dbContext.Messages.Update(message);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(string messageId, int status)
    {
        var message = await dbContext.Messages.FirstOrDefaultAsync(m => m.Id == messageId);
        if (message != null)
        {
            message.Status = (MessageStatus)status; 
            dbContext.Messages.Update(message);
            await dbContext.SaveChangesAsync();
        }
    }
}