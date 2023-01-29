using Application.UseCases.Post.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Post;

public class UseCaseDeletePost: IUseCaseWriter<bool, int>
{
    private readonly IPostRepository _postRepository;
    public UseCaseDeletePost(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    
    public bool Execute(int idPost)
    {
        // Attempt to delete the post using the post repository
        var success = _postRepository.DeletePost(idPost);

        // Return the result of the delete operation
        return success;
    }
}