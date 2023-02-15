using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TodoList.Services;
using TodoList.DTO.User;
using TodoList.DTO.Token;
using TodoList.Extensions;
using TodoList.Utils;
using TodoList.Exceptions;
using TodoList.Constants;
using System.Security.Claims;
using TodoList.Models.User;

namespace TodoList.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly RefreshTokenService _refreshTokenService;
    private readonly AuthCookieOptions _authCookieOptions;
    private readonly TokensService _tokensService;

    public AuthController(
        UserService userService,
        RefreshTokenService refreshTokenService,
        AuthCookieOptions authCookieOptions,
        TokensService tokensService)
    {
        _userService = userService;
        _refreshTokenService = refreshTokenService;
        _authCookieOptions = authCookieOptions;
        _tokensService = tokensService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginRequest login)
    {
        User user = _userService.GetUserByLogin(login);

        try
        {
            var userLastRefreshToken = _refreshTokenService.GetRefreshTokenByUserId(user.Id);
            _refreshTokenService.RemoveRefreshToken(userLastRefreshToken);
        }
        catch (Exception) { }

        var accessToken = _tokensService.CreateJWT(user);
        var refreshToken = _refreshTokenService.GenerateRefreshToken(new RefreshTokenCreate()
        {
            UserId = user.Id,
            FingerPrint = HttpContext.Request.Headers["User-Agent"].ToString()
        }).Id.ToString();

        HttpContext.Response.Cookies.Append(CommonConstants.REFRESH_TOKEN_NAME, refreshToken, _authCookieOptions.Default);
        HttpContext.Response.Cookies.Append(CommonConstants.ACCESS_TOKEN_NAME, accessToken, _authCookieOptions.Default);

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

        var user = _userService.GetUserWithAccessDeniedCheck(new UserAccessDeniedCheck()
        {
            UserClaims = ((ClaimsIdentity)userIdentity).Claims,
            UserId = ControllersUtils.GetUserIdFromPayload(((ClaimsIdentity)userIdentity).Claims)
        });

        _refreshTokenService.RemoveRefreshToken(currentRefreshToken);

        ControllersUtils.DeleteRefreshTokenCookie(HttpContext.Response.Cookies, _authCookieOptions);

        var accessToken = _tokensService.CreateJWT(user);
        var refreshToken = _refreshTokenService.GenerateRefreshToken(new RefreshTokenCreate()
        {
            UserId = user.Id,
            FingerPrint = HttpContext.Request.Headers["User-Agent"].ToString()
        }).Id.ToString();

        HttpContext.Response.Cookies.Append(CommonConstants.REFRESH_TOKEN_NAME, refreshToken, _authCookieOptions.Default);

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

        ControllersUtils.DeleteRefreshTokenCookie(HttpContext.Response.Cookies, _authCookieOptions);

        return Ok();
    }
}
