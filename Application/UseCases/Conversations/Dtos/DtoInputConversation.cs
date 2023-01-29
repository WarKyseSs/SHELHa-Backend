using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Conversations.Dtos;

public class DtoInputConversation
{
    [Required]public int IdOfSender { get; set; }
    [Required]public string UsernameOfReceiver { get; set; }
    [Required]public string LastMessage { get; set; }
    [Required]public string Subject { get; set; }
}