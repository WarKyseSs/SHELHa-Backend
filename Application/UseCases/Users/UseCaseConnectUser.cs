using Application.Services.Role;
using Application.Services.User;
using Application.UseCases.UserConnexion.Dtos;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;
using Microsoft.Extensions.Configuration;

namespace Application.UseCases.UserConnexion;

public class UseCaseConnectUser
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleService _roleService;
    private readonly IUserService _userService;
    private readonly TokenService _tokenService;
    private readonly IConfiguration _config;
    
    public UseCaseConnectUser(IUserRepository userRepository, IRoleService roleService, IUserService userService, TokenService tokenService, IConfiguration config)
    {
        _userRepository = userRepository;
        _roleService = roleService;
        _userService = userService;
        _tokenService = tokenService;
        _config = config;
    }


    public string Execute(DtoConnectionUser user)
    {
        var result = _userRepository.ConnectUser(Mapper.GetInstance().Map<DbUser>(user));
        
        //if the user if found and his password is righ, we verif if his account is valid
        if (result=="success")
        {
            if(!_userRepository.isMailValid(Mapper.GetInstance().Map<DbUser>(user)))
            {
                return "mailInvalid";
            }
        }
        return result;

    }

    public string[] generateCookie(DtoConnectionUser user)
    {
        //add cookies
        /*ask if we must to use local or glabal variables */
        var idRole = _userRepository.GetIdRoleFromUsername(Mapper.GetInstance().Map<DbUser>(user));
        var idUser = _userService.getIdOfUserByUseranme(user);
        var nameOfRole = _roleService.Fetch(idRole).nameRole;
        var generatedToken = _tokenService.BuildToken(_config?["Jwt:Key"], _config["Jwt:Issuer"], user.Username,nameOfRole,idUser);
        var generatedTokenRole = _tokenService.BuildTokenRole(_config["Jwt:KeyRole"], _config["Jwt:Issuer"],nameOfRole);
        
        return new[] { generatedToken, generatedTokenRole, };
        
    }
}