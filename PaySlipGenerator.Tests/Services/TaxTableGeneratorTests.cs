using Microsoft.Extensions.Configuration;
using Moq;
using PaySlipGenerator.Models;
using PaySlipGenerator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySlipGenerator.Tests.Services
{
    public class TaxTableGeneratorTests
    {
        [Fact]
        public void GenerateTaxTable_ShouldReturnNonEmptyList()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new[]
                {
                new KeyValuePair<string, string>("TaxBrackets:0:MinThreshold", "0"),
                new KeyValuePair<string, string>("TaxBrackets:0:MaxThreshold", "14000"),
                new KeyValuePair<string, string>("TaxBrackets:0:TaxSlabNumber", "1"),
                new KeyValuePair<string, string>("TaxBrackets:0:MarginalTaxRate", "0.105"),
                    // Add other tax brackets as needed...
                })
                .Build();

            // Arrange
            var generator = new TaxTableGenerator(configuration);

            // Act
            var taxTable = generator.GenerateTaxTable();
            var expectedCount = 1;

            // Assert
            Assert.NotNull(taxTable);
            Assert.Equal(expectedCount,taxTable.Count());
        }

        [Fact]
        public void GenerateTaxTable_ShouldContainCorrectRangesAndRates()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new[]
                {
                new KeyValuePair<string, string>("TaxBrackets:0:MinThreshold", "0"),
                new KeyValuePair<string, string>("TaxBrackets:0:MaxThreshold", "14000"),
                new KeyValuePair<string, string>("TaxBrackets:0:TaxSlabNumber", "1"),
                new KeyValuePair<string, string>("TaxBrackets:0:MarginalTaxRate", "0.105"),
                new KeyValuePair<string, string>("TaxBrackets:1:MinThreshold", "14001"),
                new KeyValuePair<string, string>("TaxBrackets:1:MaxThreshold", "40000"),
                new KeyValuePair<string, string>("TaxBrackets:1:TaxSlabNumber", "2"),
                new KeyValuePair<string, string>("TaxBrackets:1:MarginalTaxRate", "0.15"),
                })
                .Build();
            
            // Arrange
            var generator = new TaxTableGenerator(configuration);

            // Act
            var taxTable = generator.GenerateTaxTable();

            // Assert
            Assert.Contains(taxTable, table =>
                table.StartRange == 0 &&
                table.EndRange >= 14000 &&
                table.TaxRate == 0.105m &&
                table.TotalTaxFromPreviousBracket == 0
            ); ;
            Assert.Contains(taxTable, table =>
                table.StartRange == 14001 &&
                table.EndRange >= 40000 &&
                table.TaxRate == 0.15m &&
                table.TotalTaxFromPreviousBracket == 1470

            );
        }

       [Fact]
        public void GenerateTaxTable_ShouldHaveMonotonicIncreasingRanges()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new[]
                {
                new KeyValuePair<string, string>("TaxBrackets:0:MinThreshold", "0"),
                new KeyValuePair<string, string>("TaxBrackets:0:MaxThreshold", "14000"),
                new KeyValuePair<string, string>("TaxBrackets:0:TaxSlabNumber", "1"),
                new KeyValuePair<string, string>("TaxBrackets:0:MarginalTaxRate", "0.105"),
                new KeyValuePair<string, string>("TaxBrackets:1:MinThreshold", "14001"),
                new KeyValuePair<string, string>("TaxBrackets:1:MaxThreshold", "40000"),
                new KeyValuePair<string, string>("TaxBrackets:1:TaxSlabNumber", "2"),
                new KeyValuePair<string, string>("TaxBrackets:1:MarginalTaxRate", "0.15"),
                })
                .Build();
            // Arrange
            var generator = new TaxTableGenerator(configuration);

            // Act
            var taxTable = generator.GenerateTaxTable();

            // Assert
            var ranges = taxTable.Select(table => table.StartRange).ToList();
            for (int i = 1; i < ranges.Count; i++)
            {
                Assert.True(ranges[i] > ranges[i - 1]);
            }
        }
    }
}
