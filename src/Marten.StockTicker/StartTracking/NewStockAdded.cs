﻿using Marten.StockTicker.Ticker;
using MediatR;

namespace Marten.StockTicker.StartTracking
{
    public class NewStockAdded : IAsyncNotification
    {
        public Stock TrackedStock { get; }

        public NewStockAdded(Stock trackedStock)
        {
            TrackedStock = trackedStock;
        }
    }
}