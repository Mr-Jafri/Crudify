
namespace Crudify.Api.Tests;

[TestClass]
public sealed class StudentsControllerTests
{
    private Mock<IStudentsService> _studentServiceMock;
    private StudentsController _controller;

    [TestInitialize]
    public void Setup()
    {
        _studentServiceMock = new Mock<IStudentsService>();
        _controller = new StudentsController(_studentServiceMock.Object);
    }

    [TestMethod]
    public async Task GetStudents_ShouldReturnStudentsList()
    {
        // Arrange
        var expectedResponse = new ApiResponse { Success = true };
        _studentServiceMock.Setup(s => s.GetStudentsList()).ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.Students();

        // Assert
        Assert.AreEqual(expectedResponse, result);
    }

    [TestMethod]
    public async Task GetStudent_ById_ShouldReturnStudent()
    {
        // Arrange
        int studentId = 1;
        var expectedResponse = new ApiResponse { Success = true };
        _studentServiceMock.Setup(s => s.GetStudent(studentId)).ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.Students(studentId);

        // Assert
        Assert.AreEqual(expectedResponse, result);
    }

    [TestMethod]
    public async Task AddStudent_ShouldReturnResponse()
    {
        // Arrange
        var student = new Student { Id = 1, FullName = "John", Email = "this@abc.com", PhoneNumber="123456789" };
        var expectedResponse = new ApiResponse { Success = true };
        _studentServiceMock.Setup(s => s.AddStudent(student)).ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.Students(student);

        // Assert
        Assert.AreEqual(expectedResponse, result);
    }

    [TestMethod]
    public async Task UpdateStudent_ShouldReturnResponse()
    {
        // Arrange
        int studentId = 1;
        var student = new Student { Id = 1, FullName = "John Updated", Email = "this@abc.com", PhoneNumber = "123456789" };
        var expectedResponse = new ApiResponse { Success = true };
        _studentServiceMock.Setup(s => s.UpdateStudent(studentId, student)).ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.Students(studentId, student);

        // Assert
        Assert.AreEqual(expectedResponse, result);
    }

    [TestMethod]
    public async Task DeleteStudent_ShouldReturnResponse()
    {
        // Arrange
        int studentId = 1;
        var expectedResponse = new ApiResponse { Success = true };
        _studentServiceMock.Setup(s => s.DeleteStudent(studentId)).ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.DeleteStudent(studentId);

        // Assert
        Assert.AreEqual(expectedResponse, result);
    }
}
