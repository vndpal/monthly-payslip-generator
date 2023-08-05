using PaySlipGenerator.Models;

namespace PaySlipGenerator.Interfaces
{
    /// <summary>
    /// Interface for retrieving pay slip details for an employee.
    /// </summary>
    public interface IPaySlipInformation
    {
        /// <summary>
        /// Retrieves pay slip details for the provided employee details
        /// </summary>
        /// <param name="employeeDetails"></param>
        /// <returns></returns>
        PaySlipDetails GetPaySlipDetails(EmployeeDetails employeeDetails);
    }
}
