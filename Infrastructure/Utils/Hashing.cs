namespace Infrastructure.Utils;
using BCrypt.Net;
public class Hashing
{
    public static string HashPassword(string password)
    {
        return BCrypt.HashPassword(password, BCrypt.GenerateSalt(12));
    }

    public static bool ValidatePassword(string password, string correctHash)
    {
        return BCrypt.Verify(password, correctHash);
    }
}