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

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var query = "SELECT EmployeeId,FirstName,LastName FROM Employee";
            using (var connection = paylocityDatabase.CreateConnection())
            {
                var companies = await connection.QueryAsync<Employee>(query);
                return companies.ToList();
            }
        }
    }
}
