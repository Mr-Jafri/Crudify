

namespace Crudify.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> Login(string username, string password);
    }
}
