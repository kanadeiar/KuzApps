namespace KuzApps.Application.WebModels;

/// <summary>
/// Веб модель категории
/// </summary>
public class CategoryWebModel
{
    /// <summary> 
    /// Идентификатор 
    /// </summary>
    public int Id { get; set; }
    /// <summary> 
    /// Название 
    /// </summary>
    public string Name { get; set; }
    /// <summary> 
    /// Сортировка 
    /// </summary>
    public int Order { get; set; }
    /// <summary> 
    /// Родительский элемент 
    /// </summary>
    public CategoryWebModel? Parent { get; set; }
    /// <summary> 
    /// Дети 
    /// </summary>
    public List<CategoryWebModel> Children { get; set; } = new List<CategoryWebModel>();
    /// <summary>
    /// Заголовки информационных постов
    /// </summary>
    public ICollection<PostTitleWebModel> Posts { get; set; } = new List<PostTitleWebModel>();
}
