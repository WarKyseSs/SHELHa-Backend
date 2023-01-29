using Application.UseCases.Articles.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Application.UseCases.Articles;

public class UseCaseUpdateArticle: IUseCaseWriter<bool, DtoInputArticle>
{
    private readonly IArticleRepository _articleRepository;
    private readonly SlugUrlProvider _slugUrlProvider;

    public UseCaseUpdateArticle(IArticleRepository articleRepository, SlugUrlProvider slugUrlProvider)
    {
        _articleRepository = articleRepository;
        _slugUrlProvider = slugUrlProvider;
    }

    public bool Execute(DtoInputArticle input)
    {
        var dbArticle = Mapper.GetInstance().Map<DbArticle>(input);
        dbArticle.urlArticle = _slugUrlProvider.ToUrlSlug(dbArticle.Title);
        return _articleRepository.Update(dbArticle);
    }

}