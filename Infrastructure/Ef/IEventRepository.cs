using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IEventRepository
{
    IEnumerable<DbEvent> FetchAll();
    DbEvent FetchById(int id);
    DbEvent Create(DbEvent shelhaEvent);
    bool Delete(int id);
    bool Update(DbEvent shelhaEvent);
    DbEvent FetchByUrl(string urlEvent);
}