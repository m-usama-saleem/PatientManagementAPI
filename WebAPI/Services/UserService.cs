using Microsoft.EntityFrameworkCore;
using WebAPI.Contracts;
using WebAPI.DB.Models;
using WebAPI.ViewModels.ViewModels;

namespace WebAPI.Services
{
    public class UserService : IUser
    {
        private readonly ILogger _logger;
        private readonly PatientManagementContext _db;
        private readonly IConfiguration _configuration;

        public UserService(PatientManagementContext dBContext, IConfiguration configuration, ILogger<UserService> logger)
        {
            _logger = logger;
            _db = dBContext;
            _configuration = configuration;
        }

        public async Task<UserViewModel> GetUser(int id)
        {
            UserViewModel profile = null;
            var up = await _db.UserProfiles.FirstOrDefaultAsync(x => x.UserId == id);
            if (up != null)
            {
                profile = new UserViewModel
                {
                    UserId = up.Id,
                    //FirstName = up.FirstName,
                    //LastName = up.LastName,
                    Contact = up.Contact,
                    //MiddleName = up.MiddleName
                };
            }
            return profile;
        }
        public bool IsUserExists(int id)
        {
            return _db.Users.Any(x => x.Id == id);
        }
    }
}
