namespace RateCalculator.Core.Services
{
    public interface ILoanValidator
    {
        bool Validate(string loanAmount);
    }
}
