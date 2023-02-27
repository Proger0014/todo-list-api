using TodoList;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using IntegrationTests.Common;
using TodoList.DB;
using Microsoft.Extensions.DependencyInjection;
using IntegrationTests.TestDataCollections.DatabaseSeeding;

namespace IntegrationTests.Utils;

public static class DBUtils
{
    public static void CreateAndReinitializeTestDb(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var scopedServices = scope.ServiceProvider;
        var dbContext = scopedServices.GetRequiredService<PostgresqlDbContext>();

        ReinitializeDbForTests(dbContext);
    }

    public static void InitializeDbForTests(IApplicationDbContext db)
    {
        db.Database.EnsureCreated();

        var seedings = DatabaseSeedingDataCollection.DatabaseSeedings;

        foreach (var seedingList in seedings)
        {
            Type typeOfItemList = seedingList.GetType()
                .GetGenericArguments()
                .Single();

            var genericMethod = typeof(DBUtils)?
                .GetMethod(nameof(AddGenericItemsToDb), 
                    BindingFlags.NonPublic | BindingFlags.Static)?
                .MakeGenericMethod(typeOfItemList);

            genericMethod?
                .Invoke(null, new[] { db, seedingList });
        }

        db.SaveChanges();
    }

    private static void AddGenericItemsToDb<T>(IApplicationDbContext db, IEnumerable<T> items)
        where T : class
    {
        db.Set<T>().AddRange(items);
    }
    public static void ReinitializeDbForTests(IApplicationDbContext db)
    {
        //db.Database.ExecuteSqlRaw("TRUNCATE TABLE \"public\".refresh_tokens;");
        //db.Database.ExecuteSqlRaw("TRUNCATE TABLE \"public\".users CASCADE;");
        db.Database.EnsureDeleted();

        InitializeDbForTests(db);
    }
}
