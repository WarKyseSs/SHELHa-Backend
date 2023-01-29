using Application.Services.Role;
using Application.Services.Topic;
using Application.Services.User;
using Application.UseCases.Post.Dtos;
using Application.UseCases.Topic.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Post;

public class UseCaseFetchPostsByUrlTopic : IUseCaseParameterizedQuery<IEnumerable<DtoOutputPost>, string>
{
    private readonly IPostRepository _postRepository;
    private readonly ITopicService _topicService;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public UseCaseFetchPostsByUrlTopic(IPostRepository postRepository, ITopicService topicService, 
        IUserService userService, IRoleService roleService)
    {
        _postRepository = postRepository;
        _topicService = topicService;
        _userService = userService;
        _roleService = roleService;
    }

    public IEnumerable<DtoOutputPost> Execute(string urlTopic)
    {
        // Fetch the topic using the topic service
        var topic = _topicService.FetchPostsByUrlTopic(urlTopic);

        // Fetch all posts for the topic using the post repository
        var posts = _postRepository.GetAllPostsByIdTopic(topic.idCat);

        // Map the posts to DTOs
        var dtoPosts = Mapper.GetInstance().Map<IEnumerable<DtoOutputPost>>(posts).ToList();

        // Set the usernames on the DTOs to the usernames of the users who created the posts
        foreach (DtoOutputPost post in dtoPosts)
        {
            var user = _userService.FetchById(post.IdAuthor);
            var role = _roleService.Fetch(user.idUserRole);
            var dtoPost = Mapper.GetInstance().Map<DtoOutputPost>(post);
            dtoPost.Username = user.username;
            dtoPost.UserRole = role.nameRole;
        }

        // Return the DTOs
        return dtoPosts;
    }
}