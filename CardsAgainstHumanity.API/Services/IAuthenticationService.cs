using CardsAgainstHumanity.API.DataTransferObjects.UserDto;
using System.IdentityModel.Tokens.Jwt;

namespace CardsAgainstHumanity.API.Services;

public interface IAuthenticationService {
    void Register(CreateUserDto par);
    string Login(string nickname, string password);
}
