namespace Models.Accounts;
public enum AuthProvider
{
    Google = 1,
    Local = 2,
    Github = 3
}
public enum Role
{
    ADMIN = 1,
    USER = 2,
    MODERATOR = 3
}


public enum VerifyStatus
{
    Pending = 1,
    Verified = 2,
}
