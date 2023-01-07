using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Models.RefreshToken;

public class RefreshToken
{
    [Key, Column("token")]
    public string Token { get; set; }
    [Column("finger_print")]
    public string FingerPrint { get; set; }
    [Column("user_id")]
    public long UserId { get; set; }
    [Column("added_time")]
    public DateTime AddedTime { get; set; }
    [Column("expiration_time")]
    public DateTime ExpirationTime { get; set; }

    public RefreshToken(
        string token, long userId,
        string fingerPrint,
        DateTime addedTime,
        DateTime expirationTime)
    {
        Token = token;
        UserId = userId;
        FingerPrint = fingerPrint;
        AddedTime = addedTime;
        ExpirationTime = expirationTime;
    }

    public RefreshToken() { }

    public bool IsRevorked()
    {
        if (DateTime.Now > ExpirationTime)
        {
            return true;
        }

        return false;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if ((RefreshToken)obj == this) return true;
        var refreshToken = (RefreshToken)obj;

        if (Token == refreshToken.Token &&
            UserId == refreshToken.UserId &&
            FingerPrint == refreshToken.FingerPrint &&
            AddedTime == refreshToken.AddedTime &&
            ExpirationTime == refreshToken.ExpirationTime)
        {
            return true;
        }

        return false;
    }
}