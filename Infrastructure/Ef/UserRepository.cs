using Domain.Utils;
using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

// This class implements the IUserRepository interface and provides a concrete implementation
// for managing User entities in a database
public class UserRepository : IUserRepository
{
    // This field holds a reference to a ShelhaContextProvider, which is used to create new instances
    // of the database context
    private readonly ShelhaContextProvider _shelhaContextProvider;

    // The constructor takes a ShelhaContextProvider as an argument and assigns it to the field
    public UserRepository(ShelhaContextProvider shelhaContextProvider)
    {
        _shelhaContextProvider = shelhaContextProvider;
    }

    // This method creates a new User entity in the database and returns it
    public DbUser Create(DbUser user, int idImp)
    {
        // Create a new instance of the database context
        using var context = _shelhaContextProvider.NewContext();

        // Create a new DbUser object and set its properties
        var userToAdd = new DbUser
        {
            mailValidation = true,
            username = user.username,
            lastname = user.lastname,
            firstname = user.firstname,
            mailaddress = user.mailaddress,
            password = Hashing.HashPassword(user.password), // Hash the password
            idImplantation = idImp,
            registrationDate = DateTime.Now,
            // Generate a random key for email validation
            validatorkey = RandomKey.GenerateKey(),
            // Set the idUserRole based on whether the email address is valid or not
            idUserRole = UserValidator.ValidateHelhaMailAddress(user.mailaddress) ? 4 : 5
        };

        // Add the new user to the database and save the changes
        context.Users.Add(userToAdd);
        context.SaveChanges();

        userToAdd.password = "";

        // Return the new user
        return userToAdd;
    }

    // This method returns a list of all User entities by querying the Users table and mapping the results
    // to a list of DbUser objects
    public IEnumerable<DbUser> FetchAll()
    {
        // Create a new instance of the database context
        using var context = _shelhaContextProvider.NewContext();

        var users = context.Users.ToList();

        foreach (var user in users)
        {
            user.password = "";
        }

        // Return a list of all User entities
        return users;
    }

    // This method returns the User entity with the given id by querying the Users table and mapping the result
    // to a DbUser object
    public DbUser FetchById(int id)
    {
        // Create a new instance of the database context
        using var context = _shelhaContextProvider.NewContext();

        // Find the first User entity with the given id and map it to a DbUser object
        var user = context.Users.FirstOrDefault(u => u.id == id);

        // If no User entity was found, throw a KeyNotFoundException
        if (user == null) throw new KeyNotFoundException($"User with id {id} has not been found");

        user.password = "";
        
        // Return the User entity
        return user;
    }

    public DbUser FetchByUsername(string username)
    {
        // Create a new instance of the database context
        using var context = _shelhaContextProvider.NewContext();

        // Find the first User entity with the given id and map it to a DbUser object
        var user = context.Users.FirstOrDefault(u => u.username == username);

        // If no User entity was found, throw a KeyNotFoundException
        if (user == null) throw new KeyNotFoundException($"User with username {username} has not been found");

        user.password = "";
        
        // Return the User entity
        return user;
    }

    // This method updates an existing User entity in the database and returns a boolean value indicating
    // whether the update was successful
    public bool Update(DbUser user, int idImp, int idRole, Boolean changePassword)
    {
        // Create a new instance of the database context
        using var context = _shelhaContextProvider.NewContext();

        try
        {
            var userToFind = context.Users.FirstOrDefault(u => u.username == user.username);
            // Hash the password and update the idImplantation and idUserRole properties of the User entity
            if (changePassword)
            {
                userToFind.password = Hashing.HashPassword(user.password);
            }
            userToFind.idImplantation = idImp;
            userToFind.idUserRole = idRole;

            // Update the User entity in the database
            context.Users.Update(userToFind);

            // Save the changes and return a boolean value indicating whether the update was successful
            // (1 indicates success, 0 indicates failure)
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            // If a concurrency exception occurs, return false
            return false;
        }
    }

    // This method deletes the User entity with the given id and returns a boolean value indicating
    // whether the delete was successful
    public bool Delete(int id)
    {
        // Create a new instance of the database context
        using var context = _shelhaContextProvider.NewContext();

        try
        {
            // Remove the User entity with the given id from the database
            context.Users.Remove(new DbUser() { id = id });

            // Save the changes and return a boolean value indicating whether the delete was successful
            // (1 indicates success, 0 indicates failure)
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            // If a concurrency exception occurs, return false
            return false;
        }
    }
    
    // This method attempts to connect a user by verifying their username and password
    public string ConnectUser(DbUser user)
    {
        // Create a new instance of the database context
        using var context = _shelhaContextProvider.NewContext();

        // Query the database for a User entity with the given username
        var userToFind =  context.Users.FirstOrDefault(u => u.username == user.username);

        // If no such user exists, return "User not found"
        if (userToFind == null) return "User not found";

        // If the user exists, use the Hashing utility to check if the given password matches the hashed password
        // stored in the database
        if (Hashing.ValidatePassword(user.password, userToFind.password))
        {
            // If the passwords match, return "success"
            //update the last connection date 
            userToFind.connectionDate = DateTime.Now;
            context.Users.Update(userToFind);
            context.SaveChanges();
            
            return "success";
        }

        // If the passwords do not match, return "invalid"
        return "invalid";
    }

    // This method returns the idUserRole of the User entity with the given username
    public int GetIdRoleFromUsername(DbUser user)
    {
        // Create a new instance of the database context
        using var context = _shelhaContextProvider.NewContext();

        // Query the database for a User entity with the given username
        var userToFind =  context.Users.FirstOrDefault(u => u.username == user.username);

        // If the user exists, return their idUserRole
        if (userToFind != null) return userToFind.idUserRole;

        // If the user does not exist, return -1
        return -1;
    }
    
    // This method returns a boolean indicating whether the User entity with the given username has a valid email address
    public bool isMailValid(DbUser user)
    {
        // Create a new instance of the database context
        using var context = _shelhaContextProvider.NewContext();

        // Query the database for a User entity with the given username
        var userToFind =  context.Users.FirstOrDefault(u => u.username == user.username);

        // Return true if the user exists and their mailValidation field is true, else return false
        return userToFind != null && userToFind.mailValidation;
    }

// This method returns the id of the User entity with the given username
    public int GetIdFromUserByUsername(DbUser user)
    {
        // Create a new instance of the database context
        using var context = _shelhaContextProvider.NewContext();

        // Query the database for a User entity with the given username
        var userToFind =  context.Users.FirstOrDefault(u => u.username == user.username);

        // If the user exists, return their id
        if (userToFind != null) return userToFind.id;

        // If the user does not exist, return -1
        return -1;
    }
    
    
    /* allow to bind username and id to return in a conversation*/
    // Returns a dictionary that maps user IDs to their username.
    // Only includes users whose ID is present in the given list of user IDs.
    public Dictionary<int, string> GetUsernameDictionary(List<int> userIds)
    {
        using var context = _shelhaContextProvider.NewContext();
        var users = from u in context.Users
            where userIds.Contains(u.id)
            select u;
        
        Dictionary<int, string> userDict = new Dictionary<int, string>();
        
        foreach (var user in users)
        {
            userDict[user.id] = user.username;
        }
        return userDict;
    }
    
}