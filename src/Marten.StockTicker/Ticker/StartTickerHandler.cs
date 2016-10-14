using System.Linq;
using System.Threading.Tasks;
using Marten.StockTicker.Tracker;
using MediatR;
using StructureMap;

namespace Marten.StockTicker.Ticker
{
    public class StartTickerHandler : AsyncRequestHandler<StartTicker>
    {
        private readonly IContainer _container;
        private readonly IDocumentSession _session;

        public StartTickerHandler(IContainer container, IDocumentSession session)
        {
            _container = container;
            _session = session;
        }

        protected override async Task HandleCore(StartTicker message)
        {
            var stocks = await _session.Query<TrackedStock>().ToListAsync();
            await Application.InitializeTicker(_container, stocks.ToList());
        }
    }
}