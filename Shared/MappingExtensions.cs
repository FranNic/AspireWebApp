namespace AspireWebApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TSource, TDestination>(this IQueryable<TSource> queryable, int pageNumber, int pageSize, Expression<Func<TSource, TDestination>> expression)
        => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize, expression);
}
