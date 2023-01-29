using System.Text.RegularExpressions;

namespace Domain.Utils;

public static class UserValidator
{
    private static readonly Regex RegexValidatePassword = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
    private static readonly Regex RegexValidateMailAddress = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
    private static readonly Regex RegexValidateStudentMailAddress = new Regex(@"^la\d{6}@student.helha.be$");
    private static readonly Regex RegexValidateTeacherMailAddress = new Regex(@"^(([\w\.\-]+)@helha.be)$");

    public static bool ValidatePassword(string password)
    {
        return RegexValidatePassword.IsMatch(password);
    }
    
    public static bool ValidateMailAddress(string mailAddress)
    {
        return RegexValidateMailAddress.IsMatch(mailAddress);
    }

    public static bool ValidateHelhaMailAddress(string mailAddress)
    {
        return (RegexValidateStudentMailAddress.IsMatch(mailAddress)) ||
               RegexValidateTeacherMailAddress.IsMatch(mailAddress);
    }
}