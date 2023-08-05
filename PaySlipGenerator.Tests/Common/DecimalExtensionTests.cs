using PaySlipGenerator.Common;

namespace PaySlipGenerator.Tests.Common
{
    public class DecimalExtensionTests
    {
        [Theory]
        [InlineData(0, 0)]         
        [InlineData(1, 1)]         
        [InlineData(-1, -1)]       
        [InlineData(10, 10.000)]   
        [InlineData(-10, -10.000)] 
        [InlineData(1.23, 1.234)]  
        [InlineData(-1.23, -1.234)]
        [InlineData(1.24, 1.235)]  
        [InlineData(-1.24, -1.235)]
        [InlineData(1.26, 1.255)]  
        [InlineData(1.26, 1.258)]  
        [InlineData(-1.26, -1.255)]
        public void RoundDecimalValue_ShouldRoundCorrectly(decimal expected, decimal value)
        {
            // Act
            var result = value.RoundDecimalValue();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
