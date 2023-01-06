using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TodoList.Services;
using TodoList.DTO.User;
using TodoList.DTO.Token;
using TodoList.Extensions;
using TodoList.Models.RefreshToken;

namespace TodoList.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : ControllerBase
{
    private UserService _userService;
    private RefreshTokenService _refreshTokenService;

    public AuthController(
        UserService userService, 
        RefreshTokenService refreshTokenService)
    {
        _userService = userService;
        _refreshTokenService = refreshTokenService;
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

        var userLastRefreshToken = _refreshTokenService.GetRefreshTokenByUserId(user.Id);

        if (userLastRefreshToken == null)
        {
            return BadRequest();
        }

        _refreshTokenService.RemoveRefreshToken(userLastRefreshToken);

        var accessToken = user.GenerateJWT();
        var refreshToken = _refreshTokenService.GenerateRefreshToken(new RefreshTokenCreate(
            user.Id,
            HttpContext.Request.Headers["User-Agent"].ToString()));

        var cookieOptions = new CookieOptions()
        {
            HttpOnly = true,
            MaxAge = TimeSpan.FromMinutes(20),
            Path = "/api/v1/auth"
        };

        HttpContext.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);

        return Ok(new TokenResponse(accessToken));

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

    [AllowAnonymous]
    [HttpPost]
    [Route("refresh-token")]
    public IActionResult RefreshToken()
    {
        var currentRefreshTokenId = HttpContext.Request.Cookies["refreshToken"].ToString();

        if (string.IsNullOrEmpty(currentRefreshTokenId))
        {
            return BadRequest();
        }

        var currentRefreshToken = _refreshTokenService.GetRefreshToken(currentRefreshTokenId);
        var user = _userService.GetUserById(currentRefreshToken.UserId);

        if (currentRefreshToken == null ||
            currentRefreshToken.IsRevorked())
        {
            return BadRequest();
        }

        _refreshTokenService.RemoveRefreshToken(currentRefreshToken);
        HttpContext.Response.Cookies.Delete("refreshToken");

        var accessToken = user.GenerateJWT();
        var refreshToken = _refreshTokenService.GenerateRefreshToken(new RefreshTokenCreate(
            user.Id,
            HttpContext.Request.Headers["User-Agent"].ToString()));

        var cookieOptions = new CookieOptions()
        {
            HttpOnly = true,
            MaxAge = TimeSpan.FromMinutes(20),
            Path = "/api/v1/auth"
        };

        HttpContext.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);

        return Ok(new TokenResponse(accessToken));
    }
}