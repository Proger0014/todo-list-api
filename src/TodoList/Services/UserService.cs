using TodoList.DTO.User;
using TodoList.Models.User;
using TodoList.Exceptions;
using TodoList.Constants;
using TodoList.Utils;

namespace TodoList.Services;

public class UserService
{
    private IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User GetUserWithAccessDeniedCheck(UserAccessDeniedCheck accessDeniedCheck)
    {
        var existsUser = _userRepository.GetById(accessDeniedCheck.UserId);

        if (existsUser == null)
        {
            throw new NotFoundException(string.Format(ExceptionMessage.USER_NOT_FOUND_WITH_ID, accessDeniedCheck.UserId));
        }

        var userIdFromPayload = ControllersUtils.GetUserIdFromPayload(accessDeniedCheck.UserClaims);

        if (userIdFromPayload != accessDeniedCheck.UserId)
        {
            // TODO: Нормальное описание сюда добавить
            throw new AccessDeniedException();
        }

        return existsUser;
    }

    public User GetUserByLogin(UserLoginRequest userLogin)
    {
        var existsUser = _userRepository.GetByUserLogin(userLogin.Login, userLogin.Password);

        if (existsUser == null)
        {
            throw new NotFoundException(string.Format(ExceptionMessage.USER_NOT_FOUND_WITH_LOGIN, userLogin.Login));
        }

        return existsUser;
    }

    public void AddUser(UserRegisterRequest register)
    {
        var existsUserWithSameNickname = _userRepository
            .GetByUserLogin(register.Login);

        if (existsUserWithSameNickname != null)
        {
            throw new ExistsException(string.Format(ExceptionMessage.USER_IS_EXISTS, register.Nickname));
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