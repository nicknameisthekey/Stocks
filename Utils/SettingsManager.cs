using Newtonsoft.Json;
using Stocks.Models;
using Stocks.Utils;
using System;
using System.IO;

namespace Stocks
{
    /// <summary>
    /// Управление настройками приложения
    /// </summary>
    public static class SettingsManager
    {
        /// <summary>
        /// Путь до файла с настройками
        /// </summary>
        static readonly string fileName = "UserSettings.json";

        /// <summary>
        /// Срабатывает при изменении настроек
        /// </summary>
        public static event Action SettingsChanged;
        /// <summary>
        /// Актуальные настройки
        /// </summary>
        public static UserSettings Settings { get; private set; }
        /// <summary>
        /// Сохраняет настройки в файл
        /// </summary>
        public static void SaveSettings()
        {
            string json = JsonConvert.SerializeObject(Settings);
            File.WriteAllText(fileName, json);
        }
        /// <summary>
        /// Читает настройки из файла
        /// </summary>
        public static void ReadSettings()
        {
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                if (string.IsNullOrEmpty(json))
                    Settings = new UserSettings();
                else
                    Settings = JsonConvert.DeserializeObject<UserSettings>(json);
            }
            else
            {
                Settings = new UserSettings();
            }
            SettingsChanged?.Invoke();
        }
        /// <summary>
        /// Добавить тикер для отслеживания
        /// </summary>
        public static void AddTicker(string ticker)
        {
            Settings.WatchListTickers.Add(ticker);
            SaveSettings();
            SettingsChanged?.Invoke();
        }
        /// <summary>
        /// Убрать тикер для отслеживания
        /// </summary>
        public static void RemoveTicker(string ticker)
        {
            Settings.WatchListTickers.Remove(ticker);
            SaveSettings();
            SettingsChanged?.Invoke();
        }
        /// <summary>
        /// Изменить связанный с тикером ID на сайте интерфакса
        /// </summary>
        public static void ChangeInterfaxId(string ticker, string id)
        {
            if (id == null)
                id = "";
            if (Settings.InterfaxIds.ContainsKey(ticker))
                Settings.InterfaxIds[ticker] = id;
            else
                Settings.InterfaxIds.Add(ticker, id);
            SaveSettings();
            SettingsChanged?.Invoke();
        }
        /// <summary>
        /// Добавить уведомление 
        /// </summary>
        public static void AddPriceAlarm(PriceAlarm alarm)
        {
            Settings.PriceAlarms.Add(alarm);
            SaveSettings();
            SettingsChanged?.Invoke();
        }
        /// <summary>
        /// Убрать уведомление
        /// </summary>
        public static void RemovePriceAlarm(PriceAlarm alarm)
        {
            Settings.PriceAlarms.Remove(alarm);
            SaveSettings();
            SettingsChanged?.Invoke();
        }
    }
}
