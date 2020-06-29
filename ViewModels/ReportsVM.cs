using Stocks.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace Stocks.ViewModels
{
    public class ReportsVM : BaseVM
    {
        List<InterfaxData> reports;
        int selectedType = 4;
        TickerPrices selectedTicker;
        public List<InterfaxData> Reports
        {
            get => reports;
            private set { reports = value; onPropertyChange(); }
        }
        public Dictionary<int, string> ReportTypes { get; private set; }
            = new Dictionary<int, string>
            {
                [4] = "МСФО",
                [3] = "РСБУ",
                [2] = "Годовая"
            };

        public void onSelectedTypeChange(int type)
        {
            selectedType = type;
            updateList();
        }
        public ReportsVM()
        {
            MainVM.SelectedItemChanged += OnSelectedCompanychange;
        }
        public void OnSelectedCompanychange(TickerPrices ticker)
        {
            selectedTicker = ticker;
            updateList();
        }
        void updateList()
        {
            if (selectedTicker.Ticker == null) return;
            Dictionary<string, string> ids = SettingsManager.Settings.InterfaxIds;
            if (ids.ContainsKey(selectedTicker.Ticker))
            {
                Reports = DataDownloader.LoadReportsList(ids[selectedTicker.Ticker], selectedType);
            }
            else
                Reports = new List<InterfaxData>();
        }
        public void OpenReport(string uri)
        {
            WebClient client = new WebClient();
            client.DownloadFileAsync(new Uri(uri.Replace("FileInfo", "FileLoad")), "temp.zip");
            client.DownloadFileCompleted += (o, e) =>
            {
                ZipFile.ExtractToDirectory("temp.zip", "temp");
                string[] files = Directory.GetFiles("temp");
                Process.Start(new ProcessStartInfo(files[0]));
            };
        }
    }
}
