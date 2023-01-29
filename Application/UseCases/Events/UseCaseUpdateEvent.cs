using Application.UseCases.Articles.Dtos;
using Application.UseCases.Events.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Application.UseCases.Events;

public class UseCaseUpdateEvent : IUseCaseWriter<bool, DtoInputEvent>
{
    private readonly IEventRepository _eventRepository;
    private readonly SlugUrlProvider _slugUrlProvider;

    public UseCaseUpdateEvent(IEventRepository eventRepository, SlugUrlProvider slugUrlProvider)
    {
        _eventRepository = eventRepository;
        _slugUrlProvider = slugUrlProvider;
    }

    public bool Execute(DtoInputEvent input)
    {
        var dbEvent = Mapper.GetInstance().Map<DbEvent>(input);
        dbEvent.urlEvent = _slugUrlProvider.ToUrlSlug(dbEvent.Title);
        return _eventRepository.Update(dbEvent);
    }
}