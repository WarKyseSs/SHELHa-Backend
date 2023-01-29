using Domain;

namespace Application.UseCases.Users.Dtos;

// Data
// Transfer
// Object
public class DtoOutputUser
{
    public int Id { get; set; }
    public string UserRole { get; set; }
    
    public Boolean MailValidation { get; set; }
    
    public string Username { get; set; }
    public string Lastname { get; set; }
    public string Firstname { get; set; }
    public string Mailaddress { get; set; }
    
    
    public string Password { get; set; }
    public string Implantation { get; set; }
    public string Validatorkey { get; set; }
    
    public DateTime ConnectionDate { get; set; }
    public DateTime RegistrationDate { get; set; }
    
    public Boolean CanChange { get; set; }
}