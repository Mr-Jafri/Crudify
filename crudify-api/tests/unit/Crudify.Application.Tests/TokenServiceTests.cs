
using System.Security.Claims;

namespace Crudify.Application.Tests;

[TestClass]
public sealed class TokenServiceTests
{
    private TokenService _tokenService;
    private ApplicationContext _context;
    private TokenValidationParameters _tokenValidationParameters;
    private IOptionsMonitor<JwtSettings> _jwtConfig;


    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationContext(options);

        _jwtConfig = Mock.Of<IOptionsMonitor<JwtSettings>>(j =>
            j.CurrentValue == new JwtSettings { SecretKey = "SuperSecretKey12345678901234898654ABCDGSKJHKULALHFBGLRhfghfdghdgfMnl!", TokenExpiry = 30 });

        _tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.CurrentValue.SecretKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = false
        };

        _tokenService = new TokenService(_jwtConfig, _context, _tokenValidationParameters);
    }

    [TestMethod]
    public async Task GenerateToken_ValidUser_CreatesTokenAndRefreshToken()
    {
        // Arrange
        var user = new ApplicationUser { Id = Guid.NewGuid(), Email = "test@example.com" };
        var roles = new List<string> { "Admin" };

        // Act
        var result = await _tokenService.GenerateToken(user, roles);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsFalse(string.IsNullOrEmpty(result.Token));
        Assert.IsFalse(string.IsNullOrEmpty(result.RefreshToken));

        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == result.RefreshToken);
        Assert.IsNotNull(refreshToken);
    }

    [TestMethod]
    public void VerifyToken_ValidToken_ReturnsClaimsPrincipal()
    {
        // Arrange
        var user = new ApplicationUser { Id = Guid.NewGuid(), Email = "test@example.com" };
        var roles = new List<string> { "Admin" };

        var validToken = _tokenService.GenerateToken(user, roles).Result.Token;

        // Act
        var result = _tokenService.VerifyToken(validToken);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(ClaimsPrincipal));
    }

    [TestMethod]
    public void VerifyToken_ValidToken_ReturnsCorrectClaims()
    {
        // Arrange
        var user = new ApplicationUser { Id = Guid.NewGuid(), Email = "test@example.com" };
        var roles = new List<string> { "Admin" };

        var validToken = _tokenService.GenerateToken(user, roles).Result.Token;

        // Act
        var result = _tokenService.VerifyToken(validToken);

        // Assert
        Assert.IsTrue(result.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Admin"));
    }
}
