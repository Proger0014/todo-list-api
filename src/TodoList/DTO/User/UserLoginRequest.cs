using System.Text.Json.Serialization;

namespace TodoList.DTO.User;

public class UserLoginRequest
{
    [JsonPropertyName("login")]
    public string Login { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }
}