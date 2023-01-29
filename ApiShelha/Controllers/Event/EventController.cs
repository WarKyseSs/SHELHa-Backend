using System.Security.Claims;
using Application.UseCases.Events;
using Application.UseCases.Events.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShelha.Controllers.Event;

[ApiController]
[Route("api/v1/events")]
public class EventController : ControllerBase
{
    private readonly UseCaseFetchAllEvents _useCaseFetchAllEvents;
    private readonly UseCaseFetchEventById _useCaseFetchEventById;
    private readonly UseCaseCreateEvent _useCaseCreateEvent;
    private readonly UseCaseDeleteEventById _useCaseDeleteEventById;
    private readonly UseCaseUpdateEvent _useCaseUpdateEvent;
    private readonly UseCaseFetchEventByUrl _useCaseFetchEventByUrl;

    public EventController(UseCaseFetchAllEvents useCaseFetchAllEvents, UseCaseFetchEventById useCaseFetchEventById,
        UseCaseCreateEvent useCaseCreateEvent, UseCaseDeleteEventById useCaseDeleteEventById,
        UseCaseUpdateEvent useCaseUpdateEvent, UseCaseFetchEventByUrl useCaseFetchEventByUrl)
    {
        _useCaseFetchAllEvents = useCaseFetchAllEvents;
        _useCaseFetchEventById = useCaseFetchEventById;
        _useCaseCreateEvent = useCaseCreateEvent;
        _useCaseDeleteEventById = useCaseDeleteEventById;
        _useCaseUpdateEvent = useCaseUpdateEvent;
        _useCaseFetchEventByUrl = useCaseFetchEventByUrl;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<DtoOutputEvent>> FetchAll()
    {
        return Ok(_useCaseFetchAllEvents.Execute());
    }
    
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputEvent> FetchById(int id)
    {
        try
        {
            return _useCaseFetchEventById.Execute(id);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }
    
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<DtoOutputEvent> Create(DtoInputCreateEvent dto)
    {
        dto.IdAuthor = Int32.Parse(User.FindFirstValue("id"));
        return Ok(_useCaseCreateEvent.Execute(dto));
    }
    
    [HttpDelete]
    [Authorize]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<bool> Delete(int id)
    {
        return _useCaseDeleteEventById.Execute(id) ? NoContent() : NotFound();
    }
    
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Update(DtoInputEvent shelhaEvent)
    {
        shelhaEvent.IdAuthor = Int32.Parse(User.FindFirstValue("id"));
        return _useCaseUpdateEvent.Execute(shelhaEvent) ? NoContent() : NotFound();
    }
    
    [HttpGet]
    [Route("{urlEvent}")]
    public ActionResult<IEnumerable<DtoOutputEvent>> FetchEventByUrl(string urlEvent)
    {
        return Ok(_useCaseFetchEventByUrl.Execute(urlEvent));
    }
}