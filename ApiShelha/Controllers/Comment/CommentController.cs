using System.Security.Claims;
using Application.UseCases.Comment;
using Application.UseCases.Comment.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShelha.Controllers.Comments;


[ApiController]
[Route("api/v1/comment")]

public class CommentController: ControllerBase
{
    private readonly UseCaseCreateComment _useCaseCreateComment;
    private readonly UseCaseDeleteComment _useCaseDeleteComment;
    private readonly UseCaseUpdateComment _useCaseUpdateComment;
    
    public CommentController(UseCaseCreateComment useCaseCreateComment, UseCaseDeleteComment useCaseDeleteComment, 
        UseCaseUpdateComment useCaseUpdateComment)
    {
        _useCaseCreateComment = useCaseCreateComment;
        _useCaseDeleteComment = useCaseDeleteComment;
        _useCaseUpdateComment = useCaseUpdateComment;
    }

    [HttpPost]
    [Authorize(Roles="Etudiant, Community manager, Modérateur, Administrateur")]
    public ActionResult<DtoOutputComment> CreateComment(DtoInputCreateComment dto)
    {
        // Set the idUser property on the input DTO to the value of the "id" claim from the current user's claims
        dto.IdUser = int.Parse(User.FindFirstValue("id"));

        // Execute the create comment use case and store the result in a local variable
        var dtoCreate = _useCaseCreateComment.Execute(dto);

        // Set the idUserRequesting property on the result to the value of the idUser property on the input DTO
        dtoCreate.CanChange = true;

        // Return the result wrapped in an Ok action result
        return Ok(dtoCreate);
    }
    
    [HttpDelete]
    [Authorize(Roles="Etudiant, Community manager, Modérateur, Administrateur")]
    [Route("{idComment:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteComment(int idComment)
    {
        // Execute the delete comment use case 
        return _useCaseDeleteComment.Execute(idComment) ? NoContent() : NotFound();
    }
    
    [HttpPut]
    [Authorize(Roles="Etudiant, Community manager, Modérateur, Administrateur")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult UpdateComment(DtoInputComment comment)
    {
        // Execute the update comment use case
        return _useCaseUpdateComment.Execute(comment) ? NoContent() : NotFound();
    }
}