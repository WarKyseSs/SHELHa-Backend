namespace Application.UseCases.Message.Dtos;

public class DtoOutputMessage
{
    public int Id { get; set; }
    public int IdConversation { get; set; }
    public int IdSender { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
    public Boolean IsRead { get; set; }
    public string UsernameOfSender { get; set; }
}