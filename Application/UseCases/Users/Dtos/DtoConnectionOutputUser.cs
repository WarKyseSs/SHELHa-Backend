namespace Application.UseCases.UserConnexion.Dtos;

public class DtoConnectionOutputUser
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string LastName { get; set; }
    public int UserRole { get; set; }
    public string MailAddress { get; set; }
    public bool MailValidation { get; set; }

}