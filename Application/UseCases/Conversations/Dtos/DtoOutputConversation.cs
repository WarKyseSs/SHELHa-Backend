namespace Application.UseCases.Conversations.Dtos;

public class DtoOutputConversation
{
    public int Id { get; set; }
    public int IdUserOne { get; set; }
    public int IdUserTwo { get; set; }
    public string LastMessage { get; set; }
    public string Slug { get; set; }
    public string Subject { get; set; }
    public DateTime Timestamp { get; set; }
    public int IdUserRequesting { get; set; }
    public string UsernameUserOne { get; set; }
    public string UsernameUserTwo { get; set; }
}