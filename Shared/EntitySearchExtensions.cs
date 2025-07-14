namespace AspireWebApp.Shared;
using System.Linq.Expressions;

public static class EntitySearchExtensions
{
    public static IQueryable<T> Search<T>(this IQueryable<T> query, string? searchValue)
    {
        if (query == null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        var properties = typeof(T)
            .GetProperties()
            .Where(p => p.PropertyType == typeof(string));

        if (string.IsNullOrEmpty(searchValue) || !properties.Any())
        {
            return query;
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        var predicate = properties
            .Select(property =>
                (Expression)Expression.Call(
                    Expression.Property(parameter, property),
                    "Contains",
                    Type.EmptyTypes,
                    Expression.Constant(searchValue, typeof(string)))
            )
            .Aggregate(Expression.Or);

        return query.Where(Expression.Lambda<Func<T, bool>>(predicate, parameter));
    }
}