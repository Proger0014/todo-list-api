using TodoList.DTO.User;

namespace UnitTests.DTO;

public class UserDTOTests
{
    [Fact]
    public void UserRegisterDTO_SetNicknameLengthBigger60_ThrowException()
    {
        Assert.Throws<Exception>(
            () => { new UserRegister().Nickname = new string('a', 61); });
    }
}
