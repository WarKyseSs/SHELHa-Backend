using Application.Services.User;
using Application.UseCases.Conversations.Dtos;
using Infrastructure.Ef;

namespace Application.UseCases.Conversations;

public class UseCaseGetConversationBySlug
{
    private IConversationRepository _conversationRepository;
    private IUserService _userService;
    
    public UseCaseGetConversationBySlug(IConversationRepository conversationRepository, IUserService userService)
    {
        _conversationRepository = conversationRepository;
        _userService = userService;
    }

    public DtoOutputConversation Execute(string slug,int idUserRequest)
    {
        var dbConversation = _conversationRepository.FetchBySlug(slug);
        
        if (dbConversation != null)
        {
            var conversation = Mapper.GetInstance().Map<DtoOutputConversation>(dbConversation);
            var userIds = new List<int>();
            
            userIds.Add(conversation.IdUserOne);
            userIds.Add(conversation.IdUserTwo);
            
            //bind userid and user useranme of the conversations
            //GetUsernameDictionary returns a dictionary that maps user IDs to their usernames
            var userDict = _userService.GetUsernameDictionary(userIds); 
            
            var userUsernameOne = userDict[conversation.IdUserOne];
            var userUsernameTwo = userDict[conversation.IdUserTwo];
                
            var outputConversation =  new DtoOutputConversation
            {
                Id = conversation.Id,
                IdUserOne = conversation.IdUserOne,
                IdUserTwo = conversation.IdUserTwo,
                IdUserRequesting = idUserRequest,
                UsernameUserOne = userUsernameOne,
                UsernameUserTwo = userUsernameTwo,
                LastMessage = conversation.LastMessage,
                Timestamp = conversation.Timestamp,
                Slug = conversation.Slug,
                Subject = conversation.Subject
            };
            
            return outputConversation;
        }
        
        return null;
    }
}