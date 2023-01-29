namespace Infrastructure.Ef.DbEntities;

public class DbMessage
{
    public int Id { get; set; }
    public int IdConversation { get; set; }
    public int IdSender { get; set; }
    public string Message { get; set; }
    public DateTime TimeStamp { get; set; }
    public Boolean IsRead { get; set; }
}