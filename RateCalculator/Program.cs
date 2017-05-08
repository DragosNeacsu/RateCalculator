using RateCalculator.Core.Models;
using RateCalculator.Core.Services;
using SimpleInjector;
using System;
using System.Text;

namespace RateCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                DisplayUsage();
                return;
            }

            var quoteCalculator = ContainerIoC().GetInstance<IQuoteCalculator>();
            try
            {
                DisplayResult(quoteCalculator.GetQuote(args[0], args[1]));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void DisplayResult(QuoteResult quoteResult)
        {

            var output = new StringBuilder();
            output.Append($"Requested amount: {quoteResult.RequestedAmount}\n");
            output.Append($"Rate: {string.Format("{0:0.0%}", quoteResult.Rate)}\n");
            output.Append($"Monthly payment: {string.Format("{0:.00}", quoteResult.MonthlyRepayment)}\n");
            output.Append($"Total repayment: {string.Format("{0:.00}", quoteResult.TotalRepayment)}\n");
            Console.WriteLine(output);
        }

        private static void DisplayUsage()
        {
            Console.WriteLine("RateCalculator.exe [market_file] [loan_amount]");
        }

        private static Container ContainerIoC()
        {
            var container = new Container();

            container.Register<IQuoteCalculator, QuoteCalculator>();
            container.Register<ILoanValidator, LoanValidator>();
            container.Register<IOfferValidator, OfferValidator>();
            container.Register<IOffersReader, OffersReader>();

            container.Verify();
            return container;
        }
    }
}
