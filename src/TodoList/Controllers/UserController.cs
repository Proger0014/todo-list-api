using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoList.Exceptions;
using TodoList.Models.User;
using TodoList.Services;
using TodoList.Constants;
using TodoList.DTO.User;

namespace TodoList.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/user")]
public class UserController : ControllerBase
{
    private UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult GetUserByIdOwn(long id)
    {
        var userIdentity = HttpContext.User.Identity;

        if (userIdentity == null)
        {
            throw new AccessDeniedException("Not authorize");
        }

        var user = _userService.GetUserWithAccessDeniedCheck(new UserAccessDeniedCheck()
        {
            UserClaims = ((ClaimsIdentity)userIdentity).Claims,
            UserId = id
        });

        return Ok(user);
    }
}