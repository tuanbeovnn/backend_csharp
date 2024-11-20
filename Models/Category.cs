namespace Models;

public class Category : ModelBase
{
    public string Name { get; set; }

    public bool? Status { get; set; }

    public string Slug { get; set; }

    public ICollection<Post> Posts { get; set; }
}