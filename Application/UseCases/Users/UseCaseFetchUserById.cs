using Application.Services.Implantation;
using Application.Services.Role;
using Application.UseCases.Users.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Users;

// Use case for fetching a user by their ID
public class UseCaseFetchUserById : IUseCaseParameterizedQuery<DtoOutputUser, int>
{
    // Dependencies for retrieving data from the database
    private readonly IUserRepository _userRepository;
    private readonly IRoleService _roleService;
    private readonly IImplantationService _implantationService;

    // Inject dependencies in constructor
    public UseCaseFetchUserById(IUserRepository userRepository, IRoleService roleService, IImplantationService implantationService)
    {
        _userRepository = userRepository;
        _roleService = roleService;
        _implantationService = implantationService;
    }

    // Fetch the user by their ID and return their data
    public DtoOutputUser Execute(int id)
    {
        // Fetch the user from the database
        var dbUser = _userRepository.FetchById(id);

        // Use mapper to convert the DbUser object to a DtoOutputUser object
        var res = Mapper.GetInstance().Map<DtoOutputUser>(dbUser);
            
        // Retrieve the user's role and implantation using the dependencies
        // and set the corresponding properties in the DtoOutputUser object
        var role = _roleService.Fetch(dbUser.idUserRole);
        res.UserRole = role.nameRole;

        var implantation = _implantationService.Fetch(dbUser.idImplantation);
        res.Implantation = implantation.nameImplantation;
        return res;
    }
}