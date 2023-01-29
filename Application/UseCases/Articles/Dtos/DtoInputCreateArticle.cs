using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Articles.Dtos;

public class DtoInputCreateArticle
{
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }
    [Required] public DateTime DatePublication { get; set; }
    [Required] public int IdAuthor { get; set; }
}