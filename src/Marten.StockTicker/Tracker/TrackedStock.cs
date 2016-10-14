using System;

namespace Marten.StockTicker.Tracker
{
    public class TrackedStock
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public decimal CurrentRate { get; set; }

        public void Apply(TrackingStarted evt)
        {
            Symbol = evt.Symbol;
            CurrentRate = evt.StartRate;
        }

        public void Apply(StockRateChanged evt)
        {
            CurrentRate = evt.Rate;
        }
    }
}