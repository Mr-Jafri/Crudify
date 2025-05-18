
using Microsoft.AspNetCore.Authorization;

namespace Crudify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController(IStudentsService studentService) : ControllerBase
    {
        [HttpGet]
        public async Task<ApiResponse> Students() => await studentService.GetStudentsList();

        [HttpGet("{id}")]
        public async Task<ApiResponse> Students(int id) => await studentService.GetStudent(id);
        
        [HttpPost]
        public async Task<ApiResponse> Students([FromBody] Student student) => await studentService.AddStudent(student);

        [HttpPut("{id}")]
        public async Task<ApiResponse> Students(int id, Student student) => await studentService.UpdateStudent(id, student);

        [HttpDelete("delete/{id}")]
        public async Task<ApiResponse> DeleteStudent(int id) => await studentService.DeleteStudent(id);
    }
}
