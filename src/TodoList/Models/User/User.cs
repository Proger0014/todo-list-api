using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoList.Models.Id;

namespace TodoList.Models.User;

[Table("users", Schema = "public")]
public class User : ID<long>, ICloneable
{
    [Key, Column("id"), DataType("bigserial")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string NickName { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    public User(long id, string nickname, string login, string password)
    {
        Id = id;
        NickName = nickname;
        Login = login;
        Password = password;
    }

    public User() 
    {
        Id = 0;
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

    public object Clone() =>
        new User()
        {
            Id = Id,
            NickName = (string)NickName.Clone(),
            Login = (string)Login.Clone(),
            Password = (string)Password.Clone()
        };
}