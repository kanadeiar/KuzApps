namespace KuzApps.WebModels;

/// <summary> 
/// Веб модель входа в систему 
/// </summary>
public class AccountLoginWebModel
{
    [Required(ErrorMessage = "Нужно обязательно ввести логин пользователя")]
    [Display(Name = "Логин пользователя")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Нужно обязательно ввести свой пароль")]
    [Display(Name = "Пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Запомнить меня")]
    public bool RememberMe { get; set; }

    [HiddenInput(DisplayValue = false)]
    public string? ReturnUrl { get; set; }
}