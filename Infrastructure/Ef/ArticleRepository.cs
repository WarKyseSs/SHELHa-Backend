using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class ArticleRepository : IArticleRepository
{
    private readonly ShelhaContextProvider _contextProvider;
    
    public ArticleRepository(ShelhaContextProvider contextProvider)
    {
        _contextProvider = contextProvider;
    }
    
    public IEnumerable<DbArticle> FetchAll()
    {
        using var context = _contextProvider.NewContext();
        return context.Articles.ToList()
            .OrderByDescending(a => a.DatePublication);
    }
    
    public DbArticle FetchById(int idArticle)
    {
        using var context = _contextProvider.NewContext();
        var article = context.Articles.FirstOrDefault(a => a.IdArticle == idArticle);

        if (article == null) throw new KeyNotFoundException($"Article with id {idArticle} has not been found");

        return article;
    }

    public DbArticle Create(DbArticle article)
    {
        using var context = _contextProvider.NewContext();
        context.Articles.Add(article);
        context.SaveChanges();
        return article;
    }

    public bool Delete(int idArticle)
    {
        using var context = _contextProvider.NewContext();
        try
        {
            context.Articles.Remove(new DbArticle { IdArticle = idArticle });
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }

    public bool Update(DbArticle article)
    {
        using var context = _contextProvider.NewContext();
        try
        {
            context.Articles.Update(article);
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }

    public DbArticle FetchByUrl(string urlArticle)
    {
        using var context = _contextProvider.NewContext();
        var article = context.Articles.FirstOrDefault(a => string.Equals(a.urlArticle, urlArticle));
        
        if (article == null)
            throw new KeyNotFoundException($"Article with url {urlArticle} has not been found");

        return article;
    }
}