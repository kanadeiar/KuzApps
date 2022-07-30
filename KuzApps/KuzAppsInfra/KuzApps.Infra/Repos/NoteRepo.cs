namespace KuzApps.Infra.Repos;

/// <summary>
/// Репозиторий новостных заметок
/// </summary>
public class NoteRepo : DbRepo<Note>, INoteRepo
{
    public NoteRepo(DbContext context, ILogger<DbRepo<Note, int>> logger) : base(context, logger)
    {
    }
}
