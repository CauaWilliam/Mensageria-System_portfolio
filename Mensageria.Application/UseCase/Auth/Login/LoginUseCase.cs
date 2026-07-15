using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Mensageria.Application.UseCase.Auth.Login.Dto;
using Mensageria.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Mensageria.Application.UseCase.Auth.Login;

public class LoginUseCase(
    IUserRepository userRepository,
    IConfiguration configuration)
{
    public async Task<string> ExecuteAsync(LoginDto data) 
    {
        var user = await userRepository.FindByEmailAsync(data.Email);
        if (user == null)
        {
            throw new Exception("Email or password invalid"); // Mensagem genérica por segurança
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(data.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            throw new Exception("Email or password invalid");
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretKey = Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]!);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = configuration["JwtSettings:Issuer"],
            Audience = configuration["JwtSettings:Audience"],
            Expires = DateTime.UtcNow.AddHours(double.Parse(configuration["JwtSettings:ExpirationInHours"] ?? "2")),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(secretKey), 
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}