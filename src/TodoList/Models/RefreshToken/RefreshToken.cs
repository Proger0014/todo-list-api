using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoList.Models.Id;

namespace TodoList.Models.RefreshToken;

public class RefreshToken : ID<Guid>, IEquatable<RefreshToken>
{
    [Key]
    public Guid Id { get; set; }
    public string FingerPrint { get; set; }
    public long UserId { get; set; }
    public DateTime AddedTime { get; set; }
    public DateTime ExpirationTime { get; set; }

    public RefreshToken(
        Guid id, long userId,
        string fingerPrint,
        DateTime addedTime,
        DateTime expirationTime)
    {
        Id = id;
        UserId = userId;
        FingerPrint = fingerPrint;
        AddedTime = addedTime;
        ExpirationTime = expirationTime;
    }

    public RefreshToken()
    {
        Id = Guid.Empty;
        UserId = -1;
        FingerPrint = "";
        AddedTime = DateTime.Now;
        ExpirationTime = DateTime.Now;
    }

    public bool IsRevorked()
    {
        return DateTime.Now > ExpirationTime;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if ((RefreshToken)obj == this) return true;
        var refreshToken = (RefreshToken)obj;

        if (Id == refreshToken.Id &&
            UserId == refreshToken.UserId &&
            FingerPrint == refreshToken.FingerPrint &&
            AddedTime == refreshToken.AddedTime &&
            ExpirationTime == refreshToken.ExpirationTime)
        {
            return true;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public bool Equals(RefreshToken? otherRefreshToken)
    {
        if (otherRefreshToken == null) return false;
        if (otherRefreshToken.Id == Id &&
                otherRefreshToken.UserId == UserId &&
                otherRefreshToken.FingerPrint == FingerPrint &&
                otherRefreshToken.AddedTime == AddedTime &&
                otherRefreshToken.ExpirationTime == ExpirationTime)
        {
            return true;
        }

        return false;
    }
}