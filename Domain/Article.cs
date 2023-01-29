namespace Domain;

public class Article
{
    public int IdArticle { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DatePublication { get; set; }
    public int IdAuthor { get; set; } 
}