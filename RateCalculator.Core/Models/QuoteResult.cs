﻿namespace RateCalculator.Core.Models
{
    public class QuoteResult
    {
        public int RequestedAmount { get; set; }

        public decimal Rate { get; set; }

        public decimal MonthlyRepayment { get; set; }

        public decimal TotalRepayment { get; set; }
    }
}
