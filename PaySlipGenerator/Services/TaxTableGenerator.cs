using PaySlipGenerator.Interfaces;
using PaySlipGenerator.Models;
using System.ComponentModel.DataAnnotations;

namespace PaySlipGenerator.Services
{
    public class TaxTableGenerator : ITaxTableGenerator
    {
        public List<TaxTable> GenerateTaxTable()
        {
            List<TaxTable> taxTable = new List<TaxTable>();

            taxTable.Add(createTaxTable(0, 14000, 0, 0.105m));
            taxTable.Add(createTaxTable(14001, 48000, 1470, 0.175m));
            taxTable.Add(createTaxTable(48001, 70000, 7420, 0.3m));
            taxTable.Add(createTaxTable(70001, 180000, 14020, 0.33m));
            taxTable.Add(createTaxTable(180001, decimal.MaxValue, 50320, 0.39m));

            return taxTable;
        }

        private TaxTable createTaxTable(decimal minThreshold, decimal maxThreshold, decimal accumulatedTaxFromPreviousBracket, decimal marginalTaxRate)
        {
            var incomeThreshold = CalculateIncomeThreshold(minThreshold);

            TaxBracket taxBracket = new TaxBracket();
            taxBracket.TotalTaxFromPreviousBracket = accumulatedTaxFromPreviousBracket;
            taxBracket.TaxRate = marginalTaxRate;
            taxBracket.MinIncomeRange = incomeThreshold;

            TaxTable taxTable = new TaxTable();
            taxTable.StartRange = minThreshold;
            taxTable.EndRange = maxThreshold;
            taxTable.IncomeTaxBracket = taxBracket;

            return taxTable;
        }

        private static decimal CalculateIncomeThreshold(decimal minThreshold)
        {
            return (minThreshold - 1 > 0) ? minThreshold - 1 : 0;
        }
    }
}
