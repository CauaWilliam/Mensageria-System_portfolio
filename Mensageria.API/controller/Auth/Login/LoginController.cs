using Mensageria.Application.UseCase.Auth.Login;
using Mensageria.Application.UseCase.Auth.Login.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mensageria.API.controller.Auth.Login;

[Route("api/auth")]
[ApiController]
public class LoginController(LoginUseCase loginUseCase): Controller
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUserAsync([FromBody] LoginDto data)
    {
        var response = await loginUseCase.ExecuteAsync(data);
        return Ok(response);
    }
}