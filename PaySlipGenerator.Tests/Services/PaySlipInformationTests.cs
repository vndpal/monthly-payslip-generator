using Moq;
using PaySlipGenerator.Interfaces;
using PaySlipGenerator.Models;
using PaySlipGenerator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PaySlipGenerator.Common.Common;

namespace PaySlipGenerator.Tests.Services
{
    public class PaySlipInformationTests
    {
        [Fact]
        public void GetPaySlipDetails_ReturnsValidPaySlipDetails()
        {
            // Arrange
            var taxCalculatorMock = new Mock<ITaxCalculator>();
            taxCalculatorMock.Setup(x => x.CalculateMonthlyTaxFromSalary(It.IsAny<decimal>()))
                            .Returns(1000.00m); // Set a fixed tax amount for simplicity

            var paySlipInformation = new PaySlipInformation(taxCalculatorMock.Object);
            var employeeDetails = new EmployeeDetails
            {
                FirstName = "John",
                LastName = "Doe",
                AnnualSalary = 60000,
                SuperRate = "9%",
                PayPeriod = Month.March
            };

            // Act
            var result = paySlipInformation.GetPaySlipDetails(employeeDetails);

            // Assert
            Assert.Equal("John Doe", result.Name);
            Assert.Equal(5000.00m, result.GrossIncome);
            Assert.Equal(1000.00m, result.IncomeTax);
            Assert.Equal(4000.00m, result.NetIncome);
            Assert.Equal("01 March - 31 March", result.PayPeriod);
            Assert.Equal(450.00m, result.Super);
        }

        [Fact]
        public void GetPaySlipDetails_ReturnsZeroTax_WhenTaxCalculatorReturnsZero()
        {
            // Arrange
            var taxCalculatorMock = new Mock<ITaxCalculator>();
            taxCalculatorMock.Setup(x => x.CalculateMonthlyTaxFromSalary(It.IsAny<decimal>()))
                            .Returns(8.75m);

            var paySlipInformation = new PaySlipInformation(taxCalculatorMock.Object);
            var employeeDetails = new EmployeeDetails
            {
                FirstName = "Jane",
                LastName = "Smith",
                AnnualSalary = 1000,
                SuperRate = "10%",
                PayPeriod = Month.September
            };

            // Act
            var result = paySlipInformation.GetPaySlipDetails(employeeDetails);

            // Assert
            Assert.Equal(83.33m, result.GrossIncome);
            Assert.Equal(8.75m, result.IncomeTax);
            Assert.Equal(74.58m, result.NetIncome);
            Assert.Equal("01 September - 30 September", result.PayPeriod);
            Assert.Equal(8.33m, result.Super);
        }

        [Fact]
        public void GetPaySlipDetails_ReturnsZeroNetIncome_WhenGrossIncomeEqualsTax()
        {
            // Arrange
            var taxCalculatorMock = new Mock<ITaxCalculator>();
            taxCalculatorMock.Setup(x => x.CalculateMonthlyTaxFromSalary(It.IsAny<decimal>()))
                            .Returns(500);

            var paySlipInformation = new PaySlipInformation(taxCalculatorMock.Object);
            var employeeDetails = new EmployeeDetails
            {
                FirstName = "Bob",
                LastName = "Johnson",
                AnnualSalary = 6000,
                SuperRate = "3%",
                PayPeriod = Month.December
            };

            // Act
            var result = paySlipInformation.GetPaySlipDetails(employeeDetails);

            // Assert
            Assert.Equal(500, result.GrossIncome);
            Assert.Equal(500, result.IncomeTax);
            Assert.Equal(0, result.NetIncome);
            Assert.Equal("01 December - 31 December", result.PayPeriod);
            Assert.Equal(15, result.Super);
        }
    }
}
