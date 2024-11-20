using Models.Accounts;

namespace Models;

public class Post : ModelBase
{
    public string Title { get; set; }

    public string Content { get; set; }

    public string ShortDescription { get; set; }

    public string Thumbnails { get; set; }

    public string Images { get; set; }

    public string Slug { get; set; }

    public bool? Approved { get; set; }

    public bool? Status { get; set; }

    public long Favourite { get; set; } = 0;

    public Category Category { get; set; }
    public long CategoryId { get; set; }

    public Account Account { get; set; }
    public long AccountId { get; set; }

    public ICollection<Comment> Comments { get; set; }
    public ICollection<PostTag> PostTags { get; set; }
}