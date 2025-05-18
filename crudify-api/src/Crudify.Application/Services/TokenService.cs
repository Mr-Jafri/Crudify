
using System.Net;

namespace Crudify.Application.Services;

public class TokenService(IOptionsMonitor<JwtSettings> jwtConfig, ApplicationContext context, TokenValidationParameters tokenValidationParameters) : ITokenService
{
    public async Task<AuthResult> GenerateToken(ApplicationUser user, IList<string> roles)
    {
        JwtSecurityTokenHandler? jwtTokenHandler = new();

        var key = Encoding.UTF8.GetBytes(jwtConfig.CurrentValue.SecretKey);


        var claims = new List<Claim>
        {
            new("Id", user.Id.ToString()),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.Sub, user.Email),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(jwtConfig.CurrentValue.TokenExpiry),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken? token = jwtTokenHandler.CreateToken(tokenDescriptor);
        string jwtToken = jwtTokenHandler.WriteToken(token);

        
        RefreshToken refreshToken = new RefreshToken()
        {
            JwtId = token.Id,
            IsUsed = false,
            IsRevoked = false,
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow,
            ExpiredAt = DateTime.UtcNow.AddMonths(1),
            Token = GetRandomString() + Guid.NewGuid()
        };

        await context.RefreshTokens.AddAsync(refreshToken);
        await context.SaveChangesAsync();



        return new AuthResult()
        {
            Token = jwtToken,
            RefreshToken = refreshToken.Token,
            Success = true,
            HttpStatusCode = (int)HttpStatusCode.OK
        };
    }

    public ClaimsPrincipal VerifyToken(string token)
    {
        var claims = new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.CurrentValue.SecretKey)),
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
        }, out _);
        return claims;
    }

    private string GetRandomString()
    {
        Random random = new Random();
        string chars = "ABCDEFGHIJKLMNOPRSTUVYZWX0123456789";
        return new string(Enumerable.Repeat(chars, 35).Select(n => n[new Random().Next(n.Length)]).ToArray());

    }
}
