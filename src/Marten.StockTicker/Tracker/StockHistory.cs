using System;
using System.Collections.Generic;
using Marten.StockTicker.Tracker;

namespace Marten.StockTicker.Queries
{
    public class StockHistory
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public decimal StartRate { get; set; }
        public List<ChangeModel> Changes { get; set; } = new List<ChangeModel>();

        public void Apply(TrackingStarted evt)
        {
            StartRate = evt.StartRate;
            Symbol = evt.Symbol;
        }

        public void Apply(StockRateChanged evt)
        {
            Changes.Add(new ChangeModel(evt.Rate, evt.Change));
        }
    }
}