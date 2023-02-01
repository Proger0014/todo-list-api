using static TodoList.Extensions.CommonExtensions;

namespace TodoList.Utils;

public static class CommonUtils
{
    public static Setting GetEnvironment()
    {
        var env = Setting.LauchSettings.GetSetting("profiles:TodoList:environmentVariables:ASPNETCORE_ENVIRONMENT");

        return env switch
        {
            "Development" => Setting.Dev,
            "Release" or "Production" => Setting.Release,
            _ => Setting.Default
        };
    }
}