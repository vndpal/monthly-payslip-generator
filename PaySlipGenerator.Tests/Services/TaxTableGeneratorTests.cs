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
            // Arrange
            var generator = new TaxTableGenerator();

            // Act
            var taxTable = generator.GenerateTaxTable();

            // Assert
            Assert.NotNull(taxTable);
            Assert.NotEmpty(taxTable);
        }

        [Theory]
        [InlineData(0, 14000, 0, 0.105)]
        [InlineData(14001, 48000, 1470, 0.175)]
        [InlineData(48001, 70000, 7420, 0.3)]
        [InlineData(70001, 180000, 14020, 0.33)]
        [InlineData(180001, 100000000, 50320, 0.39)]
        public void GenerateTaxTable_ShouldContainCorrectRangesAndRates(decimal minThreshold, decimal maxThreshold, decimal accumulatedTax, decimal marginalTaxRate)
        {
            // Arrange
            var generator = new TaxTableGenerator();

            // Act
            var taxTable = generator.GenerateTaxTable();

            // Assert
            Assert.Contains(taxTable, table =>
                table.StartRange == minThreshold &&
                table.EndRange >= maxThreshold &&
                table.IncomeTaxBracket.TotalTaxFromPreviousBracket == accumulatedTax &&
                table.IncomeTaxBracket.TaxRate == marginalTaxRate
            );
        }

        [Fact]
        public void GenerateTaxTable_ShouldHaveMonotonicIncreasingRanges()
        {
            // Arrange
            var generator = new TaxTableGenerator();

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
