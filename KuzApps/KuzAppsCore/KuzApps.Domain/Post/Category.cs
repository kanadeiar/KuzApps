namespace KuzApps.Domain.Post;

/// <summary>
/// Категория информационных постов
/// </summary>
[Index(nameof(Name), IsUnique = true, Name = "NameIndex")]
[Index(nameof(BookName), IsUnique = false, Name = "BookNameIndex")]
public class Category : Entity
{
    [Required(ErrorMessage = "Название книги обязательно для категории информационных постов")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Название книги должно быть длинной от 1 до 50 символов")]
    public string BookName { get; set; } = null!;

    [Required(ErrorMessage = "Имя категории обязательно для категории информационных постов")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Имя категории должно быть длинной от 1 до 200 символов")]
    public string Name { get; set; } = null!;

    /// <summary> 
    /// Данные по информационным постам, относящихся к категории
    /// </summary>
    public ICollection<Post> Posts { get; set; } = new HashSet<Post>();

    /// <summary>
    /// Сортировка
    /// </summary>
    public int Order { get; set; }

    /// <summary> 
    /// Родительский элемент 
    /// </summary>
    public int? ParentId { get; set; }
    /// <summary>
    /// Родительская категория
    /// </summary>
    [ForeignKey(nameof(ParentId))]
    public Category? Parent { get; set; }

    /// <summary>
    /// Список дочерних категорий
    /// в</summary>
    public ICollection<Category> Childrens { get; set; } = new HashSet<Category>();

    public override string ToString() => Name;
}
