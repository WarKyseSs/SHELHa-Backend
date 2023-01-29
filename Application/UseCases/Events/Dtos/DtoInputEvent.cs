using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Events.Dtos;

public class DtoInputEvent
{
    [Required] public int IdEvent { get; set; }
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }
    [Required] public DateTime DatePublication { get; set; }
    [Required] public DateTime DateEvent { get; set; }
    [Required] public string City { get; set; }
    [Required] public string Street { get; set; }
    [Required] public string StreetNumber { get; set; }
    [Required] public int PostalCode { get; set; }
    [Required]  public int IdAuthor { get; set; } 
}