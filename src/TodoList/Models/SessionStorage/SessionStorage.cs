using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Models.SessionStorage;

public class SessionStorage
{
    [Key, Column("id")]
    public string Id { get; set; }
    [Column("user_id")]
    public long UserId { get; set; }
    [Column("finger_print")]
    public string FingerPrint { get; set; }
    [Column("refresh_token")]
    public string RefreshToken { get; set; }

    public SessionStorage(
        string id, long userId, 
        string fingerPrint, 
        string refreshToken)
    {
        Id = id;
        UserId = userId;
        FingerPrint = fingerPrint;
        RefreshToken = refreshToken;
    }

    public SessionStorage() { }
}