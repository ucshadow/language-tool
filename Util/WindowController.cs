using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TranslateDownloader.Util {
    public static class WindowController {
        //private static MainWindow _window;

        private static TextBox _fromBox;
        private static TextBox _toBox;
        private static TextBox _fromLanguage;
        private static TextBox _toLanguage;

        private static Image _image1;
        private static Image _image2;
        private static Image _image3;

        public static void InitWindowController(MainWindow window) {
            //_window = window;
            _fromBox = ((TextBox) window.FindName("dataBox"));
            _toBox = ((TextBox) window.FindName("translatedBox"));
            _fromLanguage = ((TextBox) window.FindName("fromBox"));
            _toLanguage = ((TextBox) window.FindName("toBox"));
            _image1 = ((Image) window.FindName("image1"));
            _image2 = ((Image) window.FindName("image2"));
            _image3 = ((Image) window.FindName("image3"));
        }

        public static void MarkAsDownloaded(bool originalLanguage, string text) {
            if (originalLanguage) { }
        }

        public static IEnumerable<string> GetListOfDownloadLines() {
            return _fromBox.Text.Split('\n').Select(e => e.Trim()).ToArray();
        }

        public static string GetToLanguage() {
            return _toLanguage?.Text;
        }

        public static string GetFromLanguage() {
            return _fromLanguage?.Text;
        }

        public static void AddLineAsDownloaded(string line) {
            _toBox.Text += line + "\n";
        }

        public static void ChangeImage(string imagePath, int imageNumber) {
            imagePath = Directory.GetCurrentDirectory() + @"\Data\Images\" + $"{imagePath}_{imageNumber}.png";
            switch (imageNumber) {
                case 1:
                    _image1.Source = new BitmapImage(new Uri(imagePath));
                    break;
                case 2:
                    _image2.Source = new BitmapImage(new Uri(imagePath));
                    break;
                case 3:
                    _image3.Source = new BitmapImage(new Uri(imagePath));
                    break;
                default:
                    break;
            }
        }
    }
}