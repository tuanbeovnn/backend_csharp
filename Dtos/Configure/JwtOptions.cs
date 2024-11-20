namespace Dtos.Configure;

public class JwtOptions
{
    public const string Name = "Jwt";
    public string Key { get; set; }
    public string Issuer { get; set; }
    public double AccessTokenExpirationMinutes { get; set; }
    public double RefreshTokenExpirationDays { get; set; }
}