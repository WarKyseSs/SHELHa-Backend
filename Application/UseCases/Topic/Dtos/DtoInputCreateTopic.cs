using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Topic.Dtos;

public class DtoInputCreateTopic
{
    [Required]public string nameCat { get; set; }
    [Required]public string description { get; set; }
    [Required]public DateTime datelastPost { get; set; } 
}