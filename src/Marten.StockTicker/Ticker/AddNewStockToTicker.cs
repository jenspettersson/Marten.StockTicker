using System.Threading.Tasks;
using Marten.StockTicker.Features.StartTracking;
using MediatR;

namespace Marten.StockTicker.Ticker
{
    public class AddNewStockToTicker : IAsyncNotificationHandler<NewStockAdded>
    {
        public Task Handle(NewStockAdded notification)
        {
            Application.Ticker.AddStock(notification.TrackedStock);

            return Task.FromResult(0);
        }
    }
}