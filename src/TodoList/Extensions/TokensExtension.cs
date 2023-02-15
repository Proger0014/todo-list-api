using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoList.Models.User;

namespace TodoList.Extensions;

public class TokensService
{
    private readonly IConfiguration _configuration;

    public TokensService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateJWT(User user)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // потом также, как и nameidentifier
        };

        int expiresInMinutes = int.Parse(_configuration["Jwt:Expires"]);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(expiresInMinutes)),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}