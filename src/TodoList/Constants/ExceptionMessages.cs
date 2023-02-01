namespace TodoList.Constants;

public static class ExceptionMessage
{
    // TokenExpiredException
    public const string TOKEN_EXPIRED = "token expired";

    // AccessDeniedException
    public const string ACCESS_DENIED = "not authorize";

    // NotFoundException
    public const string NOT_EXISTING_REFRESH_TOKEN = "not existing refresh token cookie";
    public const string USER_NOT_FOUND = "user not found";

    // NotFoundException
    public const string REFRESH_TOKEN_NOT_FOUND = "refresh token not found";

    // KeyNotFoundException
    public const string KEY_IS_NOT_FOUND_SETTING = "key {0:string} is not found in settings file {1:string}";
    public const string CONNECTION_STRING_IS_NOT_FOUND = "connection string is not found in settings file {0:string}";

    // ExistsException
    public const string USER_IS_EXISTS = "user {0:string} is exists";
}