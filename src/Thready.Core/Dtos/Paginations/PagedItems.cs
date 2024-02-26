namespace Thready.Core.Dtos.Paginations;

public class PagedItems<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
}
