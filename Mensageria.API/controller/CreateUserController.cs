using Mensageria.Application.UseCase.User;
using Mensageria.Application.UseCase.User.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mensageria.API.controller;

[Route("api/users/[controller]")]
[ApiController]
public class CreateUserController(CreateUserUseCase createUserUseCase) : Controller
{
    [HttpPost("/create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequestDto data)
    {
        var response = await createUserUseCase.Execute(data);
        return Created(string.Empty, response);
    }
}