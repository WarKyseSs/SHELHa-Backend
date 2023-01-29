
using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IArticleRepository
{
    IEnumerable<DbArticle> FetchAll();
    DbArticle FetchById(int id);
    DbArticle Create(DbArticle article);
    bool Delete(int id);
    bool Update(DbArticle article);
    DbArticle FetchByUrl(string urlArticle);
}