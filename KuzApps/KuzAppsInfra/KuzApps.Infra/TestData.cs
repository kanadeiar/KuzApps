using Microsoft.AspNetCore.Identity;

namespace KuzApps;

/// <summary>
/// Тестовые данные
/// </summary>
public class TestData
{
    /// <summary>
    /// Тестовые данные авторизации
    /// </summary>
    private class AccountTestData
    {
        private Random _rnd = new Random();

        public async Task PopulateTestData(IServiceProvider provider, DbContext context, ILogger<TestData> logger)
        {
            logger.LogInformation("Начало заполнения тестовыми данными авторизации ...");

            UserManager<User> userManager = provider.GetRequiredService<UserManager<User>>();
            RoleManager<Role> roleManager = provider.GetRequiredService<RoleManager<Role>>();

            if (await roleManager.FindByNameAsync("admins") is null)
            {
                await roleManager.CreateAsync(new Role { Name = "admins", Description = "Администраторы" });
            }
            if (await roleManager.FindByNameAsync("users") is null)
            {
                await roleManager.CreateAsync(new Role { Name = "users", Description = "Пользователи" });
            }
            if (await roleManager.FindByNameAsync("clients") is null)
            {
                await roleManager.CreateAsync(new Role { Name = "clients", Description = "Клиенты" });
            }
            if (await roleManager.FindByNameAsync("guests") is null)
            {
                await roleManager.CreateAsync(new Role { Name = "guests", Description = "Гости" });
            }

            if (await userManager.FindByNameAsync("admin") is null)
            {
                var adminUser = new User
                {
                    SurName = "Админов",
                    FirstName = "Админ",
                    Patronymic = "Админович",
                    Birthday = DateTime.Today.AddYears(-22),
                    UserName = "admin",
                    Email = "admin@example.com",
                };
                var result = await userManager.CreateAsync(adminUser, "admin");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "admins");
                    await userManager.AddToRoleAsync(adminUser, "users");
                    await userManager.AddToRoleAsync(adminUser, "clients");
                    await userManager.AddToRoleAsync(adminUser, "guests");
                }
                else
                {
                    var errors = result.Errors.Select(e => e.Description).ToArray();
                    logger.LogError("Учётная запись пользователя {0} не создана по причине: {1}", adminUser.UserName, string.Join(",", errors));
                    throw new InvalidOperationException($"Ошибка при создании пользователя {adminUser.UserName}, список ошибок: {string.Join(",", errors)}");
                }
            }
            if (await userManager.FindByNameAsync("user") is null)
            {
                var adminUser = new User
                {
                    SurName = "Пользователев",
                    FirstName = "Пользователь",
                    Patronymic = "Пользователевич",
                    Birthday = DateTime.Today.AddYears(-18),
                    UserName = "user",
                    Email = "user@example.com",
                };
                var result = await userManager.CreateAsync(adminUser, "user");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "users");
                    await userManager.AddToRoleAsync(adminUser, "guests");
                }
                else
                {
                    var errors = result.Errors.Select(e => e.Description).ToArray();
                    logger.LogError("Учётная запись пользователя {0} не создана по причине: {1}", adminUser.UserName, string.Join(",", errors));
                    throw new InvalidOperationException($"Ошибка при создании пользователя {adminUser.UserName}, список ошибок: {string.Join(",", errors)}");
                }
            }
            if (await userManager.FindByNameAsync("client") is null)
            {
                var adminUser = new User
                {
                    SurName = "Клиентов",
                    FirstName = "Клиент",
                    Patronymic = "Клиентович",
                    Birthday = DateTime.Today.AddYears(-18),
                    UserName = "client",
                    Email = "client@example.com",
                };
                var result = await userManager.CreateAsync(adminUser, "client");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "clients");
                    await userManager.AddToRoleAsync(adminUser, "guests");
                }
                else
                {
                    var errors = result.Errors.Select(e => e.Description).ToArray();
                    logger.LogError("Учётная запись пользователя {0} не создана по причине: {1}", adminUser.UserName, string.Join(",", errors));
                    throw new InvalidOperationException($"Ошибка при создании пользователя {adminUser.UserName}, список ошибок: {string.Join(",", errors)}");
                }
            }
            if (await userManager.FindByNameAsync("guest") is null)
            {
                var adminUser = new User
                {
                    SurName = "Гостев",
                    FirstName = "Гость",
                    Patronymic = "Гостевич",
                    Birthday = DateTime.Today.AddYears(-18),
                    UserName = "guest",
                    Email = "guest@example.com",
                };
                var result = await userManager.CreateAsync(adminUser, "guest");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "guests");
                }
                else
                {
                    var errors = result.Errors.Select(e => e.Description).ToArray();
                    logger.LogError("Учётная запись пользователя {0} не создана по причине: {1}", adminUser.UserName, string.Join(",", errors));
                    throw new InvalidOperationException($"Ошибка при создании пользователя {adminUser.UserName}, список ошибок: {string.Join(",", errors)}");
                }
            }

            logger.LogInformation("Конец заполнения тестовыми данными авторизации");
        }
    }

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
            logger.LogInformation("База данных уже содержит данные - заполнение данными пропущено");
            return;
        }

        await new AccountTestData().PopulateTestData(provider, context, logger);
    }
}

