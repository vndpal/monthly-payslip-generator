using PaySlipGenerator.Interfaces;
using PaySlipGenerator.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

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
            var taxBrackets = _configuration.GetSection("TaxBrackets").Get<List<TaxBracketConfig>>().OrderBy(x=>x.TaxSlabNumber);

            //validate taxBrackets in configuration
            if(taxBrackets!= null)
            {
                validateTaxBracketConfig(taxBrackets.ToList());
            }
            else
            {
                throw new Exception("Tax Brackets configuration is not present");
            }

            

            // Create an empty list to store the tax table entries.
            List<TaxTable> taxTable = new List<TaxTable>();

            foreach (var bracket in taxBrackets)
            {
                taxTable.Add(createTaxTable(bracket, taxTable));
            }

            return taxTable;
        }

        private TaxTable createTaxTable(TaxBracketConfig currentTaxBracket,List<TaxTable> taxBrackets)
        {
            // Calculate the income threshold based on the minimum threshold to avoid overlapping brackets.
            var incomeThreshold = CalculateIncomeThreshold(currentTaxBracket.MinThreshold);

            decimal totalTaxInPreviousBracket = 0;

            //If its first bracket then accumulated tax value will be zero
            if (currentTaxBracket.TaxSlabNumber == 1)
            {
                totalTaxInPreviousBracket = 0;
            }
            else
            {
                //get previous tax bracket
                var lastBracket = taxBrackets.Single(x => x.TaxBracketId == (currentTaxBracket.TaxSlabNumber - 1));

                //Formula for taxAccumulation = taxAccumulated + ((max-min)*rate)
                totalTaxInPreviousBracket = lastBracket.TotalTaxFromPreviousBracket + ((lastBracket.EndRange - lastBracket.MinIncomeRange)*lastBracket.TaxRate);
            }
            

            // Create the tax table entry for the income bracket.
            TaxTable taxTable = new TaxTable();
            taxTable.StartRange = currentTaxBracket.MinThreshold;
            taxTable.EndRange = currentTaxBracket.MaxThreshold;
            taxTable.TotalTaxFromPreviousBracket = totalTaxInPreviousBracket;
            taxTable.TaxRate = currentTaxBracket.MarginalTaxRate;
            taxTable.MinIncomeRange = incomeThreshold;
            taxTable.TaxBracketId = currentTaxBracket.TaxSlabNumber;

            return taxTable;
        }

        private static decimal CalculateIncomeThreshold(decimal minThreshold)
        {
            // Calculate the income threshold as (min - 1) to avoid overlapping with the previous bracket.
            return (minThreshold - 1 > 0) ? minThreshold - 1 : 0;
        }

        //Validating config file
        private void validateTaxBracketConfig(List<TaxBracketConfig> taxBracketConfigs)
        {
            for (int i = 0; i < taxBracketConfigs.Count; i++)
            {
                //Min range should not be greater than Max range
                if (taxBracketConfigs[i].MinThreshold >= taxBracketConfigs[i].MaxThreshold)
                {
                    throw new Exception("TaxBracket Not Configured Correctly: MinThreshold can not be greater than MaxThreshold");
                }

                //TaxSlabNumber should be configured such that each bracket have uniue incremental Id
                if (taxBracketConfigs[i].TaxSlabNumber != i+1)
                {
                    throw new Exception("TaxBracket Not Configured Correctly: TaxSlabNumber should be configured correctly");
                }

                //Current bracket should always be more than previous bracket
                if(i!=0 && taxBracketConfigs[i-1].MaxThreshold != taxBracketConfigs[i].MinThreshold-1)
                {
                    throw new Exception("TaxBracket Not Configured Correctly: Next bracket MinThreshold should be greater by 1 than MaxThreshold of previous bracet");
                }
            }

        }
    }
}
