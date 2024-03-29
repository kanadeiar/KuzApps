﻿namespace KuzApps.Application.Interfaces.Repos;

/// <summary>
/// Репозиторий категорий информационных постов
/// </summary>
public interface IPostCategoryRepo : IKndRepo<PostCategory, int>
{
    /// <summary>
    /// Получить элементы по указанному названию книги
    /// </summary>
    /// <param name="bookName">Имя книги, которую требуется получить из репозитория</param>
    /// <param name="cancel">Признак отмены асинхронной операции</param>
    /// <returns>Сущности с указанным именем книги в случае её наличия</returns>
    Task<IEnumerable<PostCategory>> GetByBookName(string bookName, CancellationToken cancel = default);
}
