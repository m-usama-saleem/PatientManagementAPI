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
    public class PatientController : ControllerBase
    {
        private IPatient _patientService;
        private ICrud<PatientViewModel> _patientCruder;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public PatientController(IPatient patientService, IConfiguration configuration,
            ILogger<UserController> logger, ICrud<PatientViewModel> patientCruder)
        {
            _patientService = patientService;
            _patientCruder = patientCruder;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet, Route("get")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Get() Called");
            var result = await _patientService.ActivePatientList();
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "PatientController", "Get");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "No Patients Found", "PatientController", "Get");
            }
        }

        [HttpGet, Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _patientService.PatientList();
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "PatientController", "GetAll");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "No Patients Found", "PatientController", "GetAll");
            }
        }
        [HttpGet, Route("test")]
        public async Task<IActionResult> Test()
        {
            try
            {
                var result = await _patientService.ActivePatientList();
            }
            catch(Exception ex)
            {
                return new ApiResult(HttpStatusCode.OK, ex.Message + " \n " + ex.InnerException?.Message, "PatientController", "Test");
            }
            return new ApiResult(HttpStatusCode.OK, "test", "PatientController", "Test");
        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create(PatientViewModel obj)
        {
            _logger.LogInformation("Create() Called");
            if (obj == null)
                return new ApiResult(HttpStatusCode.BadRequest, "Model cannot be null", "PatientController", "Create");
            var result = await _patientCruder.SaveRecord(obj);
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "PatientController", "Create");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "No Patients Found", "PatientController", "Create");
            }
        }
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(PatientViewModel obj)
        {
            _logger.LogInformation("Update() Called");
            if (obj == null)
                return new ApiResult(HttpStatusCode.BadRequest, "Model cannot be null", "PatientController", "Update");
            var result = await _patientCruder.UpdateRecord(obj);
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "PatientController", "Update");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "No Patients Found", "PatientController", "Update");
            }
        }
        [HttpPost, Route("Delete")]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {
            _logger.LogInformation("Update() Called");
            if (model.Id <= 0)
                return new ApiResult(HttpStatusCode.BadRequest, "id cannot be null or zero(0)", "PatientController", "Delete");
            var result = await _patientCruder.DeleteRecord(model.Id);
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "PatientController", "Delete");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "No Patients Found", "PatientController", "Delete");
            }
        }
    }
}
