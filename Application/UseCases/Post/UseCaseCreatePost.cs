using Application.Services.User;
using Application.UseCases.Post.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Application.UseCases.Post;

public class UseCaseCreatePost: IUseCaseWriter<DtoOutputPost, DtoInputCreatePost>
{
    private readonly IPostRepository _postRepository;
    private readonly SlugUrlProvider _slugUrlProvider;
    private readonly IUserService _userService;
    public UseCaseCreatePost(IPostRepository postRepository, SlugUrlProvider slugUrlProvider, IUserService userService)
    {
        _postRepository = postRepository;
        _slugUrlProvider = slugUrlProvider;
        _userService = userService;
    }
    
    public DtoOutputPost Execute(DtoInputCreatePost input)
    {
        // Map the input DTO to a database post object
        var dbPost = Mapper.GetInstance().Map<DbPost>(input);

        // Generate a URL slug for the post using the slug URL 
        dbPost.urlPost = _slugUrlProvider.ToUrlSlug(dbPost.sujet);

        // Create the post in the database using the post repository
        dbPost = _postRepository.CreatePost(dbPost);
        
        var user = _userService.FetchById(dbPost.idAuthor);
        
        var dtoPost = Mapper.GetInstance().Map<DtoOutputPost>(dbPost);
        dtoPost.Username = user.username;

        // Map the database post to a DTO and return it
        return dtoPost;
    }
}