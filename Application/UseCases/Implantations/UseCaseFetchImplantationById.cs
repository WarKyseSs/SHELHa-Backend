using Application.UseCases.Implantations.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Users;

// Use case for fetching an implantation by its ID
public class UseCaseFetchImplantationById : IUseCaseParameterizedQuery<DtoOutputImplantation, int>
{
    // Repository for accessing implantation data in the database
    private readonly IImplantationRepository _implantationRepository;

    // Constructor for dependency injection
    public UseCaseFetchImplantationById(IImplantationRepository implantationRepository)
    {
        _implantationRepository = implantationRepository;
    }

    // Executes the use case
    public DtoOutputImplantation Execute(int id)
    {
        // Retrieves the implantation from the database
        var dbImplantation = _implantationRepository.FetchById(id);
        // Maps the database implantation to the output DTO
        return Mapper.GetInstance().Map<DtoOutputImplantation>(dbImplantation);
    }
}