using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Post.Dtos;

public class DtoInputCreatePost
{
    [Required]public int IdAuthor { get; set; }
    [Required]public int IdCat { get; set; }
    [Required]public string Message { get; set; } 
    [Required]public string Sujet { get; set; }
}