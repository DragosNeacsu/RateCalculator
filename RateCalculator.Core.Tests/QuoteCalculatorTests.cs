using NSubstitute;
using NUnit.Framework;
using RateCalculator.Core.Models;
using RateCalculator.Core.Services;
using System.Collections.Generic;

namespace RateCalculator.Core.Tests
{
    public class QuoteCalculatorTests
    {
        private IQuoteCalculator _quoteCalculator;
        private ILoanValidator _loanValidator;
        private IOfferValidator _offerValidator;
        private IOffersReader _offerReader;

        [SetUp]
        public void SetUp()
        {
            _loanValidator = Substitute.For<ILoanValidator>();
            _offerValidator = Substitute.For<IOfferValidator>();
            _offerReader = Substitute.For<IOffersReader>();
            _offerValidator.Validate(Arg.Any<int>(), Arg.Any<List<Offer>>()).Returns(true);
            _loanValidator.Validate(Arg.Any<string>()).Returns(true);
            _offerReader.ReadOffers(Arg.Any<string>())
                .Returns(new List<Offer>
                {
                    new Offer { Available = 12, Rate=0 },
                    new Offer { Available = 12, Rate=1 },
                    new Offer { Available = 12, Rate=2 },
                });

            _quoteCalculator = new QuoteCalculator(_offerReader, _loanValidator, _offerValidator);
        }

        [Test]
        public void GetQuote_ShouldCallLoanValidator()
        {
            // When
            var result = _quoteCalculator.GetQuote("filePath", "10");

            // Then
            _loanValidator.Received(1).Validate("10");
        }

        [Test]
        public void GetQuote_ShouldCallOfferValidator()
        {
            // When
            var result = _quoteCalculator.GetQuote("filePath", "10");

            // Then
            _offerValidator.Received(1).Validate(Arg.Any<int>(), Arg.Any<List<Offer>>());
        }

        [Test]
        public void GetQuote_ShouldCallOfferReader()
        {
            // When
            var result = _quoteCalculator.GetQuote("filePath", "10");

            // Then
            _offerReader.Received(1).ReadOffers("filePath");
        }

        [Test]
        public void GetQuote_ShouldReturnLoanAmount()
        {
            // Given
            var requestedAmount = 10;

            // When
            var result = _quoteCalculator.GetQuote("filepath", requestedAmount.ToString());

            // Then
            Assert.IsNotNull(result);
            Assert.AreEqual(requestedAmount, result.RequestedAmount);
        }

        [Test]
        public void GetQuote_ShouldCAlculateLowestRates()
        {
            // Given
            var requestedAmount = 12;

            // When
            var result = _quoteCalculator.GetQuote("filepath", requestedAmount.ToString());

            // Then
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Rate);
        }

        [Test]
        public void GetQuote_ShouldCalculateTotalRepayment()
        {
            // Given
            var requestedAmount = 36;

            // When
            var result = _quoteCalculator.GetQuote("filePath", requestedAmount.ToString());

            // Then
            Assert.IsNotNull(result);
            Assert.AreEqual(72, result.TotalRepayment);
        }

        [Test]
        public void GetQuote_ShouldCalculateMonthlyPayment()
        {
            // Given
            var requestedAmount = 36;

            // When
            var result = _quoteCalculator.GetQuote("filePath", requestedAmount.ToString());

            // Then
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.MonthlyRepayment);
        }
    }
}
