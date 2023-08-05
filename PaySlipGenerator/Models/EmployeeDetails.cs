using System.Collections.Generic;
using static PaySlipGenerator.Common.Common;

namespace PaySlipGenerator.Models
{
    public class EmployeeDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AnnualSalary { get; set; }
        public string SuperRate { get; set; }
        public Month PayPeriod { get; set; }
    }
}
