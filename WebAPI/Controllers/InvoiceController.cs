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
    public class InvoiceController : ControllerBase
    {
        private IInvoice _invoiceService;
        private ICrud<InvoiceViewModel> _invoiceCruder;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public InvoiceController(IInvoice invoiceService, IConfiguration configuration,
            ILogger<UserController> logger, ICrud<InvoiceViewModel> invoiceCruder)
        {
            _invoiceService = invoiceService;
            _invoiceCruder = invoiceCruder;
            _configuration = configuration;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Get(long id)
        {
            _logger.LogInformation("Get() Called");
            if (id <= 0)
                return new ApiResult(HttpStatusCode.BadRequest, "id cannot be null or zero(0)", "InvoiceController", "Get");
            var result = await _invoiceCruder.GetRecordById(id);
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "InvoiceController", "Get");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "No Invoice Found", "InvoiceController", "Get");
            }
        }
        [HttpGet, Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll() Called");
            var result = await _invoiceService.InvoiceList();
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "InvoiceController", "GetAll");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "No Invoices Found", "InvoiceController", "GetAll");
            }
        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create(InvoiceViewModel obj)
        {
            _logger.LogInformation("Create() Called");
            if (obj == null)
                return new ApiResult(HttpStatusCode.BadRequest, "Model cannot be null", "InvoiceController", "Create");
            var result = await _invoiceCruder.SaveRecord(obj);
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "InvoiceController", "Create");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "No Invoice Found", "InvoiceController", "Create");
            }
        }
        [HttpPost, Route("SavePayment")]
        public async Task<IActionResult> SavePayment(InvoiceViewModel obj)
        {
            _logger.LogInformation("Create() Called");
            if (obj == null)
                return new ApiResult(HttpStatusCode.BadRequest, "Model cannot be null", "InvoiceController", "Create");
            var result = await _invoiceCruder.UpdateRecord(obj);
            if (result != null)
            {
                return new ApiResult(HttpStatusCode.OK, result, "InvoiceController", "Create");
            }
            else
            {
                return new ApiResult(HttpStatusCode.NotFound, "No Invoice Found", "InvoiceController", "Create");
            }
        }
        
    }
}
