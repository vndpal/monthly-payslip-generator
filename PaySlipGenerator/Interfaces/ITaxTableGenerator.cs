using PaySlipGenerator.Models;

namespace PaySlipGenerator.Interfaces
{
    public interface ITaxTableGenerator
    {
        List<TaxTable> GenerateTaxTable();
    }
}