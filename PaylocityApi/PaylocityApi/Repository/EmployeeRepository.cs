using PaylocityApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;


namespace PaylocityApi.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IPaylocityDatabase paylocityDatabase;

        public EmployeeRepository(IPaylocityDatabase paylocityDatabase)
        {
            this.paylocityDatabase = paylocityDatabase;
        }

        public async Task<IEnumerable<EmployeeCostToCompany>> GetEmployees()
        {
            var query = "SELECT EmployeeId,EmployeeName,DependentName1,DependentName2,DependentName3,DependentName4,Salary,Benefits,CostToCompany,UpdatedDate FROM EmployeeCostToCompany";
            using (var connection = paylocityDatabase.CreateConnection())
            {
                var companies = await connection.QueryAsync<EmployeeCostToCompany>(query);
                return companies.ToList();
            }
        }
    }
}
