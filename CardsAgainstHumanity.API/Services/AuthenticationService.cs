using CardsAgainstHumanity.API.DataTransferObjects.UserDto;
using CardsAgainstHumanity.API.Middleware.ErrorHandlers;
using CardsAgainstHumanity.DatabaseAccess.DataAccess;
using CardsAgainstHumanity.DatabaseAccess.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CardsAgainstHumanity.API.Services;

public class AuthenticationService : IAuthenticationService {
    private readonly CahDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthenticationService(CahDbContext context, IConfiguration configuration) {
        _context = context;
        _configuration = configuration;
    }

    public void Register(CreateUserDto par) {
        var (hash, salt) = PasswordHasher.Hash(par.Password);

        var user = new User {
            Nickname = par.Nickname,
            Role = UserRole.User,
            Email = par.Email,
            Hash = hash,
            Salt = salt,
            Created = DateTime.Now
        };

        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public string Login(string nickname, string password) {
        var user = 
            (from u in _context.Users
            where u.Nickname == nickname
            select u).FirstOrDefault();

        if(user == null) {
            throw ApiError.NotFound("User not found");
        }

        var valid = PasswordHasher.Verify(password, user.Hash, user.Salt);

        if(!valid) {
            throw ApiError.Unauthorized("Invalid password");
        }

        var token = TokenManager.GenerateToken(user, _configuration);
        return token;
    }
}

static class TokenManager {
    public static string GenerateToken(User user, IConfiguration configuration) {
        var claims = new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        });

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
        var tokenDescriptor = new SecurityTokenDescriptor {
            Issuer = configuration["JWT:ValidAudience"],
            Audience = configuration["JWT:ValidAudience"],
            Expires = DateTime.Now.AddHours(24),
            Subject = claims,
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }
}

static class PasswordHasher {
    public static (Byte[] hash, Byte[] salt) Hash(string password) {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var saltBytes = new byte[16];
        RandomNumberGenerator.Fill(saltBytes);

        var passwordWithSaltBytes = passwordBytes.Concat(saltBytes).ToArray();
        var hash = SHA256.HashData(passwordWithSaltBytes);

        return (hash, saltBytes);
    }

    public static bool Verify(string password, Byte[] hash, Byte[] salt) {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var saltBytes = salt;

        var passwordWithSaltBytes = passwordBytes.Concat(saltBytes).ToArray();
        var generatedHash = SHA256.HashData(passwordWithSaltBytes);

        return hash.SequenceEqual(generatedHash);
    }
}