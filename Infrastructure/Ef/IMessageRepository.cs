using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IMessageRepository
{
    DbMessage CreateMessage(DbMessage message,DbConversation conversation);
    IEnumerable<DbMessage> FetchAll();
    IEnumerable<DbMessage> FetchAllMessageOfConversation(int idOfConversation);
    
}