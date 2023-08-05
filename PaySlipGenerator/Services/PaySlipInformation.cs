using PaySlipGenerator.Common;
using PaySlipGenerator.Interfaces;
using PaySlipGenerator.Models;

namespace PaySlipGenerator.Services
{
    public class PaySlipInformation : IPaySlipInformation
    {
        private ITaxCalculator _taxCalculator;
        public PaySlipInformation(ITaxCalculator taxCalculator) 
        {
            _taxCalculator = taxCalculator;
        }
        public PaySlipDetails GetPaySlipDetails(EmployeeDetails employeeDetails)
        {

            var salary = employeeDetails.AnnualSalary;

            decimal totalMonthlySalary = (salary / 12m) * 1.00m;

            decimal grossIncome = totalMonthlySalary.RoundDecimalValue();

            decimal incomeTax = _taxCalculator.CalculateMonthlyTaxFromSalary(salary).RoundDecimalValue();

            decimal super = (grossIncome * Helper.StringPercentageToDecimal(employeeDetails.SuperRate)).RoundDecimalValue();

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
