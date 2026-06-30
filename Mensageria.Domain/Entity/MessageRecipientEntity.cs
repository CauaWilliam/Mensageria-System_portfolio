namespace Mensageria.Domain.Entity;

public class MessageRecipientEntity
{
    public string Id { get; set; } = string.Empty;
    public string MessageId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Sent { get; set; } 
    public string CreatedAt { get; set; } = string.Empty;
    public string? UpdatedAt { get; set; }
}