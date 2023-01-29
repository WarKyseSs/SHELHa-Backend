using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Comment.Dtos;

public class DtoInputComment
{
    [Required]public int IdComment { get; set; }
    [Required]public int IdPost { get; set; }
    [Required]public int IdUser { get; set; }
    [Required]public string Message { get; set; }
    [Required]public DateTime DateComment { get; set; }
    [Required]public DateTime DateLastEdit { get; set; }
}