namespace KuzApps.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly SignInManager<User> _signInManager;
    public AccountService(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    public async Task<AccountIndexWebModel> GetIndexData(string? userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            return new AccountIndexWebModel();
        }
        var user = await _userManager.FindByNameAsync(userName);
        var roles = await _userManager.GetRolesAsync(user);
        var model = new AccountIndexWebModel
        {
            User = user,
            UserRoleNames = _roleManager.Roles.Where(r => roles.Contains(r.Name)).Select(r => r.Description).ToArray(),
        };
        return model;
    }

    public async Task<(bool success, string[] errors)> RequestRegisterUser(AccountRegisterWebModel model)
    {
        var user = new User
        {
            SurName = model.SurName,
            FirstName = model.FirstName,
            Patronymic = model.Patronymic,
            UserName = model.UserName,
            Email = model.Email,
            Birthday = model.Birthday,
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "guests");
            await _signInManager.SignInAsync(user, false);
            return (true, Array.Empty<string>());
        }
        var errors = result.Errors.Select(e => IdentityErrorCodes.GetDescription(e.Code)).ToArray();
        return (false, errors);
    }

    public async Task<bool> LoginPasswordSignIn(AccountLoginWebModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
        return result.Succeeded;
    }

    public async Task<AccountEditWebModel?> GetEditData(string? userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            return null;
        }
        if (await _userManager.FindByNameAsync(userName) is User user)
        {
            var model = new AccountEditWebModel
            {
                SurName = user.SurName,
                FirstName = user.FirstName,
                Patronymic = user.Patronymic,
                Email = user.Email,
                Birthday = user.Birthday,
            };
            return model;
        }
        return null;
    }

    public async Task<(bool success, string[] errors)> RequestUpdateUser(string? userName, AccountEditWebModel model)
    {
        if (userName is null)
        {
            return (false, new string[] { "Должно быть указано имя пользователя" });
        }
        if (await _userManager.FindByNameAsync(userName) is User user)
        {
            user.SurName = model.SurName;
            user.FirstName = model.FirstName;
            user.Patronymic = model.Patronymic;
            user.Email = model.Email;
            user.Birthday = model.Birthday;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return (true, Array.Empty<string>());
            }
            var errors = result.Errors.Select(e => IdentityErrorCodes.GetDescription(e.Code)).ToArray();
            return (false, errors);
        }
        return (false, new string[] { "Не удалось найти сущность в базе данных" });
    }

    public async Task<(bool success, string[] errors)> ChangeUserPassword(string? userName, AccountPasswordWebModel model)
    {
        if (userName is null)
        {
            return (false, new string[] { "Должно быть указано имя пользователя" });
        }
        var errors = new List<string>();
        if (await _userManager.FindByNameAsync(userName) is User user)
        {
            var resultCheck = await _signInManager.CheckPasswordSignInAsync(user, model.OldPassword, false);
            if (resultCheck.Succeeded)
            {
                var resultRemove = await _userManager.RemovePasswordAsync(user);
                if (resultRemove.Succeeded)
                {
                    var resultAdd = await _userManager.AddPasswordAsync(user, model.Password);
                    if (resultAdd.Succeeded)
                    {
                        return (true, Array.Empty<string>());
                    }
                    else
                    {
                        foreach (var error in resultAdd.Errors)
                        {
                            errors.Add(IdentityErrorCodes.GetDescription(error.Code));
                        }
                    }
                }
                else
                {
                    foreach (var error in resultRemove.Errors)
                    {
                        errors.Add(IdentityErrorCodes.GetDescription(error.Code));
                    }
                }
            }
            else
            {
                errors.Add("Неправильный старый пароль");
            }
        }
        else
        {
            errors.Add("Не удалось найти сущность в базе данных");
        }
        return (false, errors.ToArray());
    }

    public async Task SignOut()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<bool> UserNameIsFree(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        return user is null;
    }
}