using Application.Services.Post;
using Application.Services.Role;
using Application.Services.User;
using Application.UseCases.Comment.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Post;

public class UseCaseFetchCommentsByUrl: IUseCaseParameterizedQuery<IEnumerable<DtoOutputComment>, string>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostService _postService;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public UseCaseFetchCommentsByUrl(ICommentRepository commentRepository, IPostService postService
        , IUserService userService, IRoleService roleService)
    {
        _commentRepository = commentRepository;
        _postService = postService;
        _userService = userService;
        _roleService = roleService;
    }

    public IEnumerable<DtoOutputComment> Execute(string urlPost)
    {
        var service = _postService.FetchByUrlPost(urlPost);

        // Fetch the comments for the post by its ID using the comment repository
        var comments = _commentRepository.GetAllCommentsByIdPost(service.idPost);

        // Map the comments to DTOs
        var dtoComments = Mapper.GetInstance().Map<IEnumerable<DtoOutputComment>>(comments).ToList();

        // For each comment DTO, set the username to the username of the user who created the comment
        foreach (DtoOutputComment comment in dtoComments)
        {
            // Fetch the user who created the comment using the user service
            var user = _userService.FetchById(comment.IdUser);
            var role = _roleService.Fetch(user.idUserRole);
            // Map the comment to a DTO
            var dtoComment = Mapper.GetInstance().Map<DtoOutputComment>(comment);

            // Set the username on the DTO to the username of the user who created the comment
            dtoComment.Username = user.username;
            dtoComment.UserRole = role.nameRole;
        }

        // Return the DTOs
        return dtoComments;
    }
}