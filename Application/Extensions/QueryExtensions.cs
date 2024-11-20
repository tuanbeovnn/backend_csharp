namespace Application.Extensions;
internal static class QueryExtensions
{
    public static void PagingQuery<T>(this IQueryable<T> query, int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        query = query.Skip((page - 1) * pageSize)
            .Take(pageSize);
    }
}
