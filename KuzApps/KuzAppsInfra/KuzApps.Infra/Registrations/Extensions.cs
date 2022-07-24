namespace KuzApps.Infra.Registrations;

public static class Extensions
{
    /// <summary>
    /// Регистрация базы данных
    /// </summary>
    public static IServiceCollection RegisterMyDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<KuzAppsDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
#if DEBUG
            options.EnableSensitiveDataLogging();
#endif
        });
        services.AddScoped<DbContext, KuzAppsDbContext>();
        return services;
    }

    /// <summary>
    /// Регистрация тестовых данных
    /// </summary>
    public static IServiceCollection RegisterMyTestData(this IServiceCollection services)
    {
        services.AddTransient<TestData>();
        return services;
    }
}