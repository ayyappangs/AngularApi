using PaylocityApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

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

        public async Task SaveEmployee(EmployeeCostToCompany employeeCostToCompany)
        {
            var query = @"INSERT INTO [dbo].[EmployeeCostToCompany]([EmployeeName],[DependentName1],[DependentName2] ,[DependentName3],[DependentName4],[Salary]
           ,[Benefits]
           ,[CostToCompany]
           ,[UpdatedDate])
     VALUES
           (@EmployeeName
           ,@DependentName1
           ,@DependentName2
           ,@DependentName3
           ,@DependentName4
           ,@Salary
           ,@Benefits
           ,@CostToCompany
           ,@UpdatedDate)";
            var parameters = new DynamicParameters();
            parameters.Add("EmployeeName", employeeCostToCompany.EmployeeName, DbType.String);
            parameters.Add("DependentName1", employeeCostToCompany.DependentName1, DbType.String);
            parameters.Add("DependentName2", employeeCostToCompany.DependentName2, DbType.String);
            parameters.Add("DependentName3", employeeCostToCompany.DependentName3, DbType.String);
            parameters.Add("DependentName4", employeeCostToCompany.DependentName4, DbType.String);
            parameters.Add("Salary", employeeCostToCompany.Salary, DbType.Decimal);
            parameters.Add("Benefits", employeeCostToCompany.Benefits, DbType.Decimal);
            parameters.Add("CostToCompany", employeeCostToCompany.CostToCompany, DbType.Decimal);
            parameters.Add("UpdatedDate", DateTime.Now, DbType.DateTime);
            using (var connection = paylocityDatabase.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
