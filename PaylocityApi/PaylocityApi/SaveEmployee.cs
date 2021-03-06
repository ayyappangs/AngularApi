using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using PaylocityApi.Models;
using PaylocityApi.Services;

namespace PaylocityApi
{
    public class SaveEmployee
    {
        private readonly ILogger<SaveEmployee> _logger;
        private readonly IEmployeeService employeeService;

        public SaveEmployee(ILogger<SaveEmployee> log, IEmployeeService employeeService)
        {
            _logger = log;
            this.employeeService = employeeService;
        }

        [FunctionName("SaveEmployee")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            EmployeeCostToCompany employeeCostToCompany = JsonConvert.DeserializeObject<EmployeeCostToCompany>(requestBody);
            await employeeService.SaveEmployee(employeeCostToCompany);

            return new OkObjectResult("Success");
        }
    }
}

