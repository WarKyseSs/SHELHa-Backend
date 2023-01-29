namespace Infrastructure.Ef.DbEntities;

public class DbComment
{
    public int idComment { get; set; }
    public int idPost { get; set; }
    public int idUser { get; set; }
    public string message { get; set; }
    public DateTime dateComment { get; set; }
    public DateTime dateLastEdit { get; set; }
}