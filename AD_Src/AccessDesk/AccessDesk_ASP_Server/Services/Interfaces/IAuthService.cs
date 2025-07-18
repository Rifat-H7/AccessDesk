using AccessDesk_ASP_Server.Models.DTOs.Auth;
using AccessDesk_ASP_Server.Models.DTOs.Common;

namespace AccessDesk_ASP_Server.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<RegisterResponseDto>> RegisterAsync(RegisterRequestDto request);
        Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request);
        Task<ApiResponse<LoginResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request);
        Task<ApiResponse<string>> LogoutAsync(string userId);
    }
}
