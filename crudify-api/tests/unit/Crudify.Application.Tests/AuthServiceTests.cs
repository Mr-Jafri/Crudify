
namespace Crudify.Application.Tests;

[TestClass]
public sealed class AuthServiceTests
{
    private Mock<UserManager<ApplicationUser>> _userManagerMock;
    private Mock<ITokenService> _tokenServiceMock;
    private AuthService _authService;

    [TestInitialize]
    public void Setup()
    {
        _userManagerMock = MockUserManager();
        _tokenServiceMock = new Mock<ITokenService>();
        _authService = new AuthService(_userManagerMock.Object, _tokenServiceMock.Object);
    }

    [TestMethod]
    public async Task Login_EmailIsNull_ReturnsError()
    {
        // Act
        var result = await _authService.Login(null, "somepassword");

        // Assert
        Assert.IsFalse(result.Success);
        CollectionAssert.Contains(result.Errors, "Email is required.");
    }

    [TestMethod]
    public async Task Login_UserNotFound_ReturnsError()
    {
        // Arrange
        _userManagerMock
            .Setup(m => m.FindByEmailAsync("notfound@example.com"))
            .ReturnsAsync((ApplicationUser)null);

        // Act
        var result = await _authService.Login("notfound@example.com", "somepassword");

        // Assert
        Assert.IsFalse(result.Success);
        CollectionAssert.Contains(result.Errors, "Email address is not registered.");
    }

    [TestMethod]
    public async Task Login_WrongPassword_ReturnsError()
    {
        var user = new ApplicationUser { Email = "test@example.com" };

        _userManagerMock.Setup(m => m.FindByEmailAsync(user.Email))
            .ReturnsAsync(user);

        _userManagerMock.Setup(m => m.CheckPasswordAsync(user, "wrongpassword"))
            .ReturnsAsync(false);

        // Act
        var result = await _authService.Login(user.Email, "wrongpassword");

        // Assert
        Assert.IsFalse(result.Success);
        CollectionAssert.Contains(result.Errors, "Wrong password");
    }

    [TestMethod]
    public async Task Login_ValidCredentials_ReturnsAuthResult()
    {
        var user = new ApplicationUser { Email = "test@example.com" };
        var roles = new List<string> { "Admin" };
        var tokenResult = new AuthResult { Token = "mock-token", Success = true };

        _userManagerMock.Setup(m => m.FindByEmailAsync(user.Email))
            .ReturnsAsync(user);

        _userManagerMock.Setup(m => m.CheckPasswordAsync(user, "correctpassword"))
            .ReturnsAsync(true);

        _userManagerMock.Setup(m => m.GetRolesAsync(user))
            .ReturnsAsync(roles);

        _tokenServiceMock.Setup(m => m.GenerateToken(user, roles))
            .ReturnsAsync(tokenResult);

        // Act
        var result = await _authService.Login(user.Email, "correctpassword");

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual("mock-token", result.Token);
    }

    private static Mock<UserManager<ApplicationUser>> MockUserManager()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        return new Mock<UserManager<ApplicationUser>>(
            store.Object, null, null, null, null, null, null, null, null);
    }
}
