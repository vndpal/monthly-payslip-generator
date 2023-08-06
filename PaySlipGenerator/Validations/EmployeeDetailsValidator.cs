using FluentValidation;
using PaySlipGenerator.Models;
using static PaySlipGenerator.Common.Common;

namespace PaySlipGenerator.Validations
{
    /// <summary>
    /// Validator class to validate the properties of the <see cref="EmployeeDetails"/> model.
    /// </summary>
    public class EmployeeDetailsValidator : AbstractValidator<EmployeeDetails>
    {
        public EmployeeDetailsValidator()
        {
            RuleFor(p => p.FirstName)
            .NotEmpty().WithMessage("{PropertyName} should not be emtpy")
            .Length(2, 50).WithMessage("Length ({TotalLength}) of {PropertyName} Invalid");

            RuleFor(p => p.FirstName)
            .Must(BeValidName).When(p => !string.IsNullOrEmpty(p.FirstName)).WithMessage("{PropertyName} contains Invalid characters");

            RuleFor(p => p.LastName)
            .NotNull().NotEmpty().WithMessage("{PropertyName} should not be emtpy")
            .Length(2, 50).WithMessage("Length ({TotalLength}) of {PropertyName} Invalid");

            RuleFor(p => p.LastName)
            .Must(BeValidName).When(p => !string.IsNullOrEmpty(p.LastName)).WithMessage("{PropertyName} contains Invalid characters");

            RuleFor(employee => employee.AnnualSalary)
            .NotNull().NotEmpty().WithMessage("{PropertyName} is required.")
            .LessThanOrEqualTo(999999999).WithMessage("Maximum {PropertyName} supported is 999999999.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
            .Must(BeAnInteger).WithMessage("{PropertyName} must be an integer.");

            RuleFor(employee => employee.SuperRate)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(employee => employee.SuperRate)
            .Must(BeValidPercentage).When(p => !string.IsNullOrEmpty(p.SuperRate)).WithMessage("Invalid {PropertyName} value. {PropertyName} should be between 0% and 50%.");

            RuleFor(employee => employee.PayPeriod)
            .NotNull().NotEmpty().WithMessage("Pay Period is required.")
            .Must(BeValidPayPeriod).WithMessage("Invalid Pay Period. Please enter Pay period from 1 to 12 indicating Month number. 1 for January, 2 for Febuary and so on");
        }

        // Custom validation method to check if the name contains only valid characters
        protected bool BeValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            name = name.Replace(".", "");
            return name.All(Char.IsLetter);
        }

        // Custom validation method to check if the value is an integer
        protected bool BeAnInteger(int salary)
        {
            return salary % 1 == 0;
        }

        // Custom validation method to check if the value is a valid percentage
        protected bool BeValidPercentage(string percentage)
        {
            if (string.IsNullOrWhiteSpace(percentage))
                return false;

            if (percentage.EndsWith("%"))
            {
                var numericValue = percentage.TrimEnd('%');
                if (decimal.TryParse(numericValue, out decimal percentageValue))
                {
                    return percentageValue >= 0 && percentageValue <= 50;
                }
            }

            return false;
        }

        // Custom validation method to check if the value is a valid Pay Period
        private bool BeValidPayPeriod(Month payPeriod)
        {
            // Use Enum.IsDefined to check if the value is one of the defined enum values
            return Enum.IsDefined(typeof(Month), payPeriod);
        }
    }
}
