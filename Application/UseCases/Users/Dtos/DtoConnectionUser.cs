using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.UserConnexion.Dtos;

public class DtoConnectionUser
{
    [Required] public string Username { get; set; }
    [Required] public string Password { get; set; }
    
}