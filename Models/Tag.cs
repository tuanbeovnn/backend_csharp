namespace Models;

public class Tag : ModelBase
{
    public TagType Type { get; set; }
    public string? Name { get; set; }
    public ICollection<PostTag> PostTags { get; set; }
}