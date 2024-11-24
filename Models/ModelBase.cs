namespace Models;

public abstract class ModelBase
{
    public long Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public DataStatus DataStatus { get; set; }
    public int OwnerId { get; set; }
}

public enum DataStatus
{
    Deleted = -1,
    Published = 0,
    Draft = 1,
}