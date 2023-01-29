using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IConversationRepository
{
    IEnumerable<DbConversation> getConversationsOfUser(int id);
    DbConversation FetchById(int id);
    DbConversation FetchBySlug(string slug);

    bool Update(DbConversation conversation);
    
    DbConversation Create(DbConversation conversation, int idOfReceiver);
}