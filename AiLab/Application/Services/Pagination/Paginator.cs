using AiLab.Application.Model;

namespace AiLab.Application.Services.Pagination;

public static class Paginator
{
    public static PagedResult<TDest> Paginate<TSource, TDest>(IEnumerable<TSource> source,
        Func<TSource, TDest> projector,
        int page,
        int pageSize,
        Func<IQueryable<TSource>, string?, IQueryable<TSource>>? applySort = null,
        string? sort = null,
        string? filters = null)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();

        var q = source.AsQueryable();
        if (applySort != null)
        {
            q = applySort(q, sort);
        }
        else
        {
            q = ApplySortDefault(q, sort);
        }

        var total = q.LongCount();
        var totalPages = (int)Math.Ceiling(total / (double)pageSize);
        var items = q.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        var dtoItems = items.Select(projector).ToList();

        sw.Stop();

        return new PagedResult<TDest>
        {
            Items = dtoItems,
            Meta = new Meta
            {
                PageNumber = page,
                PageSize = pageSize,
                ItemCount = dtoItems.Count,
                TotalItems = total,
                TotalPages = totalPages,
                FirstItemIndex = total == 0 ? 0 : (page - 1) * pageSize + 1,
                LastItemIndex = total == 0 ? 0 : (page - 1) * pageSize + dtoItems.Count,
                HasPreviousPage = page > 1,
                HasNextPage = page < totalPages,
                AppliedSort = sort,
                AppliedFilters = filters,
                ExecutionTimeMs = sw.ElapsedMilliseconds
            }
        };
    }

    public static (string Field, bool Desc) ParseSort(string? sortStr)
    {
        if (string.IsNullOrWhiteSpace(sortStr))
            return (string.Empty, false);

        var s = sortStr.Split(',')[0];
        var desc = s.StartsWith("-");
        var field = desc ? s[1..] : s;
        return (field, desc);
    }

    private static IQueryable<TSource> ApplySortDefault<TSource>(IQueryable<TSource> q, string? sortStr)
    {
        var (field, desc) = ParseSort(sortStr);
        if (string.IsNullOrEmpty(field)) return q;

        // support nested properties with dot notation
        var param = System.Linq.Expressions.Expression.Parameter(typeof(TSource), "x");
        System.Linq.Expressions.Expression? body = param;
        foreach (var part in field.Split('.'))
        {
            body = System.Linq.Expressions.Expression.PropertyOrField(body!, part);
        }

        // convert to object
        var converted = System.Linq.Expressions.Expression.Convert(body!, typeof(object));
        var selector = System.Linq.Expressions.Expression.Lambda<Func<TSource, object>>(converted, param);

        return desc ? q.OrderByDescending(selector) : q.OrderBy(selector);
    }
}
