using System.Collections.Generic;
using static PaySlipGenerator.Common.Common;

namespace PaySlipGenerator.Models
{
    /// <summary>
    /// Represents the details of an employee for generating a pay slip.
    /// </summary>
    public class EmployeeDetails
    {
        /// <summary>
        /// Gets or sets the first name of the employee.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the employee.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the annual salary of the employee.
        /// </summary>
        public int AnnualSalary { get; set; }

        /// <summary>
        /// Gets or sets the superannuation rate of the employee as a string.
        /// </summary>
        /// <remarks>
        /// Superannuation rate is represented as a string, e.g., "9.5%".
        /// It will be converted to a numeric value for calculations.
        /// </remarks>
        public string SuperRate { get; set; }

        /// <summary>
        /// Gets or sets the pay period for which the pay slip is generated.
        /// </summary>
        public Month PayPeriod { get; set; }
    }
}
