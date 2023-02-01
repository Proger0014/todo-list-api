using TodoList.Extensions;

namespace TodoList.Utils;

public static class CommonCookieOptions
{
    public static CookieOptions Default => new()
    {
        HttpOnly = true,
        MaxAge = TimeSpan.FromMinutes(MaxAgeRefreshToken()),
        Path = "/api/v1/auth"
    };

    public static CookieOptions Delete => new()
    {
        HttpOnly = true,
        MaxAge = TimeSpan.Zero,
        Expires = DateTime.Now.AddDays(-1),
        Path = "/api/v1/auth"
    };


    public static int MaxAgeRefreshToken()
    {
        var env = CommonUtils.GetEnvironment();

        return int.Parse(env.GetSetting("Jwt:RefreshToken:MaxAge"));
    }
}