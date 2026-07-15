using Mensageria.Application.UseCase.Message;
using Mensageria.Application.UseCase.Message.Dto.UpdateDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mensageria.API.controller.Message;

[ApiController]
[Route("api/messages")]
public class UpdateMessageController(UpdateMessageUseCase useCase) : Controller
{
    [HttpPatch("update/{id}")]
    [Authorize]
    [Tags("Messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateMessageAsync([FromRoute] string id, [FromBody] UpdateMessageDto data)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                     ?? throw new Exception("User not found");
        
        var response = useCase.ExecuteAsync(id, data, userId);
        
        return Ok(response);
    }
}