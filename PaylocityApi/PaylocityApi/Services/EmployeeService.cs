using PaylocityApi.Models;
using PaylocityApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaylocityApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeCostToCompany>> GetEmployees()
        {
            return await employeeRepository.GetEmployees();
        }

        public async Task SaveEmployee(EmployeeCostToCompany employee)
        {
            await employeeRepository.SaveEmployee(employee);
        }
    }
}
