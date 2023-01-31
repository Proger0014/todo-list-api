using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoList.Models.User;
using TodoList.Utils;

namespace TodoList.Extensions;

public static class TokensExtension
{
    public static string GenerateJWT(this User user)
    {
        var env = CommonUtils.GetEnvironment();

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Jwt:Key".GetSetting(env)));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // потом также, как и nameidentifier
        };

        int expiresInMinutes = int.Parse("Jwt:Expires".GetSetting(env));

        var token = new JwtSecurityToken(
            issuer: "Jwt:Issuer".GetSetting(env),
            audience: "Jwt:Audience".GetSetting(env),
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(expiresInMinutes)),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}