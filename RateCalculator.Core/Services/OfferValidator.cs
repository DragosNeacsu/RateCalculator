using RateCalculator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RateCalculator.Core.Services
{
    public class OfferValidator : IOfferValidator
    {
        public bool Validate(int requestedAmount, List<Offer> offers)
        {
            if (requestedAmount > offers.Sum(x => x.Available))
            {
                throw new Exception("It is not possible to provide a quote now");
            }
            return true;
        }
    }
}
