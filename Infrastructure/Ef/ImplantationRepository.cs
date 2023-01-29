using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Infrastructure.Ef;

// This class implements the IImplantationRepository interface and provides a concrete implementation
// for fetching Implantation entities from a database
public class ImplantationRepository : IImplantationRepository
{
    // This field holds a reference to a ShelhaContextProvider, which is used to create new instances
    // of the database context
    private readonly ShelhaContextProvider _shelhaContextProvider;

    // The constructor takes a ShelhaContextProvider as an argument and assigns it to the field
    public ImplantationRepository(ShelhaContextProvider shelhaContextProvider)
    {
        _shelhaContextProvider = shelhaContextProvider;
    }

    // This method returns a list of all Implantation entities by querying the Implantations table
    // and mapping the results to a list of DbImplantation objects
    public IEnumerable<DbImplantation> FetchAll()
    {
        // Create a new instance of the database context
        using var context = _shelhaContextProvider.NewContext();

        // Return a list of all Implantation entities
        return context.Implantations.ToList();
    }

    // This method returns the Implantation entity with the given string value by querying the Implantations table
    // and mapping the result to a DbImplantation object
    public DbImplantation FetchByString(string imp)
    {
        // Create a new instance of the database context
        using var context = _shelhaContextProvider.NewContext();

        // Find the first Implantation entity with the given string value and map it to a DbImplantation object
        var implantation = context.Implantations.FirstOrDefault(i => i.nameImplantation == imp);

        // If no Implantation entity was found, throw a KeyNotFoundException
        if (implantation == null) throw new KeyNotFoundException($"Implantation with string {imp} has not been found ");

        // Return the Implantation entity
        return implantation;
    }

    // This method returns the Implantation entity with the given id by querying the Implantations table
    // and mapping the result to a DbImplantation object
    public DbImplantation FetchById(int id)
    {
        // Create a new instance of the database context
        using var context = _shelhaContextProvider.NewContext();

        // Find the first Implantation entity with the given id and map it to a DbImplantation object
        var implantation = context.Implantations.FirstOrDefault(i => i.idImplantation == id);

        // If no Implantation entity was found, throw a KeyNotFoundException
        if (implantation == null) throw new KeyNotFoundException($"Implantation with id {id} has not been found");

        // Return the Implantation entity
        return implantation;
    }
}
