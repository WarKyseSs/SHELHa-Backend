using Application.Services.User;
using Application.UseCases.Conversations.Dtos;
using Application.UseCases.Users.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Conversations;

public class UseCaseGetConversations :IUseCaseParameterizedQuery<IEnumerable<DtoOutputConversation>, int>
{
    private IConversationRepository _conversationRepository;
    private IUserService _userService;
    
    
    public UseCaseGetConversations(IConversationRepository conversationRepository, IUserService userService)
    {
        _conversationRepository = conversationRepository;
        _userService = userService;
    }


    public IEnumerable<DtoOutputConversation> Execute(int userId)
    { 
        //get all conversations of user
        var conversations = _conversationRepository.getConversationsOfUser(userId);
        
        if(conversations.Any())
        {
            //separately retrieve all users involved in a conversation with the user id sent as an argument
            var userIds = conversations
                .SelectMany(c => new[] { c.IdUserOne, c.IdUserTwo })
                .Distinct()
                .ToList();

            //bind userid and user useranme of the conversations
            //GetUsernameDictionary returns a dictionary that maps user IDs to their usernames
            var userDict = _userService.GetUsernameDictionary(userIds); 

            //map DbConversations to DtoOutputConversation 
            var outputConversations = 
                from c in conversations
                
                //retreive the username of user by id
                let userUsernameOne = userDict[c.IdUserOne]
                let userUsernameTwo = userDict[c.IdUserTwo]
           
                select new DtoOutputConversation
                {
                    Id = c.Id,
                    IdUserOne = c.IdUserOne,
                    IdUserTwo = c.IdUserTwo,
                    IdUserRequesting = userId,
                    UsernameUserOne = userUsernameOne,
                    UsernameUserTwo = userUsernameTwo,
                    LastMessage = c.LastMessage,
                    Timestamp = c.Timestamp,
                    Slug = c.Slug,
                    Subject = c.Subject
                };
            
            return outputConversations;
        }
        return Enumerable.Empty<DtoOutputConversation>();
    }
}