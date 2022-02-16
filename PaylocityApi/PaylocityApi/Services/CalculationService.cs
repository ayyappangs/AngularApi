using PaylocityApi.Common;
using PaylocityApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaylocityApi.Services
{
    public class CalculationService: ICalculationService
    {
        private readonly IBenefitsService benefitsService;

        public CalculationService(IBenefitsService benefitsService)
        {
            this.benefitsService = benefitsService;
        }

        public EmployeeCostToCompany Calculate(Employee employee)
        {
            EmployeeCostToCompany employeeCostToCompany = new EmployeeCostToCompany();            

            var costOfBenefits = PayloCityConstants.EmployeeBenefitCost + benefitsService.GetDependentBenfitsCost(employee.Dependents);

            var salary = CalculateSalary();

            var costToCompany = salary + costOfBenefits;

            employeeCostToCompany.EmployeeId = employee.EmployeeId;

            employeeCostToCompany.Benefits = costOfBenefits;

            employeeCostToCompany.Salary = salary;

            employeeCostToCompany.CostToCompany = costToCompany;

            return employeeCostToCompany;
        }
        

        private decimal CalculateSalary()
        {
            return PayloCityConstants.EmployeeSalary * PayloCityConstants.NumberOfPayChecks;
        }
    }
}
