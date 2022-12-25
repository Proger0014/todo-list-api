using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TodoList.Services;
using TodoList.DTO.User;
using TodoList.DTO.Token;

namespace TodoList.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : ControllerBase
{
    private UserService _userService;
    private TokenService _tokenService;

    public AuthController(UserService userService, TokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }


    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] UserLogin login)
    {
        var user = _userService.GetUserByLogin(login);

        if (user != null)
        {
            var token = _tokenService.GenerateToken(user);
            return Ok(new TokenResponse(token, "123"));
        }

        return NotFound("User not found");
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public IActionResult Register([FromBody] UserRegister register)
    {
        try 
        {
            _userService.AddUser(register);
            return Ok();
        } 
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [Authorize]
    [HttpPost]
    [Route("refresh-token")]
    public IActionResult RefreshToken()
    {
        return BadRequest();
    }
}