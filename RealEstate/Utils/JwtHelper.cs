using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public static class JwtHelper
{
    public static string GenerateAccessToken(string userId, string userName)
    {
        var secretKey = System.Configuration.ConfigurationManager.AppSettings["JwtSecret"];
        var issuer = System.Configuration.ConfigurationManager.AppSettings["JwtIssuer"];
        var audience = System.Configuration.ConfigurationManager.AppSettings["JwtAudience"];
        var expireMinutes = int.Parse(System.Configuration.ConfigurationManager.AppSettings["AccessToken"]);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim("UserId", userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString().Replace("-", "");
    }
}
