namespace KuzApps.Domain.Post;

/// <summary>
/// Комментарий информационного поста
/// </summary>
public class Comment : Entity
{
    /// <summary>
    /// Дата создания комментария к информационному посту
    /// </summary>
    public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;

    /// <summary>
    /// Запись к которой принадлежит комментарий
    /// </summary>
    [Required]
    public Post Post { get; set; } = null!;

    [Required(ErrorMessage = "Текст комментария обязателен для комментария")]
    public string Text { get; set; } = null!;

    [Required(ErrorMessage = "Автор комментария обязателен для комментария")]
    public User User { get; set; } = null!;

    /// <summary>
    /// Родительский комментарий
    /// </summary>
    public Comment? Parent { get; set; }

    /// <summary>
    /// Список дочерних комментарие
    /// в</summary>
    public ICollection<Comment> Childrens { get; set; } = new HashSet<Comment>();

    /// <summary>
    /// Признак удалённой записи
    /// </summary>
    public bool IsDeleted { get; set; }

    public override string ToString() => $"{Date}, {User.UserName}: {Text}";
}
