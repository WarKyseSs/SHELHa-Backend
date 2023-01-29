using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.Services.Conversation;

public class ConversationService: IConversationService
{
    private IConversationRepository _conversationRepository;

    public ConversationService(IConversationRepository conversationRepository)
    {
        _conversationRepository = conversationRepository;
    }

    public DbConversation FetchBySlug(string slug)
    {
        return _conversationRepository.FetchBySlug(slug);
    }
}