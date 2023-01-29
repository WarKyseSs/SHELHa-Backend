using Application.UseCases.Role;
using Application.UseCases.Role.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShelha.Controllers.Role;

[ApiController]
[Route("api/v1/roles")]
public class RoleController : ControllerBase
{
    // Use cases for fetching roles
    private readonly UseCaseFetchAllRoles _useCaseFetchAllRoles;
    private readonly UseCaseFetchRoleById _useCaseFetchRoleById;
    
    // Dependency injection constructor to inject the use cases into the controller
    public RoleController(UseCaseFetchRoleById useCaseFetchRoleById, UseCaseFetchAllRoles useCaseFetchAllRoles)
    {
        _useCaseFetchRoleById = useCaseFetchRoleById;
        _useCaseFetchAllRoles = useCaseFetchAllRoles;
    }
    
    // Action to handle HTTP GET request to retrieve all roles
    [HttpGet]
    [Authorize]
    public ActionResult<IEnumerable<DtoOutputRole>> FetchAll()
    {
        // Use the use case to retrieve all roles and return them to the client as a list of DtoOutputRole objects
        return Ok(_useCaseFetchAllRoles.Execute());
    }
    
    // Action to handle HTTP GET request to retrieve a role by ID
    [HttpGet]
    [Authorize]
    [Route("{id:int}")]
    public ActionResult<DtoOutputRole> FetchById(int id)
    {
        // Use the use case to retrieve a role by ID and return it to the client as a DtoOutputRole object
        return Ok(_useCaseFetchRoleById.Execute(id));
    }
}