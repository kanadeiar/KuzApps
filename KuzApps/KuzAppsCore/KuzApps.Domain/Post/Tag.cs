namespace KuzApps.Domain.Post;

/// <summary>
/// Тег
/// </summary>
public class Tag : Entity
{
    [Required(ErrorMessage = "Имя тега обязательно для тега постов")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Имя тега должно быть длинной от 1 до 200 символов")]
    public string Name { get; set; } = null!;

    /// <summary> 
    /// Данные по информационным постам, относящихся к тегу
    /// </summary>
    public ICollection<Post> Posts { get; set; } = new HashSet<Post>();

    public override string ToString() => Name;
}
