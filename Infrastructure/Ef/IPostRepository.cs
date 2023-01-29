using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IPostRepository
{
    DbPost CreatePost(DbPost post);

    bool DeletePost(int idPost);
    
    bool UpdatePost(DbPost post);

    DbPost FetchByUrl(string urlPost);
    
    IEnumerable<DbPost> GetAllPostsByIdTopic(int idTopic);
}