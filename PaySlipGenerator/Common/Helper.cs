using System.Text.RegularExpressions;
using static PaySlipGenerator.Common.Common;

namespace PaySlipGenerator.Common
{
    public class Helper
    {
        public static decimal StringPercentageToDecimal(string input)
        {
            const string pattern = @"^(\d*.+\d*)%$";
            var result = Regex.Match(input, pattern);
            return Convert.ToDecimal(result.Groups[1].Value) * 0.01m;
        }

        public static string GetFirstAndLastDateOfMonth(Month month)
        {
            // Get the current date
            DateTime currentDate = DateTime.Now;

            int monthNumber = (int)month;

            int currentMonth = DateTime.Now.Month;

            // If the selected month is greater than or equal to the current month,
            // it means it is in the future, and we need to go back one year.
            int yearOffset = monthNumber >= currentMonth ? 1 : 0;

            DateTime firstDateOfMonth = new DateTime(DateTime.Now.Year - yearOffset, monthNumber, 1);
            DateTime lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);

            // Format the dates as strings in the desired format
            string firstDateStr = firstDateOfMonth.ToString("dd MMMM");
            string lastDateStr = lastDateOfMonth.ToString("dd MMMM");

            return $"{firstDateStr} - {lastDateStr}";
        }
    }
}
