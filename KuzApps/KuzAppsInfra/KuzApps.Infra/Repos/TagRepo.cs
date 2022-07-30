namespace KuzApps.Infra.Repos;

/// <summary>
/// Репозиторий тегов
/// </summary>
public class TagRepo : DbRepo<Tag>, ITagRepo
{
    public TagRepo(DbContext context, ILogger<DbRepo<Tag, int>> logger) : base(context, logger)
    {
    }
}
