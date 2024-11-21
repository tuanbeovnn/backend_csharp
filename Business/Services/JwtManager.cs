using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dtos.Configure;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Accounts;
using Utilities;

namespace Business.Services;

public class JwtManager
{
    private readonly JwtOptions _jwtOptions;

    public JwtManager(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


        var sectoken = new JwtSecurityToken(_jwtOptions.Issuer, _jwtOptions.Issuer,
            claims,
            expires: DateTime.Now.AddMinutes(_jwtOptions.AccessTokenExpirationMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(sectoken);
    }

    public RefreshToken GenerateRefreshToken()
    {
        string token = RandomUtils.GenerateId();
        return new RefreshToken
        {
            Token = token,
            ExpireDate = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationDays)
        };
    }
}