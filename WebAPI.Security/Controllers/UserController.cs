using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebAPI.Security.Services;
using WebAPI.ViewModels.ViewModels;

namespace WebAPI.Security.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserViewModel userRegistration)
        {

            var userResult = await _userService.RegisterUserAsync(userRegistration);
            return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel user)
        {
            return !await _userService.ValidateUserAsync(user)
                ? Unauthorized()
                : Ok(new { Token = await _userService.CreateTokenAsync() });
        }
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {

                var roles = await _userService.GetRoles();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException?.Message + "\n" + ex.Message);

            }
        }

    }
}
