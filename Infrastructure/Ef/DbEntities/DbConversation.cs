namespace Infrastructure.Ef.DbEntities;

public class DbConversation
{
    public int Id;
    public int IdUserOne;
    public int IdUserTwo;
    public string LastMessage;
    public string Slug;
    public string Subject;
    public DateTime Timestamp;
}