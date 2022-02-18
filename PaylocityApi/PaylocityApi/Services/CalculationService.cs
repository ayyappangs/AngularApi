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
            EmployeeCostToCompany employeeCostToCompany = MapNames(employee);            

            var costOfBenefits = benefitsService.GetEmployeeBenefits(employee.FirstName) + benefitsService.GetDependentBenfitsCost(employee.Dependents);

            var salary = CalculateSalary();

            var costToCompany = salary + costOfBenefits;

            employeeCostToCompany.EmployeeId = new Random().Next();

            employeeCostToCompany.Benefits = costOfBenefits;

            employeeCostToCompany.Salary = salary;

            employeeCostToCompany.CostToCompany = costToCompany;

            return employeeCostToCompany;
        }

        //This is not good interms of domain model. :) 
        private EmployeeCostToCompany MapNames(Employee employee)
        {
            EmployeeCostToCompany employeeCostToCompany = new EmployeeCostToCompany();
            employeeCostToCompany.EmployeeName = employee.FirstName;
            employeeCostToCompany.DependentName1 = employee.Dependents[0].Name;
            employeeCostToCompany.DependentName2 = employee.Dependents[1].Name;
            employeeCostToCompany.DependentName3 = employee.Dependents[2].Name;
            employeeCostToCompany.DependentName4 = employee.Dependents[3].Name;

            return employeeCostToCompany;
        }


        private double CalculateSalary()
        {
            return PayloCityConstants.EmployeeSalary * PayloCityConstants.NumberOfPayChecks;
        }
    }
}
