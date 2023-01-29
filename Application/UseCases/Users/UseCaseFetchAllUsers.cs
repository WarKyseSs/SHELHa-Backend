using Application.Services.Implantation;
using Application.Services.Role;
using Application.UseCases.Users.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Users;

public class UseCaseFetchAllUsers : IUseCaseQuery<IEnumerable<DtoOutputUser>>
{
    // Repository for retrieving users from the database
    private readonly IUserRepository _userRepository;
    // Service for retrieving information about user roles
    private readonly IRoleService _roleService;
    // Service for retrieving information about user implantations
    private readonly IImplantationService _implantationService;

    // Constructor taking the dependencies needed for implementing the Execute method as input
    public UseCaseFetchAllUsers(IUserRepository userRepository, IRoleService roleService, IImplantationService implantationService)
    {
        _userRepository = userRepository;
        _roleService = roleService;
        _implantationService = implantationService;
    }

    // Implementation of the use case's Execute method
    public IEnumerable<DtoOutputUser> Execute()
    {
        // Retrieve all users from the database
        var dbUsers = _userRepository.FetchAll();
        // Convert the retrieved objects to DtoOutputUser objects using AutoMapper
        var res = Mapper.GetInstance().Map<IEnumerable<DtoOutputUser>>(dbUsers);

        // For each DtoOutputUser object in the list, retrieve the associated role and implantation information
        // for the corresponding user in the database
        foreach (var resDtoOutputUser in res)
        {
            var dbUser = _userRepository.FetchById(resDtoOutputUser.Id);
            var role = _roleService.Fetch(dbUser.idUserRole);
            resDtoOutputUser.UserRole = role.nameRole;

            var implantation = _implantationService.Fetch(dbUser.idImplantation);
            resDtoOutputUser.Implantation = implantation.nameImplantation;
        }

        // Return the list of DtoOutputUser objects with the completed role and implantation information
        return res;
    }
}