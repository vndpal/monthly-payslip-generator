using PaySlipGenerator.Models;
using PaySlipGenerator.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PaySlipGenerator.Common.Common;

namespace PaySlipGenerator.Tests.Validations
{
    public class EmployeeDetailsValidatorTests
    {
        private readonly EmployeeDetailsValidator _validator;

        public EmployeeDetailsValidatorTests()
        {
            _validator = new EmployeeDetailsValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void FirstName_ShouldNotBeEmpty(string firstName)
        {
            var employeeDetails = new EmployeeDetails { FirstName = firstName };
            var result = _validator.Validate(employeeDetails);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(EmployeeDetails.FirstName));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void LastName_ShouldNotBeEmpty(string lastName)
        {
            var employeeDetails = new EmployeeDetails { LastName = lastName };
            var result = _validator.Validate(employeeDetails);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(EmployeeDetails.LastName));
        }

        [Theory]
        [InlineData("A")]
        [InlineData("A very long last name that exceeds fifty characters")]
        public void LastName_InvalidLength_ShouldHaveValidationError(string lastName)
        {
            var employeeDetails = new EmployeeDetails { LastName = lastName };
            var result = _validator.Validate(employeeDetails);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(EmployeeDetails.LastName));
        }

        [Theory]
        [InlineData("Doe")]
        [InlineData("John Smith")]
        [InlineData("Mickey-Mouse")]
        public void LastName_ValidName_ShouldNotHaveValidationError(string lastName)
        {
            var employeeDetails = new EmployeeDetails { FirstName = "test", LastName = lastName, SuperRate = "10%", PayPeriod = Month.February, AnnualSalary = 104454 };
            var result = _validator.Validate(employeeDetails);

            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void AnnualSalary_InvalidValue_ShouldHaveValidationError(int salary)
        {
            var employeeDetails = new EmployeeDetails { AnnualSalary = salary };
            var result = _validator.Validate(employeeDetails);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(EmployeeDetails.AnnualSalary));
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(50000)]
        public void AnnualSalary_ValidValue_ShouldNotHaveValidationError(int salary)
        {
            var employeeDetails = new EmployeeDetails { FirstName ="test",LastName="test",SuperRate="10%" ,PayPeriod=Month.February,AnnualSalary = salary };
            var result = _validator.Validate(employeeDetails);

            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("abc")]
        [InlineData("60%")]
        [InlineData("25")]
        public void SuperRate_InvalidValue_ShouldHaveValidationError(string superRate)
        {
            var employeeDetails = new EmployeeDetails { SuperRate = superRate };
            var result = _validator.Validate(employeeDetails);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(EmployeeDetails.SuperRate));
        }

        [Theory]
        [InlineData("10%")]
        [InlineData("50%")]
        [InlineData("25%")]
        [InlineData("0%")]
        public void SuperRate_ValidValue_ShouldNotHaveValidationError(string superRate)
        {            
            var employeeDetails = new EmployeeDetails { FirstName = "test", LastName = "test", SuperRate = superRate, PayPeriod = Month.February, AnnualSalary = 12045 };
            var result = _validator.Validate(employeeDetails);

            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(Month.January)]
        [InlineData(Month.February)]
        [InlineData(Month.December)]
        public void PayPeriod_ValidValue_ShouldNotHaveValidationError(Month payPeriod)
        {
            var employeeDetails = new EmployeeDetails { FirstName = "test", LastName = "test", SuperRate = "10%", PayPeriod = payPeriod, AnnualSalary = 1054545 };
            var result = _validator.Validate(employeeDetails);

            Assert.True(result.IsValid);
        }
    }
}
