using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Marten.StockTicker.Features.Changes;
using Marten.StockTicker.Ticker;
using Marten.StockTicker.Tracker;
using MediatR;
using StructureMap;

namespace Marten.StockTicker
{
    public static class Application
    {
        public static StockTickerSimulator Ticker;
        public static CancellationTokenSource CancellationTokenSource;

        private static IMediator _mediator;
        private static IWriter _writer;

        public static async Task InitializeTicker(IContainer container, List<TrackedStock> stocks)
        {
            _mediator = container.GetInstance<IMediator>();
            _writer = container.GetInstance<IWriter>();

            CancellationTokenSource = new CancellationTokenSource();
            Ticker = new StockTickerSimulator(1000);
            Ticker.OnTick += TickerOnOnTick;
            Ticker.OnPaused += TickerOnPaused;
            foreach (var tracked in stocks)
            {
                Ticker.AddStock(new Stock
                {
                    Id = tracked.Id,
                    Symbol = tracked.Symbol,
                    Rate = tracked.CurrentRate
                });
            }
            _isRunning = true;
            await Ticker.StartTicker(CancellationTokenSource.Token);
        }

        private static void TickerOnPaused(object sender, List<Stock> stocks)
        {
            _writer.SetColor(ConsoleColor.Red);
            PrintStocks(stocks);
        }

        private static void TickerOnOnTick(object sender, List<Stock> stocks)
        {
            if (!_isRunning)
                return;

            _writer.SetColor(ConsoleColor.Green);
            PrintStocks(stocks);
        }

        private static void PrintStocks(List<Stock> stocks)
        {
            if (!stocks.Any())
            {
                return;
            }
            foreach (var stock in stocks)
            {
                _writer.WriteLine($"{stock.Symbol}\t{stock.Rate}\t{stock.LastChange}");
            }
            _writer.Render();
            _mediator.PublishAsync(new StockRatesUpdated(stocks));
        }

        private static bool _isRunning;

        public static void ToggleTicker()
        {
            if (_isRunning)
            {
                Ticker.Pause();
                _isRunning = false;
            }
            else
            {
                Ticker.Resume();
                _isRunning = true;
            }
        }
    }
}