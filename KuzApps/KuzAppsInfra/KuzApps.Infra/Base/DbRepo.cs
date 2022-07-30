namespace KuzApps.Infra.Base;

/// <summary>
/// Базовый репозиторий элементов
/// </summary>
/// <typeparam name="T">Тип элементов, хранимых в репозитории</typeparam>
/// <typeparam name="TKey">Тип первичного ключа</typeparam>
public class DbRepo<T, TKey> : IRepo<T, TKey> where T : class, IEntity<TKey>
{
    private readonly DbContext _db;
    private readonly ILogger<DbRepo<T, TKey>> _logger;
    protected DbSet<T> Set { get; }

    public DbRepo(DbContext context, ILogger<DbRepo<T, TKey>> logger)
    {
        _db = context;
        _logger = logger;
        Set = _db.Set<T>();
    }

    public IQueryable<T> Query => Set;

    public async Task<bool> ExistId(TKey id, CancellationToken cancel = default)
    {
        if (id is null)
            throw new ArgumentNullException(nameof(id));
        var result = await Query.AnyAsync(x => x.Id!.Equals(id), cancel).ConfigureAwait(false);
        return result;
    }

    public async Task<bool> Exist(T item, CancellationToken cancel = default)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));
        var result = await Query.AnyAsync(x => x.Equals(item), cancel).ConfigureAwait(false);
        return result;
    }

    public async Task<int> GetCount(CancellationToken cancel = default)
    {
        var result = await Query.CountAsync(cancel).ConfigureAwait(false);
        return result;
    }

    public async IAsyncEnumerable<T> GetAllAsync(CancellationToken cancel = default)
    {
        foreach (var item in Query
            .AsNoTracking())
        {
            if (cancel.IsCancellationRequested)
                yield break;
            yield return item;
        }
    }

    public async IAsyncEnumerable<T> GetSkipAsync(int skip, int count, CancellationToken cancel = default)
    {
        if (skip < 0)
            throw new ArgumentOutOfRangeException(nameof(skip), skip, "Число пропускаемых элементов должно быть больше, либо равно 0");
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), count, "Число запрашиваемых элементов должно быть больше, либо равно 0");
        foreach (var item in Query
            .OrderBy(_ => _.Id)
            .Skip(skip)
            .Take(count)
            .AsNoTracking())
        {
            if (cancel.IsCancellationRequested)
                yield break;
            yield return item;
        }
    }

    public async Task<IPage<T>> GetPage(int pageNumber, int pageSize, CancellationToken cancel = default)
    {
        if (pageNumber < 0)
            throw new ArgumentOutOfRangeException(nameof(pageNumber), pageNumber, "Номер страницы должен быть больше, либо равен 0");
        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "Размер страницы должен быть больше 0");
        IQueryable<T> query = Query.OrderBy(item => item.Id);
        var totalCount = await query.CountAsync(cancel).ConfigureAwait(false);
        if (pageNumber > 0)
            query = query.Skip(pageNumber * pageSize);
        query = query.Take(pageSize);
        var items = await query
            .AsNoTracking()
            .ToArrayAsync(cancel);
        var page = new Page<T>(items, pageNumber, pageSize, totalCount);
        return page;
    }

    public async Task<T?> GetById(TKey id, CancellationToken cancel = default)
    {
        if (id is null)
            throw new ArgumentNullException(nameof(id));
        var entity = await Set.FindAsync(new object[] { id }, cancel).ConfigureAwait(false);
        return entity;
    }

    public async Task<T> Add(T entity, bool saveChanges = true, CancellationToken cancel = default)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        await Set.AddAsync(entity, cancel).ConfigureAwait(false);
        if (saveChanges)
        {
            await _db.SaveChangesAsync();
        }
        return entity;
    }

    public async Task AddRange(IEnumerable<T> items, bool saveChanges = true, CancellationToken cancel = default)
    {
        if (items is null)
            throw new ArgumentNullException(nameof(items));
        await _db.AddRangeAsync(items, cancel).ConfigureAwait(false);
        if (saveChanges)
        {
            await _db.SaveChangesAsync();
        }
    }

    public async Task<T> Update(T entity, bool saveChanges = true, CancellationToken cancel = default)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        Set.Update(entity);
        if (saveChanges)
        {
            await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
        }
        return entity;
    }

    public async Task UpdateRange(IEnumerable<T> items, bool saveChanges = true, CancellationToken cancel = default)
    {
        if (items is null)
            throw new ArgumentNullException(nameof(items));
        _db.UpdateRange(items);
        await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
    }

    public async Task Delete(T entity, bool saveChanges = true, CancellationToken cancel = default)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        Set.Remove(entity);
        if (saveChanges)
        {
            await _db.SaveChangesAsync().ConfigureAwait(false);
        }
    }

    public async Task DeleteRange(IEnumerable<T> items, bool saveChanges = true, CancellationToken cancel = default)
    {
        if (items is null)
            throw new ArgumentNullException(nameof(items));
        Set.RemoveRange(items);
        if (saveChanges)
        {
            await _db.SaveChangesAsync().ConfigureAwait(false);
        }
    }

    public async Task<T?> DeleteById(TKey id, bool saveChanges = true, CancellationToken cancel = default)
    {
        if (id is null)
            throw new ArgumentNullException(nameof(id));
        if (await GetById(id, cancel).ConfigureAwait(false) is { } item)
        {
            _db.Remove(item);
            if (saveChanges)
            {
                await _db.SaveChangesAsync();
            }
            return item;
        }
        return null;
    }

    public async Task<int> Commit(CancellationToken cancel)
    {
        var count = await _db.SaveChangesAsync().ConfigureAwait(false);
        return count;
    }
}

/// <summary>
/// Базовый репозиторий элементв с целочисленными идентификаторами
/// </summary>
/// <typeparam name="T">Тип элементов</typeparam>
public class DbRepo<T> : DbRepo<T, int> where T : class, IEntity<int>
{
    public DbRepo(DbContext context, ILogger<DbRepo<T, int>> logger) : base(context, logger) { }
}
