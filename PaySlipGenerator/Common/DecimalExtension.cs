namespace PaySlipGenerator.Common
{
    public static class DecimalExtension
    {
        public static decimal RoundDecimalValue(this decimal value)
        {
            decimal roundedValue = Math.Round(value, 2, MidpointRounding.AwayFromZero);
            return roundedValue;
        }
    }
}
