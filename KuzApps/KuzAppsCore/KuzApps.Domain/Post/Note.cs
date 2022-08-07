namespace KuzApps.Domain.Post;

/// <summary>
/// Новостная заметка
/// </summary>
public class Note : KndEntity<int>
{
    /// <summary>
    /// Дата новостной заметки
    /// </summary>
    public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;

    [Required(ErrorMessage = "Статус заметки обезателен для новостной заметки")]
    public Status Status { get; set; }

    [Required(ErrorMessage = "Заголовок новостной заметки обязателен для новостной заметки")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Заголовок новостной заметки должен быть длинной от 1 до 200 символов")]
    public string Title { get; set; } = default!;

    [Required(ErrorMessage = "Тело новостной заметки обязательно для новостной заметки")]
    [MinLength(10, ErrorMessage = "Тело новостной заметки должно быть длинной от 10 символов")]
    public string Body { get; set; } = default!;

    /// <summary>
    /// Список комментариев относящихся к новостной заметке
    /// </summary>
    public ICollection<NoteComment> Comments { get; set; } = new HashSet<NoteComment>();

    public override string ToString() => $"{Date}: {Title}";
}
