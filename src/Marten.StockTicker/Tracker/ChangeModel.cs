namespace Marten.StockTicker.Tracker
{
    public class ChangeModel
    {
        public decimal Rate { get; set; }
        public decimal Change { get; set; }

        public ChangeModel(decimal rate, decimal change)
        {
            Rate = rate;
            Change = change;
        }
    }
}