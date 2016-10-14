using System;
using System.Linq;
using System.Threading.Tasks;
using Marten.StockTicker.Ticker;
using Marten.StockTicker.Tracker;
using MediatR;

namespace Marten.StockTicker.Features.StartTracking
{
    public class StartTrackStockHandler : AsyncRequestHandler<StartTrackStock>
    {
        private readonly IDocumentSession _session;
        private readonly IMediator _mediator;

        public StartTrackStockHandler(IDocumentSession session, IMediator mediator)
        {
            _session = session;
            _mediator = mediator;
        }

        protected override async Task HandleCore(StartTrackStock message)
        {
            var existingStock = await _session.Query<TrackedStock>()
                .Where(x => x.Symbol == message.Symbol)
                .FirstOrDefaultAsync();

            if (existingStock != null)
                return;

            var streamId = Guid.NewGuid();

            var trackedStock = new Stock { Id = streamId, Symbol = message.Symbol, Rate = message.StartingRate };
            var trackingStarted = new TrackingStarted {Symbol = trackedStock.Symbol, StartRate = trackedStock.Rate};

            _session.Events.Append(streamId, trackingStarted);
            
            await _session.SaveChangesAsync();
            await _mediator.PublishAsync(new NewStockAdded(trackedStock));
        }
    }
}