namespace PaySlipGenerator.Models
{
    public class TaxBracketConfig
    {
        public int TaxSlabNumber { get; set; }
        public decimal MinThreshold { get; set; }
        public decimal MaxThreshold { get; set; }
        public decimal MarginalTaxRate { get; set; }
    }
}
