namespace KuzApps.Domain.Identity;

/// <summary>
/// Роль пользователей
/// </summary>
public class Role : IdentityRole
{
    [Required(ErrorMessage = "Описание роли обязательно для роли пользователей")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Описание роли должно быть длинной от 3 до 200 символов")]
    public string Description { get; set; } = String.Empty;

    /// <summary> 
    /// Признак удаления
    /// </summary>
    public bool IsDeleted { get; set; }
}