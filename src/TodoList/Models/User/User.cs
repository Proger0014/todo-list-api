using TodoList.Models.Id;

namespace TodoList.Models.User;

public class User : ID<long>, IEquatable<User>
{
    public long Id { get; set; }
    public string NickName { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    public ICollection<RefreshToken.RefreshToken> RefreshTokens { get; set; }

    public User(long id, string nickname, string login, string password)
    {
        Id = id;
        NickName = nickname;
        Login = login;
        Password = password;
    }

    public User() 
    {
        NickName = "";
        Login = "";
        Password = "";
    }

    public override bool Equals(object? obj)
    {
        if (obj == this) return true;
        if (obj == null) return false;

        User targetUser = (User)obj;

        if (targetUser.Id == Id &&
            targetUser.Login == Login &&
            targetUser.NickName == NickName &&
            targetUser.Password == Password)
            return true;

        return false;
    }

    public override int GetHashCode()
    {
        return
            NickName.GetHashCode() +
            Login.GetHashCode() + 
            Password.GetHashCode();
    }

    public bool Equals(User? other)
    {
        if (other == null) return false;
        if (other.Id == Id &&
            other.Login == Login &&
            other.NickName == NickName &&
            other.Password == Password) return true;

        return false;
    }
}