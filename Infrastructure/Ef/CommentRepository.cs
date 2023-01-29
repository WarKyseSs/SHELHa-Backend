using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Ef;

public class CommentRepository: ICommentRepository
{
    private readonly ShelhaContextProvider _shellaContextProvider;

    public CommentRepository(ShelhaContextProvider shelhaContextProvider)
    {
        _shellaContextProvider = shelhaContextProvider;
    }

    public DbComment CreateComment(DbComment comment)
    {
        // Create a new context for the database operation
        using var context = _shellaContextProvider.NewContext();

        // Create a new comment entity with the provided information and the current timestamp
        var commentToAdd = new DbComment()
        {
            idComment = comment.idComment,
            idPost = comment.idPost,
            idUser = comment.idUser,
            message = comment.message,
            dateComment = DateTime.Now
        };

        // Add the new comment to the database
        context.Comment.Add(commentToAdd);

        // Save the changes to the database
        context.SaveChanges();

        // Return the newly created comment
        return commentToAdd;
    }

    public bool DeleteComment(int idComment)
    {
        // Create a new context for the database operation
        using var context = _shellaContextProvider.NewContext();
        try
        {
            // Attempt to delete the comment from the database
            context.Comment.Remove(new DbComment { idComment = idComment });
            
            // Return true if the comment was successfully deleted, false otherwise
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            // Return false if there was a concurrency exception
            return false;
        }
    }

    public bool UpdateComment(DbComment comment)
    {
        // Create a new context for the database operation
        using var context = _shellaContextProvider.NewContext();
        try
        {
            // Set the last edit timestamp for the comment
            comment.dateLastEdit = DateTime.Now;
            
            // Attempt to update the comment in the database
            context.Comment.Update(comment);
            
            // Return true if the comment was successfully updated, false otherwise
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            // Return false if there was a concurrency exception
            return false;
        }
    }

    public DbComment FetchById(int idComment)
    {
        // Create a new context for the database operation
        using var context = _shellaContextProvider.NewContext();
        
        // Query the database for the comment with the specified ID
        var comment = context.Comment.FirstOrDefault(p => p.idComment == idComment);

        // If the comment was not found, throw a KeyNotFoundException
        if (comment == null)
            throw new KeyNotFoundException($"Comment with id {idComment} has not been found");

        // Return the found comment
        return comment;
    }

    public IEnumerable<DbComment> GetAllCommentsByIdPost(int idPost)
    {
        // Create a new context for the database operation
        using var context = _shellaContextProvider.NewContext();
        
        // Query the database for all comments with the specified post ID
        var comments = context.Comment.Where(c => c.idPost == idPost).ToList();
        
        // Return the found comments
        return comments;
    }
}