using TodoList.DTO.User;
using TodoList.Models.User;

namespace TodoList.Services;

public class UserService
{
    private IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User? GetUserById(long id)
    {
        return _userRepository.GetById(id);
    }

    public User? GetUserByLogin(UserLoginRequest userLogin)
    {
        return _userRepository.GetByUserLogin(userLogin.Login, userLogin.Password);
    }

    public void AddUser(UserRegisterRequest register)
    {
        var existsUserWithSameNickname = _userRepository
            .GetByUserLogin(register.Login);

        if (existsUserWithSameNickname != null)
        {
			throw new Exception("This user is exists");
        }

        var user = new User(
            0,
            register.Nickname,
            register.Login,
            register.Password
        );

        _userRepository.Insert(user);
    }
}