namespace TodoList.Extensions;

public static class CommonExtensions
{
    public static string GetSetting(this string key, Setting env)
    {
        var configuration = SettingsConfigurationInit(env);

        var value = configuration[key];

        if (string.IsNullOrEmpty(value))
        {
            throw new KeyNotFoundException("Key is not found in sttings file");
        }

        return value;
    }

    public static Setting GetEnvironment()
    {
        var env = "profiles:TodoList:environmentVariables:ASPNETCORE_ENVIRONMENT".GetSetting(Setting.LauchSettings);

        return env switch
        {
            "Development" => Setting.Dev,
            "Release" or "Production" => Setting.Release,
            _ => Setting.Default
        };
    }

    public static string GetConnectionString(this string connectionName, Setting setting = Setting.LauchSettings)
    {
        if (setting == Setting.LauchSettings)
        {
            setting = GetEnvironment();
        }
        
        var configuration = setting.SettingsConfigurationInit();

        var connectionString = configuration.GetConnectionString(connectionName);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new KeyNotFoundException("Connection string is not found");
        }

        return connectionString;
    }

    private static IConfiguration SettingsConfigurationInit(this Setting setting)
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(setting.GetSettingName())
            .Build();
    }

    private static string GetSettingName(this Setting setting)
    {
        return EnvironmentAppsettings[setting];
    }

    private static Dictionary<Setting, string> EnvironmentAppsettings = new()
    {
        { Setting.Default, "appsettings.json"},
        { Setting.Dev, "appsettings.Development.json" },
        { Setting.Release, "appsettings.Release.json" },
        { Setting.Test, "appsettings.Test.json" },
        { Setting.LauchSettings, "Properties/launchSettings.json" }
    };

    public enum Setting
    {
        Default,
        Dev,
        Release,
        Test,
        LauchSettings
    }
}
