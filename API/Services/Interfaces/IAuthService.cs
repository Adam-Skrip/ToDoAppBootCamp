using API.Entities.Domain;
using API.Entities.DTO;
using API.Entities.Models;

namespace API.Services.Interfaces;

public interface IAuthService
{
    Task<int> Register(UserDto userDto);

    Task<string> Login(string username, string password);
}