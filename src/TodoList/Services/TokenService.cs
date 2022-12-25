using TodoList.Models.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using TodoList.Extensions;

namespace TodoList.Services;

public class TokenService
{
    public string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Jwt:Key".GetAppSetting()));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("name", user.Nickname),
        };

        var token = new JwtSecurityToken(
            "Jwt:Issuer".GetAppSetting(), 
            "Jwt:Audience".GetAppSetting(),
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}