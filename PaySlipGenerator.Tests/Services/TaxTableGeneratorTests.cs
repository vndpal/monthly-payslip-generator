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
                new KeyValuePair<string, string>("TaxBrackets:0:AccumulatedTaxFromPreviousBracket", "0"),
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

        [Theory]
        [InlineData(0, 14000, 0, 0.105)]
        [InlineData(14001, 48000, 1470, 0.175)]
        [InlineData(48001, 70000, 7420, 0.3)]
        [InlineData(70001, 180000, 14020, 0.33)]
        [InlineData(180001, 100000000, 50320, 0.39)]
        public void GenerateTaxTable_ShouldContainCorrectRangesAndRates(decimal minThreshold, decimal maxThreshold, decimal accumulatedTax, decimal marginalTaxRate)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new[]
                {
                new KeyValuePair<string, string>("TaxBrackets:0:MinThreshold", minThreshold.ToString()),
                new KeyValuePair<string, string>("TaxBrackets:0:MaxThreshold", maxThreshold.ToString()),
                new KeyValuePair<string, string>("TaxBrackets:0:AccumulatedTaxFromPreviousBracket", accumulatedTax.ToString()),
                new KeyValuePair<string, string>("TaxBrackets:0:MarginalTaxRate", marginalTaxRate.ToString()),
                })
                .Build();
            
            // Arrange
            var generator = new TaxTableGenerator(configuration);

            // Act
            var taxTable = generator.GenerateTaxTable();

            // Assert
            Assert.Contains(taxTable, table =>
                table.StartRange == minThreshold &&
                table.EndRange >= maxThreshold &&
                table.TotalTaxFromPreviousBracket == accumulatedTax &&
                table.TaxRate == marginalTaxRate
            );
        }

        [Theory]
        [InlineData(0, 14000, 0, 0.105)]
        [InlineData(14001, 48000, 1470, 0.175)]
        [InlineData(48001, 70000, 7420, 0.3)]
        [InlineData(70001, 180000, 14020, 0.33)]
        [InlineData(180001, 100000000, 50320, 0.39)]
        public void GenerateTaxTable_ShouldHaveMonotonicIncreasingRanges(decimal minThreshold, decimal maxThreshold, decimal accumulatedTax, decimal marginalTaxRate)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new[]
                {
                new KeyValuePair<string, string>("TaxBrackets:0:MinThreshold", minThreshold.ToString()),
                new KeyValuePair<string, string>("TaxBrackets:0:MaxThreshold", maxThreshold.ToString()),
                new KeyValuePair<string, string>("TaxBrackets:0:AccumulatedTaxFromPreviousBracket", accumulatedTax.ToString()),
                new KeyValuePair<string, string>("TaxBrackets:0:MarginalTaxRate", marginalTaxRate.ToString()),
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
