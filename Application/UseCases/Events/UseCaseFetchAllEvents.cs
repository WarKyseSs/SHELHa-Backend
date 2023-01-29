using Application.Services.User;
using Application.UseCases.Articles.Dtos;
using Application.UseCases.Events.Dtos;
using Infrastructure.Ef;

namespace Application.UseCases.Events;

public class UseCaseFetchAllEvents
{
    private readonly IEventRepository _eventRepository;
    private readonly IUserService _userService;

    public UseCaseFetchAllEvents(IEventRepository eventRepository, IUserService userService)
    {
        _eventRepository = eventRepository;
        _userService = userService;
    }

    public IEnumerable<DtoOutputEvent> Execute()
    {
        var dbEvents = _eventRepository.FetchAll();
        var events = Mapper.GetInstance().Map<IEnumerable<DtoOutputEvent>>(dbEvents);
        
        foreach (DtoOutputEvent shelhaEvent in events)
        {
            var user = _userService.FetchById(shelhaEvent.IdAuthor);
            var dtoArticle = Mapper.GetInstance().Map<DtoOutputEvent>(shelhaEvent);
            dtoArticle.username = user.username;
        }
        return events;
    }
}