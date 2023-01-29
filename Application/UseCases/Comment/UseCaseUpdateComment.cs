using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Comment.Dtos;

public class UseCaseUpdateComment: IUseCaseWriter<bool, DtoInputComment>
{
    private readonly ICommentRepository _commentRepository;

    public UseCaseUpdateComment(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public bool Execute(DtoInputComment input)
    {
        // Map the input DTO to a database comment object
        var dbComment = Mapper.GetInstance().Map<DbComment>(input);

        // Update the comment in the database using the comment repository
        var success = _commentRepository.UpdateComment(dbComment);

        // Return the result of the update operation
        return success;
    }
}