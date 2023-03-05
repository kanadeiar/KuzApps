namespace KuzApps.Domain.Post;

/// <summary>
/// Комментарий новостной заметки
/// </summary>
public class NoteComment : KndEntity<int>
{
    /// <summary>
    /// Дата комментария к новостной заметке
    /// </summary>
    public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;

    /// <summary>
    /// Новостная заметка, к которому принадлежит комментарий
    /// </summary>
    [Required]
    public Note Note { get; set; } = default!;

    [Required(ErrorMessage = "Текст комментария обязателен для комментария")]
    public string Text { get; set; } = default!;

    [Required(ErrorMessage = "Автор комментария обязателен для комментария")]
    public User User { get; set; } = default!;

    /// <summary>
    /// Родительский комментарий
    /// </summary>
    public NoteComment? Parent { get; set; }

    /// <summary>
    /// Список дочерних комментарие
    /// в</summary>
    public ICollection<NoteComment> Childrens { get; set; } = new HashSet<NoteComment>();

    /// <summary>
    /// Признак удалённого комментария
    /// </summary>
    public bool IsDeleted { get; set; }

    public override string ToString() => $"{Date}, {User.UserName}: {Text}";
}
