using Moq;
using PaySlipGenerator.Interfaces;
using PaySlipGenerator.Models;
using PaySlipGenerator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySlipGenerator.Tests.Services
{
    public class TaxCalculatorTests
    {
        [Theory]
        [InlineData(50000, 375)] // Test case for a salary within a tax bracket
        [InlineData(75000, 739.58)] // Test case for a salary within a different tax bracket
        [InlineData(10000, 75)]    // Test case for a salary below the lowest tax bracket
        [InlineData(200000, 3187.5)] // Test case for a salary above the highest tax bracket
        public void CalculateMonthlyTaxFromSalary_ShouldReturnCorrectTax(decimal salary, decimal expectedMonthlyTax)
        {
            // Arrange
            var taxTableGeneratorMock = new Mock<ITaxTableGenerator>();
            var taxTable = new List<TaxTable>
        {
            new TaxTable { StartRange = 0, EndRange = 50000, MinIncomeRange = 0, TaxRate = 0.09m, TotalTaxFromPreviousBracket = 0  },
            new TaxTable { StartRange = 50001, EndRange = 100000, MinIncomeRange = 50000, TaxRate = 0.175m, TotalTaxFromPreviousBracket = 4500  },
            new TaxTable { StartRange = 100001, EndRange = 200000, MinIncomeRange = 100000, TaxRate = 0.25m, TotalTaxFromPreviousBracket = 13250  },
            new TaxTable { StartRange = 200001, EndRange = decimal.MaxValue, MinIncomeRange = 200000, TaxRate = 0.3m, TotalTaxFromPreviousBracket = 38250  }
        };

            taxTableGeneratorMock.Setup(x => x.GenerateTaxTable()).Returns(taxTable);

            var taxCalculator = new TaxCalculator(taxTableGeneratorMock.Object);

            // Act
            var monthlyTax = taxCalculator.CalculateMonthlyTaxFromSalary(salary);

            // Assert
            Assert.Equal(expectedMonthlyTax, monthlyTax);
        }

        [Fact]
        public void CalculateMonthlyTaxFromSalary_ShouldReturnZeroForNegativeSalary()
        {
            // Arrange
            var taxTableGeneratorMock = new Mock<ITaxTableGenerator>();
            var taxCalculator = new TaxCalculator(taxTableGeneratorMock.Object);

            // Act
            var monthlyTax = taxCalculator.CalculateMonthlyTaxFromSalary(-10000);

            // Assert
            Assert.Equal(0, monthlyTax);
        }
    }
}
