using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Models.RefreshToken;

public class RefreshToken
{
    [Key, Column("token")]
    public string Token { get; set; }
    [Column("user_id")]
    public long UserId { get; set; }
    [Column("added_time")]
    public DateTime AddedTime { get; set; }
    [Column("expiration_time")]
    public DateTime ExpirationTime { get; set; }

    public RefreshToken(
        string token, long userId, 
        DateTime addedTime,
        DateTime expirationTime)
    {
        Token = token;
        UserId = userId;
        AddedTime = addedTime;
        ExpirationTime = expirationTime;
    }

    public RefreshToken() { }

    public bool IsRevorked()
    {
        if (AddedTime > ExpirationTime)
        {
            return true;
        }

        return false;
    }
}