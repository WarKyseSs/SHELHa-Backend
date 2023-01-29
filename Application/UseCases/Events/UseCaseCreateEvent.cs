using Application.UseCases.Articles.Dtos;
using Application.UseCases.Events.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Application.UseCases.Events;

public class UseCaseCreateEvent: IUseCaseWriter<DtoOutputEvent, DtoInputCreateEvent>
{
    
    private readonly IEventRepository _eventRepository;
    private readonly SlugUrlProvider _slugUrlProvider;

    public UseCaseCreateEvent(IEventRepository eventRepository, SlugUrlProvider slugUrlProvider)
    {
        _eventRepository = eventRepository;
        _slugUrlProvider = slugUrlProvider;
    }

    public DtoOutputEvent Execute(DtoInputCreateEvent input)
    {
        var dbEvent = Mapper.GetInstance().Map<DbEvent>(input);
        dbEvent.urlEvent = _slugUrlProvider.ToUrlSlug(dbEvent.Title);
        dbEvent = _eventRepository.Create(dbEvent);
        return Mapper.GetInstance().Map<DtoOutputEvent>(dbEvent);
    }
}