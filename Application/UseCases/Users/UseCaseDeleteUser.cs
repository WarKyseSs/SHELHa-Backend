using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Users;

// Use case for deleting a user
public class UseCaseDeleteUser : IUseCaseWriter<bool, int>
{
    // Repository for accessing user data in the database
    private readonly IUserRepository _userRepository;

    // Constructor for dependency injection
    public UseCaseDeleteUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // Executes the use case
    public bool Execute(int id)
    { 
        // Deletes the user from the database
        var dbUser = _userRepository.Delete(id);
        // Returns a boolean indicating if the user was successfully deleted
        return dbUser;
    }
}