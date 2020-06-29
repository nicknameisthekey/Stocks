using Stocks.Core;
using Stocks.Models;
using System.Collections.Generic;

namespace Stocks.Utils
{
    /// <summary>
    /// Настройки пользователя
    /// </summary>
    public class UserSettings
    {
        /// <summary>
        /// Тикеры для отображения
        /// </summary>
        public List<string> WatchListTickers { get; set; } = new List<string>();
        /// <summary>
        /// ID с сайта интерфакса связанные с тикером
        /// </summary>
        public Dictionary<string, string> InterfaxIds { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// Уведомления
        /// </summary>
        public List<PriceAlarm> PriceAlarms { get; set; } = new List<PriceAlarm>();
    }
}
