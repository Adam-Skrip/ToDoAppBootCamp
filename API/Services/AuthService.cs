using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Database;
using API.Entities.Domain;
using API.Entities.DTO;
using API.Entities.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(ApplicationContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<int> Register(UserDto userDto)
    {
        if (await UserExists(userDto.Username))
        {
            throw new Exception("User already exists!");
        }

        if (userDto.Password != userDto.PasswordConfirmation)
        {
            throw new Exception("Passwords do not match!");
        }

        this.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        User user = new User
        {
            Username = userDto.Username,
            PasswordSalt = passwordSalt,
            PasswordHash = passwordHash,
            Email = userDto.Email
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return StatusCodes.Status201Created;
    }

    public async Task<string> Login(string username, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());

        if (user is null)
        {
            throw new Exception("user was not found!");
        }
        
        if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            throw new Exception("Bad password");
        }
        
        return CreateToken(user);
    }

    public async Task<bool> UserExists(string username)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username.ToLower());

        return user is not null;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hash = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hash.Key;
            passwordHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hash = new System.Security.Cryptography.HMACSHA512(passwordSalt))
        {
            var computeHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computeHash.SequenceEqual(passwordHash);
        }
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var tokenKey = _configuration["Token:Key"];
        var issuer = _configuration["Token:Issuer"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials,
            Issuer = issuer
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }
}