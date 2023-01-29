using Application.UseCases.Articles.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Application.UseCases.Articles;

public class UseCaseCreateArticle : IUseCaseWriter<DtoOutputArticle, DtoInputCreateArticle>
{
    
    private readonly IArticleRepository _articleRepository;
    private readonly SlugUrlProvider _slugUrlProvider;

    public UseCaseCreateArticle(IArticleRepository articleRepository, SlugUrlProvider slugUrlProvider)
    {
        _articleRepository = articleRepository;
        _slugUrlProvider = slugUrlProvider;
    }

    public DtoOutputArticle Execute(DtoInputCreateArticle input)
    {
        var dbArticle = Mapper.GetInstance().Map<DbArticle>(input);
        dbArticle.urlArticle = _slugUrlProvider.ToUrlSlug(dbArticle.Title);
        dbArticle = _articleRepository.Create(dbArticle);
        return Mapper.GetInstance().Map<DtoOutputArticle>(dbArticle);
    }
}