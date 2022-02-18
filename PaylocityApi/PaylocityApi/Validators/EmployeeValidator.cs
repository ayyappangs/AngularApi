using PaylocityApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaylocityApi.Validators
{
    public  class EmployeeValidator: IEmployeeValidator
    {
        public bool IsValidEmployee(Employee employee)
        {
            return !string.IsNullOrEmpty(employee.FirstName);
        }

    }
}
