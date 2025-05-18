using System.ComponentModel.DataAnnotations;

namespace Crudify.Application.Dtos.Auth.Request;

public record TokenRequestDTO
{
    
    public required string Token { get; set; }    
    
    public required string RefreshToken { get; set; }

}
