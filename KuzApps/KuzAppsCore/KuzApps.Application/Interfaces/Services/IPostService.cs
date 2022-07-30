namespace KuzApps.Application.Interfaces.Services;

/// <summary>
/// Репозиторий информационных постов
/// </summary>
public interface IPostService
{
    /// <summary>
    /// Получить информационный пост по идентфикатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>Вебмодель</returns>
    Task<PostWebModel> GetPostFromId(int id);
}