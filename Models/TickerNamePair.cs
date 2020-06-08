namespace Stocks
{
    public class TickerNamePair
    {
        public string Ticker { get; private set; }
        public string Name { get; private set; }
        public TickerNamePair(string t, string n)
        {
            Ticker = t;
            Name = n;
        }
    }
}
