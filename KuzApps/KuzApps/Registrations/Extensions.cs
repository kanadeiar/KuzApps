namespace KuzApps.Registrations;

public static class Extensions
{
    /// <summary>
    /// Регистрация данных авторизации
    /// </summary>
    public static IServiceCollection MyConfigAccount(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>().AddEntityFrameworkStores<KuzAppsDbContext>();
        services.Configure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, options =>
        {
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
        });
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        });

        return services;
    }

    /// <summary>
    /// Заполнить базу данных тестовыми данными
    /// </summary>
    public static IApplicationBuilder MySeedTestData(this IApplicationBuilder builder)
    {
        using (var scope = builder.ApplicationServices.CreateScope())
        {
            var seeder = scope.ServiceProvider
                .GetRequiredService<TestData>()
                .SeedTestData(scope.ServiceProvider);
        }

        return builder;
    }

    /// <summary>
    /// Добавить сервисы в приложение
    /// </summary>
    public static IServiceCollection MyAddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }
}