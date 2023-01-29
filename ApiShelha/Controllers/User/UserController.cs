using System.Security.Claims;
using Application.UseCases.Users;
using Application.UseCases.Users.Dtos;
using Domain.Utils;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.User;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    // Use cases for managing users
    private readonly UseCaseCreateUser _useCaseCreateUser;
    private readonly UseCaseFetchAllUsers _useCaseFetchAllUsers;
    private readonly UseCaseFetchUserById _useCaseFetchUserById;
    private readonly UseCaseFetchUserByUsername _useCaseFetchUserByUsername;
    private readonly UseCaseDeleteUser _useCaseDeleteUser;
    private readonly UseCaseUpdateUser _useCaseUpdateUser;
    
    // Dependency injection constructor to inject the use cases into the controller
    public UserController(UseCaseCreateUser useCaseCreateUser, UseCaseFetchAllUsers useCaseFetchAllUsers,
        UseCaseFetchUserById useCaseFetchUserById, UseCaseFetchUserByUsername useCaseFetchUserByUsername,
        UseCaseDeleteUser useCaseDeleteUser, UseCaseUpdateUser useCaseUpdateUser)
    {
        _useCaseCreateUser = useCaseCreateUser;
        _useCaseFetchAllUsers = useCaseFetchAllUsers;
        _useCaseFetchUserById = useCaseFetchUserById;
        _useCaseFetchUserByUsername = useCaseFetchUserByUsername;
        _useCaseDeleteUser = useCaseDeleteUser;
        _useCaseUpdateUser = useCaseUpdateUser;
    }
    
    // Action to handle HTTP GET request to retrieve all users
    [HttpGet]
    [Authorize(Roles = "Administrateur, Modérateur")]
    public IEnumerable<DtoOutputUser> FetchAll()
    {
        // Use the use case to retrieve all users and return them to the client as a list of DtoOutputUser objects
        var users = _useCaseFetchAllUsers.Execute();
        
        var userRequesting = User.FindFirstValue("id") ?? "-1";

        foreach (var user in users)
        {
            user.CanChange = user.Id == Int32.Parse(userRequesting);
        }
        return users;
    }

    // Action to handle HTTP GET request to retrieve a user by ID
    [HttpGet]
    [Authorize]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputUser> FetchById(int id)
    {
        try
        {
            var user = _useCaseFetchUserById.Execute(id);
            var userRequesting = User.FindFirstValue("id") ?? "-1";
            
            user.CanChange = user.Id == Int32.Parse(userRequesting);

            // Use the use case to retrieve a user by ID and return it to the client as a DtoOutputUser object
            return user;
        }
        catch (KeyNotFoundException e)
        {
            // If no user with the specified ID is found, return a 404 Not Found response to the client
            return NotFound(new
            {
                e.Message
            });
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("{username}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputUser> FetchByUsername(string username)
    {
        try
        {
            var user = _useCaseFetchUserByUsername.Execute(username);
            var userRequesting = User.FindFirstValue("id") ?? "-1";
            
            user.CanChange = user.Id == Int32.Parse(userRequesting);
            
            // Use the use case to retrieve a user by ID and return it to the client as a DtoOutputUser object
            return user;
        }
        catch (KeyNotFoundException e)
        {
            // If no user with the specified ID is found, return a 404 Not Found response to the client
            return NotFound(new
            {
                e.Message
            });
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("profil")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputUser> Fetch()
    {
        try
        {
            var userRequesting = Int32.Parse(User.FindFirstValue("id"));
            var user = _useCaseFetchUserById.Execute(userRequesting);
            var username = user.Username;
            
            var res = _useCaseFetchUserByUsername.Execute(username);
            res.CanChange = user.Id == userRequesting;
                
            return res;
        }
        catch (KeyNotFoundException e)
        {
            // If no user with the specified ID is found, return a 404 Not Found response to the client
            return NotFound(new
            {
                e.Message
            });
        }
    }

    // Action to handle HTTP POST request to create a new user
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    public ActionResult<DtoOutputUser> Create(DtoInputCreateUser dto)
    {
        // Validate the email address and password of the new user
        if (!UserValidator.ValidateMailAddress(dto.Mailaddress))
        {
            // If the email address is not valid, return a 406 Not Acceptable response to the client
            return StatusCode(406, "L'adresse mail n'est pas valide !");
        }
        if (!UserValidator.ValidatePassword(dto.Password))
        {
            // If the password is not valid, return a 406 Not Acceptable response to the client
            return StatusCode(406, "Le mot de passe n'est pas valide !");
        }
        
        // Check if the user already exists in the database
        /*var user = _useCaseFetchUserByUsername.Execute(dto.Username);
        if (user != null)
        {
            // If the user already exists, return a 406 Not Acceptable response to the client
            return StatusCode(406, "L'utilisateur existe déjà dans la base de données !");
        }*/
        
        // Use the use case create the user and return it to the client as a DtoOutputUser object
        var output = _useCaseCreateUser.Execute(dto);
        return CreatedAtAction(
            nameof(FetchById),
            new { id = output.Id },
            output
        );
    }
    
    // Action to handle HTTP DELETE request to delete an user by ID
    [HttpDelete]
    [Authorize(Roles = "Administrateur, Modérateur")]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteUser(int id)
    {   
        // Use the use case delete the user and return a code 200 Ok to the client or 404 Not Found response if no user with the specified ID is found
        return _useCaseDeleteUser.Execute(id) ? Ok() : NotFound();
    }
    
    // Action to handle HTTP PUT request to update an user
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    public ActionResult UpdateUser(DtoInputUpdateUser user, bool changePassword)
    {
        // Validate the email address and password of the updated user
        if (!UserValidator.ValidateMailAddress(user.Mailaddress))
        {
            // If the email address is not valid, return a 406 Not Acceptable response to the client
            return StatusCode(406, "L'adresse mail n'est pas valide !");
        }

        if (changePassword)
        {
            if (!UserValidator.ValidatePassword(user.Password))
            {
                // If the password is not valid, return a 406 Not Acceptable response to the client
                return StatusCode(406, "Le mot de passe n'est pas valide !");
            }
        }
        
        // Use the use case update the user and return a code 200 Ok to the client or 404 Not Found response if the user is not found
        return _useCaseUpdateUser.Execute(user) ? Ok() : NotFound();
    }
}