using Infrastructure.Ef;

namespace Application.Services.Role;

// Service for managing roles
public class RoleService : IRoleService
{
    // Repository for accessing role data in the database
    private readonly IRoleRepository _roleRepository;

    // Constructor for dependency injection
    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    // Fetches a role by its ID
    public Domain.Role Fetch(int id)
    {
        // Retrieves the role from the database
        var dbRole = _roleRepository.FetchById(id);
        // Maps the database role to the domain role
        return Mapper.GetInstance().Map<Domain.Role>(dbRole);
    }

    // Fetches a role by its string identifier
    public Domain.Role FetchByString(string nameRole)
    {
        // Retrieves the role from the database
        var dbRole = _roleRepository.FetchByString(nameRole);
        // Maps the database role to the domain role
        return Mapper.GetInstance().Map<Domain.Role>(dbRole);
    }
}