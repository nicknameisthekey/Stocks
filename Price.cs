namespace Stocks
{
    public struct Price
    {
        public string Ticker { get; private set; }
        public float OpenPrice { get; private set; }
        public float LowPrice { get; private set; }
        public float HighPrice { get; private set; }
        public float CurrentPrice { get; private set; }
        public float PriceChange => CurrentPrice - OpenPrice;
        public string PriceChangePrcnt =>
            ((PriceChange / OpenPrice) * 100).ToString("0.0") + "%";
        public Price(string ticker, string openPrice, string lowPrice,
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
