using PaySlipGenerator.Common;

namespace PaySlipGenerator.Tests.Common
{
    public class DecimalExtensionTests
    {
        [Theory]
        [InlineData(0, 0)]            // Minimum positive value
        [InlineData(1, 1)]            // Value with no decimal places
        [InlineData(-1, -1)]          // Negative value with no decimal places
        [InlineData(10, 10.000)]      // Value with three decimal places
        [InlineData(-10, -10.000)]    // Negative value with three decimal places
        [InlineData(1.23, 1.234)]     // Value with more than two decimal places
        [InlineData(-1.23, -1.234)]   // Negative value with more than two decimal places
        [InlineData(1.24, 1.235)]     // Value with three decimal places and midpoint rounding to 2
        [InlineData(-1.24, -1.235)]   // Negative value with three decimal places and midpoint rounding to 2
        [InlineData(1.26, 1.255)]     // Value with three decimal places and midpoint rounding to 2
        [InlineData(1.26, 1.258)]     // Value with three decimal places and value higher than midpoint
        [InlineData(-1.26, -1.255)]   // Negative value with three decimal places and midpoint rounding to 2
        public void RoundDecimalValue_ShouldRoundCorrectly(decimal expected, decimal value)
        {
            // Arrange

            // Act
            var result = value.RoundDecimalValue();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
