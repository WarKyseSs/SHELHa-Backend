using Infrastructure.Ef.DbEntities;

namespace Application.Services.Conversation;

public interface IConversationService
{
    DbConversation FetchBySlug(string slug);
}