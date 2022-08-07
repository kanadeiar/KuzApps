namespace KuzApps.Infra.Repos;

/// <summary>
/// Репозиторий комментариев информационных постов
/// </summary>
public class PostCommentRepo : DbRepo<PostComment, int>, IPostCommentRepo
{
    public PostCommentRepo(DbContext context, ILogger<DbRepo<PostComment, int>> logger) : base(context, logger)
    {
    }
}
