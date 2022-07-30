namespace KuzApps.Infra.Repos;

/// <summary>
/// Репозиторий комментариев
/// </summary>
public class CommentRepo : DbRepo<Comment>, ICommentRepo
{
    public CommentRepo(DbContext context, ILogger<DbRepo<Comment, int>> logger) : base(context, logger)
    {
    }
}
