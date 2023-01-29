using Application.Services.Implantation;
using Application.Services.Role;
using Application.UseCases.Users.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Users;

public class UseCaseFetchUserByUsername : IUseCaseParameterizedQuery<DtoOutputUser, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleService _roleService;
    private readonly IImplantationService _implantationService;

    public UseCaseFetchUserByUsername(IUserRepository userRepository, IRoleService roleService, IImplantationService implantationService)
    {
        _userRepository = userRepository;
        _roleService = roleService;
        _implantationService = implantationService;
    }

    public DtoOutputUser Execute(string username)
    {
        var dbUser = _userRepository.FetchByUsername(username);

        var res = Mapper.GetInstance().Map<DtoOutputUser>(dbUser);
            
        var role = _roleService.Fetch(dbUser.idUserRole);
        res.UserRole = role.nameRole;

        var implantation = _implantationService.Fetch(dbUser.idImplantation);
        res.Implantation = implantation.nameImplantation;
        return res;
    }
}