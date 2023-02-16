using Microsoft.Extensions.Configuration;

namespace UnitTests.Utils;

public static class CommonUtils
{
    public static IConfiguration CreateConfigurationFake(Dictionary<string, string> settings)
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();
    }
}
