using System.Text.RegularExpressions;
using System.Net;
using TodoList.Constants;
using TodoList.Utils;

namespace TodoList.Extensions;

public static class CommonExtensions
{
    public enum Setting
    {
        Default,
        Dev,
        Release,
        Test,
        LauchSettings
    }


    private static Dictionary<Setting, string> EnvironmentAppsettings = new()
    {
        { Setting.Default, "appsettings.json"},
        { Setting.Dev, "appsettings.Development.json" },
        { Setting.Release, "appsettings.Release.json" },
        { Setting.Test, "appsettings.Test.json" },
        { Setting.LauchSettings, "Properties/launchSettings.json" }
    };

    public static string GetSetting(this Setting env, string key)
    {
        var configuration = SettingsConfigurationInit(env);

        var value = configuration[key];

        if (string.IsNullOrEmpty(value))
        {
            throw new KeyNotFoundException(string.Format(ExceptionMessage.KEY_IS_NOT_FOUND_SETTING, key, env.GetSettingName()));
        }

        return value;
    }

    public static string GetConnectionString(this Setting setting, string connectionName)
    {
        if (setting == Setting.LauchSettings)
        {
            setting = CommonUtils.GetEnvironment();
        }

        var configuration = setting.SettingsConfigurationInit();

        var connectionString = configuration.GetConnectionString(connectionName);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new KeyNotFoundException(string.Format(ExceptionMessage.CONNECTION_STRING_IS_NOT_FOUND, setting.GetSettingName()));
        }

        return connectionString;
    }

    public static string SplitCamelCase(this string input)
    {
        return Regex
            .Replace(input, "([A-Z])", " $1", RegexOptions.Compiled)
            .Trim();
    }

    public static string GetStatusTitle(this HttpStatusCode statusCode)
    {
        return HttpStatusCodeTitles.HttpStatusCodeTitlesSet[statusCode];
    }

    private static IConfiguration SettingsConfigurationInit(this Setting setting)
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(setting.GetSettingName())
            .Build();
    }

    public static string GetSettingName(this Setting setting)
    {
        return EnvironmentAppsettings[setting];
    }
}
