using System.Security.Claims;
using TodoList.Constants;
using TodoList.Exceptions;
using TodoList.Services;

namespace TodoList.Utils;

public static class ControllersUtils
{
    public static void DeleteRefreshTokenCookie(IResponseCookies cookies, AuthCookieOptions authCookieOptions)
    {
        cookies.Delete(CommonConstants.REFRESH_TOKEN_NAME, authCookieOptions.Delete);
    }

    public static string GetExistingRefreshTokenId(IRequestCookieCollection cookies)
    {
        bool hasRefreshToken = cookies
            .TryGetValue(CommonConstants.REFRESH_TOKEN_NAME, out string? currentRefreshTokenId);

        if (!hasRefreshToken || string.IsNullOrEmpty(currentRefreshTokenId))
        {
            throw new NotFoundException(ExceptionMessage.NOT_EXISTING_REFRESH_TOKEN);
        }

        return currentRefreshTokenId;
    }

    public static long GetUserIdFromPayload(IEnumerable<Claim> userClaims)
    {
        return long.Parse(userClaims
            .FirstOrDefault(uc => uc.Type == ClaimTypes.NameIdentifier)!.Value);
    }
}