using System.Linq;
using System.Threading.Tasks;
using Marten.StockTicker.Tracker;
using MediatR;

namespace Marten.StockTicker.Features.Queries
{
    public class GetStock : IAsyncRequest<TrackedStock>
    {
        public string Symbol { get; }

        public GetStock(string symbol)
        {
            Symbol = symbol;
        }
    }

    public class GetStockHandler : IAsyncRequestHandler<GetStock, TrackedStock>
    {
        private readonly IQuerySession _session;

        public GetStockHandler(IQuerySession session)
        {
            _session = session;
        }

        public async Task<TrackedStock> Handle(GetStock message)
        {
            return await _session.Query<TrackedStock>().Where(x => x.Symbol == message.Symbol).FirstOrDefaultAsync();
        }
    }
}