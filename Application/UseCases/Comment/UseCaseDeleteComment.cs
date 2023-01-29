using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Comment.Dtos;

public class UseCaseDeleteComment: IUseCaseWriter<bool, int>
{
    private readonly ICommentRepository _commentRepository;
    public UseCaseDeleteComment(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    
    public bool Execute(int idComment)
    {
        // Attempt to delete the comment using the comment repository
        var success = _commentRepository.DeleteComment(idComment);

        // Return the result of the delete operation
        return success;
    }
}