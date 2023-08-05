namespace PaySlipGenerator.Models
{
    public class TaxTable
    {
        public decimal StartRange { get; set; }
        public decimal EndRange { get; set; }
        public TaxBracket IncomeTaxBracket { get; set; }
    }
}
