using NUnit.Framework;
using RateCalculator.Core.Models;
using RateCalculator.Core.Services;
using System.Collections.Generic;
using System.IO;

namespace RateCalculator.Core.Tests
{
    public class OfferReaderTests
    {
        private IOffersReader _offerReader;

        [SetUp]
        public void SetUp()
        {
            _offerReader = new OffersReader();
        }

        [Test]
        public void ReadOffers_CsvWithOffers_ShouldReturnListOfOffers()
        {
            //Given
            var expectedResults = new List<Offer>
            {
                new Offer{ Name="Test", Rate=0.01m, Available=100},
                new Offer{ Name="Test2", Rate=0.2m, Available=110},
                new Offer{ Name="Test3", Rate=1, Available=120}
            };
            var csvFile = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Resources/market.csv");

            //When
            var result = _offerReader.ReadOffers(csvFile);

            //Then
            Assert.AreEqual(3, result.Count);
            for (int i = 0; i < expectedResults.Count; i++)
            {
                Assert.AreEqual(expectedResults[i].Name, result[i].Name);
                Assert.AreEqual(expectedResults[i].Rate, result[i].Rate);
                Assert.AreEqual(expectedResults[i].Available, result[i].Available);
            }

        }
    }
}
