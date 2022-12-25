using TodoList.DTO.User;
using TodoList.Models.User;

namespace TodoList.Services;

public class UserService
{
    private UserRepository _userRepository;

	public UserService(UserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public User? GetUserById(long id)
	{
		return _userRepository.GetUserById(id);
	}

	public User? GetUserByLogin(UserLogin login)
	{
		return _userRepository.GetUserByLogin(login);
	}

	public void AddUser(UserRegister register)
	{
		var userWithSameLogin = _userRepository.GetUsers()
			.FirstOrDefault(u => u.Login == register.Login);

		if (userWithSameLogin != null)
		{
			throw new Exception();
		}

		var user = new User(
			0,
			register.Nickname,
			register.Login,
			register.Password
		);

		_userRepository.AddUser(user);
	}
}