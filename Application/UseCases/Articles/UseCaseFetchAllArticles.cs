using Application.Services.User;
using Application.UseCases.Articles.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Articles;

public class UseCaseFetchAllArticles : IUseCaseQuery<IEnumerable<DtoOutputArticle>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUserService _userService;

    public UseCaseFetchAllArticles(IArticleRepository articleRepository, IUserService userService)
    {
        _articleRepository = articleRepository;
        _userService = userService;
    }

    public IEnumerable<DtoOutputArticle> Execute()
    {
        var dbArticles = _articleRepository.FetchAll();
        var articles = Mapper.GetInstance().Map<IEnumerable<DtoOutputArticle>>(dbArticles);
        foreach (DtoOutputArticle article in articles)
        {
            var user = _userService.FetchById(article.IdAuthor);
            var dtoArticle = Mapper.GetInstance().Map<DtoOutputArticle>(article);
            dtoArticle.username = user.username;
        }
        return articles;
    }
}