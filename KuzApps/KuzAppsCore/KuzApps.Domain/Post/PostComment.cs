namespace KuzApps.Domain.Post;

/// <summary>
/// Комментарий информационного поста
/// </summary>
public class PostComment : KndEntity<int>
{
    /// <summary>
    /// Дата комментария к информационному посту
    /// </summary>
    public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;

    /// <summary>
    /// Информационный пост, к которому принадлежит комментарий
    /// </summary>
    [Required]
    public Post Post { get; set; } = default!;

    [Required(ErrorMessage = "Текст комментария обязателен для комментария")]
    public string Text { get; set; } = default!;

    [Required(ErrorMessage = "Автор комментария обязателен для комментария")]
    public User User { get; set; } = default!;

    /// <summary>
    /// Родительский комментарий
    /// </summary>
    public PostComment? Parent { get; set; }

    /// <summary>
    /// Список дочерних комментарие
    /// в</summary>
    public ICollection<PostComment> Childrens { get; set; } = new HashSet<PostComment>();

    /// <summary>
    /// Признак удалённого комментария
    /// </summary>
    public bool IsDeleted { get; set; }

    public override string ToString() => $"{Date}, {User.UserName}: {Text}";
}
