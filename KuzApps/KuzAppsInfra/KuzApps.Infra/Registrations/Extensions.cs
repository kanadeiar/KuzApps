namespace KuzApps.Infra.Registrations;

public static class Extensions
{
    /// <summary>
    /// Регистрация базы данных
    /// </summary>
    public static IServiceCollection InfraMyDatabase(this IServiceCollection services, IConfiguration configuration)
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
    public static IServiceCollection InfraMyTestData(this IServiceCollection services)
    {
        services.AddTransient<TestData>();
        return services;
    }

    /// <summary>
    /// Регистрация репозиториев
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection InfraMyRepos(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepo, CategoryRepo>();
        services.AddScoped<ICommentRepo, CommentRepo>();
        services.AddScoped<INoteRepo, NoteRepo>();
        services.AddScoped<IPostRepo, PostRepo>();
        services.AddScoped<ITagRepo, TagRepo>();

        return services;
    }
}