namespace Stocks.Models
{
    public class CompanyData
    {
        public string FullName { get; private set; }
        public string ShortName { get; private set; }
        public string Ticker { get; private set; }
        public TickerPrices Prices { get; private set; }

        public CompanyData(string fullName, string shortName, string ticker, TickerPrices prices)
        {
            FullName = fullName;
            ShortName = shortName;
            Ticker = ticker;
            Prices = prices;
        }
    }
}
