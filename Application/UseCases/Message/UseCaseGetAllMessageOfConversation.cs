using Application.Services.Conversation;
using Application.Services.User;
using Application.UseCases.Message.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Message;

public class UseCaseGetAllMessageOfConversation : IUseCaseParameterizedQuery<IEnumerable<DtoOutputMessage>, string>
{
    private IMessageRepository _messageRepository;
    private IConversationService _conversationService;
    private IUserService _userService;

    public UseCaseGetAllMessageOfConversation(IMessageRepository messageRepository,
        IConversationService conversationService, IUserService userService)
    {
        _messageRepository = messageRepository;
        _conversationService = conversationService;
        _userService = userService;
    }

    public IEnumerable<DtoOutputMessage> Execute(string slug)
    {
        var dbConversation = _conversationService.FetchBySlug(slug);

        if (dbConversation != null)
        {
            var dbMessages = _messageRepository.FetchAllMessageOfConversation(dbConversation.Id);

            var dtos = Mapper.GetInstance().Map<IEnumerable<DtoOutputMessage>>(dbMessages);
            
            foreach (var dto in dtos)
            {
                dto.UsernameOfSender = _userService.FetchById(dto.IdSender).username;
            }

            return dtos;
        }

        return Enumerable.Empty<DtoOutputMessage>();
    }
}