namespace Crudify.Application.Dtos.Auth;

public class AuthResult
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public bool Success { get; set; }
    public List<string>? Errors { get; set; }
    public int HttpStatusCode { get; set;}
}
