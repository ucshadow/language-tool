using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TranslateDownloader.Util;
using TranslateDownloader.Workers;

namespace TranslateDownloader.Loaders {   

    internal class Controller {

        private readonly MainWindow _window;

        //private string speak = "https://www.bing.com/translator/api/language/Speak?locale=ko-KR&gender=female&media=audio/mp3&text=예쁜+여자";
        //private string translate = "https://www.bing.com/translator/api/Dictionary/Lookup?from=en&to=ko&text=tall%20woman";

//        private TextBox _dataBox;
//        private TextBox translatedBox;
//        private TextBox _fromBox;
//        private TextBox _toBox;

        public Controller(MainWindow window) {
            _window = window;
            
            WindowController.InitWindowController(window);
            ConsoleProvider.InitConsole(window);
            Start();
        }

        private void Start() {
            var b = (Button)_window.FindName("startButt");
            if (b != null) b.Click += Load;
        }

        private void Load(object sender, RoutedEventArgs e) {
            var b = new BingFileDownloader(_window);
            b.DownloadTargetLanguage();
        }
    }
}
