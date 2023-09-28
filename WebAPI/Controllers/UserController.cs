using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Contracts;
using WebAPI.Helpers;
using WebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser _userService;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public UserController(IUser userService, IConfiguration configuration, ILogger<UserController> logger)
        {
            _userService = userService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet, Route("profile/{id?}")]
        public async Task<IActionResult> Profile(int id)
        {
            _logger.LogInformation("Profile() Called");
            var result = await _userService.GetUser(id);
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "UserController", "Profile");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "Profile not exists", "UserController", "Profile");
            }
        }
    }
}
