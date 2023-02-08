using TodoList.Constants;
using TodoList.Extensions;
using static TodoList.Extensions.CommonExtensions;

namespace UnitTests.ExtensionsTests;

public class CommonExtensionsTests
{
    [Fact]
    public void GetConnectionString_ExistsConnectionString_GetConnectionString()
    {
        var expected = "Host=localhost;Port=5432;Database=testdb;Username=postgres;Password=postgres";
        var actual = Setting.LauchSettings.GetConnectionString("DefaultConnection");

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetConnectionString_NotExistsConnectionString_ThrowsKeyNotFoundException()
    {
        string connectionName = "someConnectionString";
        var envFile = "appsettings.Development.json";

        string expectedExceptionMessage = string.Format(ExceptionMessage.CONNECTION_STRING_IS_NOT_FOUND, envFile);

        var exception = Assert.Throws<KeyNotFoundException>(() => { Setting.LauchSettings.GetConnectionString(connectionName); });
        Assert.Equal(expectedExceptionMessage, exception.Message);
    }

    [Fact]
    public void GetAppSetting_ExistsAppSetting_GetString()
    {
        var expected = "my_super_secret_key_1";
        var actual = Setting.Dev.GetSetting("Jwt:Key");

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetAppSetting_NotExistsAppSetting_ThrowsKeyNotFoundException()
    {
        string envFile = "appsettings.Development.json";
        string someAppSettingKey = "someAppSettingKey";

        string expectedExceptionMessage = string.Format(ExceptionMessage.KEY_IS_NOT_FOUND_SETTING, someAppSettingKey, envFile);

        var exception = Assert.Throws<KeyNotFoundException>(() => { Setting.Dev.GetSetting(someAppSettingKey); });
        Assert.Equal(expectedExceptionMessage, exception.Message);
    }
}
