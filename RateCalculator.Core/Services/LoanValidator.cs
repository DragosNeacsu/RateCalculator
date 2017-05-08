using System;

namespace RateCalculator.Core.Services
{
    public class LoanValidator : ILoanValidator
    {
        private const int loanIncrement = 100;
        private const int amountMinValue = 1000;
        private const int amountMaxValue = 15000;

        public bool Validate(string loanAmount)
        {
            int amount;

            if (!int.TryParse(loanAmount, out amount))
            {
                throw new ArgumentException("Loan amount should be integer");
            }

            if (amount % loanIncrement != 0)
            {
                throw new ArgumentException($"Loan amount must be multiple of {loanIncrement}");
            }

            if (amount < amountMinValue || amount > amountMaxValue)
            {
                throw new ArgumentException($"Loan amount must be between {amountMinValue} and {amountMaxValue}");
            }

            return true;
        }
    }
}
