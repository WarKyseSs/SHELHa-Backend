using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Infrastructure.Ef;

public class MessageRepository : IMessageRepository
{
    private readonly ShelhaContextProvider _shellaContextProvider;

    public MessageRepository(ShelhaContextProvider shellaContextProvider)
    {
        _shellaContextProvider = shellaContextProvider;
    }

    public DbMessage CreateMessage(DbMessage message, DbConversation conversation)
    {
        using var context = _shellaContextProvider.NewContext();

        //verify is the sender is is part of conversation
        if (conversation.IdUserOne != message.IdSender && conversation.IdUserTwo != message.IdSender)
        {
            throw new InvalidOperationException("The sender is not part of the conversation");
        }

        var messageToAdd = new DbMessage
        {
            IdConversation = conversation.Id,
            IdSender = message.IdSender,
            Message = message.Message,
            TimeStamp = DateTime.Now,
            IsRead = false
        };

        context.Messages.Add(messageToAdd);
        context.SaveChanges();
    
        return messageToAdd;
        
    }
    

    public IEnumerable<DbMessage> FetchAll()
    {
        using var context = _shellaContextProvider.NewContext();
        return context.Messages.ToList();
    }

    public IEnumerable<DbMessage> FetchAllMessageOfConversation(int idOfConversation)
    {
        //DEMANDER AU PROF COMMENT FERMER UN CONTEXT MANUELLEMENT
        //sorts the list so that the latest messages are displayed first
        using var context = _shellaContextProvider.NewContext();
        var messages = context.Messages
            .Where(m => m.IdConversation == idOfConversation)
            .OrderByDescending(m => m.TimeStamp)
            .ToList();

        if (!messages.Any())
        {
            throw new KeyNotFoundException($"no message found for conversation with id {idOfConversation}");
        }
    
        return messages;
    }
}