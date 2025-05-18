using System.Net;

namespace Crudify.Application.Services;

public class AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService) : IAuthService
{
    public async Task<AuthResult> Login(string email, string password)
    {
        if (email is null)
        {
            return new RegisterResponseDTO()
            {
                Errors = ["Email is required."],
                Success = false,
                HttpStatusCode = (int)HttpStatusCode.BadRequest
            };
        }

        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return new LoginResponseDTO()
            {
                Errors = ["Email address is not registered."],
                Success = false,
                HttpStatusCode = (int)HttpStatusCode.Unauthorized
            };
        }

        bool isUserCorrect = await userManager.CheckPasswordAsync(user, password);
        if (isUserCorrect)
        {
            var roles = await userManager.GetRolesAsync(user);

            AuthResult authResult = await tokenService.GenerateToken(user, roles);
            return authResult;
        }
        else
        {
            return new LoginResponseDTO()
            {
                Errors = ["Wrong password"],
                Success = false,
                HttpStatusCode = (int)HttpStatusCode.Unauthorized
            };
        }

    }
}
