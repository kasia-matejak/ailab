namespace AiLab.Application.Model;

public class Meta
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int ItemCount { get; set; }
    public long TotalItems { get; set; }
    public int TotalPages { get; set; }
    public int FirstItemIndex { get; set; }
    public int LastItemIndex { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
    public string? AppliedSort { get; set; }
    public string? AppliedFilters { get; set; }
    public long ExecutionTimeMs { get; set; }
}

public class PagedResult<T>
{
    public Meta Meta { get; set; } = new Meta();
    public List<T> Items { get; set; } = new List<T>();
}
