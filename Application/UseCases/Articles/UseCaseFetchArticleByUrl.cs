using Application.Services.User;
using Application.UseCases.Articles.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Articles;

public class UseCaseFetchArticleByUrl: IUseCaseParameterizedQuery<DtoOutputArticle, string>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUserService _userService;

    public UseCaseFetchArticleByUrl(IArticleRepository articleRepository, IUserService userService)
    {
        _articleRepository = articleRepository;
        _userService = userService;
    }

    public DtoOutputArticle Execute(string urlArticle)
    {
        var article = _articleRepository.FetchByUrl(urlArticle);
        var user = _userService.FetchById(article.IdAuthor);
        var dtoArticle = Mapper.GetInstance().Map<DtoOutputArticle>(article);
        dtoArticle.username = user.username;
        return dtoArticle;
    }
}