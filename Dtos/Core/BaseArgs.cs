using Models.Accounts;

namespace Dtos.Core;

public class BaseArgs
{
    public long Id { get; set; }
    public string? CurrentUser { get; set; }
    public Role UserRole { get; set; }
}

public class SearchArgs : BaseArgs
{
    public int OwnerId { get; set; }
    public string? Keyword { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
}