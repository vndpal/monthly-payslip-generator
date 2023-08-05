namespace PaySlipGenerator.Common
{
    /// <summary>
    /// Class for Decimal extension menthod
    /// </summary>
    public static class DecimalExtension
    {
        /// <summary>
        /// This extension method will round the decimal value to 2 digits
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Ronded value updo 2 decimal points</returns>
        public static decimal RoundDecimalValue(this decimal value)
        {
            //MidpointRounding.AwayFromZero will round value towards higher value if value is at midpoint e.g. 10.165 will be rounded to 10.17 not 10.16
            decimal roundedValue = Math.Round(value, 2, MidpointRounding.AwayFromZero);
            return roundedValue;
        }
    }
}
