using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface ICommentRepository
{
    DbComment CreateComment(DbComment comment);

    bool DeleteComment(int idComment);
    
    bool UpdateComment(DbComment comment);

    DbComment FetchById(int idComment);
    
    IEnumerable<DbComment> GetAllCommentsByIdPost(int idPost);

    
}