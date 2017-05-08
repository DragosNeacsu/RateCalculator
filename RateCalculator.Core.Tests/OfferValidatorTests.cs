using NUnit.Framework;
using RateCalculator.Core.Models;
using RateCalculator.Core.Services;
using System;
using System.Collections.Generic;

namespace RateCalculator.Core.Tests
{
    public class OfferValidatorTests
    {
        private IOfferValidator _offerValidator;

        [SetUp]
        public void SetUp()
        {
            _offerValidator = new OfferValidator();
        }

        [Test]
        public void GetQuote_NoOffers_ShouldThrowException()
        {
            //Given
            var requestedAmount = 10;
            var offers = new List<Offer>();

            //When Then
            var result = Assert.Throws<Exception>(() => _offerValidator.Validate(requestedAmount, offers));
            Assert.AreEqual("It is not possible to provide a quote now", result.Message);
        }

        [Test]
        public void GetQuote_TotalOffersLessThenAmount_ShouldThrowException()
        {
            //Given
            var requestedAmount = 40;
            var offers = new List<Offer>
                {
                    new Offer { Available = 12, Rate=0 },
                    new Offer { Available = 12, Rate=1 },
                    new Offer { Available = 12, Rate=2 },
                };

            //When Then
            var result = Assert.Throws<Exception>(() => _offerValidator.Validate(requestedAmount, offers));
            Assert.AreEqual("It is not possible to provide a quote now", result.Message);
        }
    }
}
