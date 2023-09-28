using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Contracts;
using WebAPI.Helpers;
using WebAPI.ViewModels.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("AllowOrigin")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private IDoctor _doctorService;
        private ICrud<DoctorViewModel> _doctorCruder;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public DoctorController(IDoctor doctorService, IConfiguration configuration,
            ILogger<UserController> logger, ICrud<DoctorViewModel> doctorCruder)
        {
            _doctorService = doctorService;
            _doctorCruder = doctorCruder;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet, Route("get")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Get() Called");
            var result = await _doctorService.ActiveDoctorList();
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "DoctorController", "Get");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "No Doctors Found", "DoctorController", "Get");
            }
        }

        [HttpGet, Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll() Called");
            var result = await _doctorService.DoctorList();
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "DoctorController", "GetAll");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "No Doctors Found", "DoctorController", "GetAll");
            }
        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create(DoctorViewModel obj)
        {
            _logger.LogInformation("Create() Called");
            if (obj == null)
                return new ApiResult(HttpStatusCode.BadRequest, "Model cannot be null", "DoctorController", "Create");
            var result = await _doctorCruder.SaveRecord(obj);
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "DoctorController", "Create");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "No Doctor Found", "DoctorController", "Create");
            }
        }
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(DoctorViewModel obj)
        {
            _logger.LogInformation("Update() Called");
            if (obj == null)
                return new ApiResult(HttpStatusCode.BadRequest, "Model cannot be null", "DoctorController", "Update");
            var result = await _doctorCruder.UpdateRecord(obj);
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "DoctorController", "Update");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "No Doctor Found", "DoctorController", "Update");
            }
        }
        [HttpPost, Route("Delete")]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {
            _logger.LogInformation("Update() Called");
            if (model.Id <= 0)
                return new ApiResult(HttpStatusCode.BadRequest, "id cannot be null or zero(0)", "DoctorController", "Delete");
            var result = await _doctorCruder.DeleteRecord(model.Id);
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "DoctorController", "Delete");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "No Doctor Found", "DoctorController", "Delete");
            }
        }
    }
}
