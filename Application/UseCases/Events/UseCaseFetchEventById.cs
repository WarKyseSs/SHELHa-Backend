using Application.UseCases.Articles.Dtos;
using Application.UseCases.Events.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Events;

public class UseCaseFetchEventById: IUseCaseParameterizedQuery<DtoOutputEvent, int>
{
    private readonly IEventRepository _eventRepository;

    public UseCaseFetchEventById(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public DtoOutputEvent Execute(int id)
    {
        var dbEvent = _eventRepository.FetchById(id);
        return Mapper.GetInstance().Map<DtoOutputEvent>(dbEvent);
    }
}