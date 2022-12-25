namespace TodoList.Models.User;

public class UserRepository
{
    private ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public User? GetUserById(long id)
    {
        return _context.Users.SingleOrDefault(u => u.Id == id);
    }

    public User? GetUserByLogin(DTO.User.UserLogin login)
    {
        return _context.Users.SingleOrDefault(u => u.Login == login.Login && u.Password == login.Password);
    }

    public List<User> GetUsers()
    {
        return _context.Users.ToList();
    }

    public void ChangeUser(User user)
    {
        var tempUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
        DeleteUser(tempUser);
        AddUser(user);
    }

    public void AddUser(User user)
    {
        var tempUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);

        if (tempUser != null)
        {
            throw new Exception();
        }

        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void DeleteUser(User user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
}