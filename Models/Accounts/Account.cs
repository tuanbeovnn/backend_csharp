namespace Models.Accounts;

public class Account : ModelBase
{
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? UserName { get; set; }

    public bool Active { get; set; }
    public string? Password { get; set; }

    public VerifyStatus VerifyStatus { get; set; } = VerifyStatus.Pending;

    public AuthProvider Provider { get; set; }

    public Role Role { get; set; }

    public long Followers { get; set; } = 0L;
    public ICollection<Comment> Comments { get; set; }

    public ICollection<Post> Posts { get; set; }

    public Profile? Profile { get; set; }
}

public class RefreshToken
{
    public string Token { get; set; }
    public DateTime Expire { get; set; }
}