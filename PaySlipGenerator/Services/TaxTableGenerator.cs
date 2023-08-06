using PaySlipGenerator.Interfaces;
using PaySlipGenerator.Models;
using System.ComponentModel.DataAnnotations;

namespace PaySlipGenerator.Services
{
    /// <summary>
    /// Service class responsible for generating the tax table used for tax calculations.
    /// </summary>
    public class TaxTableGenerator : ITaxTableGenerator
    {
        private IConfiguration _configuration;
        public TaxTableGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Generates the tax table containing income brackets and corresponding tax rates.
        /// </summary>
        /// <returns>A list of <see cref="TaxTable"/> objects representing the tax table.</returns>
        public List<TaxTable> GenerateTaxTable()
        {
            var taxBrackets = _configuration.GetSection("TaxBrackets").Get<List<TaxBracketConfig>>();
            // Create an empty list to store the tax table entries.
            List<TaxTable> taxTable = new List<TaxTable>();

            foreach (var bracket in taxBrackets)
            {
                taxTable.Add(createTaxTable(
                    bracket.MinThreshold,
                    bracket.MaxThreshold,
                    bracket.AccumulatedTaxFromPreviousBracket,
                    bracket.MarginalTaxRate
                ));
            }

            return taxTable;
        }

        private TaxTable createTaxTable(decimal minThreshold, decimal maxThreshold, decimal accumulatedTaxFromPreviousBracket, decimal marginalTaxRate)
        {
            // Calculate the income threshold based on the minimum threshold to avoid overlapping brackets.
            var incomeThreshold = CalculateIncomeThreshold(minThreshold);

            // Create the tax table entry for the income bracket.
            TaxTable taxTable = new TaxTable();
            taxTable.StartRange = minThreshold;
            taxTable.EndRange = maxThreshold;
            taxTable.TotalTaxFromPreviousBracket = accumulatedTaxFromPreviousBracket;
            taxTable.TaxRate = marginalTaxRate;
            taxTable.MinIncomeRange = incomeThreshold;

            return taxTable;
        }

        private static decimal CalculateIncomeThreshold(decimal minThreshold)
        {
            // Calculate the income threshold as (min - 1) to avoid overlapping with the previous bracket.
            return (minThreshold - 1 > 0) ? minThreshold - 1 : 0;
        }
    }
}
