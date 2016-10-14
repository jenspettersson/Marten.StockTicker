using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Marten.StockTicker.Ticker
{
    public class StockTickerSimulator
    {
        private readonly List<Stock> _stocks;
        private readonly int _interval;
        private readonly List<decimal> _factors = new List<decimal>();

        public StockTickerSimulator(int refreshRate)
        {
            _interval = refreshRate;

            for (var i = -1m; i <= 1m; i += 0.01m)
            {
                _factors.Add(i);
            }

            _stocks = new List<Stock>();
        }

        public event EventHandler<List<Stock>> OnTick;
        public event EventHandler<List<Stock>> OnPaused;

        private bool _paused;
        public Task StartTicker(CancellationToken token)
        {
            return Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (_paused)
                        continue;

                    if (token.IsCancellationRequested)
                        break;

                    Thread.Sleep(_interval);

                    lock (_stocks)
                    {
                        foreach (var stock in _stocks)
                        {
                            var factor = _factors.OrderBy(x => Guid.NewGuid()).First();
                            var newRate = stock.Rate - factor;
                            if (newRate < 0)
                                newRate = 0;
                            stock.SetNewRate(newRate);
                        }

                        OnTick?.Invoke(this, _stocks);
                    }
                }
            }, token);
        }

        public void AddStock(Stock tracked)
        {
            _stocks.Add(tracked);
        }

        public void Pause()
        {
            _paused = true;
            OnPaused?.Invoke(this, _stocks);
        }

        public void Resume()
        {
            _paused = false;
        }
    }
}