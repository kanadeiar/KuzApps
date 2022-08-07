namespace KuzApps.Infra.Repos;

/// <summary>
/// Репозиторий категорий
/// </summary>
public class PostCategoryRepo : DbRepo<PostCategory, int>, IPostCategoryRepo
{
    public PostCategoryRepo(DbContext context, ILogger<DbRepo<PostCategory, int>> logger) : base(context, logger)
    {
    }

    public async Task<IEnumerable<PostCategory>> GetByBookName(string bookName, CancellationToken cancel = default)
    {
        var items = await Set
            .Where(x => x.BookName.Equals(bookName))
            .OrderBy(x => x.Order)
            .Include(x => x.Posts)
            .AsNoTracking()
            .ToArrayAsync(cancel)
            .ConfigureAwait(false);
        return items;
    }
}
