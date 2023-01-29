using System.Security.Claims;
using Application.UseCases.Conversations;
using Application.UseCases.Conversations.Dtos;
using Application.UseCases.Message;
using Application.UseCases.Message.Dtos;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShelha.Controllers.User;

[ApiController]
[Route("api/v1/user/messaging")]
public class MessageController : ControllerBase
{
    private readonly UseCaseCreateMessage useCaseCreateMessage;
    private readonly UseCaseGetAllMessageOfConversation _useCaseGetAllMessageOfConversation;
    private readonly UseCaseGetAllMessages _useCaseGetAllMessages;

    public MessageController(UseCaseCreateMessage useCaseCreateMessage, UseCaseGetAllMessageOfConversation useCaseGetAllMessageOfConversation, UseCaseGetAllMessages useCaseGetAllMessages)
    {
        this.useCaseCreateMessage = useCaseCreateMessage;
        _useCaseGetAllMessageOfConversation = useCaseGetAllMessageOfConversation;
        _useCaseGetAllMessages = useCaseGetAllMessages;
    }

    [HttpPost]
    [Microsoft.AspNetCore.Authorization.Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<DtoOutputMessage> sendPrivateMessage(DtoInputMessage dto)
    {
        try
        {
            dto.IdSender = Int32.Parse(User.FindFirstValue("id"));
            return Ok(useCaseCreateMessage.Execute(dto));
        }
        catch (InvalidOperationException e)
        {
            return StatusCode(403, e.Message);
        }
    }
    
    [HttpGet]
    [Route("conversation/{slug}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<IEnumerable<DtoOutputConversation>> getMessagesOfConversation(string slug)
    {
        try
        {
            return Ok(_useCaseGetAllMessageOfConversation.Execute(slug));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }
    
    [HttpGet]
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Administrateur")]
    public ActionResult<IEnumerable<DtoOutputConversation>> FetchAll()
    {
        return Ok(_useCaseGetAllMessages.Execute());
    }
}