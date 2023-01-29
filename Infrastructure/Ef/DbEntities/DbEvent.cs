namespace Infrastructure.Ef.DbEntities;

public class DbEvent
{
    public int IdEvent { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DatePublication { get; set; }
    public DateTime DateEvent { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string StreetNumber { get; set; }
    public int PostalCode { get; set; }
    public int IdAuthor { get; set; } 
    public string urlEvent{ get; set; }
}