
using Crudify.Application.Dtos.Auth;
using Crudify.Domain.Entities;

namespace Crudify.Application.Interfaces
{
    public interface ITokenService
    {
        Task<AuthResult> GenerateToken(ApplicationUser user, IList<string> roles);
        ClaimsPrincipal VerifyToken(string token);
    }
}
