namespace KuzApps.Application.Interfaces.Services;

/// <summary>
/// Сервис категорий информационных постов
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Получить категории информационных постов по имени книги
    /// </summary>
    /// <param name="bookName">мия книги</param>
    /// <param name="categoryId">идентификатор выбранной категории</param>
    /// <returns>Категории, Идентификатор выбранной</returns>
    Task<(IEnumerable<CategoryWebModel>, int?)> GetCategoriesFromBookName(string bookName, int? categoryId = null);
}