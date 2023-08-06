using Microsoft.VisualBasic;
using PaySlipGenerator.Common;
using PaySlipGenerator.Interfaces;
using PaySlipGenerator.Models;

namespace PaySlipGenerator.Services
{
    /// <summary>
    /// Service class responsible for calculating monthly income tax based on an employee's salary.
    /// </summary>
    public class TaxCalculator : ITaxCalculator
    {
        private ITaxTableGenerator _taxTableGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxCalculator"/> class.
        /// </summary>
        /// <param name="taxTableGenerator">The tax table generator used to retrieve tax brackets for calculations.</param>
        public TaxCalculator(ITaxTableGenerator taxTableGenerator) 
        {
            _taxTableGenerator = taxTableGenerator;
        }

        /// <summary>
        /// Calculates the monthly income tax from the provided annual salary.
        /// </summary>
        /// <param name="salary">The annual salary of the employee.</param>
        /// <returns>The calculated monthly income tax as a decimal value.</returns>
        public decimal CalculateMonthlyTaxFromSalary(decimal salary)
        {
            // If the salary is less than or equal to zero, there is no tax to be calculated.
            if (salary <= 0)
            {
                return 0;
            }

            // Generate the tax table containing income brackets and tax rates.
            var taxTable = _taxTableGenerator.GenerateTaxTable();

            // Find the appropriate tax bracket based on the given salary.
            var taxBracket = taxTable.Single(x => x.StartRange <= salary && x.EndRange >= salary);

            // Calculate the tax amount for the portion of income within the tax bracket range.
            var marginalTaxableAmount = salary - taxBracket.MinIncomeRange;
            var marginalTax =  marginalTaxableAmount * taxBracket.TaxRate;

            // Calculate the monthly income tax by dividing the total annual tax by 12.
            var monthlyIncomeTax = (taxBracket.TotalTaxFromPreviousBracket + marginalTax) / 12;

            return monthlyIncomeTax.RoundDecimalValue();
        }
    }
}
