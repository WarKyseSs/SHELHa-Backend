using System.Security.Claims;
using Application.UseCases.UserConnexion;
using Application.UseCases.UserConnexion.Dtos;
using Application.UseCases.Users;
using Domain.Utils;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebApiShelha.Controllers.User;

[ApiController]
[Route("api/v1/connexion")]
public class ConnexionController : ControllerBase
{
    private readonly UseCaseConnectUser _useCaseConnectUser;

    public ConnexionController(UseCaseConnectUser useCaseConnectUser)
    {
        _useCaseConnectUser = useCaseConnectUser;
    }

    [Route("log")]
    [HttpPost]
    [AllowAnonymous] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<String> Login(DtoConnectionUser user)
    {
        if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
        {
            return Unauthorized("les identifiants sont invalides.");
        }
        
        //if doesn't respect password conditions, it must be a user that does not exist 
        if (!UserValidator.ValidatePassword(user.Password)) return Unauthorized("les identifiants sont invalides.");

        /* ask if you need to use 2 different key from generete token */
        var result = _useCaseConnectUser.Execute(user);
        
        if (result == "mailInvalid")
        {
            return Unauthorized("Veuillez valider votre adresse mail pour vous connecter !");
        }
        else if (result != "success")
        {
            return Unauthorized("les identifiants sont invalides.");
        }

        var resultCookieGenerated = _useCaseConnectUser.generateCookie(user);
        
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true
        };
            
        var cookieOptionsRole = new CookieOptions
        {
            HttpOnly = false,
            Secure = true
        };
    
        Response.Cookies.Append("tokenCookie", resultCookieGenerated[0], cookieOptions);
        Response.Cookies.Append("tokenCookieRole", resultCookieGenerated[1], cookieOptionsRole);
        
        
    
        //bonne pratique de return user ?
        return Ok();

    }
    
    [Authorize]
    [Route("isconnected")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult IsConnected()
    {
        return Ok(); 
    }
    
    
    [Authorize]
    [Route("logOut")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult logOut()
    {
        Response.Cookies.Delete("tokenCookie");
        Response.Cookies.Delete("tokenCookieRole");
        return Ok();
    }
    
    [HttpGet]
    [Route("idOfUserConnected")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<int> IdOfUserConnecteds()
    {
        return Ok(Int32.Parse(User.FindFirstValue("id")));
    }
}