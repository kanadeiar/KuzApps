namespace KuzApps.Domain.Post;

/// <summary>
/// Информационный пост
/// </summary>
public class Post : Entity
{
    /// <summary>
    /// Дата создания информационного поста
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

    [Required, Range(1, int.MaxValue, ErrorMessage = "Должна быть выбрана категория информационного поста")]
    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = null!;

    [Required(ErrorMessage = "Автор поста обязателен для информационного поста")]
    public User User { get; set; } = null!;

    /// <summary>
    /// Список тегов относящихся к информационнному посту
    /// </summary>
    public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

    /// <summary>
    /// Список комментариев относящихся к информационному посту
    /// </summary>
    public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

    public override string ToString() => $"{Date}, {User.UserName}: {Title}";
}
