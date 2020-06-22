namespace Stocks.Models
{
    public class PriceAlarm
    {
        public string Ticker { get; private set; }
        public float TargetPrice { get; private set; }
        public bool AlarmIfTargetHigher { get; private set; }

        public PriceAlarm(string ticker, float targetPrice, bool alarmIfTargetHigher)
        {
            Ticker = ticker;
            TargetPrice = targetPrice;
            AlarmIfTargetHigher = alarmIfTargetHigher;
        }
    }
}
