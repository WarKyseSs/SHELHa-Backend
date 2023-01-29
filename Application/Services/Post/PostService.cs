using Infrastructure.Ef;

namespace Application.Services.Post;

public class PostService: IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    
    public Domain.Post FetchByUrlPost(string urlPost)
    {
        // Fetch the post from the database using the post repository
        var dbPost = _postRepository.FetchByUrl(urlPost);

        // Map the database post to a domain post and return it
        return Mapper.GetInstance().Map<Domain.Post>(dbPost);
    }
}