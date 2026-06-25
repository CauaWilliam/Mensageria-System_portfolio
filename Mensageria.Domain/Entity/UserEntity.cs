namespace Mensageria.Domain.Entity;

public class UserEntity
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}