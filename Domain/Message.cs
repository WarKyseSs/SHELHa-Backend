namespace Domain;

public class Message
{
    private int Id { get; set; }
    private int Sender_id { get; set; }
    private int Receiver_id { get; set; }
    private string message { get; set; }
    private DateTime Date { get; set; }
}