using PaylocityApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaylocityApi.Services
{
    public interface IBenefitsService
    {
        decimal GetDependentBenfitsCost(List<Dependent> dependents);
    }
}
