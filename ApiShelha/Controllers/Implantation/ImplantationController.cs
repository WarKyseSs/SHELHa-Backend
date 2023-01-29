using Application.UseCases.Implantations.Dtos;
using Application.UseCases.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShelha.Controllers.Implantation;

[ApiController]
[Route("api/v1/implantations")]
public class ImplantationController : ControllerBase
{
    // Use cases for fetching implantations
    private readonly UseCaseFetchAllImplantation _useCaseFetchAllImplantation;
    private readonly UseCaseFetchImplantationById _useCaseFetchImplantationById;

    // Dependency injection constructor to inject the use cases into the controller
    public ImplantationController(UseCaseFetchAllImplantation useCaseFetchAllImplantation, UseCaseFetchImplantationById useCaseFetchImplantationById)
    {
        _useCaseFetchAllImplantation = useCaseFetchAllImplantation;
        _useCaseFetchImplantationById = useCaseFetchImplantationById;
    }
    
    // Action to handle HTTP GET request to retrieve all implantations
    [HttpGet]
    public ActionResult<IEnumerable<DtoOutputImplantation>> FetchAll()
    {
        // Use the use case to retrieve all implantations and return them to the client as a list of DtoOutputImplantation objects
        return Ok(_useCaseFetchAllImplantation.Execute());
    }
    
    // Action to handle HTTP GET request to retrieve an implantation by ID
    [HttpGet]
    [Authorize]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputImplantation> FetchById(int id)
    {
        try
        {
            // Use the use case to retrieve an implantation by ID and return it to the client as a DtoOutputImplantation object
            return _useCaseFetchImplantationById.Execute(id);
        }
        catch (KeyNotFoundException e)
        {
            // If no implantation with the specified ID is found, return a 404 Not Found response to the client
            return NotFound(new
            {
                e.Message
            });
        }
    }
}