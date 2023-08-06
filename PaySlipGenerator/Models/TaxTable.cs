namespace PaySlipGenerator.Models
{
    /// <summary>
    /// Represents a tax table entry containing income range and corresponding tax bracket details.
    /// </summary>
    public class TaxTable
    {
        /// <summary>
        /// Gets or sets the start range of income for this tax table entry.
        /// </summary>
        public decimal StartRange { get; set; }

        /// <summary>
        /// Gets or sets the end range of income for this tax table entry.
        /// </summary>
        public decimal EndRange { get; set; }

        /// <summary>
        /// Gets or sets the total tax amount calculated from the previous tax bracket.
        /// </summary>
        public decimal TotalTaxFromPreviousBracket { get; set; }

        /// <summary>
        /// Gets or sets the tax rate for this tax bracket.
        /// </summary>
        public decimal TaxRate { get; set; }

        /// <summary>
        /// Gets or sets the minimum income range for this tax bracket.
        /// </summary>
        public decimal MinIncomeRange { get; set; }

        /// <summary>
        /// Gets or sets the Tax Bracket Id.
        /// </summary>
        public decimal TaxBracketId { get; set; }
    }
}
