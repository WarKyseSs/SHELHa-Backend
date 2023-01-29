using Application.Services.Implantation;
using Application.UseCases.Users.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Users;

// Use case for creating a user
public class UseCaseCreateUser : IUseCaseWriter<DtoOutputUser, DtoInputCreateUser>
{
    // Repository for accessing user data in the database
    private readonly IUserRepository _userRepository;
    // Service for fetching implantations
    private readonly IImplantationService _implantationService;

    // Constructor for dependency injection
    public UseCaseCreateUser(IUserRepository userRepository, IImplantationService implantationService)
    {
        _userRepository = userRepository;
        _implantationService = implantationService;
    }

    // Executes the use case
    public DtoOutputUser Execute(DtoInputCreateUser input)
    {
        // Fetches the implantation for the user
        var imp = _implantationService.FetchByString(input.Implantation);
        // Creates the user in the database and retrieves the created user
        var dbUser = _userRepository.Create(Mapper.GetInstance().Map<DbUser>(input), imp.idImplantation);
        // Maps the database user to the output DTO
        return Mapper.GetInstance().Map<DtoOutputUser>(dbUser);
    }
}