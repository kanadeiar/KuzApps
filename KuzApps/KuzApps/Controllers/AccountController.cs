using KuzApps.Application.Interfaces.Services;

namespace KuzApps.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var model = (User.Identity!.IsAuthenticated)
            ? await _accountService.GetIndexData(User.Identity.Name)
            : new AccountIndexWebModel();
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
        var (result, errors) = await _accountService.RequestRegisterUser(model);
        if (result)
        {
            return RedirectToAction("Index", "Home");
        }
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
        var result = await _accountService.LoginPasswordSignIn(model);
        if (result)
        {
            return LocalRedirect(model.ReturnUrl ?? "/");
        }
        ModelState.AddModelError("", "Ошибка в имени пользователя, либо в пароле при входе в систему");
        return View();
    }

    public async Task<IActionResult> Edit()
    {
        var result = await _accountService.GetEditData(User.Identity!.Name);
        if (result is { } model)
        {
            return View(model);
        }
        return NotFound();
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AccountEditWebModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var (result, errors) = await _accountService.RequestUpdateUser(User.Identity!.Name, model);
        if (result)
        {
            return RedirectToAction("Index", "Account");
        }
        foreach (var error in errors)
        {
            ModelState.AddModelError("", error);
        }
        return View(model);
    }

    public IActionResult Password()
    {
        return View(new AccountPasswordWebModel());
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Password(AccountPasswordWebModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var (result, errors) = await _accountService.ChangeUserPassword(User.Identity!.Name, model);
        if (result)
        {
            return RedirectToAction("Index", "Account");
        }
        foreach (var error in errors)
        {
            ModelState.AddModelError("", error);
        }
        return View(model);
    }

    public async Task<IActionResult> Logout(string returnUrl)
    {
        await _accountService.SignOut();
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
        var result = await _accountService.UserNameIsFree(UserName);
        return Json(result ? "true" : "Такой логин уже занят другим пользователем");
    }

    #endregion
}