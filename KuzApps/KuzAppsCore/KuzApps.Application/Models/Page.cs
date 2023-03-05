namespace KuzApps.Application.Models;

public record Page<T>(IEnumerable<T> Items, int PageNumber, int PageSize, int TotalCount) : IKndPage<T>
{
    public int ItemsCount => (int)Math.Ceiling((double)TotalCount / PageSize);
}
