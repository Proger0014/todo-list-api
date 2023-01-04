using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoList.Models.SessionStorage;
using TodoList.Models.User;

namespace TodoList.Extensions;

public static class TokensExtension
{
    public static string GenerateJWT(this User user)
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

    public static void ChangeSession(this Session targetSession, Session changedSession)
    {
        targetSession.Expiration = changedSession.Expiration;
        targetSession.RefreshToken = changedSession.RefreshToken;
    }
}