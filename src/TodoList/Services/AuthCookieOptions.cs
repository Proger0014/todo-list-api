using TodoList.Constants;
using TodoList.Services.DateTimeProvider;

namespace TodoList.Services;

public class AuthCookieOptions
{
    private readonly IConfiguration _configuration;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AuthCookieOptions(
        IConfiguration configuration,
        IDateTimeProvider dateTimeProvider)
    {
        _configuration = configuration;
        _dateTimeProvider = dateTimeProvider;
    }

    public CookieOptions Default => new()
    {
        HttpOnly = true,
        MaxAge = TimeSpan.FromMinutes(MaxAgeRefreshToken),
        Path = "/api/v1/auth"
    };

    public CookieOptions Delete => new()
    {
        HttpOnly = true,
        MaxAge = TimeSpan.Zero,
        Expires = _dateTimeProvider.DateTimeNow.AddDays(-1),
        Path = "/api/v1/auth"
    };

    public int MaxAgeRefreshToken 
    { 
        get
        {
            const string MAX_AGE_SETTING_KEY = "Jwt:RefreshToken:MaxAge";

            string? maxAgeSetting = _configuration[MAX_AGE_SETTING_KEY];

            if (string.IsNullOrEmpty(maxAgeSetting))
            {
                throw new KeyNotFoundException(string.Format(
                    ExceptionMessage.IS_NOT_FOUND_SETTING, MAX_AGE_SETTING_KEY));
            }

            return int.Parse(maxAgeSetting);
        }
    }
}