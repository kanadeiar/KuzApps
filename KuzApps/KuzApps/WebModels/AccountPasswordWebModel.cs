namespace KuzApps.WebModels;

/// <summary>
/// Вебмодель смены пароля
/// </summary>
public class AccountPasswordWebModel
{
    [Required(ErrorMessage = "Нужно обязательно ввести свой текущий пароль")]
    [Display(Name = "Текущий пароль")]
    [DataType(DataType.Password)]
    public string OldPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Нужно обязательно придумать и ввести какой-либо новый пароль")]
    [Display(Name = "Новый пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Нужно обязательно ввести подтверждение нового пароля")]
    [Display(Name = "Подтверждение нового пароля")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
    public string PasswordConfirm { get; set; } = string.Empty;
}
