namespace Stocks.Models
{
    /// <summary>
    /// Информация о уведомлении
    /// </summary>
    public class PriceAlarm
    {
        /// <summary>
        /// Тикер, к которому привязано уведомление
        /// </summary>
        public string Ticker { get; private set; }
        /// <summary>
        /// Цена срабатывания
        /// </summary>
        public float TargetPrice { get; private set; }
        /// <summary>
        /// Условие срабатывания, true - цена должна быть выше или равна цены-условия
        /// false - ниже
        /// </summary>
        public bool AlarmIfTargetHigher { get; private set; }

        public PriceAlarm(string ticker, float targetPrice, bool alarmIfTargetHigher)
        {
            Ticker = ticker;
            TargetPrice = targetPrice;
            AlarmIfTargetHigher = alarmIfTargetHigher;
        }
    }
}
