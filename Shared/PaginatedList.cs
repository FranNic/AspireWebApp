namespace AspireWebApp.Shared;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

public class PaginatedList<TDestination>
{
    public List<TDestination> Items { get; set; } = new();
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public PaginatedList()
    {

    }
    public PaginatedList(List<TDestination> items, int count, int pageNumber, int pageSize)
    {
        Items = items;
        TotalCount = count;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }

    public static async Task<PaginatedList<TDestination>> CreateAsync<TSource>(IQueryable<TSource> source, int pageNumber, int pageSize, Expression<Func<TSource, TDestination>> expression)
    {
        if (pageNumber == 0)
        {
            pageNumber = 1;
        }

        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(expression).ToListAsync();

        return new PaginatedList<TDestination>(items, count, pageNumber, pageSize);
    }
}
