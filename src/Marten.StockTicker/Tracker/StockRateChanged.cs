namespace Marten.StockTicker.Tracker
{
    public class StockRateChanged
    {
        public decimal Rate { get; set; }
        public decimal Change { get; set; }
    }
}