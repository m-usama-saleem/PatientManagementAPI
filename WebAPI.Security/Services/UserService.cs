using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.DB.Models;
using WebAPI.ViewModels.ViewModels;

namespace WebAPI.Security.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IConfiguration _configuration;
        private IdentityUser _user;
        
        public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> RegisterUserAsync(UserViewModel userRegistration)
        {
            _user = new IdentityUser()
            {
                Email = userRegistration.Email,
                UserName = userRegistration.UserName,
                PasswordHash = userRegistration.Password,
                PhoneNumber = userRegistration.Contact,
            };
            var result = await _userManager.CreateAsync(_user, userRegistration.Password);
            if (result.Succeeded)
            {
                var defaultrole = _roleManager.FindByNameAsync(userRegistration.RoleName).Result;
                if (defaultrole != null)
                {
                    IdentityResult roleresult = await _userManager.AddToRoleAsync(_user, defaultrole.Name);
                }

            }
            return result;
        }

        public async Task<bool> ValidateUserAsync(LoginViewModel loginViewModel)
        {
            _user = await _userManager.FindByNameAsync(loginViewModel.UserName);
            var result = _user != null && await _userManager.CheckPasswordAsync(_user, loginViewModel.Password);
            return result;
        }

        public async Task<string> CreateTokenAsync()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtConfig = _configuration.GetSection("jwtConfig");
            var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtConfig");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiresIn"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }


        public async Task<List<IdentityRole>> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }
    }
}
