namespace PaySlipGenerator.Models
{
    /// <summary>
    /// Represents a tax bracket used for tax calculations.
    /// </summary>
    public class TaxBracket
    {
        /// <summary>
        /// Gets or sets the total tax amount calculated from the previous tax bracket.
        /// </summary>
        public decimal TotalTaxFromPreviousBracket { get; set;}

        /// <summary>
        /// Gets or sets the tax rate for this tax bracket.
        /// </summary>
        public decimal TaxRate { get; set; }

        /// <summary>
        /// Gets or sets the minimum income range for this tax bracket.
        /// </summary>
        public decimal MinIncomeRange { get; set; }
    }
}
