using PaylocityApi.Common;
using PaylocityApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaylocityApi.Services
{
    public class BenefitsService : IBenefitsService
    {
        public decimal GetDependentBenfitsCost(List<Dependent> dependents)
        {

            int countWithANames = dependents.Where(x => x.Name.ToLower().StartsWith(PayloCityConstants.SmallA)).Count();
            
            int countOthers = dependents.Where(x=> !string.IsNullOrEmpty(x.Name)).Count() - countWithANames;

            return GetDependentBenefits(countOthers) + GetDependentBenefitsNameStartsWithA(countWithANames);
        }

        private decimal GetDependentBenefits(int count)
        {
            return count * PayloCityConstants.DependentBenefitCost;
        }

        private decimal GetDependentBenefitsNameStartsWithA(int count)
        {
            return count * (PayloCityConstants.DependentBenefitCost * PayloCityConstants.DiscountPercent);
        }
    }
}
