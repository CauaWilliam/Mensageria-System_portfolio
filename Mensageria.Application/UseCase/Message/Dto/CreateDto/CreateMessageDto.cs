using Mensageria.Domain.Entity;

namespace Mensageria.Application.UseCase.Message.Dto.CreateDto;

public class CreateMessageDto
{    
    public string Content { get; set; }
    public string SentAt { get; set; }
    public List<string> Emails {get; set;}
}