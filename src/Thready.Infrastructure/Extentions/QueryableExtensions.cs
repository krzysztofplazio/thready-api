using Microsoft.EntityFrameworkCore;
using Thready.Core.Dtos.Paginations;

namespace Thready.Infrastructure.Extentions;

public static class QueryableExtensions
{
    public static async Task<PagedItems<T>> ToPaginatedListAsync<T>(
        this IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var totalCount = await query.CountAsync().ConfigureAwait(false);
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return new PagedItems<T>
        {
            Items = items,
            Page = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
        };
    }
}