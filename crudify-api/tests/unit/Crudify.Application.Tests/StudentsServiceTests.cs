namespace Crudify.Application.Tests;

[TestClass]
public sealed class StudentsServiceTests
{
    private ApplicationContext _context;
    private StudentsService _service;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationContext(options);
        _service = new StudentsService(_context);
    }

    [TestMethod]
    public async Task GetStudentsList_WhenStudentsExist_ReturnsSuccess()
    {
        // Arrange
        _context.Students.Add(new Student {Id = 1, FullName = "Test Student", Email = "this@abc.com", PhoneNumber = "12345678" });
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetStudentsList();

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Payload);
    }

    [TestMethod]
    public async Task GetStudentsList_WhenNoStudents_ReturnsEmptyList()
    {
        // Act
        var result = await _service.GetStudentsList();

        // Assert
        Assert.IsFalse(result.Success);
        var students = result.Payload as List<Student>;
        Assert.IsNull(students);
    }

    [TestMethod]
    public async Task GetStudent_WhenExists_ReturnsStudent()
    {
        var student = new Student { Id = 1, FullName = "John Doe", Email = "this@abc.com", PhoneNumber = "12345678"
        };
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        var result = await _service.GetStudent(1);

        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Payload);
        Assert.AreEqual("John Doe", ((Student)result.Payload).FullName);
    }

    [TestMethod]
    public async Task GetStudent_WhenNotExists_ReturnsError()
    {
        var result = await _service.GetStudent(99);

        Assert.IsFalse(result.Success);
        CollectionAssert.Contains(result.Errors, "No record found against the provided studentId.");
    }

    [TestMethod]
    public async Task AddStudent_WithValidInput_ReturnsSuccess()
    {
        var student = new Student {Id = 1, FullName = "Jane Doe", Email = "this@abc.com", PhoneNumber = "12345678" };

        var result = await _service.AddStudent(student);

        Assert.IsTrue(result.Success);
        Assert.AreEqual(1, _context.Students.Count());
        StringAssert.Contains(result.Message.FirstOrDefault(), "Jane Doe");
    }

    [TestMethod]
    public async Task AddStudent_WithNullInput_ReturnsError()
    {
        var result = await _service.AddStudent(null);

        Assert.IsFalse(result.Success);
        CollectionAssert.Contains(result.Errors, "Invalid payload");
    }

    [TestMethod]
    public async Task UpdateStudent_WithValidData_ReturnsSuccess()
    {
        var student = new Student { Id = 1, FullName = "Old Name", Email = "this@abc.com", PhoneNumber = "12345678" };
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        student.FullName = "Updated Name";
        var result = await _service.UpdateStudent(1, student);

        Assert.IsTrue(result.Success);
        Assert.AreEqual("Updated Name", _context.Students.Find(1).FullName);
    }

    [TestMethod]
    public async Task UpdateStudent_WithIdMismatch_ReturnsError()
    {
        var student = new Student { Id = 2, FullName = "Mismatch" };

        var result = await _service.UpdateStudent(1, student);

        Assert.IsFalse(result.Success);
        CollectionAssert.Contains(result.Errors, "Id MisMatch");
    }

    [TestMethod]
    public async Task DeleteStudent_WhenExists_ReturnsSuccess()
    {
        var student = new Student {Id = 1, FullName = "To Delete", Email = "this@abc.com", PhoneNumber = "12345678" };
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        var result = await _service.DeleteStudent(1);

        Assert.IsTrue(result.Success);
        Assert.AreEqual(0, _context.Students.Count());
    }

    [TestMethod]
    public async Task DeleteStudent_WhenNotExists_ReturnsError()
    {
        var result = await _service.DeleteStudent(100);

        Assert.IsFalse(result.Success);
        CollectionAssert.Contains(result.Errors, "No record found.");
    }
}
