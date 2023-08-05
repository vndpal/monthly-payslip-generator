using PaySlipGenerator.Models;

namespace PaySlipGenerator.Interfaces
{
    public interface IPaySlipInformation
    {
        PaySlipDetails GetPaySlipDetails(EmployeeDetails employeeDetails);
    }
}
