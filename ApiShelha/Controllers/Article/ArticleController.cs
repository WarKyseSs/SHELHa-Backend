using System.Security.Claims;
using Application.UseCases.Articles;
using Application.UseCases.Articles.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShelha.Controllers.Article;

[ApiController]
[Route("api/v1/articles")]
public class ArticleController : ControllerBase
{
    private readonly UseCaseFetchAllArticles _useCaseFetchAllArticles;
    private readonly UseCaseCreateArticle _useCaseCreateArticle;
    private readonly UseCaseFetchArticleById _useCaseFetchArticleById;
    private readonly UseCaseDeleteArticleById _useCaseDeleteArticleById;
    private readonly UseCaseUpdateArticle _useCaseUpdateArticle;
    private readonly UseCaseFetchArticleByUrl _useCaseFetchArticleByUrl;

    public ArticleController(UseCaseFetchAllArticles useCaseFetchAllArticles, UseCaseCreateArticle useCaseCreateArticle,
        UseCaseFetchArticleById useCaseFetchArticleById, UseCaseDeleteArticleById useCaseDeleteArticleById,
        UseCaseUpdateArticle useCaseUpdateArticle, UseCaseFetchArticleByUrl useCaseFetchArticleByUrl)
    {
        _useCaseFetchAllArticles = useCaseFetchAllArticles;
        _useCaseCreateArticle = useCaseCreateArticle;
        _useCaseFetchArticleById = useCaseFetchArticleById;
        _useCaseDeleteArticleById = useCaseDeleteArticleById;
        _useCaseUpdateArticle = useCaseUpdateArticle;
        _useCaseFetchArticleByUrl = useCaseFetchArticleByUrl;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<DtoOutputArticle>> FetchAll()
    {
        return Ok(_useCaseFetchAllArticles.Execute());
    }
    
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputArticle> FetchById(int id)
    {
        try
        {
            return _useCaseFetchArticleById.Execute(id);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<DtoOutputArticle> Create(DtoInputCreateArticle dto)
    {
        dto.IdAuthor = Int32.Parse(User.FindFirstValue("id"));
        return Ok(_useCaseCreateArticle.Execute(dto));
    }
    
    [HttpDelete]
    [Authorize]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<bool> Delete(int id)
    {
        return _useCaseDeleteArticleById.Execute(id) ? NoContent() : NotFound();
    }
    
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Update(DtoInputArticle article)
    {
        article.IdAuthor = Int32.Parse(User.FindFirstValue("id"));
        return _useCaseUpdateArticle.Execute(article) ? NoContent() : NotFound();
    }
    
    [HttpGet]
    [Route("{urlArticle}")]
    public ActionResult<IEnumerable<DtoOutputArticle>> FetchArticleByUrl(string urlArticle)
    {
        return Ok(_useCaseFetchArticleByUrl.Execute(urlArticle));
    }
    
}