using System.Text;
using System.Text.Json;

namespace IntegrationTests.Utils;

public static class CommonUtils
{
    public static StringContent StringContentJsonFromEntity<TEntity>(TEntity entity)
    {
        var serializedObject = JsonSerializer.Serialize<TEntity>(entity);

        return new StringContent(serializedObject, Encoding.UTF8, "application/json");
    }
}
