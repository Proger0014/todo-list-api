namespace IntegrationTests.TestDataCollections.DatabaseSeeding;

public static class DatabaseSeedingDataCollection
{
    public static IEnumerable<object> DatabaseSeedings { get; } =
        DatabaseSeedingDataCollectionsInit.GetDatabaseSeedings();
}
