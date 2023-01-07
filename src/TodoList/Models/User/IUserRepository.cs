using TodoList.DTO.User;
using TodoList.Models.Base;

namespace TodoList.Models.User;

public interface IUserRepository : IBaseRepository<User>
{
    User GetByUserLogin(UserLogin userLogin);
}