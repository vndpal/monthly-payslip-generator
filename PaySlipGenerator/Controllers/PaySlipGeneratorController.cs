using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaySlipGenerator.Interfaces;
using PaySlipGenerator.Models;
using PaySlipGenerator.Services;
using System.Reflection;

namespace PaySlipGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaySlipGeneratorController : ControllerBase
    {
        private IPaySlipInformation _paySlipInformation;

        // Constructor to inject the IPaySlipInformation dependency
        public PaySlipGeneratorController(IPaySlipInformation paySlipInformation)
        {
            _paySlipInformation = paySlipInformation;
        }

        // HTTP POST action to generate pay slips for employees
        [HttpPost(Name = "GeneratePaySlip")]
        public IActionResult GeneratePaySlips([FromBody] EmployeeDetails employeeDetails)
        {
            //Validation login based on Fluent validation set in EmployeeDetailsValidator class
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            try
            {
                // Call the service to get the pay slip details for the provided employee details
                var result = _paySlipInformation.GetPaySlipDetails(employeeDetails);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // TODO: Log the error in AppInsights or a file or DB

                // An error occurred during processing, log the error and return 500 Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
            
        }
    }
}
