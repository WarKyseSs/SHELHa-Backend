using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Users.Dtos;

public class DtoInputUpdateUser
{
    [Required] public int Id { get; set; }
    public string UserRole { get; set; }
    public Boolean MailValidation { get; set; }
    [Required] public string Username { get; set; }
    public string Lastname { get; set; }
    public string Firstname { get; set; }
    [Required] public string Mailaddress { get; set; }
    public string Password { get; set; }
    [Required] public string Implantation { get; set; }
    public string Validatorkey { get; set; }
    
    public DateTime ConnectionDate { get; set; }
    public DateTime RegistrationDate { get; set; }
    
    public Boolean ChangePassword { get; set; }
}