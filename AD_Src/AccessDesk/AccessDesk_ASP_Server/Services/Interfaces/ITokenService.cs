using AccessDesk_ASP_Server.Models.Entities;
using System.Security.Claims;

namespace AccessDesk_ASP_Server.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateAccessTokenAsync(ApplicationUser user);
        Task<string> GenerateRefreshTokenAsync(ApplicationUser user);
        Task<ClaimsPrincipal?> ValidateTokenAsync(string token);
        Task<bool> RevokeRefreshTokenAsync(string token);
        Task<ApplicationUser?> GetUserFromRefreshTokenAsync(string refreshToken);
    }
}
