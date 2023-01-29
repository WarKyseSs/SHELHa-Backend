using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Events;

public class UseCaseDeleteEventById: IUseCaseWriter<bool, int>
{
    private readonly IEventRepository _eventRepository;

    public UseCaseDeleteEventById(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public bool Execute(int id)
    {
        var dbEvent = _eventRepository.Delete(id);
        return dbEvent;
    }
}