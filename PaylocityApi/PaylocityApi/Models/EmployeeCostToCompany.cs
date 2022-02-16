using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaylocityApi.Models
{
    public  class EmployeeCostToCompany
    {
        public int EmployeeId { get; set; }

        public decimal Salary { get; set; }

        public decimal Benefits { get; set; }

        public decimal CostToCompany { get; set; }
    }
}
