
namespace Crudify.Application.Services;

public class StudentsService(ApplicationContext context) : IStudentsService
{
    public async Task<ApiResponse> GetStudentsList()
    {
        var student = await context.Students.ToListAsync();

        if (student is null || !student.Any())
        {
            return new ApiResponse
            {
                Success = false,
                Errors = ["No record found."],
                HttpStatusCode = (int)HttpStatusCode.NoContent
            };
        }

        return new ApiResponse
        {
            Success = true,
            Payload = student,
            HttpStatusCode = (int)HttpStatusCode.OK
        };
    }

    public async Task<ApiResponse> GetStudent(int id)
    {
        var student = await context.Students.FindAsync(id);

        if(student is null)
        {
            return new ApiResponse
            {
                Success = false,
                Errors = ["No record found against the provided studentId."],
                HttpStatusCode = (int)HttpStatusCode.NoContent
            };
        }

        return new ApiResponse
        {
            Success = true,
            Payload = student,
            HttpStatusCode = (int)HttpStatusCode.OK
        };
    }

    public async Task<ApiResponse> AddStudent(Student student)
    {
        if (student is null)
        {
            return new ApiResponse
            {
                Success = false,
                Errors = ["Invalid payload"],
                HttpStatusCode = (int)HttpStatusCode.BadRequest
            };
        }
        student.CreatedOn = DateTime.UtcNow;
        context.Students.Add(student);
        await context.SaveChangesAsync();

        return new ApiResponse
        {
            Success = true,
            Message = [$"{student.FullName} - added successfully"],
            HttpStatusCode = (int)HttpStatusCode.OK
        };
    }

    public async Task<ApiResponse> UpdateStudent(int id, Student student)
    {
        if(id != student.Id)
        {
            return new ApiResponse
            {
                Success = false,
                Errors = ["Id MisMatch"],
                HttpStatusCode = (int)HttpStatusCode.BadRequest
            };
        }
        student.ModifiedOn = DateTime.UtcNow;
        context.Entry(student).State = EntityState.Modified;

        await context.SaveChangesAsync();

        return new ApiResponse
        {
            Success = true,
            Message = ["Record Updated."],
            HttpStatusCode = (int)HttpStatusCode.OK
        };
    }

    public async Task<ApiResponse> DeleteStudent(int id)
    {
        var student = await context.Students.FindAsync(id);

        if (student is null)
        {
            return new ApiResponse
            {
                Success = false,
                Errors = ["No record found."],
                HttpStatusCode = (int)HttpStatusCode.NoContent
            };
        }

        context.Students.Remove(student);
        await context.SaveChangesAsync();
        return new ApiResponse
        {
            Success = true,
            Message = ["Record Deleted."],
            HttpStatusCode = (int)HttpStatusCode.OK
        };
    }
}
