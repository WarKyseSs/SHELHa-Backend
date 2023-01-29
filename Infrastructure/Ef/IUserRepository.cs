using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

// This interface defines the methods that a repository for User entities should implement
public interface IUserRepository
{
    // This method should create a new User entity in the database and return it
    DbUser Create(DbUser user, int idImp);

    // This method should return a list of all User entities
    IEnumerable<DbUser> FetchAll();

    // This method should return the User entity with the given id
    DbUser FetchById(int id);
    DbUser FetchByUsername(string username);

    // This method should update an existing User entity in the database and return a boolean value indicating
    // whether the update was successful
    bool Update(DbUser user, int idImp, int idRole, Boolean changePassword);

    // This method should delete the User entity with the given id and return a boolean value indicating
    // whether the delete was successful
    bool Delete(int id);

    // This method should return the string representation of the role of the user with the given credentials
    string ConnectUser(DbUser user);

    // This method should return the id of the role of the user with the given username
    int GetIdRoleFromUsername(DbUser user);

    // This method should return the id of the user with the given username
    int GetIdFromUserByUsername(DbUser user);

    // This method should return a boolean value indicating whether the email of the given user is valid
    bool isMailValid(DbUser user);
    public Dictionary<int, string> GetUsernameDictionary(List<int> userIds);
}