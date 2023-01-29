using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Message.Dtos;

public class DtoInputMessage
{
    [Required]public int IdSender { get; set; }
    [Required]public string Message { get; set; }
    [Required]public string ConversationSlug { get; set; }
}