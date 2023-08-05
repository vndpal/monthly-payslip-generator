namespace PaySlipGenerator.Interfaces
{
    /// <summary>
    /// Interface for calculating monthly tax based on an employee's salary.
    /// </summary>
    public interface ITaxCalculator
    {
        /// <summary>
        /// Calculates the monthly tax amount from the given employee salary
        /// </summary>
        /// <param name="salary"></param>
        /// <returns>The calculated tax amount as a decimal value.</returns>
        decimal CalculateMonthlyTaxFromSalary(decimal salary);
    }
}
