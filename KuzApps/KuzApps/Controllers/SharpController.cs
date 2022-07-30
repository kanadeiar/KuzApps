namespace KuzApps.Controllers;

public class SharpController : Controller
{
    const string SharpDotNetBook = "csharpdotnet";
    const string SharpDesktopBook = "csharpdesktop";
    const string SharpWebBook = "csharpweb";

    private readonly ICategoryService _categoryService;
    private readonly IPostService _postService;
    public SharpController(ICategoryService categoryService, IPostService postService)
    {
        _categoryService = categoryService;
        _postService = postService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> General()
    {
        var model = await _categoryService.GetCategoriesFromBookName(SharpDotNetBook);
        return View(model.Item1);
    }

    public async Task<IActionResult> Desktop()
    {
        var model = await _categoryService.GetCategoriesFromBookName(SharpDesktopBook);
        return View(model.Item1);
    }

    public async Task<IActionResult> Web()
    {
        var model = await _categoryService.GetCategoriesFromBookName(SharpWebBook);
        return View(model.Item1);
    }

    public async Task<IActionResult> Post(int id)
    {
        var model = await _postService.GetPostFromId(id);
        return View(model);
    }
}
