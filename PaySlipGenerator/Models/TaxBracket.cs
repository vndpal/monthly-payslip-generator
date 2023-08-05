namespace PaySlipGenerator.Models
{
    public class TaxBracket
    {
        public decimal TotalTaxFromPreviousBracket { get; set;}
        public decimal TaxRate { get; set; }
        public decimal MinIncomeRange { get; set; }
    }
}
