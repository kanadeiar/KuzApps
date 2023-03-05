namespace KuzApps;

/// <summary>
/// Тестовые данные
/// </summary>
public class TestData
{
    /// <summary>
    /// Создание базы данных и заполнение тестовыми данными
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task SeedTestData(IServiceProvider provider)
    {
        provider = provider.CreateScope().ServiceProvider;
        var logger = provider.GetRequiredService<ILogger<TestData>>();
        using var context = new KuzAppsDbContext(provider.GetRequiredService<DbContextOptions<KuzAppsDbContext>>());

        if (context == null || context.Users == null)
        {
            logger.LogError("Контекст базы данных KuzAppsDbContext = null");
            throw new ArgumentNullException("Контекст базы данных KuzAppsDbContext = null");
        }
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            logger.LogInformation($"Применение миграций: {string.Join(",", pendingMigrations)}");
            await context.Database.MigrateAsync();
        }
        if (context.Users.Any())
        {
            logger.LogInformation("База данных уже содержит данные по авторизации - заполнение данными авторизации пропущено");
        }
        else
        {

            await new AccountTestData().PopulateTestData(provider, context);
        }
        if (context.Posts.Any())
        {
            logger.LogInformation("База данных уже содержит данные по информационным постам - заполнение постами пропущено");
        }
        else
        {
            await new PostTestData().PopulateTestData(provider, context);
        }
    }
}

