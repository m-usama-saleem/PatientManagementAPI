using Microsoft.AspNetCore.Identity;
using WebAPI.DB.Models;
using WebAPI.ViewModels.ViewModels;

namespace WebAPI.Security.Services
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(UserViewModel userForRegistration);
        Task<bool> ValidateUserAsync(LoginViewModel loginDto);
        Task<string> CreateTokenAsync();
        Task<List<IdentityRole>> GetRoles();
    }
}
