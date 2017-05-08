using RateCalculator.Core.Models;
using System.Collections.Generic;

namespace RateCalculator.Core.Services
{
    public interface IOffersReader
    {
        List<Offer> ReadOffers(string filePath);
    }
}
