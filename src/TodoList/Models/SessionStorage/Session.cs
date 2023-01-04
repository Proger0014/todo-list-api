using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Models.SessionStorage;

public class Session
{
    [Key, Column("id")]
    public string Id { get; set; }
    [Column("user_id")]
    public long UserId { get; set; }
    [Column("finger_print")]
    public string FingerPrint { get; set; }
    [Column("refresh_token")]
    public string RefreshToken { get; set; }
    [Column("expiration")]
    public DateTime Expiration { get; set; }

    public Session(
        string id, long userId, 
        string fingerPrint, 
        string refreshToken,
        DateTime expiration)
    {
        Id = id;
        UserId = userId;
        FingerPrint = fingerPrint;
        RefreshToken = refreshToken;
        Expiration = expiration;
    }

    public Session() { }

    public bool IsRevorked()
    {
        if (DateTime.Now > Expiration)
        {
            return true;
        }

        return false;
    }
}