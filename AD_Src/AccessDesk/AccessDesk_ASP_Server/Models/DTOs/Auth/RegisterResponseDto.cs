namespace AccessDesk_ASP_Server.Models.DTOs.Auth
{
    public class RegisterResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; } = "User registered successfully";
    }
}
