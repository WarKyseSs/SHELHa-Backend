namespace Application.UseCases.Post.Dtos;

public class DtoOutputPost
{
    public int IdPost { get; set; }
    public int IdAuthor { get; set; }
    public string Username { get; set; }
    public int IdCat { get; set; }
    public string Message { get; set; }
    public string Sujet { get; set; }
    public DateTime DatePost { get; set; }
    public DateTime DateLastEdit { get; set; }
    public string UrlPost { get; set; }
    public bool CanChange { get; set; }
    public string UserRole { get; set; }
}