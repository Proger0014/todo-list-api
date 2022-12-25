using System.Text.Json.Serialization;

namespace TodoList.DTO.User;

public class UserRegister
{
    private string nickName;
    [JsonPropertyName("nick_name")]
    public string Nickname { 
        get 
        { 
            return nickName; 
        }
        set 
        {
            if (value.Length <= 60 && value.Length >= 0) {
                nickName = value;
                return;
            }

            throw new Exception("value is bigger");
        }
    }
    [JsonPropertyName("login")]
    public string Login { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }
}