using Application.UseCases.Articles.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Articles;

public class UseCaseFetchArticleById : IUseCaseParameterizedQuery<DtoOutputArticle, int>
{
    private readonly IArticleRepository _articleRepository;

    public UseCaseFetchArticleById(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public DtoOutputArticle Execute(int id)
    {
        var dbArticle = _articleRepository.FetchById(id);
        return Mapper.GetInstance().Map<DtoOutputArticle>(dbArticle);
    }
}