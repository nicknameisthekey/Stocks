using Stocks.Core;
using Stocks.Models;
using System.Collections.Generic;

namespace Stocks.Utils
{
    public class UserSettings
    {
        public List<string> WatchListTickers { get; set; } = new List<string>();
        public Dictionary<string, string> InterfaxIds { get; set; } = new Dictionary<string, string>();
        public List<PriceAlarm> PriceAlarms { get; set; } = new List<PriceAlarm>();
    }
}
