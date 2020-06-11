namespace Stocks.Models
{
    public struct InterfaxData
    {
        public string Link { get; private set; }
        public string Text { get; private set; }
        public string Date { get; private set; }
        public InterfaxData(string link, string text, string date)
        {
            Link = link;
            Text = text;
            Date = date;
        }
    }
}
