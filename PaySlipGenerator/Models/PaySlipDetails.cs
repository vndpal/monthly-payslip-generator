using static PaySlipGenerator.Common.Common;

namespace PaySlipGenerator.Models
{
    /// <summary>
    /// Represents the details of a pay slip for an employee.
    /// </summary>
    public class PaySlipDetails
    {
        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the pay period for which the pay slip is generated.
        /// </summary>
        public string PayPeriod { get; set; }

        /// <summary>
        /// Gets or sets the gross income of the employee.
        /// </summary>
        public decimal GrossIncome { get; set; }

        /// <summary>
        /// Gets or sets the income tax amount deducted from the employee's salary.
        /// </summary>
        public decimal IncomeTax { get; set; }

        /// <summary>
        /// Gets or sets the net income of the employee after tax deductions.
        /// </summary>
        public decimal NetIncome { get; set; }

        /// <summary>
        /// Gets or sets the super contribution for the employee.
        /// </summary>
        public decimal Super { get; set; }
    }
}
