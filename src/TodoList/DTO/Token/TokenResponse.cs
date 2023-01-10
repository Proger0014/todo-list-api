using System.Text.Json.Serialization;

namespace TodoList.DTO.Token;

public class TokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    public TokenResponse()
    {
        AccessToken = string.Empty;
    }
}