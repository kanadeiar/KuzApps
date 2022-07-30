namespace KuzApps.Application.Services;

/// <summary>
/// Репозиторий информационных постов
/// </summary>
public class PostService : IPostService
{
    private readonly IPostRepo _postRepo;
    public PostService(IPostRepo postRepo)
    {
        _postRepo = postRepo;
    }

    public async Task<PostWebModel> GetPostFromId(int id)
    {
        var post = await _postRepo.GetById(id);
        var model = new PostWebModel
        {
            Id = post.Id,
            Title = post.Title,
            Body = post.Body,
        };
        return model;
    }
}
