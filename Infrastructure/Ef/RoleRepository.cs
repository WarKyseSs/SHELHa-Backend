using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Infrastructure.Ef;

public class RoleRepository : IRoleRepository
{
    private readonly ShelhaContextProvider _shelhaContextProvider;

    public RoleRepository(ShelhaContextProvider shelhaContextProvider)
    {
        _shelhaContextProvider = shelhaContextProvider;
    }

    public IEnumerable<DbRole> FetchAll()
    {
        using var context = _shelhaContextProvider.NewContext();
        return context.Roles.ToList();
    }

    public DbRole FetchById(int id)
    {
        using var context = _shelhaContextProvider.NewContext();
        var role = context.Roles.FirstOrDefault(r => r.idRole == id);

        if (role == null) throw new KeyNotFoundException($"Role with id {id} has not been found");

        return role;
    }

    public DbRole FetchByString(string nameRole)
    {
        using var context = _shelhaContextProvider.NewContext();
        var role = context.Roles.FirstOrDefault(r => r.nameRole == nameRole);

        if (role == null) throw new KeyNotFoundException($"Role with name {nameRole} has not been found");

        return role;
    }
}
