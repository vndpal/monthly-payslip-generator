namespace PaySlipGenerator.Interfaces
{
    public interface ITaxCalculator
    {
        decimal CalculateMonthlyTaxFromSalary(decimal salary);
    }
}
