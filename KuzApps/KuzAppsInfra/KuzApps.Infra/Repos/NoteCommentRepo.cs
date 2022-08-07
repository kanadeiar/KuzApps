namespace KuzApps.Infra.Repos;

/// <summary>
/// Репозиторий комментариев новостных заметок
/// </summary>
public class NoteCommentRepo : DbRepo<NoteComment, int>, INoteCommentRepo
{
    public NoteCommentRepo(DbContext context, ILogger<DbRepo<NoteComment, int>> logger) : base(context, logger)
    {
    }
}
