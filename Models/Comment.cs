using Models.Accounts;

namespace Models;

public class Comment : ModelBase
{
    public string Content { get; set; }

    public bool Status { get; set; }

    public Account Account { get; set; }
    public long AccountId { get; set; }
    public Post Post { get; set; }
    public long PostId { get; set; }
    public long ParentId { get; set; }
}