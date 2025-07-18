using AccessDesk_ASP_Server.Configurations;
using AccessDesk_ASP_Server.Data;
using AccessDesk_ASP_Server.Models.DTOs.Auth;
using AccessDesk_ASP_Server.Models.Entities;
using AccessDesk_ASP_Server.Models.Validators;
using AccessDesk_ASP_Server.Services.Implementations;
using AccessDesk_ASP_Server.Services.Interfaces;
using AccessDesk_ASP_Server.Utilities;
using AccessDesk_ASP_Server.Utilities.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AccessDesk_ASP_Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AD_DBConnection")));

            // Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // JWT Configuration
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings!.SecretKey)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Authorization Policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AppConstants.AuthPolicies.AdminOnly, policy =>
                    policy.RequireRole(AppConstants.Roles.Admin));

                options.AddPolicy(AppConstants.AuthPolicies.UserOrAdmin, policy =>
                    policy.RequireRole(AppConstants.Roles.User, AppConstants.Roles.Admin));
            });

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();

            // Validators
            services.AddScoped<IValidator<RegisterRequestDto>, RegisterRequestValidator>();
            services.AddScoped<IValidator<LoginRequestDto>, LoginRequestValidator>();

            // AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
