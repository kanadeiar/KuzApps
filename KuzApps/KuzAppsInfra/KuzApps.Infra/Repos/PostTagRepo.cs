namespace KuzApps.Infra.Repos;

/// <summary>
/// Репозиторий тегов
/// </summary>
public class PostTagRepo : DbRepo<PostTag, int>, IPostTagRepo
{
    public PostTagRepo(DbContext context, ILogger<DbRepo<PostTag, int>> logger) : base(context, logger)
    {
    }
}
