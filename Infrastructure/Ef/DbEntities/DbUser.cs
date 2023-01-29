namespace Infrastructure.Ef.DbEntities;

public class DbUser
{
    public int id { get; set; }
    public int idUserRole { get; set; }
    //public string role { get; set; } 
    public Boolean mailValidation { get; set; }
    
    public string username { get; set; }
    public string lastname { get; set; }
    public string firstname { get; set; }
    public string mailaddress { get; set; }
    
    
    public string password { get; set; }
    public int idImplantation { get; set; }
    public string validatorkey { get; set; }
    
    public DateTime connectionDate { get; set; }
    public DateTime registrationDate { get; set; }
}