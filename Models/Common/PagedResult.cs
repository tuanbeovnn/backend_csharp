namespace Models;

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public long Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling((decimal)Total / PageSize);
}