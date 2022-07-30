namespace KuzApps.Infra.Repos;

/// <summary>
/// Репозиторий категорий
/// </summary>
public class CategoryRepo : DbRepo<Category>, ICategoryRepo
{
    public CategoryRepo(DbContext context, ILogger<DbRepo<Category, int>> logger) : base(context, logger)
    {
    }

    public async Task<IEnumerable<Category>> GetByBookName(string bookName, CancellationToken cancel = default)
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
