namespace Stocks.Models
{
    public struct TickerPrices
    {
        public string Ticker { get; private set; }
        public float OpenPrice { get; private set; }
        public float LowPrice { get; private set; }
        public float HighPrice { get; private set; }
        public float CurrentPrice { get; private set; }
        public float PriceChange => CurrentPrice - OpenPrice;
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
