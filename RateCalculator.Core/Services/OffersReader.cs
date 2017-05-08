using CsvHelper;
using RateCalculator.Core.CsvMapper;
using RateCalculator.Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RateCalculator.Core.Services
{
    public class OffersReader : IOffersReader
    {
        public List<Offer> ReadOffers(string filePath)
        {
            using (var reader = new CsvReader(File.OpenText(filePath)))
            {
                RegisterMapper(reader);
                return reader.GetRecords<Offer>().ToList();
            }
        }

        private void RegisterMapper(CsvReader reader)
        {
            reader.Configuration.RegisterClassMap<OfferMap>();
        }
    }
}
