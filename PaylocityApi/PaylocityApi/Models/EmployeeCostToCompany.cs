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

        public string EmployeeName { get; set; }

        public string DependentName1 { get; set; }

        public string DependentName2 { get; set; }

        public string DependentName3 { get; set; }

        public string DependentName4 { get; set; }

        public double Salary { get; set; }

        public double Benefits { get; set; }

        public double CostToCompany { get; set; }
    }
}
