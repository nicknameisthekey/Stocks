namespace Stocks.Models
{
    /// <summary>
    /// Текущие биржевые цены компании
    /// </summary>
    public struct TickerPrices
    {
        /// <summary>
        /// Тикер
        /// </summary>
        public string Ticker { get; private set; }
        /// <summary>
        /// Цена открытия
        /// </summary>
        public float OpenPrice { get; private set; }
        /// <summary>
        /// Низшая цена за день
        /// </summary>
        public float LowPrice { get; private set; }
        /// <summary>
        /// Высшая цена за день
        /// </summary>
        public float HighPrice { get; private set; }
        /// <summary>
        /// Текущая цена
        /// </summary>
        public float CurrentPrice { get; private set; }
        /// <summary>
        /// Изменение цены с открытия
        /// </summary>
        public float PriceChange => CurrentPrice - OpenPrice;
        /// <summary>
        /// Изменение цены с открытия в %
        /// </summary>
        public float PriceChangePrcnt => (PriceChange/OpenPrice) * 100;
        public TickerPrices(string ticker, string openPrice, string lowPrice,
            string highPrice, string currentPrice)
        {
            Ticker = ticker;
            OpenPrice = float.Parse(openPrice);
            LowPrice = float.Parse(lowPrice);
            HighPrice = float.Parse(highPrice);
            CurrentPrice = float.Parse(currentPrice);
        }
    }
}
