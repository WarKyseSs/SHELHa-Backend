using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IRoleRepository
{
    IEnumerable<DbRole> FetchAll();
    DbRole FetchById(int id);
    DbRole FetchByString(string nameRole);
}