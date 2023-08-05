using PaySlipGenerator.Common;
using PaySlipGenerator.Interfaces;
using PaySlipGenerator.Models;

namespace PaySlipGenerator.Services
{
    /// <summary>
    /// Service class responsible for generating pay slip details for an employee.
    /// </summary>
    public class PaySlipInformation : IPaySlipInformation
    {
        private ITaxCalculator _taxCalculator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaySlipInformation"/> class.
        /// </summary>
        /// <param name="taxCalculator">The tax calculator used for tax calculations.</param>
        public PaySlipInformation(ITaxCalculator taxCalculator) 
        {
            _taxCalculator = taxCalculator;
        }

        /// <summary>
        /// Generates pay slip details for the provided employee.
        /// </summary>
        /// <param name="employeeDetails">The <see cref="EmployeeDetails"/> object containing employee information.</param>
        /// <returns>A <see cref="PaySlipDetails"/> object representing the employee's pay slip information.</returns>
        public PaySlipDetails GetPaySlipDetails(EmployeeDetails employeeDetails)
        {
            // Calculate Gross Income and round it to the nearest decimal value.
            var salary = employeeDetails.AnnualSalary;
            decimal totalMonthlySalary = (salary / 12m) * 1.00m;
            decimal grossIncome = totalMonthlySalary.RoundDecimalValue();

            // calculate income tax from TaxCalulator class and round it to the nearest decimal value.
            decimal incomeTax = _taxCalculator.CalculateMonthlyTaxFromSalary(salary).RoundDecimalValue();

            // Calculate super contribution and round it to the nearest decimal value.
            decimal super = (grossIncome * Helper.StringPercentageToDecimal(employeeDetails.SuperRate)).RoundDecimalValue();

            // Create the pay slip details object with the calculated values.
            var result = new PaySlipDetails
            {
                GrossIncome = grossIncome,
                IncomeTax = incomeTax,
                Name = employeeDetails.FirstName.Trim() + " " + employeeDetails.LastName.Trim(),
                NetIncome = grossIncome - incomeTax,
                PayPeriod = Helper.GetFirstAndLastDateOfMonth(employeeDetails.PayPeriod),
                Super = super
            };

            return result;
        }
    }
}
