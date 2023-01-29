using Infrastructure.Ef;

namespace Application.Services.Implantation;

// Service for managing implantations
public class ImplantationService : IImplantationService
{
    // Repository for accessing implantation data in the database
    private readonly IImplantationRepository _implantationRepository;

    // Constructor for dependency injection
    public ImplantationService(IImplantationRepository implantationRepository)
    {
        _implantationRepository = implantationRepository;
    }

    // Fetches an implantation by its ID
    public Domain.Implantation Fetch(int id)
    {
        // Retrieves the implantation from the database
        var dbImplantation = _implantationRepository.FetchById(id);
        // Maps the database implantation to the domain implantation
        return Mapper.GetInstance().Map<Domain.Implantation>(dbImplantation);
    }

    // Fetches an implantation by its string identifier
    public Domain.Implantation FetchByString(string imp)
    {
        // Retrieves the implantation from the database
        var dbImplantation = _implantationRepository.FetchByString(imp);
        // Maps the database implantation to the domain implantation
        return Mapper.GetInstance().Map<Domain.Implantation>(dbImplantation);
    }
}