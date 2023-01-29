using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Users.Dtos;

public class DtoInputCreateUser
{
    [Required] public string Username { get; set; }
    [Required] public string Lastname { get; set; }
    [Required] public string Firstname { get; set; }
    [Required] public string Mailaddress { get; set; }
    [Required] public string Password { get; set; }
    [Required] public string Implantation { get; set; }
}