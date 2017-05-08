using RateCalculator.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace RateCalculator.Core.Services
{
    public class QuoteCalculator : IQuoteCalculator
    {
        public const int loanLength = 36;
        private readonly IOffersReader _offersReader;
        private readonly ILoanValidator _loanValidator;
        private readonly IOfferValidator _offerValidator;

        public QuoteCalculator(IOffersReader offersReader, ILoanValidator loanValidator, IOfferValidator offerValidator)
        {
            _offersReader = offersReader;
            _loanValidator = loanValidator;
            _offerValidator = offerValidator;
        }

        public QuoteResult GetQuote(string filePath, string loanAmount)
        {
            _loanValidator.Validate(loanAmount);
            var requestedAmount = int.Parse(loanAmount);
            var offers = _offersReader.ReadOffers(filePath);
            _offerValidator.Validate(requestedAmount, offers);

            var totalRepayment = GetTotalRepayment(offers, requestedAmount);
            var rate = (totalRepayment - requestedAmount) / requestedAmount;
            var monthlyRepayment = requestedAmount * (1 + rate) / loanLength;

            return new QuoteResult
            {
                RequestedAmount = requestedAmount,
                Rate = rate,
                MonthlyRepayment = monthlyRepayment,
                TotalRepayment = totalRepayment
            };
        }

        private static decimal GetTotalRepayment(IEnumerable<Offer> offers, int requestedAmount)
        {
            decimal totalRepayment = 0;
            var borrowed = 0;

            foreach (var offer in offers.OrderBy(x => x.Rate))
            {
                var amountToBorrow = 0;

                if (borrowed >= requestedAmount)
                {
                    break;
                }

                if (requestedAmount < borrowed + offer.Available)
                {
                    amountToBorrow = requestedAmount - borrowed;
                }
                else
                {
                    amountToBorrow = offer.Available;
                }
                borrowed += amountToBorrow;
                totalRepayment += amountToBorrow * (1 + offer.Rate);
            }
            return totalRepayment;
        }
    }
}
