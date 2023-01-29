using Application.UseCases.UserConnexion.Dtos;
using Infrastructure.Ef.DbEntities;

namespace Application.Services.User;

public interface IUserService
{
    Domain.User FetchById(int idUser);
    int GetIdFromUserByUsername(DbUser user);
    Dictionary<int, string> GetUsernameDictionary(List<int> userIds);
    public int getIdOfUserByUseranme(DtoConnectionUser user);
}