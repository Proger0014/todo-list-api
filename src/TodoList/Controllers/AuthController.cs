using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TodoList.Services;
using TodoList.DTO.User;
using TodoList.DTO.Token;
using TodoList.Extensions;
using System.Linq.Expressions;

namespace TodoList.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : ControllerBase
{
    private UserService _userService;
    private RefreshTokenService _refreshTokenService;
    private SessionService _sessionService;

    public AuthController(
        UserService userService, 
        RefreshTokenService refreshTokenService, 
        SessionService sessionService)
    {
        _userService = userService;
        _refreshTokenService = refreshTokenService;
        _sessionService = sessionService;
    }


    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] UserLogin login)
    {
        var user = _userService.GetUserByLogin(login);

        if (user == null)
        {
            return NotFound("User not found");
        }

        var accessToken = user.GenerateJWT();
        var refreshToken = _refreshTokenService.GenerateRefreshToken(user);

        _sessionService.NewSession(new Models.SessionStorage.Session(
            HttpContext.Session.Id,
            user.Id,
            HttpContext.Request.Headers["User-Agent"].ToString(),
            refreshToken,
            DateTime.Now.Add(TimeSpan.FromMinutes(20))));

        var cookieOptions = new CookieOptions()
        {
            HttpOnly = true,
            MaxAge = TimeSpan.FromMinutes(20)
        };

        HttpContext.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);


        return Ok(new TokenResponse(accessToken, refreshToken));

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