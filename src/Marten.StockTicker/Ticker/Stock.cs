using System;

namespace Marten.StockTicker.Ticker
{
    public class Stock
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public decimal Rate { get; set; }
        public decimal LastChange { get; set; }
        public void SetNewRate(decimal rate)
        {
            LastChange = rate - Rate;
            Rate = rate;
        }
    }
}
