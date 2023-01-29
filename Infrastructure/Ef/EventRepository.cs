using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class EventRepository: IEventRepository
{
    private readonly ShelhaContextProvider _contextProvider;
    
    public EventRepository(ShelhaContextProvider contextProvider)
    {
        _contextProvider = contextProvider;
    }

    public IEnumerable<DbEvent> FetchAll()
    {
        using var context = _contextProvider.NewContext();
        return context.Events.ToList()
            .OrderByDescending(e => e.DateEvent);
    }

    public DbEvent FetchById(int id)
    {
        using var context = _contextProvider.NewContext();
        var shelhaEvent = context.Events.FirstOrDefault(e => e.IdEvent == id);

        if (shelhaEvent == null) throw new KeyNotFoundException($"Event with id {id} has not been found");

        return shelhaEvent;
    }

    public DbEvent Create(DbEvent shelhaEvent)
    {
        using var context = _contextProvider.NewContext();
        context.Events.Add(shelhaEvent);
        context.SaveChanges();
        return shelhaEvent;
    }

    public bool Delete(int id)
    {
        using var context = _contextProvider.NewContext();
        try
        {
            context.Events.Remove(new DbEvent { IdEvent = id });
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }

    public bool Update(DbEvent shelhaEvent)
    {
        using var context = _contextProvider.NewContext();
        try
        {
            context.Events.Update(shelhaEvent);
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }

    public DbEvent FetchByUrl(string urlEvent)
    {
        using var context = _contextProvider.NewContext();
        var shelhaEvent = context.Events.FirstOrDefault(e => string.Equals(e.urlEvent, urlEvent));
        
        if (shelhaEvent == null)
            throw new KeyNotFoundException($"Event with url {urlEvent} has not been found");

        return shelhaEvent;
    }
}