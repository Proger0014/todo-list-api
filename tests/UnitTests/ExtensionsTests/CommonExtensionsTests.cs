using TodoList.Extensions;

namespace UnitTests.ExtensionsTests;

public class CommonExtensionsTests
{
    [Fact]
    public void GetConnectionString_ExistsConnectionString_GetConnectionString()
    {
        var expected = "Host=localhost;Port=5432;Database=testdb;Username=postgres;Password=postgres";
        var actual = "DefaultConnection".GetConnectionString();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetConnectionString_NotExistsConnectionString_ThrowsKeyNotFoundException()
    {
        string connectionName = "someConnectionString";
        Assert.Throws<KeyNotFoundException>(() => { connectionName.GetConnectionString(); });
    }

    [Fact]
    public void GetAppSetting_ExistsAppSetting_GetString()
    {
        var expected = "my_super_secret_key_1";
        var actual = "Jwt:Key".GetSetting(CommonExtensions.Setting.Dev);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetAppSetting_NotExistsAppSetting_ThrowsKeyNotFoundException()
    {
        string someAppSettingKey = "someAppSettingKey";
        Assert.Throws<KeyNotFoundException>(() => { someAppSettingKey.GetSetting(CommonExtensions.Setting.Dev); });
    }
}
