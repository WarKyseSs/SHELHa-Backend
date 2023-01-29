using Application.Services.Conversation;
using Application.UseCases.Message.Dtos;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Message;

public class UseCaseCreateMessage
{
    private readonly IMessageRepository _messageRepository;
    private readonly IConversationService _conversationService;
    private readonly IConversationRepository _conversationRepository;
    
    public UseCaseCreateMessage(IMessageRepository messageRepository, IConversationService conversationService, IConversationRepository conversationRepository)
    {
        _messageRepository = messageRepository;
        _conversationService = conversationService;
        _conversationRepository = conversationRepository;
    }

    //create a new message if the receiver exists
    public DtoOutputMessage Execute(DtoInputMessage dtoInput)
    {
        var message = Mapper.GetInstance().Map<DtoOutputMessage>(_messageRepository.CreateMessage(
            Mapper.GetInstance().Map<DbMessage>(dtoInput),
            _conversationService.FetchBySlug(dtoInput.ConversationSlug)));

        if (message != null)
        {
            //update the last message send in conversation for conversation list view 
            var conversation = _conversationRepository.FetchBySlug(dtoInput.ConversationSlug);
            conversation.LastMessage = dtoInput.Message;
            _conversationRepository.Update(conversation);
            return message;
        }
        return null;
    }
}