using NUnit.Framework;
using RateCalculator.Core.Services;
using System;

namespace RateCalculator.Core.Tests
{
    public class LoanValidatorTests
    {
        private ILoanValidator _loanValidator;

        [SetUp]
        public void SetUp()
        {
            _loanValidator = new LoanValidator();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("not valid")]
        public void Validate_LoanAmountNotInteger_ShouldThrowException(string loanAmount)
        {
            //When Then
            var result = Assert.Throws<ArgumentException>(() => _loanValidator.Validate(loanAmount));
            Assert.AreEqual("Loan amount should be integer", result.Message);
        }

        [Test]
        public void Validate_LoanAmountNotMultipleOf100_ShouldThrowException()
        {
            //Given
            var loanAmount = "1222";

            //When Then
            var result = Assert.Throws<ArgumentException>(() => _loanValidator.Validate(loanAmount));
            Assert.AreEqual("Loan amount must be multiple of 100", result.Message);
        }

        [Test]
        [TestCase("900")]
        [TestCase("15100")]
        public void Validate_LoanAmountOutOfRange_ShouldThrowException(string loanAmount)
        {
            //When Then
            var result = Assert.Throws<ArgumentException>(() => _loanValidator.Validate(loanAmount));
            Assert.AreEqual("Loan amount must be between 1000 and 15000", result.Message);
        }

        [Test]
        public void Validate_ValidLoanAmount_ShouldReturnTrue()
        {
            //Given
            var loanAmount = "1500";

            //When
            var result = _loanValidator.Validate(loanAmount);

            //Then
            Assert.IsTrue(result);
        }
    }
}
