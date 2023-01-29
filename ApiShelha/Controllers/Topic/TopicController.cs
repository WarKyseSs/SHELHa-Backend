using System.Collections.Generic;
using System.Security.Claims;
using Application.UseCases.Post;
using Application.UseCases.Post.Dtos;
using Application.UseCases.Topic;
using Application.UseCases.Topic.Dtos;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShelha.Controllers.Topic;

[ApiController]
[Route("api/v1/topics")]
public class Topic : ControllerBase
{
    private readonly UseCaseCreateTopic _useCaseCreateTopic;
    private readonly UseCaseFetchAllTopic _useCaseFetchAllTopic;
    private readonly UseCaseDeleteTopic _useCaseDeleteTopic;
    private readonly UseCaseUpdateTopic _useCaseUpdateTopic;
    private readonly UseCaseFetchTopicById _useCaseFetchTopicById;
    private readonly UseCaseFetchPostsByUrlTopic _useCaseFetchPostsByUrlTopic;

    public Topic(UseCaseCreateTopic useCaseCreateTopic, UseCaseFetchAllTopic useCaseFetchAllTopic, 
        UseCaseDeleteTopic useCaseDeleteTopic, UseCaseUpdateTopic useCaseUpdateTopic, 
        UseCaseFetchTopicById useCaseFetchTopicById,
        UseCaseFetchPostsByUrlTopic useCaseFetchPostsByUrlTopic)
    {
        _useCaseCreateTopic = useCaseCreateTopic;
        _useCaseFetchAllTopic = useCaseFetchAllTopic;
        _useCaseDeleteTopic = useCaseDeleteTopic;
        _useCaseUpdateTopic = useCaseUpdateTopic;
        _useCaseFetchTopicById = useCaseFetchTopicById;
        _useCaseFetchPostsByUrlTopic = useCaseFetchPostsByUrlTopic;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<DtoOutputTopic>> FetchAll()
    {
        return Ok(_useCaseFetchAllTopic.Execute());
    }

    [HttpGet]
    [Route("{idTopic:int}")]
    public ActionResult<IEnumerable<DtoOutputTopic>> FetchById(int idTopic)
    {
        return Ok(_useCaseFetchTopicById.Execute(idTopic));
    }

    [HttpPost]
    [Authorize(Roles="Modérateur, Administrateur")]
    public ActionResult<DtoOutputTopic> Create(DtoInputCreateTopic dto)
    {
        return Ok(_useCaseCreateTopic.Execute(dto));
    }
    
    [HttpDelete]
    [Authorize(Roles="Modérateur, Administrateur")]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeletePost(int id)
    {
        return _useCaseDeleteTopic.Execute(id) ? NoContent() : NotFound();
    }
    
    [HttpPut]
    [Authorize(Roles="Modérateur, Administrateur")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult UpdatePost(DtoInputTopic post)
    {
        return _useCaseUpdateTopic.Execute(post) ? NoContent() : NotFound();
    }
    
    
    [HttpGet]
    [Route("{urlTopic}/posts")]
    public ActionResult<IEnumerable<DtoOutputTopic>> FetchUrl(string urlTopic)
    {
        var posts = _useCaseFetchPostsByUrlTopic.Execute(urlTopic);
        var userRequesting = User.FindFirstValue("id");
        if (userRequesting == null)
        {
            userRequesting = "-1";
        }
        foreach (var post in posts)
        {
            if (post.IdAuthor == Int32.Parse(userRequesting))
            {
                post.CanChange = true;
            }
            else
            {
                post.CanChange = false;
            }
        }
        return Ok(posts);
    }
}