namespace KuzApps.Domain.Post;

/// <summary>
/// Информационный пост
/// </summary>
public class Post : KndEntity<int>
{
    /// <summary>
    /// Дата информационного поста
    /// </summary>
    public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;

    [Required(ErrorMessage = "Статус поста обезателен для информационного поста")]
    public Status Status { get; set; }

    [Required(ErrorMessage = "Заголовок информационного поста обязателен для информационного поста")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Заголовок информационного поста должен быть длинной от 1 до 200 символов")]
    public string Title { get; set; } = default!;

    [Required(ErrorMessage = "Тело поста обязательно для информационного поста")]
    [MinLength(10, ErrorMessage = "Тело поста должно быть длинной от 10 символов")]
    public string Body { get; set; } = default!;

    [Required, Range(1, int.MaxValue, ErrorMessage = "Должна быть выбрана категория информационного поста")]
    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public PostCategory PostCategory { get; set; } = null!;

    [Required(ErrorMessage = "Автор поста обязателен для информационного поста")]
    public User User { get; set; } = null!;

    /// <summary>
    /// Список тегов относящихся к информационнному посту
    /// </summary>
    public ICollection<PostTag> Tags { get; set; } = new HashSet<PostTag>();

    /// <summary>
    /// Список комментариев относящихся к информационному посту
    /// </summary>
    public ICollection<PostComment> Comments { get; set; } = new HashSet<PostComment>();

    public override string ToString() => $"{Date}, {User.UserName}: {Title}";
}
