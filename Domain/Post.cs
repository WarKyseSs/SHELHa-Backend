namespace Domain;

public class Post
{
    public int idPost { get; set; }
    
    public int idAuthor { get; set; }
    
    public int idCat { get; set; }

    public string message { get; set; }
    
    public string sujet { get; set; }
    
    public DateTime datePost { get; set; }
    
    public DateTime dateLastEdit { get; set; }
    
    public string urlPost { get; set; }
}