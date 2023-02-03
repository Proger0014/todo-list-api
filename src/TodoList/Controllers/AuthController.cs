using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TodoList.Services;
using TodoList.DTO.User;
using TodoList.DTO.Token;
using TodoList.Extensions;
using TodoList.Utils;
using TodoList.Exceptions;
using TodoList.Constants;

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
        }).Id.ToString();

        HttpContext.Response.Cookies.Append(CommonConstants.REFRESH_TOKEN_NAME, refreshToken, CommonCookieOptions.Default);

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
        string currentRefrehsTokenId = ControllersUtils.GetExistingRefreshTokenId(HttpContext.Request.Cookies);

        var currentRefreshToken = _refreshTokenService.GetRefreshToken(currentRefrehsTokenId);

        if (currentRefreshToken.IsRevorked())
        {
            throw new TokenExpiredException(ExceptionMessage.TOKEN_EXPIRED);
        }

        var userIdentity = HttpContext.User.Identity;

        if (userIdentity == null)
        {
            throw new AccessDeniedException(ExceptionMessage.ACCESS_DENIED);
        }

        var user = _userService.GetUserById(currentRefreshToken.UserId);

        _refreshTokenService.RemoveRefreshToken(currentRefreshToken);

        ControllersUtils.DeleteRefreshTokenCookie(HttpContext.Response.Cookies);

        var accessToken = user.GenerateJWT();
        var refreshToken = _refreshTokenService.GenerateRefreshToken(new RefreshTokenCreate()
        {
            UserId = user.Id,
            FingerPrint = HttpContext.Request.Headers["User-Agent"].ToString()
        }).Id.ToString();

        HttpContext.Response.Cookies.Append(CommonConstants.REFRESH_TOKEN_NAME, refreshToken, CommonCookieOptions.Default);

        return Ok(new TokenResponse()
        {
            AccessToken = accessToken
        });
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        string currentRefreshTokenId = ControllersUtils.GetExistingRefreshTokenId(HttpContext.Request.Cookies);

        var currentRefreshToken = _refreshTokenService.GetRefreshToken(currentRefreshTokenId);

        _refreshTokenService.RemoveRefreshToken(currentRefreshToken);

        ControllersUtils.DeleteRefreshTokenCookie(HttpContext.Response.Cookies);

        return Ok();
    }
}
