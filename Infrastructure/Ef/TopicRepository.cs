using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class TopicRepository : ITopicRepository
{
    private readonly ShelhaContextProvider _shellaContextProvider;

    public TopicRepository(ShelhaContextProvider shellaContextProvider)
    {
        _shellaContextProvider = shellaContextProvider;
    }

    public IEnumerable<DbTopic> GetAllTopics()
    {
        using var context = _shellaContextProvider.NewContext();
        return context.Topic.ToList();
    }

    public DbTopic CreateTopic(DbTopic topic)
    {
        using var context = _shellaContextProvider.NewContext();
        var topicToAdd = new DbTopic
        {
            idCat = topic.idCat,
            nameCat = topic.nameCat,
            description = topic.description,
            urlTopic = topic.urlTopic
        };
        context.Topic.Add(topicToAdd);
        context.SaveChanges();
        return topicToAdd;
    }

    public bool DeleteTopic(int idCat)
    {
        using var context = _shellaContextProvider.NewContext();
        try
        {
            context.Topic.Remove(new DbTopic { idCat = idCat });
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }

    public bool UpdateTopic(DbTopic topic)
    {
        using var context = _shellaContextProvider.NewContext();
        try
        {
            context.Topic.Update(topic);
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }

    public DbTopic FetchById(int idTopic)
    {
        using var context = _shellaContextProvider.NewContext();
        var topic = context.Topic.FirstOrDefault(p => p.idCat == idTopic);
        
        if (topic == null)
            throw new KeyNotFoundException($"Game with id {idTopic} has not been found");

        return topic;
    }

    public DbTopic FetchByUrl(string urlTopic)
    {
        using var context = _shellaContextProvider.NewContext();
        var
            post = context.Topic.FirstOrDefault(p => string.Equals(p.urlTopic, urlTopic));
        
        if (post == null)
            throw new KeyNotFoundException($"Post with url {urlTopic} has not been found");

        return post;
    }
}