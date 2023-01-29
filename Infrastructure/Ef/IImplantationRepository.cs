using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

// This interface defines the methods that a repository for Implantation entities should implement
public interface IImplantationRepository
{
    // This method should return a list of all Implantation entities
    IEnumerable<DbImplantation> FetchAll();

    // This method should return the Implantation entity with the given string value
    DbImplantation FetchByString(string imp);

    // This method should return the Implantation entity with the given id
    DbImplantation FetchById(int id);
}