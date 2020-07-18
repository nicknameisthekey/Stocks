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

        public ReportsVM(string id)
            => Reports = DataDownloader.LoadReportsList(id, selectedType);
        public void OnSelectedTypeChange(int type) 
            => selectedType = type;
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
