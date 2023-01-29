using Application.UseCases.UserConnexion.Dtos;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.Services.User;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Domain.User FetchById(int idUser)
    {
        // Fetch the user from the database using the user repository
        var dbUser = _userRepository.FetchById(idUser);

        // Map the database user to a domain user and return it
        return Mapper.GetInstance().Map<Domain.User>(dbUser);
    }

    public int GetIdRoleFromUsername(DbUser user)
    {
        return _userRepository.GetIdRoleFromUsername(user);
    }

    public int GetIdFromUserByUsername(DbUser user)
    {
      return  _userRepository.GetIdFromUserByUsername(user);
    }
    
    public Dictionary<int, string> GetUsernameDictionary(List<int> userIds)
    {
        return _userRepository.GetUsernameDictionary(userIds);
    }
    
    
    public int getIdOfUserByUseranme(DtoConnectionUser user)
    {
        return _userRepository.GetIdFromUserByUsername(Mapper.GetInstance().Map<DbUser>(user));
    }
    
    public bool isMailValid(DtoConnectionUser user)
    {
        return _userRepository.isMailValid(Mapper.GetInstance().Map<DbUser>(user));
    }
}