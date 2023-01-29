using Application.UseCases.Implantations.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Users;

// Use case for fetching all implantations
public class UseCaseFetchAllImplantation : IUseCaseQuery<IEnumerable<DtoOutputImplantation>>
{
    // Repository for accessing implantation data in the database
    private readonly IImplantationRepository _implantationRepository;

    // Constructor for dependency injection
    public UseCaseFetchAllImplantation(IImplantationRepository implantationRepository)
    {
        _implantationRepository = implantationRepository;
    }

    // Executes the use case
    public IEnumerable<DtoOutputImplantation> Execute()
    {
        // Retrieves all implantations from the database
        var dbImplantations = _implantationRepository.FetchAll();
        // Maps the database implantations to the output DTOs
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputImplantation>>(dbImplantations);
    }
}