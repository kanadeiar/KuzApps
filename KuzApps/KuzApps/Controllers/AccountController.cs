namespace KuzApps.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var model = new AccountIndexWebModel();
        if (User.Identity is { } && User.Identity.IsAuthenticated)
        {
            model.User = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(model.User);
            model.UserRoleNames = _roleManager.Roles.Where(r => roles.Contains(r.Name)).Select(r => r.Description);
        }
        return View(model);
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
        return View(new AccountRegisterWebModel());
    }
    [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
    public async Task<IActionResult> Register(AccountRegisterWebModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
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
            await _userManager.AddToRoleAsync(user, "guest");
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }
        var errors = result.Errors.Select(e => IdentityErrorCodes.GetDescription(e.Code)).ToArray();
        foreach (var error in errors)
        {
            ModelState.AddModelError("", error);
        }
        return View(model);
    }

    [AllowAnonymous]
    public IActionResult Login(string returnUrl)
    {
        if (returnUrl.StartsWith("/Account"))
        {
            returnUrl = "/";
        }
        return View(new AccountLoginWebModel { ReturnUrl = returnUrl });
    }
    [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
    public async Task<IActionResult> Login(AccountLoginWebModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
        if (result.Succeeded)
        {
            return LocalRedirect(model.ReturnUrl ?? "/");
        }
        ModelState.AddModelError("", "Ошибка в имени пользователя, либо в пароле при входе в систему");
        return View();
    }

    public async Task<IActionResult> Logout(string returnUrl)
    {
        await _signInManager.SignOutAsync();
        if (returnUrl.StartsWith("/Account"))
        {
            returnUrl = "/";
        }
        return LocalRedirect(returnUrl ?? "/");
    }

    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }

    #region WebAPI

    [AllowAnonymous]
    public async Task<IActionResult> IsNameFree(string UserName)
    {
        var user = await _userManager.FindByNameAsync(UserName);
        return Json(user is null ? "true" : "Такой логин уже занят другим пользователем");
    }

    #endregion
}