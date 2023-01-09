using TodoList.Models.Base;

namespace TodoList.Models.User;

public interface IUserRepository : 
    IBaseRepository<User, long>.Delete,
    IBaseRepository<User, long>.GetById,
    IBaseRepository<User, long>.Insert,
    IBaseRepository<User, long>.Save
{
    User GetByUserLogin(string login, string password);
    User GetByUserLogin(string login);
}