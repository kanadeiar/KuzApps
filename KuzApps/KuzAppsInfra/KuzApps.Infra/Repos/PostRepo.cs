namespace KuzApps.Infra.Repos;

/// <summary>
/// Репозиторий информационных постов
/// </summary>
public class PostRepo : DbRepo<Post>, IPostRepo
{
    public PostRepo(DbContext context, ILogger<DbRepo<Post, int>> logger) : base(context, logger)
    {
    }
}
