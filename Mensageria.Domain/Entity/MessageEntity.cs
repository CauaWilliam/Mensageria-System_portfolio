using System.Text.Json.Serialization;

namespace Mensageria.Domain.Entity;

public class MessageEntity
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; }  = string.Empty;
    public string Content { get; set; }  = string.Empty;
    public List<MessageRecipientEntity> Recipients { get; set; } = new();
    public MessageStatus Status  { get; set; }
    public string? SentAt { get; set; }  = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
    public string? UpdatedAt { get; set; }


    public void UpdateContent(string newContent)
    {
        if (Status != MessageStatus.Sent)
        {
            throw new InvalidOperationException("Não é possível editar uma mensagem que já foi processada ou enviada.");

            Content = newContent;
            UpdatedAt = DateTime.UtcNow.ToString("O");
        }
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MessageStatus
{
    Pending,
    Sent,
    Received,
    Failure,
}