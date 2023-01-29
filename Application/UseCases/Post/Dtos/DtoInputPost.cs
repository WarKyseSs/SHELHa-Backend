using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Post.Dtos;

public class DtoInputPost
{
    [Required]public int IdPost { get; set; }
    [Required]public int IdAuthor { get; set; }
    [Required]public int IdCat { get; set; }
    [Required]public string Message { get; set; } 
    [Required]public string Sujet { get; set; }
    [Required]public DateTime DatePost { get; set; }
    [Required]public DateTime DateLastEdit { get; set; }
    [Required]public string UrlPost { get; set; }
}