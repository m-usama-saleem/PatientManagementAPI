using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace WebAPI.Helpers
{
    public class ApiResult : IActionResult
    {
        public ApiResult()
        {

        }
        private readonly WebApiLogger _logger;
        public HttpStatusCode StatusCode { get; set; }
        public object Content { get; private set; }
        public string ControllerName { get; set; }
        public string MethodName { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }

        public ApiResult(HttpStatusCode statusCode, object content, string controllerName, string methodName)
        {
            _logger = WebApiLogger.GetInstance();
            if (statusCode == HttpStatusCode.BadRequest || statusCode == HttpStatusCode.InternalServerError)
            {
                _logger.LogError(string.Format("{0}.{1}(), StatusCode: {2}", controllerName, methodName, statusCode));

            }
            else
            {
                _logger.LogInformation(string.Format("{0}.{1}(), StatusCode: {2}", controllerName, methodName, statusCode));
            }

            this.StatusCode = statusCode;
            this.Content = content;
            this.ControllerName = controllerName;
            this.MethodName = methodName;
        }
        async Task IActionResult.ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(this.Content)
            {
                StatusCode = (int)this.StatusCode,

            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
