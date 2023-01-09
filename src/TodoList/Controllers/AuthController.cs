using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TodoList.Services;
using TodoList.DTO.User;
using TodoList.DTO.Token;
using TodoList.Extensions;
using TodoList.Utils;

namespace TodoList.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
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
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginRequest login)
    {
        var user = _userService.GetUserByLogin(login);

        if (user == null)
        {
            return NotFound("User not found");
        }

        var userLastRefreshToken = _refreshTokenService.GetRefreshTokenByUserId(user.Id);

        if (userLastRefreshToken != null)
        {
            _refreshTokenService.RemoveRefreshToken(userLastRefreshToken);
        }

        var accessToken = user.GenerateJWT();
        var refreshToken = _refreshTokenService.GenerateRefreshToken(new RefreshTokenCreate(
            user.Id,
            HttpContext.Request.Headers["User-Agent"].ToString()));

        HttpContext.Response.Cookies.Append("refreshToken", refreshToken, CommonCookieOptions.Default);

        return Ok(new TokenResponse()
        {
            AccessToken = accessToken
        });

    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register([FromBody] UserRegisterRequest register)
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

    /**
     * Почему для всех? если рефреш токен истечет, то я все равно смогу сделать рефреш токен
     */
    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public IActionResult RefreshToken()
    {
        bool hasRefreshToken = HttpContext.Request.Cookies
            .TryGetValue("refreshToken", out string? currentRefreshTokenId);

        if (!hasRefreshToken)
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
        DeleteRefreshTokenCookie();

        var accessToken = user.GenerateJWT();
        var refreshToken = _refreshTokenService.GenerateRefreshToken(new RefreshTokenCreate(
            user.Id,
            HttpContext.Request.Headers["User-Agent"].ToString()));

        HttpContext.Response.Cookies.Append("refreshToken", refreshToken, CommonCookieOptions.Default);

        return Ok(new TokenResponse()
        {
            AccessToken = accessToken
        });
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        bool hasRefreshToken = HttpContext.Request.Cookies
            .TryGetValue("refreshToken", out string? currentRefreshTokenId);

        if (!hasRefreshToken)
        {
            return BadRequest();
        }

        var currentRefreshToken = _refreshTokenService.GetRefreshToken(currentRefreshTokenId);

        _refreshTokenService.RemoveRefreshToken(currentRefreshToken);

        DeleteRefreshTokenCookie();

        return Ok();
    }

    private void DeleteRefreshTokenCookie()
    {
        HttpContext.Response.Cookies
            .Delete("refreshToken", CommonCookieOptions.Delete);
    }
}
