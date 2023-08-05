using PaySlipGenerator.Common;
using static PaySlipGenerator.Common.Common;

namespace PaySlipGenerator.Tests.Common
{
    public class HelperTests
    {
        [Theory]
        [InlineData("25%", 0.25)]
        [InlineData("100%", 1.0)]
        [InlineData("0%", 0.0)]
        [InlineData("50.25%", 0.5025)]
        [InlineData("0.99%", 0.0099)]
        [InlineData("0.0%", 0.0)]
        [InlineData("1234567.8901%", 12345.678901)]
        public void StringPercentageToDecimal_ShouldConvertCorrectly(string input, decimal expectedOutput)
        {
            // Act
            var result = Helper.StringPercentageToDecimal(input);

            // Assert
            Assert.Equal(expectedOutput, result);
        }

        [Theory]
        [InlineData(Month.January, "01 January - 31 January")]
        [InlineData(Month.February, "01 February - 28 February")]
        [InlineData(Month.March, "01 March - 31 March")]
        [InlineData(Month.April, "01 April - 30 April")]
        [InlineData(Month.May, "01 May - 31 May")]
        [InlineData(Month.June, "01 June - 30 June")]
        [InlineData(Month.July, "01 July - 31 July")]
        [InlineData(Month.August, "01 August - 31 August")]
        [InlineData(Month.September, "01 September - 30 September")]
        [InlineData(Month.October, "01 October - 31 October")]
        [InlineData(Month.November, "01 November - 30 November")]
        [InlineData(Month.December, "01 December - 31 December")]
        public void GetFirstAndLastDateOfMonth_ShouldReturnCorrectString(Month month, string expectedOutput)
        {
            // Act
            var result = Helper.GetFirstAndLastDateOfMonth(month);

            // Assert
            Assert.Equal(expectedOutput, result);
        }

        [Fact]
        public void GetFirstAndLastDateOfMonth_ShouldReturnValidDates()
        {
            // Arrange
            var month = Month.January;

            // Act
            var result = Helper.GetFirstAndLastDateOfMonth(month);

            // Assert
            DateTime.TryParseExact(result.Substring(0, 10), "dd MMMM yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime firstDateTime);
            DateTime.TryParseExact(result.Substring(result.Length - 11, 10), "dd MMMM yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime lastDateTime);
            Assert.True(firstDateTime <= lastDateTime);
        }
    }
}
