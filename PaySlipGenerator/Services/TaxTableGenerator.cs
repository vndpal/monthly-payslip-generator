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
        /// <summary>
        /// Generates the tax table containing income brackets and corresponding tax rates.
        /// </summary>
        /// <returns>A list of <see cref="TaxTable"/> objects representing the tax table.</returns>
        public List<TaxTable> GenerateTaxTable()
        {
            // Create an empty list to store the tax table entries.
            List<TaxTable> taxTable = new List<TaxTable>();

            // Add tax table entries for different income brackets and tax rates.
            taxTable.Add(createTaxTable(0, 14000, 0, 0.105m));
            taxTable.Add(createTaxTable(14001, 48000, 1470, 0.175m));
            taxTable.Add(createTaxTable(48001, 70000, 7420, 0.3m));
            taxTable.Add(createTaxTable(70001, 180000, 14020, 0.33m));
            taxTable.Add(createTaxTable(180001, decimal.MaxValue, 50320, 0.39m));

            return taxTable;
        }

        private TaxTable createTaxTable(decimal minThreshold, decimal maxThreshold, decimal accumulatedTaxFromPreviousBracket, decimal marginalTaxRate)
        {
            // Calculate the income threshold based on the minimum threshold to avoid overlapping brackets.
            var incomeThreshold = CalculateIncomeThreshold(minThreshold);

            // Create the tax bracket object for the income bracket.
            TaxBracket taxBracket = new TaxBracket();
            taxBracket.TotalTaxFromPreviousBracket = accumulatedTaxFromPreviousBracket;
            taxBracket.TaxRate = marginalTaxRate;
            taxBracket.MinIncomeRange = incomeThreshold;

            // Create the tax table entry for the income bracket.
            TaxTable taxTable = new TaxTable();
            taxTable.StartRange = minThreshold;
            taxTable.EndRange = maxThreshold;
            taxTable.IncomeTaxBracket = taxBracket;

            return taxTable;
        }

        private static decimal CalculateIncomeThreshold(decimal minThreshold)
        {
            // Calculate the income threshold as (min - 1) to avoid overlapping with the previous bracket.
            return (minThreshold - 1 > 0) ? minThreshold - 1 : 0;
        }
    }
}
