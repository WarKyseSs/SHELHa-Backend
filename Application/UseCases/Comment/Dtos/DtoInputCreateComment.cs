using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Comment.Dtos;

public class DtoInputCreateComment
{
    [Required]public int IdPost { get; set; }
    [Required]public int IdUser { get; set; }
    [Required]public string Message { get; set; }
}