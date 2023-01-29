namespace Application.Services.Role;

public interface IRoleService
{
    // Fetches a role by its ID
    Domain.Role Fetch(int id);

    // Fetches a role by its string identifier
    Domain.Role FetchByString(string nameRole);
}