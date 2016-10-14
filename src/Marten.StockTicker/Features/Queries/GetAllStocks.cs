using System.Collections.Generic;
using System.Threading.Tasks;
using Marten.StockTicker.Tracker;
using MediatR;

namespace Marten.StockTicker.Features.Queries
{
    public class GetAllStocks : IAsyncRequest<IEnumerable<TrackedStock>>
    {
        
    }

    public class GetAllStocksHandler : IAsyncRequestHandler<GetAllStocks, IEnumerable<TrackedStock>>
    {
        private readonly IQuerySession _session;

        public GetAllStocksHandler(IQuerySession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<TrackedStock>> Handle(GetAllStocks message)
        {
            return await _session.Query<TrackedStock>().ToListAsync();
        }
    }
}