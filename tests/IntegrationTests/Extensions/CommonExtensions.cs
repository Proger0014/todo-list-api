using IntegrationTests.Utils;

namespace IntegrationTests.Extensions;

public static class CommonExtensions
{
    public static StringContent StringContentJsonFromEntity<TEntity>(this TEntity entity)
    {
        return CommonUtils.StringContentJsonFromEntity<TEntity>(entity);
    }
}
