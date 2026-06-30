using Mensageria.Application.UseCase.Message;
using Mensageria.Application.UseCase.Message.Dto.CreateDto;
using Microsoft.AspNetCore.Mvc;

namespace Mensageria.API.controller.Message;

[Route("api/message/[controller]")]
[ApiController]
public class CreateMessageController(CreateMessageUseCase messageUseCase) : Controller
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateMessageDto data)
    {
        var response = await messageUseCase.Execute(data);
        return Created(string.Empty, response);
    }
}