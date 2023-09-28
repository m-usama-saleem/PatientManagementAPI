using Microsoft.AspNetCore.Mvc;
using WebAPI.DB.Models;
using WebAPI.ViewModels.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Contracts
{
    public interface IUser
    {
        bool IsUserExists(int id);
        Task<UserViewModel> GetUser(int id);
    }
}
