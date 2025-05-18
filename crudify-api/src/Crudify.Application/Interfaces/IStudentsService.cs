
using Crudify.Application.Dtos;

namespace Crudify.Application.Interfaces;

public interface IStudentsService
{
    Task<ApiResponse> GetStudentsList();
    Task<ApiResponse> GetStudent(int id);
    Task<ApiResponse> AddStudent(Student student);
    Task<ApiResponse> UpdateStudent(int id, Student student);
    Task<ApiResponse> DeleteStudent(int id);

}
