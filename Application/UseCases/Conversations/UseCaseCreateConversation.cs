using Application.Services.User;
using Application.UseCases.Conversations.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Application.UseCases.Conversations;

public class UseCaseCreateConversation : IUseCaseWriter<DtoOutputConversation, DtoInputConversation>
{
    private readonly IConversationRepository _conversationRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IUserService _userService;
    private readonly SlugUrlProvider _slugUrlProvider;

    public UseCaseCreateConversation(IConversationRepository conversationRepository, IMessageRepository messageRepository, IUserService userService, SlugUrlProvider slugUrlProvider)
    {
        _conversationRepository = conversationRepository;
        _messageRepository = messageRepository;
        _userService = userService;
        _slugUrlProvider = slugUrlProvider;
    }
    
    public DtoOutputConversation Execute(DtoInputConversation dtoInput)
    {
        //retreive receiver user 
        DbUser userReveiver = new DbUser();
        userReveiver.username = dtoInput.UsernameOfReceiver;
        int idOfReceiver = _userService.GetIdFromUserByUsername(userReveiver);

        //if user exist
        if (idOfReceiver != -1)
        {
            //mapping manual 
            DbConversation dbConversation = new DbConversation
            {
                IdUserOne = dtoInput.IdOfSender,
                IdUserTwo = idOfReceiver,
                LastMessage = dtoInput.LastMessage,
                Subject = dtoInput.Subject,
                Timestamp = DateTime.Now,
                Slug = _slugUrlProvider.ToUrlSlug(dtoInput.Subject+DateTime.Now)
            };
            
            var conversation = _conversationRepository.Create(dbConversation,idOfReceiver);
            
            
            if (conversation != null)
            {
                //when you create a conversation, you also create the first message sent in the conversation
                DbMessage dbMessage = new DbMessage();
                dbMessage.Message = conversation.LastMessage;
                dbMessage.IdSender = dtoInput.IdOfSender;
                _messageRepository.CreateMessage(dbMessage, conversation);
                
                //bind userid and user useranme of the conversations
                //GetUsernameDictionary returns a dictionary that maps user IDs to their usernames
                var userIds = new List<int>();
                userIds.Add(conversation.IdUserOne);
                userIds.Add(conversation.IdUserTwo);
                var userDict = _userService.GetUsernameDictionary(userIds);
                var userUsernameOne = userDict[conversation.IdUserOne];
                var userUsernameTwo = userDict[conversation.IdUserTwo];
                
                
                var outputConversation =  new DtoOutputConversation
                {
                    Id = conversation.Id,
                    IdUserOne = conversation.IdUserOne,
                    IdUserTwo = conversation.IdUserTwo,
                    IdUserRequesting = dtoInput.IdOfSender,
                    UsernameUserOne = userUsernameOne,
                    UsernameUserTwo = userUsernameTwo,
                    LastMessage = conversation.LastMessage,
                    Timestamp = conversation.Timestamp,
                    Slug = conversation.Slug,
                    Subject = conversation.Subject
                
                };
            
                return outputConversation;
            }
            
            return  null;
        }
        
        return null;
    }
}