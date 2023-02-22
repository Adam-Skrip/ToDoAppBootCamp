using API.Entities.DTO;
using API.Entities.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;


    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
    [SwaggerOperation(
        summary: "Register User",
        description: "Register a User",
        OperationId = "registerUser",
        Tags = new[] { "Auth API" })]
    public async Task<ActionResult> RegisterAsync(
        [FromBody, Bind] UserDto userDto
    )
    {
        await _authService.Register(userDto);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
    [SwaggerOperation(
        summary: "Login User",
        description: "Login a User",
        OperationId = "loginuser",
        Tags = new[] { "Auth API" })]
    public async Task<ActionResult> LoginAsync(
        [FromBody, Bind()] UserModel model 
    )
    {
        try
        {
            var jwtToken = await _authService.Login(model.Username, model.Password);
            
            return Ok(jwtToken);
        }
        catch (Exception e)
        {
            return Unauthorized(e.Message);
        }
    }
}