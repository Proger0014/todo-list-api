namespace TodoList.Extensions;

public static class CommonExtensions
{
    public static string GetAppSetting(this string key)
    {
        var configuration = AppSettingsConfigurationInit();

        var value = configuration[key];

        if (string.IsNullOrEmpty(value))
        {
            throw new KeyNotFoundException("Key is not found in appsettings");
        }

        return value;
    }

    public static string GetConnectionString(this string connectionName)
    {
        var configuration = AppSettingsConfigurationInit();

        var connectionString = configuration.GetConnectionString(connectionName);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new KeyNotFoundException("Connection string is not found");
        }

        return connectionString;
    }

    private static IConfiguration AppSettingsConfigurationInit()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
    }
}
