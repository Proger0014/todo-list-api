using System.Text.Json.Serialization;

namespace TodoList.DTO.User;

public class UserLogin
{
    [JsonPropertyName("login")]
    public string Login { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }
}