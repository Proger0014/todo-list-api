using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TodoList.Services;
using TodoList.DTO.User;
using TodoList.DTO.Token;
using TodoList.Extensions;
using TodoList.Utils;
using TodoList.Exceptions;

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

        Models.User.User user = _userService.GetUserByLogin(login);

        try
        {
            var userLastRefreshToken = _refreshTokenService.GetRefreshTokenByUserId(user.Id);
            _refreshTokenService.RemoveRefreshToken(userLastRefreshToken);
        }
        catch (Exception) { }

        var accessToken = user.GenerateJWT();
        var refreshToken = _refreshTokenService.GenerateRefreshToken(new RefreshTokenCreate()
        {
            UserId = user.Id,
            FingerPrint = HttpContext.Request.Headers["User-Agent"].ToString()
        });

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
        _userService.AddUser(register);

        return Ok();
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

        if (!hasRefreshToken || string.IsNullOrEmpty(currentRefreshTokenId))
        {
            throw new NotFoundException("not existing refresh token cookie");
        }

        var currentRefreshToken = _refreshTokenService.GetRefreshToken(currentRefreshTokenId);

        if (currentRefreshToken.IsRevorked())
        {
            throw new TokenExpiredException("token expired");
        }

        var user = _userService.GetUserById(currentRefreshToken.UserId);

        _refreshTokenService.RemoveRefreshToken(currentRefreshToken);
        DeleteRefreshTokenCookie();

        var accessToken = user.GenerateJWT();
        var refreshToken = _refreshTokenService.GenerateRefreshToken(new RefreshTokenCreate()
        {
            UserId = user.Id,
            FingerPrint = HttpContext.Request.Headers["User-Agent"].ToString()
        });

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

        if (!hasRefreshToken || string.IsNullOrEmpty(currentRefreshTokenId))
        {
            throw new NotFoundException("not existing refresh token cookie");
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
