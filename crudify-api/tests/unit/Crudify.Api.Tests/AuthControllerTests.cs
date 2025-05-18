
namespace Crudify.Api.Tests;

[TestClass]
public sealed class AuthControllerTests
{
    private Mock<IAuthService> _mockAuthService;
    private AuthController _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockAuthService = new Mock<IAuthService>();
        _controller = new AuthController(_mockAuthService.Object);
    }

    [TestMethod]
    public async Task Login_InvalidPayload_ReturnsBadRequest()
    {
        // Arrange
        var invalidUser = new LoginUserDTO
        {
            Email = null,
            Password = null
        };

        // Act
        var result = await _controller.Login(invalidUser);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success);
        CollectionAssert.Contains(result.Errors, "Invalid payload");
    }

    [TestMethod]
    public async Task Login_ValidCredentials_ReturnsOkWithAuthResult()
    {
        // Arrange
        var userDto = new LoginUserDTO
        {
            Email = "user@example.com",
            Password = "Password123"
        };

        var expectedAuthResult = new AuthResult
        {
            Success = true,
            Token = "fake-jwt-token"
        };

        _mockAuthService
            .Setup(s => s.Login(userDto.Email, userDto.Password))
            .ReturnsAsync(expectedAuthResult);

        // Act
        var result = await _controller.Login(userDto);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(expectedAuthResult.Token, result.Token);
    }
}
