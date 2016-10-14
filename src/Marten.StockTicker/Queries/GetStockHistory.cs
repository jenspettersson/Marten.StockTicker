using System.Linq;
using System.Threading.Tasks;
using Marten.StockTicker.Tracker;
using MediatR;

namespace Marten.StockTicker.Queries
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
}