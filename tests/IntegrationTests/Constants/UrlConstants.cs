namespace IntegrationTests.Constants;

public static class UrlConstants
{
    public const string Base = "https://localhost:7278";

    public const string ApiVersion = "api/v1";
    public const string AuthEndpoint = "auth";
    public const string UserEndpoint = "user";


    public const string LoginUrl = $"{Base}/{ApiVersion}/{AuthEndpoint}/login";
    public const string RegisterUrl = $"{Base}/{ApiVersion}/{AuthEndpoint}/register";
    public const string RefreshTokenUrl = $"{Base}/{ApiVersion}/{AuthEndpoint}/refresh-token";
    public const string LogoutUrl = $"{Base}/{ApiVersion}/{AuthEndpoint}/logout";

    // 0 - id:long
    public const string UserGetUrl = $"{Base}/{ApiVersion}/{UserEndpoint}/{{0}}";
}
