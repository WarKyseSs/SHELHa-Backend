using Application.UseCases.Role.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Role;

// Use case for fetching all roles
public class UseCaseFetchAllRoles : IUseCaseQuery<IEnumerable<DtoOutputRole>>
{
    // Repository for accessing role data in the database
    private readonly IRoleRepository _roleRepository;
        
    // Constructor for dependency injection
    public UseCaseFetchAllRoles(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    // Executes the use case
    public IEnumerable<DtoOutputRole> Execute()
    {
        // Retrieves all roles from the database
        var dbRoles = _roleRepository.FetchAll();
        // Maps the database roles to the output DTOs
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputRole>>(dbRoles);
    }
}