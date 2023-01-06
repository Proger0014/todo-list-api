using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Models.User;

[Table("users", Schema = "public")]
public class User
{
    [Key, Column("id"), DataType("bigserial")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Column("nick_name")]
    public string Nickname { get; set; }
    [Column("login")]
    public string Login { get; set; }
    [Column("password")]
    public string Password { get; set; }

    public User(long id, string nickname, string login, string password)
    {
        Id = id;
        Nickname = nickname;
        Login = login;
        Password = password;
    }

    public User() 
    {
        Id = 0;
        Nickname = "";
        Login = "";
        Password = "";
    }
}