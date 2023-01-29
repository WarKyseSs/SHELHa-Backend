using System.Net;
using System.Security.Claims;
using Application.UseCases.Conversations;
using Application.UseCases.Conversations.Dtos;
using Application.UseCases.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShelha.Controllers.User;

[ApiController]
[Route("api/v1/user/conversation")]
public class ConversationController : ControllerBase
{
    private readonly UseCaseGetConversations _useCaseGetConversations;
    private readonly UseCaseCreateConversation _useCaseCreateConversation;
    private readonly UseCaseGetConversationBySlug _useCaseGetConversationBySlug;

    
    public ConversationController(UseCaseGetConversations useCaseGetConversations, UseCaseCreateConversation useCaseCreateConversation, UseCaseGetConversationBySlug useCaseGetConversationBySlug)
    {
        _useCaseGetConversations = useCaseGetConversations;
        _useCaseCreateConversation = useCaseCreateConversation;
        _useCaseGetConversationBySlug = useCaseGetConversationBySlug;
    }


    [HttpGet]
    [Route("UserConnected")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<IEnumerable<DtoOutputConversation>> getConversationsOfUserConnected()
    {
        var conversations = _useCaseGetConversations.Execute(Int32.Parse(User.FindFirstValue("id")));
        if (conversations.Any())
        {
            return Ok(conversations);
        }
        return NotFound("Vous n'avez aucune conversation");
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<DtoOutputConversation> createConversation( DtoInputConversation  dto)
    {
        //set id of user connected
        dto.IdOfSender = Int32.Parse(User.FindFirstValue("id"));
        var conversation = _useCaseCreateConversation.Execute(dto);
        if (conversation != null)
        {
            return StatusCode(201,conversation);
        }

        return BadRequest("Conversation non crée");
    }
    
    [HttpGet]
    [Route("user{id:int}")]
    [Authorize(Roles = "Administrateur")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputConversation>> getConversationsOfUser(int id)
    {
        return Ok(_useCaseGetConversations.Execute(id));
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    [Route("{slug}")]
    public ActionResult<IEnumerable<DtoOutputConversation>> getConversationBySlug(string slug)
    {
        var conversation = _useCaseGetConversationBySlug.Execute(slug, Int32.Parse(User.FindFirstValue("id")));
        
        if (conversation == null)
        {
            return NotFound();
        }
        return Ok(conversation);
    }
}