using PaySlipGenerator.Models;

namespace PaySlipGenerator.Interfaces
{
    /// <summary>
    /// Interface for generating the tax table.
    /// The tax table contains income brackets and corresponding tax rates used for tax calculations.
    /// </summary>
    public interface ITaxTableGenerator
    {
        /// <summary>
        /// Generates the tax table containing income brackets and tax rates.
        /// </summary>
        /// <returns>A list of <see cref="TaxTable"/> objects representing the tax table.</returns>
        List<TaxTable> GenerateTaxTable();
    }
}