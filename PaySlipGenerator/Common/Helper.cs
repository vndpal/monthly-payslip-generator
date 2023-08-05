using PaySlipGenerator.Models;
using System.Text.RegularExpressions;
using static PaySlipGenerator.Common.Common;

namespace PaySlipGenerator.Common
{
    /// <summary>
    /// This is an helper class containing helper methods throughout applicaiton
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// This method converts percentage value to a valid decimal value
        /// e.g. input 12% output 0.12
        /// </summary>
        /// <param name="input"></param>
        /// <returns>A dceimal value from valid percentage string</returns>
        public static decimal StringPercentageToDecimal(string input)
        {
            const string pattern = @"^(\d*.+\d*)%$";
            var result = Regex.Match(input, pattern);
            return Convert.ToDecimal(result.Groups[1].Value) * 0.01m;
        }

        /// <summary>
        /// This method will return first and last date of the month based on Month passed
        /// e.g. March will return 01 March - 31 March
        /// This method considers the past one year for calculation
        /// </summary>
        /// <param name="month"></param>
        /// <returns>A string with first and last date of month e.g. 01 March - 31 March</returns>
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

        /// <summary>
        /// Combines the first name and last name to create the full name of an employee.
        /// </summary>
        /// <param name="firstName">The first name of the employee.</param>
        /// <param name="lastName">The last name of the employee.</param>
        /// <returns>The full name of the employee.</returns>
        public static string GetFullNameOfEmployee(string firstName, string lastName)
        {
            // Trim the first name and last name to remove leading and trailing spaces.
            string trimmedFirstName = firstName.Trim();
            string trimmedLastName = lastName.Trim();

            // Combine the trimmed first name and last name to create the full name.
            string fullName = $"{trimmedFirstName} {trimmedLastName}";

            return fullName;
        }
    }
}
