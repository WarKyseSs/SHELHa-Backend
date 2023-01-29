using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Ef;

public class PostRepository: IPostRepository
{
    private readonly ShelhaContextProvider _shellaContextProvider;
    
    public PostRepository(ShelhaContextProvider shelhaContextProvider)
    {
        _shellaContextProvider = shelhaContextProvider;
    }

    public DbPost CreatePost(DbPost post)
    {
        // Create a new context for the database operation
        using var context = _shellaContextProvider.NewContext();
        
        // Create a new post object with the provided data
        var postToAdd = new DbPost
        {
            idAuthor = post.idAuthor,
            idCat = post.idCat,
            message = post.message,
            sujet = post.sujet,
            datePost = DateTime.Now,
            urlPost = post.urlPost
        };
        
        // Add the new post to the database
        context.Post.Add(postToAdd);
        
        // Save the changes to the database
        context.SaveChanges();
        
        // Return the newly created post
        return postToAdd;
    }

    public bool DeletePost(int idPost)
    {
        // Create a new context for the database operation
        using var context = _shellaContextProvider.NewContext();
        
        try
        {
            // Remove the post with the specified ID from the database
            context.Post.Remove(new DbPost { idPost = idPost });
            
            // Save the changes to the database
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            // Return false if an exception is thrown
            return false;
        }
    }

    public bool UpdatePost(DbPost post)
    {
        // Create a new context for the database operation
        using var context = _shellaContextProvider.NewContext();
        try
        {
            // Update the "dateLastEdit" property of the post object
            post.dateLastEdit = DateTime.Now;
            
            // Update the post in the database using the "Update" method of the "Post" property of the context object
            context.Post.Update(post);
            
            // Save the changes to the database
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            // Return false if an exception is thrown
            return false;
        }
    }
    
    public DbPost FetchById(int idPost)
    {
        // Create a new context for the database operation
        using var context = _shellaContextProvider.NewContext();

        // Retrieve the post from the database using the "FirstOrDefault" method of the "Post" property of the context object
        // The method returns the first post that matches the specified condition (in this case, the id of the post must match the provided id)
        // If no post is found, the method returns "null"
        var post = context.Post.FirstOrDefault(p => p.idPost == idPost);

        // If the post was not found, throw a "KeyNotFoundException" with a custom message
        if (post == null)
            throw new KeyNotFoundException($"Post with id {idPost} has not been found");

        // Return the retrieved post
        return post;
    }

    public DbPost FetchByUrl(string urlPost)
    {
        // Create a new database context using the context provider
        using var context = _shellaContextProvider.NewContext();
    
        // Retrieve the first post matching the specified URL
        var post = context.Post.FirstOrDefault(p => string.Equals(p.urlPost, urlPost));
    
        // If no post is found, throw an exception
        if (post == null)
            throw new KeyNotFoundException($"Post with url {urlPost} has not been found");

        return post;
    }

    public IEnumerable<DbPost> GetAllPostsByIdTopic(int idTopic)
    {
        // Create a new instance of the context object
        using var context = _shellaContextProvider.NewContext();

        // Retrieve a list of Post objects from the context where the idCat property is equal to the idTopic parameter
        var posts = context.Post.Where(p => p.idCat == idTopic).ToList();

        // Return the list of posts
        return posts;
    }
}