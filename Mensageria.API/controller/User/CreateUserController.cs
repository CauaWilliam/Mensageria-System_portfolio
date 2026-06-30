using Mensageria.Application.UseCase.User;
using Mensageria.Application.UseCase.User.Dto.CreateDto;
using Microsoft.AspNetCore.Mvc;

namespace Mensageria.API.controller.User;

[Route("api/users/[controller]")]
[ApiController]
public class CreateUserController(CreateUserUseCase createUserUseCase) : Controller
{
    [HttpPost("/create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto data)
    {
        var response = await createUserUseCase.Execute(data);
        return Created(string.Empty, response);
    }
}
