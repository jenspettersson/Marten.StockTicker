using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marten.StockTicker.Tracker;
using MediatR;

namespace Marten.StockTicker.Features.Queries
{
    public class GetStockHistory : IAsyncRequest<StockHistory>
    {
        public string Symbol { get; }

        public GetStockHistory(string symbol)
        {
            Symbol = symbol;
        }
    }

    public class GetStockHistoryHandler : IAsyncRequestHandler<GetStockHistory, StockHistory>
    {
        private readonly IDocumentSession _session;

        public GetStockHistoryHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<StockHistory> Handle(GetStockHistory message)
        {
            var stock = await _session.Query<TrackedStock>().Where(x => x.Symbol == message.Symbol).FirstOrDefaultAsync();

            if (stock == null)
                return null;

            var streamId = stock.Id;
            return await _session.Events.AggregateStreamAsync<StockHistory>(streamId);
        }
    }

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

    public class ChangeModel
    {
        public decimal Rate { get; set; }
        public decimal Change { get; set; }

        public ChangeModel(decimal rate, decimal change)
        {
            Rate = rate;
            Change = change;
        }
    }

}