using TodoList.Models.Base;

namespace TodoList.Models.User;

public class UserRepository :
    CommonProps<User, long>,
    IUserRepository
{
    public UserRepository(ApplicationDBContext context)
        : base(context) { }

    public User? GetByUserLogin(string login, string password)
    {
        return _context.Users?
            .SingleOrDefault(u => u.Login == login && u.Password == password);
    }

    public User? GetByUserLogin(string login)
    {
        return _context.Users?
            .SingleOrDefault(u => u.Login == login);
    }
}