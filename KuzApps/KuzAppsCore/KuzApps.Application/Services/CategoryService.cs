namespace KuzApps.Application.Services;

/// <summary>
/// Сервис категорий информационных постов
/// </summary>
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepo _categoryRepo;
    public CategoryService(ICategoryRepo categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    public async Task<(IEnumerable<CategoryWebModel>, int?)> GetCategoriesFromBookName(string bookName, int? categoryId = null)
    {
        int? selectedCategoryId = null;

        var items = await _categoryRepo.GetByBookName(bookName);
        var parents = items.Where(x => x.ParentId is null);
        var patentsModels = parents.Select(p => new CategoryWebModel
        {
            Id = p.Id,
            Name = p.Name,
            Order = p.Order,
            Posts = p.Posts.Select(x => new PostTitleWebModel { Id = x.Id, Title = x.Title }).ToList(),
        }).ToList();
        foreach (var parentModel in patentsModels)
        {
            var childrens = items.Where(x => x.ParentId == parentModel.Id);
            foreach (var child in childrens)
            {
                if (child.Id == categoryId)
                    selectedCategoryId = child.ParentId;
                parentModel.Children.Add(new CategoryWebModel
                {
                    Id = child.Id,
                    Name = child.Name,
                    Order = child.Order,
                    Parent = parentModel,
                    Posts = child.Posts.Select(x => new PostTitleWebModel { Id = x.Id, Title = x.Title }).ToList(),
                });;
            }
            parentModel.Children.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
        }
        patentsModels.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));

        var models = items.Select(x => new CategoryWebModel 
        { 
            Id = x.Id,
            Name = x.Name,
            Order = x.Order,
            Posts = x.Posts.Select(x => new PostTitleWebModel { Id = x.Id, Title = x.Title }).ToList(),
        });
        return (patentsModels, selectedCategoryId);
    }
}
