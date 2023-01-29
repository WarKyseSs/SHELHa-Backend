using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class ConversationRepository: IConversationRepository
{
    private ShelhaContextProvider _shelhaContextProvider;

    public ConversationRepository(ShelhaContextProvider shelhaContextProvider)
    {
        _shelhaContextProvider = shelhaContextProvider;
    }

    /** get conversations of user connected */
    public IEnumerable<DbConversation> getConversationsOfUser(int userId)
    {
        using var context = _shelhaContextProvider.NewContext();
            return context.Conversations
            .Where(c => c.IdUserOne == userId || c.IdUserTwo == userId)
            .ToList();
    }

    public DbConversation FetchById(int id)
    {
        using var context = _shelhaContextProvider.NewContext();
        var conversation = context.Conversations.FirstOrDefault(c => c.Id == id);

        if (conversation == null) throw new KeyNotFoundException($"Conversation with id {id} has not been found");

        return conversation;
    }

    public DbConversation FetchBySlug(string slug)
    {
        using var context = _shelhaContextProvider.NewContext();
        
        var conversation = context.Conversations.FirstOrDefault(c => c.Slug == slug);

        if (conversation == null) throw new KeyNotFoundException($"Conversation with slug {slug} has not been found");

        return conversation;
    }

    public bool Update(DbConversation conversation)
    {
        using var context = _shelhaContextProvider.NewContext();

        try
        {
            context.Conversations.Update(conversation);
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }

    public DbConversation Create(DbConversation conversation,int idOfReceiver)
    {
        using var context = _shelhaContextProvider.NewContext();
        
        var conversationToAdd = new DbConversation
        {
            IdUserOne = conversation.IdUserOne,
            IdUserTwo = idOfReceiver,
            LastMessage = conversation.LastMessage,
            Subject = conversation.Subject,
            Slug = conversation.Slug,
            Timestamp = DateTime.Now
        };
        
        context.Conversations.Add(conversationToAdd);
        context.SaveChanges();
        
        return conversationToAdd;
    }

    
}