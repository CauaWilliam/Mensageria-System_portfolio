namespace Mensageria.Application.UseCase.User.Dto.CreateDto;

public class CreateUserDto
{
   public string Name { get; set; }
   public string Email { get; set; }
   public string Password { get; set; }
   public string ConfirmPassword { get; set; }
}