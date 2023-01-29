using Application.Services.Implantation;
using Application.Services.Role;
using Application.UseCases.Users.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Users;

// Use case for updating a user in the system
public class UseCaseUpdateUser : IUseCaseWriter<bool, DtoInputUpdateUser>
{
    // Dependencies for retrieving and updating data
    private readonly IUserRepository _userRepository;
    private readonly IImplantationService _implantationService;
    private readonly IRoleService _roleService;

    // Inject dependencies in constructor
    public UseCaseUpdateUser(IUserRepository userRepository, IImplantationService implantationService, IRoleService roleService)
    {
        _userRepository = userRepository;
        _implantationService = implantationService;
        _roleService = roleService;
    }

    // Update the user in the system
    public bool Execute(DtoInputUpdateUser input)
    {
        // Retrieve the role and implantation for the user
        var role = _roleService.FetchByString(input.UserRole);
        var imp = _implantationService.FetchByString(input.Implantation);

        // Use mapper to convert input data to a DbUser object
        // Then update the user in the database using the repository
        var dbUser = _userRepository.Update(Mapper.GetInstance().Map<DbUser>(input), imp.idImplantation, role.idRole, input.ChangePassword);
        return dbUser;
    }
}