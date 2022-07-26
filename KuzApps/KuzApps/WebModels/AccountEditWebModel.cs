namespace KuzApps.WebModels;

/// <summary>
/// Веб модель редактирования пользователя
/// </summary>
public class AccountEditWebModel
{
    [Required(ErrorMessage = "Фамилия обязательна для пользователя")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Фамилия должна быть длинной от 3 до 200 символов")]
    [Display(Name = "Фамилия пользователя")]
    public string SurName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Имя обязательно для пользователя")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Имя должно быть длинной от 3 до 200 символов")]
    [Display(Name = "Имя пользователя")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Отчество обязательно для пользователя")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Отчество должно быть длинной от 3 до 200 символов")]
    [Display(Name = "Отчество пользователя")]
    public string Patronymic { get; set; } = string.Empty;

    [Required(ErrorMessage = "Нужно обязательно ввести свой адрес электронной почты")]
    [EmailAddress(ErrorMessage = "Нужно ввести корректный адрес своей электронной почты")]
    [Display(Name = "Адрес электронной почты E-mail")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Дата рождения обязательна для ввода")]
    [Display(Name = "День рождения пользователя")]
    public DateTime Birthday { get; set; } = DateTime.Today.AddYears(-18);
}
