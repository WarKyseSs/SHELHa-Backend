using Application.Services.Role;
using Application.Services.User;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Post.Dtos;

public class UseCaseFetchPostByUrl:IUseCaseParameterizedQuery<DtoOutputPost, string>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public UseCaseFetchPostByUrl(IPostRepository postRepository, IUserService userService
        , IRoleService roleService)
    {
        _postRepository = postRepository;
        _userService = userService;
        _roleService = roleService;
    }

    public DtoOutputPost Execute(string urlPost)
    {
        var post = _postRepository.FetchByUrl(urlPost);

        // Fetch the user who created the post using the user service
        var user = _userService.FetchById(post.idAuthor);
        var role = _roleService.Fetch(user.idUserRole);

        // Map the post to a DTO
        var dtoPost = Mapper.GetInstance().Map<DtoOutputPost>(post);

        // Set the username on the DTO to the username of the user who created the post
        dtoPost.Username = user.username;
        dtoPost.UserRole = role.nameRole;

        // Return the DTO
        return dtoPost;
    }
}