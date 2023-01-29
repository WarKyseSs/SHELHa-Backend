using Application.UseCases.Articles.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Articles;

public class UseCaseDeleteArticleById : IUseCaseWriter<bool, int>
{
    private readonly IArticleRepository _articleRepository;

    public UseCaseDeleteArticleById(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public bool Execute(int idArticle)
    {
        var dbArticle = _articleRepository.Delete(idArticle);
        return dbArticle;
    }
}