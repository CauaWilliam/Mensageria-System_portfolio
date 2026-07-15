using Microsoft.AspNetCore.Identity;

namespace Mensageria.Domain.Entity;

public class UserEntity 
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
    public string UpdatedAt { get; set; } =  string.Empty;
}