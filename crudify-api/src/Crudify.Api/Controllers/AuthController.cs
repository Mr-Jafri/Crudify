
using System.Net;

namespace Crudify.Api.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<AuthResult> Login([FromBody] LoginUserDTO user)
    {

        if (user is { Email: null, Password: null } 
            && string.IsNullOrWhiteSpace(user.Email)
            && string.IsNullOrWhiteSpace(user.Password))
        {
            return (new AuthResult
            {
                Errors = ["Invalid payload"],
                Success = false,
                HttpStatusCode = (int)HttpStatusCode.BadRequest
            });
        }

        return await authService.Login(user.Email, user.Password);

    }
}
