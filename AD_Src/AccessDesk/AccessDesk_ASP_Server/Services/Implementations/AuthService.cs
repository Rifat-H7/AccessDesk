using AccessDesk_ASP_Server.Models.DTOs.Auth;
using AccessDesk_ASP_Server.Models.DTOs.Common;
using AccessDesk_ASP_Server.Models.Entities;
using AccessDesk_ASP_Server.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace AccessDesk_ASP_Server.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService,
            IMapper mapper,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<RegisterResponseDto>> RegisterAsync(RegisterRequestDto request)
        {
            try
            {
                // Check if user exists
                var existingUser = await _userManager.FindByEmailAsync(request.Email);
                if (existingUser != null)
                {
                    return ApiResponse<RegisterResponseDto>.ErrorResult("User with this email already exists");
                }

                existingUser = await _userManager.FindByNameAsync(request.Username);
                if (existingUser != null)
                {
                    return ApiResponse<RegisterResponseDto>.ErrorResult("Username is already taken");
                }

                // Create new user
                var user = new ApplicationUser
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    UserName = request.Username,
                    EmailConfirmed = true // For simplicity, auto-confirm email
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return ApiResponse<RegisterResponseDto>.ErrorResult("Registration failed", errors);
                }

                // Add default role (Admin)
                await _userManager.AddToRoleAsync(user, "Admin");

                var response = _mapper.Map<RegisterResponseDto>(user);
                _logger.LogInformation("User {Email} registered successfully", request.Email);

                return ApiResponse<RegisterResponseDto>.SuccessResult(response, "User registered successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during registration for email {Email}", request.Email);
                return ApiResponse<RegisterResponseDto>.ErrorResult("An error occurred during registration");
            }
        }

        public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request)
        {
            try
            {
                // Find user by email or username
                var user = await _userManager.FindByEmailAsync(request.UsernameOrEmail) ??
                           await _userManager.FindByNameAsync(request.UsernameOrEmail);

                if (user == null || !user.IsActive)
                {
                    return ApiResponse<LoginResponseDto>.ErrorResult("Invalid credentials");
                }

                // Check password
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (!result.Succeeded)
                {
                    return ApiResponse<LoginResponseDto>.ErrorResult("Invalid credentials");
                }

                // Generate tokens
                var accessToken = await _tokenService.GenerateAccessTokenAsync(user);
                var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user);

                var userDto = _mapper.Map<UserDto>(user);
                userDto.Roles = (await _userManager.GetRolesAsync(user)).ToList();

                var response = new LoginResponseDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(15), // Match JWT expiration
                    User = userDto
                };

                _logger.LogInformation("User {Email} logged in successfully", user.Email);
                return ApiResponse<LoginResponseDto>.SuccessResult(response, "Login successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login for {UsernameOrEmail}", request.UsernameOrEmail);
                return ApiResponse<LoginResponseDto>.ErrorResult("An error occurred during login");
            }
        }

        public async Task<ApiResponse<LoginResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            try
            {
                var user = await _tokenService.GetUserFromRefreshTokenAsync(request.RefreshToken);
                if (user == null)
                {
                    return ApiResponse<LoginResponseDto>.ErrorResult("Invalid refresh token");
                }

                // Revoke old refresh token
                await _tokenService.RevokeRefreshTokenAsync(request.RefreshToken);

                // Generate new tokens
                var accessToken = await _tokenService.GenerateAccessTokenAsync(user);
                var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user);

                var userDto = _mapper.Map<UserDto>(user);
                userDto.Roles = (await _userManager.GetRolesAsync(user)).ToList();

                var response = new LoginResponseDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(15),
                    User = userDto
                };

                return ApiResponse<LoginResponseDto>.SuccessResult(response, "Token refreshed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during token refresh");
                return ApiResponse<LoginResponseDto>.ErrorResult("An error occurred during token refresh");
            }
        }

        public async Task<ApiResponse<string>> LogoutAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return ApiResponse<string>.ErrorResult("User not found");
                }

                // Revoke all refresh tokens for the user
                // This would be implemented in TokenService
                _logger.LogInformation("User {UserId} logged out successfully", userId);

                return ApiResponse<string>.SuccessResult("Logged out successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during logout for user {UserId}", userId);
                return ApiResponse<string>.ErrorResult("An error occurred during logout");
            }
        }
    }
}
