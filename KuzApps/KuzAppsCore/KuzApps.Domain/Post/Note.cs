namespace KuzApps.Domain.Post;

/// <summary>
/// Новостная заметка
/// </summary>
public class Note : Entity
{
    /// <summary>
    /// Дата новостной заметки
    /// </summary>
    public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;

    [Required(ErrorMessage = "Статус поста обезателен для информационного поста")]
    public Status Status { get; set; }

    [Required(ErrorMessage = "Заголовок информационного поста обязателен для информационного поста")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Заголовок информационного поста должен быть длинной от 1 до 200 символов")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Тело поста обязательно для информационного поста")]
    [MinLength(10, ErrorMessage = "Тело поста должно быть длинной от 10 символов")]
    public string Body { get; set; } = null!;

    public override string ToString() => $"{Date}: {Title}";
}
