using System;
using System.Collections.Generic;
using Marten.StockTicker.Ticker;
using Microsoft.Owin.Hosting;

namespace Marten.StockTicker.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://+:8989";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Press enter to exit");

                while (true)
                {
                    Console.ReadLine();
                    Application.ToggleTicker();
                }
            }

            //QueryCurrentStocks();
            //RunTicker();
        }

        private static void RunTicker()
        {
            //_documentStore.Advanced.Clean.CompletelyRemoveAll();

            //var stocks = new List<Stock>
            //{
            //    new Stock {Symbol = "MSFT", Rate = 56.73m},
            //    new Stock {Symbol = "GOOG", Rate = 776.26m},
            //    new Stock {Symbol = "TSLA", Rate = 200.77m},
            //    new Stock {Symbol = "FORD", Rate = 1.27m},
            //    new Stock {Symbol = "AAPL", Rate = 116.53m}
            //};
            
            //var stockTicker = new StockTickerSimulator();

            //stockTicker.OnTick += StockTickerOnOnTick;
            //var cancellationToken = new CancellationTokenSource();
            //stockTicker.StartTicker(cancellationToken.Token);
            //while (true)
            //{
            //    Console.ReadLine();
            //    cancellationToken.Cancel();
            //    break;
            //}
        }

        private static void StockTickerOnOnTick(object sender, List<Stock> e)
        {
            
        }
    }
}
