namespace KuzApps.Application.WebModels;

/// <summary>
/// Веб модель категории информационных постов
/// </summary>
public class PostCategoryWebModel
{
    /// <summary> 
    /// Идентификатор 
    /// </summary>
    public int Id { get; set; }
    /// <summary> 
    /// Название 
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary> 
    /// Сортировка 
    /// </summary>
    public int Order { get; set; }
    /// <summary> 
    /// Родительский элемент 
    /// </summary>
    public PostCategoryWebModel? Parent { get; set; }
    /// <summary> 
    /// Дети 
    /// </summary>
    public List<PostCategoryWebModel> Children { get; set; } = new List<PostCategoryWebModel>();
    /// <summary>
    /// Заголовки информационных постов
    /// </summary>
    public ICollection<PostTitleWebModel> Posts { get; set; } = new List<PostTitleWebModel>();
}
