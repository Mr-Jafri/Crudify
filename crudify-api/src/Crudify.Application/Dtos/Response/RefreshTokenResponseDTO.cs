namespace Crudify.Application.Dtos.Auth.Response;

public class RefreshTokenResponseDTO 
{
    public Guid? UserId { get; set; }
    public bool Success { get; set; }
    public List<string> Errors { get; set; }
}
