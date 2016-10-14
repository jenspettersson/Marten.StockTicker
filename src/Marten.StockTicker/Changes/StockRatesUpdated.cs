using System.Collections.Generic;
using Marten.StockTicker.Ticker;
using MediatR;

namespace Marten.StockTicker.Changes
{
    public class StockRatesUpdated : IAsyncNotification
    {
        public List<Stock> Stocks { get; set; }

        public StockRatesUpdated(List<Stock> stocks)
        {
            Stocks = stocks;
        }
    }
}