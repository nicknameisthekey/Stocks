namespace Stocks.Models
{
    /// <summary>
    /// Текущие биржевые цены компании
    /// </summary>
    public struct TickerPrices
    {
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
        public TickerPrices(string openPrice, string lowPrice,
            string highPrice, string currentPrice)
        {
            float.TryParse(openPrice, out float openPriceParsed);
            OpenPrice = openPriceParsed;
            float.TryParse(lowPrice, out float lowPriceParsed);
            LowPrice = lowPriceParsed;
            float.TryParse(highPrice, out float highPriceParsed);
            HighPrice = highPriceParsed;
            float.TryParse(currentPrice, out float currentPriceParsed);
            CurrentPrice = currentPriceParsed;
        }
    }
}
