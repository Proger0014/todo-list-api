using TodoList.DTO.User;
using TodoList.Models.Base;

namespace TodoList.Models.User;

public class UserRepository :
    BaseRepository<User>,
    IUserRepository
{
    private ApplicationContext _context;

    public UserRepository(ApplicationContext context)
        : base(context)
    {
        _context = context;
    }

    public User GetByUserLogin(UserLogin userLogin)
    {
        var targetUser = _context.Users
            .SingleOrDefault(u => u.Login == userLogin.Login && u.Password == userLogin.Password);

        if (targetUser == null)
        {
            throw new Exception("entity not found");
        }

        return targetUser;
    }
}