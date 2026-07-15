using Mensageria.Application.UseCase.Message;
using Mensageria.Application.UseCase.Message.Dto.CreateDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mensageria.API.controller.Message;

[Route("api/messages")]
[ApiController]
[Tags("Messages")]
public class CreateMessageController(CreateMessageUseCase messageUseCase) : Controller
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateMessageDto data)
    {
        
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
            ?? throw new Exception("User not found");
        
        
        var response = await messageUseCase.Execute(data,  userId);
        return Created(string.Empty, response);
    }
}