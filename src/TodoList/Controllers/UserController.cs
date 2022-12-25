using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoList.DTO.User;
using TodoList.Models.User;
using TodoList.Services;

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
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetUserById(long id)
    {
        var user = _userService.GetUserById(id);

        if (user == null) 
        {
            return NotFound();
        }

        var identity = HttpContext.User.Identity;

        if (identity != null)
        {
            var userClaims = ((ClaimsIdentity) identity).Claims;

            return Ok(
                new User(
                    user.Id,
                    userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                    user.Login,
                    user.Password));
        }

        return NotFound();
    }
}