using Mensageria.Domain.Entity;

namespace Mensageria.Domain.Interfaces.Repositories;

public interface IMessageRepository
{
    Task<MessageEntity> CreateAsync(MessageEntity message);
    Task<MessageEntity?> FindByIdAsync(string id);
}