using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Topic.Dtos;

public class DtoInputTopic
{
    
    [Required]public int idCat { get; set; }
    [Required]public string nameCat { get; set; }
    [Required]public string description { get; set; }
    [Required]public string urlTopic { get; set; }

}