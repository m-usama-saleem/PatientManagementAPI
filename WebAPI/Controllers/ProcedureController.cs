using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Contracts;
using WebAPI.Helpers;
using WebAPI.ViewModels.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedureController : ControllerBase
    {
        private IProcedure _procedureService;
        private ICrud<ProcedureViewModel> _procedureCruder;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public ProcedureController(IProcedure procedureService, IConfiguration configuration,
            ILogger<UserController> logger, ICrud<ProcedureViewModel> procedureCruder)
        {
            _procedureService = procedureService;
            _procedureCruder = procedureCruder;
            _configuration = configuration;
            _logger = logger;
        }
        
        [HttpGet, Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll() Called");
            var result = await _procedureService.ProcedureList();
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "ProcedureController", "GetAll");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "No Procedures Found", "ProcedureController", "GetAll");
            }
        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create(ProcedureViewModel obj)
        {
            _logger.LogInformation("Create() Called");
            if (obj == null)
                return new ApiResult(HttpStatusCode.BadRequest, "Model cannot be null", "ProcedureController", "Create");
            var result = await _procedureCruder.SaveRecord(obj);
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "ProcedureController", "Create");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "No Procedure Found", "ProcedureController", "Create");
            }
        }
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(ProcedureViewModel obj)
        {
            _logger.LogInformation("Update() Called");
            if (obj == null)
                return new ApiResult(HttpStatusCode.BadRequest, "Model cannot be null", "ProcedureController", "Update");
            var result = await _procedureCruder.UpdateRecord(obj);
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "ProcedureController", "Update");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "No Procedure Found", "ProcedureController", "Update");
            }
        }
        [HttpPost, Route("Delete")]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {
            _logger.LogInformation("Update() Called");
            if (model.Id <= 0)
                return new ApiResult(HttpStatusCode.BadRequest, "id cannot be null or zero(0)", "ProcedureController", "Delete");
            var result = await _procedureCruder.DeleteRecord(model.Id);
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "ProcedureController", "Delete");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "No Procedure Found", "ProcedureController", "Delete");
            }
        }
    }
}
