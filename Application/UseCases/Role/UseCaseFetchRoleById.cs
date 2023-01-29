using Application.UseCases.Role.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Role;

// Use case for fetching a role by its ID
public class UseCaseFetchRoleById : IUseCaseParameterizedQuery<DtoOutputRole, int>
{
    // Repository for accessing role data in the database
    private readonly IRoleRepository _roleRepository;

    // Constructor for dependency injection
    public UseCaseFetchRoleById(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    // Executes the use case
    public DtoOutputRole Execute(int id)
    {
        // Retrieves the role from the database
        var dbRole = _roleRepository.FetchById(id);
        // Maps the database role to the output DTO
        return Mapper.GetInstance().Map<DtoOutputRole>(dbRole);
    }
}