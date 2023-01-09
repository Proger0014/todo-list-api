using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoList.Models.Id;

namespace TodoList.Models.User;

[Table("users", Schema = "public")]
public class User : ID<long>
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
}