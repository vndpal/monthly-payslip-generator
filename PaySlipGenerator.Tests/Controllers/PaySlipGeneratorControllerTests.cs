using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PaySlipGenerator.Controllers;
using PaySlipGenerator.Interfaces;
using PaySlipGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PaySlipGenerator.Common.Common;

namespace PaySlipGenerator.Tests.Controllers
{
    public class PaySlipGeneratorControllerTests
    {
        [Fact]
        public void GeneratePaySlips_ValidEmployeeDetails_ReturnsOkResult()
        {
            // Arrange
            var employeeDetails = getTestEmployeeDetails();
            var mockPaySlipInformation = new Mock<IPaySlipInformation>();
            var controller = new PaySlipGeneratorController(mockPaySlipInformation.Object);

            // Act
            var result = controller.GeneratePaySlips(employeeDetails) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void GeneratePaySlips_InvalidEmployeeDetails_ReturnsBadRequest()
        {
            // Arrange
            var employeeDetails = getTestEmployeeDetails();
            employeeDetails.SuperRate = "89%"; // Adding an invalid super rate
            var mockPaySlipInformation = new Mock<IPaySlipInformation>();
            var controller = new PaySlipGeneratorController(mockPaySlipInformation.Object);
            controller.ModelState.AddModelError("ErrorKey", "Error Message");

            // Act
            var result = controller.GeneratePaySlips(employeeDetails) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public void GeneratePaySlips_PaySlipInformationThrowsException_ReturnsServerError()
        {
            // Arrange
            var employeeDetails = getTestEmployeeDetails();
            var mockPaySlipInformation = new Mock<IPaySlipInformation>();
            mockPaySlipInformation.Setup(x => x.GetPaySlipDetails(It.IsAny<EmployeeDetails>())).Throws(new Exception("Some error occurred."));
            var controller = new PaySlipGeneratorController(mockPaySlipInformation.Object);

            // Act
            var result = controller.GeneratePaySlips(employeeDetails) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        /// <summary>
        /// Get mocked employee details for testing
        /// </summary>
        /// <returns>EmployeeDetails object</returns>
        private EmployeeDetails getTestEmployeeDetails()
        {
            var employeeDetails = new EmployeeDetails
            {
                AnnualSalary = 60005,
                FirstName = "Test",
                LastName = "User",
                PayPeriod = Month.July,
                SuperRate = "47%"
            };

            return employeeDetails;
        }

    }
}
