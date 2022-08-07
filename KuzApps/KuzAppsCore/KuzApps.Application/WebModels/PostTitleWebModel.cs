namespace KuzApps.Application.WebModels;

/// <summary>
/// Вебмодель информационного поста содержащую только заголовок
/// </summary>
public class PostTitleWebModel
{
    /// <summary> 
    /// Идентификатор 
    /// </summary>
    public int Id { get; set; }
    /// <summary> 
    /// Заголовок 
    /// </summary>
    public string Title { get; set; } = default!;
}
