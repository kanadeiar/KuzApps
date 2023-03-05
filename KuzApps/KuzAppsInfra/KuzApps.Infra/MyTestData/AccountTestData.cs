namespace KuzApps.Infra.MyTestData;

/// <summary>
/// Тестовые данные авторизации
/// </summary>
public class AccountTestData
{
    /// <summary>
    /// Заполнение базы данных тестовыми данными авторизации
    /// </summary>
    public async Task PopulateTestData(IServiceProvider provider, DbContext context)
    {
        var logger = provider.GetRequiredService<ILogger<AccountTestData>>();

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
