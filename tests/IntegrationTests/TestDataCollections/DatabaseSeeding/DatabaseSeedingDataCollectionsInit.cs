using TodoList.Models.User;

namespace IntegrationTests.TestDataCollections.DatabaseSeeding;

internal class DatabaseSeedingDataCollectionsInit
{
    internal static IEnumerable<object> GetDatabaseSeedings()
    {
        yield return GetSeedingUsers();
    }

    private static IEnumerable<object> GetSeedingUsers()
    {
        const int MAX_COUNT = 2;

        var users = new List<User>();

        for (int i = 0; i < MAX_COUNT; i++)
        {
            users.Add(
                new User()
                {
                    Id = i,
                    NickName = $"user-test-{i}",
                    Login = $"user-login-{i}",
                    Password = $"user-password-{i}"
                });
        }

        return users;
    }
}
