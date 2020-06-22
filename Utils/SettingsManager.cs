using Newtonsoft.Json;
using Stocks.Models;
using Stocks.Utils;
using System;
using System.IO;

namespace Stocks
{
    public static class SettingsManager
    {
        public static event Action SettingsChanged;
        static readonly string fileName = "UserSettings.json";
        public static UserSettings Settings { get; private set; }
        public static void SaveSettings()
        {
            string json = JsonConvert.SerializeObject(Settings);
            File.WriteAllText(fileName, json);
        }
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
        public static void AddTicker(string ticker)
        {
            Settings.WatchListTickers.Add(ticker);
            SaveSettings();
            SettingsChanged?.Invoke();
        }
        public static void RemoveTicker(string ticker)
        {
            Settings.WatchListTickers.Remove(ticker);
            SaveSettings();
            SettingsChanged?.Invoke();
        }
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
        public static void AddPriceAlarm(PriceAlarm alarm)
        {
            Settings.PriceAlarms.Add(alarm);
            SaveSettings();
            SettingsChanged?.Invoke();
        }
        public static void RemovePriceAlarm(PriceAlarm alarm)
        {
            Settings.PriceAlarms.Remove(alarm);
            SaveSettings();
            SettingsChanged?.Invoke();
        }
    }
}
