using Marten.StockTicker.Queries;
using Marten.StockTicker.StartTracking;
using MediatR;
using Nancy;
using Nancy.ModelBinding;

namespace Marten.StockTicker.Host.Modules
{
    public class StockModule : NancyModule
    {
        private readonly IMediator _mediator;

        public StockModule(IMediator mediator) : base("stock")
        {
            _mediator = mediator;
            Post["/", true] = async (x, ct) =>
            {
                var inputModel = this.Bind<TrackStockInputModel>();
                
                return await _mediator.SendAsync(new StartTrackStock(inputModel.Symbol, inputModel.StartRate));
            };

            Get["/", true] = async (x, ct) =>
            {
                return await _mediator.SendAsync(new GetAllStocks());
            };

            Get["/{symbol}", true] = async (x, ct) =>
            {
                string symbol = x.symbol;
                return await _mediator.SendAsync(new GetStock(symbol));
            };

            Get["/{symbol}/history", true] = async (x, ct) =>
            {
                string symbol = x.symbol;
                return await _mediator.SendAsync(new GetStockHistory(symbol));
            };

        }
    }

    public class TrackStockInputModel
    {
        public string Symbol { get; set; }
        public decimal StartRate { get; set; }
    }
}