namespace Application.UseCases.Comment.Dtos;

public class DtoOutputComment
{
    public int IdComment { get; set; }
    public int IdPost { get; set; }
    public string Username { get; set; }
    public int IdUser { get; set; }
    public string Message { get; set; }
    public DateTime DateComment { get; set; }
    public DateTime DateLastEdit { get; set; }
    public bool CanChange { get; set; }
    public string UserRole { get; set; }
}