using RateCalculator.Core.Models;
using System.Collections.Generic;

namespace RateCalculator.Core.Services
{
    public interface IOfferValidator
    {
        bool Validate(int loanAmount, List<Offer> offers);
    }
}
