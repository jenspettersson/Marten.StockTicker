namespace Marten.StockTicker.Tracker
{
    public class TrackingStarted
    {
        public decimal StartRate { get; set; }
        public string Symbol { get; set; }
    }
}