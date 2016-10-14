using MediatR;

namespace Marten.StockTicker.Features.StartTracking
{
    public class StartTrackStock : IAsyncRequest
    {
        public string Symbol { get; set; }
        public decimal StartingRate { get; set; }

        public StartTrackStock(string symbol, decimal startingRate)
        {
            Symbol = symbol;
            StartingRate = startingRate;
        }
    }
}