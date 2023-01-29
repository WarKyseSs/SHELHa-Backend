using Application.UseCases.Post.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Application.UseCases.Post;

public class UseCaseUpdatePost: IUseCaseWriter<bool, DtoInputPost>
{
    private readonly IPostRepository _postRepository;
    private readonly SlugUrlProvider _slugUrlProvider;

    public UseCaseUpdatePost(IPostRepository postRepository, SlugUrlProvider slugUrlProvider)
    {
        _postRepository = postRepository;
        _slugUrlProvider = slugUrlProvider;
    }

    public bool Execute(DtoInputPost input)
    {
        // Map the DTO to a database entity
        var dbPost = Mapper.GetInstance().Map<DbPost>(input);

        // Update the URL slug for the post
        dbPost.urlPost = _slugUrlProvider.ToUrlSlug(dbPost.sujet);

        // Attempt to update the post using the post repository
        var success = _postRepository.UpdatePost(dbPost);

        // Return the result of the update operation
        return success;
    }
}