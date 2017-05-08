using RateCalculator.Core.Models;

namespace RateCalculator.Core.Services
{
    public interface IQuoteCalculator
    {
        QuoteResult GetQuote(string filePath, string amount);
    }
}
