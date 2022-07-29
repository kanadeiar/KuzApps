namespace KuzApps.Application.WebModels;

/// <summary>
/// Веб модель сведений о пользователе
/// </summary>
public class AccountIndexWebModel
{
    /// <summary> 
    /// Сведения о пользовате 
    /// </summary>
    public User User { get; set; } = default!;
    /// <summary> 
    /// Роли пользователя 
    /// </summary>
    public IEnumerable<string> UserRoleNames { get; set; } = Enumerable.Empty<string>();
}