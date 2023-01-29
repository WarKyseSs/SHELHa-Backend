using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface ITopicRepository
{
    IEnumerable<DbTopic> GetAllTopics();

    DbTopic CreateTopic(DbTopic topic);

    bool DeleteTopic(int idTopic);
    
    bool UpdateTopic(DbTopic topic);

    DbTopic FetchById(int idTopic);

    DbTopic FetchByUrl(string urlTopic);

}