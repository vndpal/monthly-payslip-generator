using Microsoft.VisualBasic;
using PaySlipGenerator.Common;
using PaySlipGenerator.Interfaces;
using PaySlipGenerator.Models;

namespace PaySlipGenerator.Services
{
    public class TaxCalculator : ITaxCalculator
    {
        private ITaxTableGenerator _taxTableGenerator;
        public TaxCalculator(ITaxTableGenerator taxTableGenerator) 
        {
            _taxTableGenerator = taxTableGenerator;
        }
        public decimal CalculateMonthlyTaxFromSalary(decimal salary)
        {
            if(salary <= 0)
            {
                return 0;
            }

            var taxTable = _taxTableGenerator.GenerateTaxTable();

            var taxBracket = taxTable.Single(x => x.StartRange <= salary && x.EndRange >= salary).IncomeTaxBracket;
            
            var marginalTaxableAmount = salary - taxBracket.MinIncomeRange;
            var marginalTax =  marginalTaxableAmount * taxBracket.TaxRate;

            var monthlyIncomeTax = (taxBracket.TotalTaxFromPreviousBracket + marginalTax) / 12;

            return monthlyIncomeTax.RoundDecimalValue();
        }
    }
}
