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
        /// Gets or sets the tax bracket details for this income range.
        /// </summary>
        public TaxBracket IncomeTaxBracket { get; set; }
    }
}
