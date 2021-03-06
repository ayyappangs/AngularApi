using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using PaylocityApi.Common;
using PaylocityApi.Models;
using PaylocityApi.Services;
using PaylocityApi.Validators;

namespace PaylocityApi
{
    public class CostToCompany
    {
        private readonly ILogger<CostToCompany> _logger;
        private readonly ICalculationService calculationService;
        private readonly IEmployeeValidator employeeValidator;

        public CostToCompany(ILogger<CostToCompany> log, ICalculationService calculationService, IEmployeeValidator employeeValidator)
        {
            _logger = log;
            this.calculationService = calculationService;
            this.employeeValidator = employeeValidator;
        }

        [FunctionName("CostToCompany")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                Employee employee = JsonConvert.DeserializeObject<Employee>(requestBody);
                if (employeeValidator.IsValidEmployee(employee))
                {
                    var employeeCostToCompany = calculationService.Calculate(employee);

                    return new OkObjectResult(employeeCostToCompany);
                }
                else
                {
                    return new BadRequestObjectResult(PayloCityConstants.BadRequestError);
                }
                
            }
            catch (System.Exception ex)
            {
                var errorObjectResult = new ObjectResult(ex);
                errorObjectResult.StatusCode = StatusCodes.Status500InternalServerError;
                return errorObjectResult;
            }
               
            
        }
    }
}

