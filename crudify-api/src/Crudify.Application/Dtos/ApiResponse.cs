namespace Crudify.Application.Dtos
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public List<string>? Errors { get; set; }
        public List<string>? Message { get; set; }
        public int? HttpStatusCode { get; set; }
        public object Payload { get; set; }
    }
}
