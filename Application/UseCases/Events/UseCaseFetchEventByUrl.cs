using Application.Services.User;
using Application.UseCases.Articles.Dtos;
using Application.UseCases.Events.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Events;

public class UseCaseFetchEventByUrl: IUseCaseParameterizedQuery<DtoOutputEvent, string>
{
    private readonly IEventRepository _eventRepository;
    private readonly IUserService _userService;

    public UseCaseFetchEventByUrl(IEventRepository eventRepository, IUserService userService)
    {
        _eventRepository = eventRepository;
         _userService = userService;
    }

    public DtoOutputEvent Execute(string urlEvent)
    {
        var shelhaEvent = _eventRepository.FetchByUrl(urlEvent);
        var user = _userService.FetchById(shelhaEvent.IdAuthor);
        var dtoEvent = Mapper.GetInstance().Map<DtoOutputEvent>(shelhaEvent);
        dtoEvent.username = user.username;
        return dtoEvent;
    }
}