namespace Domain;

public class Conversation
{
    private int Id { get; set; }
    private int IdUserOne { get; set; }
    private int IdUserTwo { get; set; }
    private string LastMessageSend { get; set; }
    private string Slug { get; set; }
    private string Subject { get; set; }
    private DateTime TimeStamp { get; set; }
    
}