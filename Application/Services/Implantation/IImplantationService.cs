namespace Application.Services.Implantation;

public interface IImplantationService
{
    // Fetches an implantation by its ID
    Domain.Implantation Fetch(int id);

    // Fetches an implantation by its string identifier
    Domain.Implantation FetchByString(string imp);
}