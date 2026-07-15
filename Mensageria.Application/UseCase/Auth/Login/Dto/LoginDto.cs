namespace Mensageria.Application.UseCase.Auth.Login.Dto;

public class LoginDto
{
    /// <example>cauabeckler0@gmail.com </example>
    public string Email { get; set; }
    
    /// <example>123456</example>
    public string Password { get; set; }
}