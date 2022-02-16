using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PaylocityApi;
using PaylocityApi.Repository;
using PaylocityApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: WebJobsStartup(typeof(Startup))]
namespace PaylocityApi
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.TryAddTransient<IPaylocityDatabase, PaylocityDatabase>();
            builder.Services.TryAddTransient<IEmployeeRepository, EmployeeRepository>();
            builder.Services.TryAddTransient<IEmployeeService, EmployeeService>();
            builder.Services.TryAddTransient<ICalculationService, CalculationService>();
            builder.Services.TryAddTransient<IBenefitsService, BenefitsService>();
        }
    }
}
