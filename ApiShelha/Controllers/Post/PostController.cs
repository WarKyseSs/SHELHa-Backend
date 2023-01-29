using System.Security.Claims;
using Application.UseCases.Post;
using Application.UseCases.Post.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiShelha.Controllers;

[ApiController]
[Route("api/v1/post")]
public class PostController : ControllerBase
{
    private readonly UseCaseCreatePost _useCaseCreatePost;
    private readonly UseCaseDeletePost _useCaseDeletePost;
    private readonly UseCaseUpdatePost _useCaseUpdatePost;
    private readonly UseCaseFetchCommentsByUrl _useCaseFetchCommentsByUrl;
    private readonly UseCaseFetchPostByUrl _useCaseFetchPostByUrl;

    public PostController(UseCaseCreatePost useCaseCreatePost, UseCaseDeletePost useCaseDeletePost, 
        UseCaseUpdatePost useCaseUpdatePost, UseCaseFetchCommentsByUrl useCaseFetchCommentsByUrl,
        UseCaseFetchPostByUrl useCaseFetchPostByUrl)
    {
        _useCaseCreatePost = useCaseCreatePost;
        _useCaseDeletePost = useCaseDeletePost;
        _useCaseUpdatePost = useCaseUpdatePost;
        _useCaseFetchCommentsByUrl = useCaseFetchCommentsByUrl;
        _useCaseFetchPostByUrl = useCaseFetchPostByUrl;
    }

    [HttpGet]
    [Route("{urlPost}")]
    public ActionResult<IEnumerable<DtoOutputPost>> FetchByIdPost(string urlPost)
    {
        // Execute the fetch post by URL topic use case and return the result wrapped in an Ok action result
        return Ok(_useCaseFetchPostByUrl.Execute(urlPost));
    }

    [HttpPost]
    [Authorize(Roles="Etudiant, Community manager, Modérateur, Administrateur")]
    public ActionResult<DtoOutputPost> CreatePost(DtoInputCreatePost dto)
    {
        // Set the idAuthor property on the input DTO to the value of the "id" claim from the current user's claims
        dto.IdAuthor = Int32.Parse(User.FindFirstValue("id"));

        // Execute the create post use case and store the result in a local variable
        var dtoCreate = _useCaseCreatePost.Execute(dto);

        // Set the idUserRequesting property on the result to the value of the idAuthor property on the input DTO
        dtoCreate.CanChange = true;

        // Return the result of the create post use case wrapped in an Ok action result
        return Ok(dtoCreate);
    }
    
    [HttpDelete]
    [Authorize(Roles="Etudiant, Community manager, Modérateur, Administrateur")]
    [Route("{idPost:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeletePost(int idPost)
    {
        // Execute the delete post use case
        return _useCaseDeletePost.Execute(idPost) ? NoContent() : NotFound();
    }
    
    [HttpPut]
    [Authorize(Roles="Etudiant, Community manager, Modérateur, Administrateur")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult UpdatePost(DtoInputPost post)
    {
        // Execute the update post use case
        return _useCaseUpdatePost.Execute(post) ? NoContent() : NotFound();
    }

    [HttpGet]
    [Route("{urlPost}/comments")]
    public ActionResult<IEnumerable<DtoOutputPost>> FetchUrl(string urlPost)
    {
        // Execute the fetch comments by URL use case and store the result in a local variable
        var comments = _useCaseFetchCommentsByUrl.Execute(urlPost);

        // Get the value of the "id" claim from the current user's claims, or set it to -1 if it is null
        var userRequesting = User.FindFirstValue("id");
        if (userRequesting == null)
        {
            Console.WriteLine(User.FindFirstValue("id"));
            userRequesting = "-1";
        }

        // Set the idUserRequesting property on each comment in the result to the value of the "id" claim from the current user's claims
        foreach (var comment in comments)
        {
            if (comment.IdUser == Int32.Parse(userRequesting))
            {
                comment.CanChange = true;
            }
            else
            {
                comment.CanChange = false;
            }
        }

        // Return the result wrapped in an Ok action result
        return Ok(comments);
    }
}
