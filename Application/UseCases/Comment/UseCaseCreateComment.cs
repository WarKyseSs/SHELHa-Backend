using Application.Services.User;
using Application.UseCases.Post.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Comment.Dtos;

public class UseCaseCreateComment: IUseCaseWriter<DtoOutputComment, DtoInputCreateComment>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserService _userService;
    
    public UseCaseCreateComment(ICommentRepository commentRepository, IUserService userService)
    {
        _commentRepository = commentRepository;
        _userService = userService;
    }

    public DtoOutputComment Execute(DtoInputCreateComment input)
    {
        // Map the input DTO to a database comment object
        // Create the comment in the database using the comment repository
        var dbComment = _commentRepository.CreateComment(Mapper.GetInstance().Map<DbComment>(input));
        
        // Fetch the user who created the comment using the user service
        var user = _userService.FetchById(input.IdUser);

        // Map the database comment to a DTO
        var dtoComment = Mapper.GetInstance().Map<DtoOutputComment>(dbComment);

        // Set the username on the DTO to the username of the user who created the comment
        dtoComment.Username = user.username;

        // Return the DTO
        return dtoComment;
    }

}