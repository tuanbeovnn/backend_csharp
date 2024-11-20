using Models.Accounts;

namespace Models;

public class Profile : ModelBase
{
    public string Bio { get; set; }

    public string Website { get; set; }

    public string Location { get; set; }

    public string AvatarUrl { get; set; }

    public Account? Account { get; set; }
    public long UserId { get; set; }

    public SocialLinks Social { get; set; }
}