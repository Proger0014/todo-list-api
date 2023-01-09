namespace TodoList.Utils;

public static class CommonCookieOptions
{
    public static CookieOptions Default
    {
        get => new CookieOptions()
        {
            HttpOnly = true,
            MaxAge = TimeSpan.FromMinutes(20),
            Path = "/api/v1/auth"
        };
    }

    public static CookieOptions Delete
    {
        get => new CookieOptions()
        {
            HttpOnly = true,
            MaxAge = TimeSpan.Zero,
            Expires = DateTime.Now.AddDays(-1),
            Path = "/api/v1/auth"
        };
    }
}