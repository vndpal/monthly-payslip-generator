namespace PaySlipGenerator.Models
{
    public class TaxBracketConfig
    {
        public decimal MinThreshold { get; set; }
        public decimal MaxThreshold { get; set; }
        public decimal AccumulatedTaxFromPreviousBracket { get; set; }
        public decimal MarginalTaxRate { get; set; }
    }
}
