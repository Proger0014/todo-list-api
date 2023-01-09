using System.Text.Json.Serialization;

namespace TodoList.DTO.User;

public class UserRegisterRequest
{
    [JsonPropertyName("nick_name")]
    public string Nickname { get; set; }
    [JsonPropertyName("login")]
    public string Login { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }
}