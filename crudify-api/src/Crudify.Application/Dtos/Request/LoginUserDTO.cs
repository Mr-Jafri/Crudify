using System.ComponentModel.DataAnnotations;

namespace Crudify.Application.Dtos.Auth.Request;

public record LoginUserDTO
{
    [EmailAddress]
    public required string Email { get; set; }

    public required string Password { get; set; }
}
