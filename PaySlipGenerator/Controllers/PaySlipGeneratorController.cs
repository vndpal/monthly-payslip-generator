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
        public PaySlipGeneratorController(IPaySlipInformation paySlipInformation)
        {
            _paySlipInformation = paySlipInformation;
        }

        [HttpPost(Name = "GeneratePaySlip")]
        public IActionResult GeneratePaySlips([FromBody] EmployeeDetails employeeDetails)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            try
            {
                var result = _paySlipInformation.GetPaySlipDetails(employeeDetails);

                return Ok(result);
            }
            catch (Exception ex)
            {
                //ToDo: Log the error in AppInsights or a file or DB - Console.log(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
            
        }
    }
}
