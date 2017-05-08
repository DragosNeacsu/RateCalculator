using RateCalculator.Core.Models;
using CsvHelper.Configuration;

namespace RateCalculator.Core.CsvMapper
{
    public sealed class OfferMap : CsvClassMap<Offer>
    {
        public OfferMap()
        {
            Map(m => m.Name).Name("Lender");
            Map(m => m.Rate).Name("Rate");
            Map(m => m.Available).Name("Available");
        }
    }
}
