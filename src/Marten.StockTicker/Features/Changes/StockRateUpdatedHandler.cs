using System.Threading.Tasks;
using Marten.StockTicker.Tracker;
using MediatR;

namespace Marten.StockTicker.Features.Changes
{
    public class StockRateUpdatedHandler : IAsyncNotificationHandler<StockRatesUpdated>
    {
        private readonly IDocumentSession _session;

        public StockRateUpdatedHandler(IDocumentSession session)
        {
            _session = session;
        }
        public async Task Handle(StockRatesUpdated notification)
        {
            foreach (var stock in notification.Stocks)
            {
                _session.Events.Append(stock.Id, new StockRateChanged {Change = stock.LastChange, Rate = stock.Rate});
            }

            await _session.SaveChangesAsync();
        }
    }
}