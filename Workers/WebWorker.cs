using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using TranslateDownloader.Util;
using static TranslateDownloader.Util.ConsoleProvider;

namespace TranslateDownloader.Workers {
    public static class WebWorker {
        private static FileSaver _fileSaver;
        private static WebData _active;
        private static WebClient _c;
        private static int _downloadedImgs;

        private static readonly Queue<WebData> Q = new Queue<WebData>();

        public static void AddToDownloadQueue(string audioUrl, string translateUrl, string text, string to,
            NameValueCollection postParams, string ig) {
            if (_fileSaver == null) {
                _fileSaver = new FileSaver();
            }

            Q.Enqueue(new WebData(audioUrl, translateUrl, text, to, postParams, ig));
        }

        public static void Act() {
            DownloadTranslationText();
        }
        
        private static void DownloadTranslationText() {
            
            if (Q.Count == 0) return;
            _active = Q.Dequeue();
            
            if (_c == null) {
                _c = new WebClient();
                _c.Headers.Add("User-Agent",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36");
            }
            
            Print($"Downloading Translation {_active.TranslateUrl} {_active.Params}");

            _c.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

            _c.UploadValuesAsync(new Uri(_active.TranslateUrl), "POST", _active.Params);

//            _c.DownloadDataAsync(new Uri(_active[1]));
            _c.UploadValuesCompleted += UploadTranslationCompleted;
        }
        
        private static void UploadTranslationCompleted(object sender, UploadValuesCompletedEventArgs e) {
            var data = e.Result;
            var asString = Encoding.UTF8.GetString(data);

            var res = JObject.Parse(asString);
            var t = (string) res.SelectToken("translationResponse");
            _active.AudioUrl =
                $"https://www.bing.com/tspeak?&format=audio%2Fmp3&language={_active.To}&IG={_active.Ig}" +
                $"=translator.5034.4&options=female&text={t}";
//
            Helpers.AddTranslatedLineToFile(_active.Text, t, _active.To);
            WindowController.AddLineAsDownloaded(t);

            DownloadAudio();
        }

        private static void DownloadAudio() {
            Print($"Downloading {_active.AudioUrl} {_active.Text}, {_active.To}");
            _c.DownloadDataAsync(new Uri(_active.AudioUrl));
            _c.DownloadDataCompleted += DownloadComplete;
        }

        private static void DownloadComplete(object sender, DownloadDataCompletedEventArgs e) {
            //Print($"Download complete -> {e.Result}");
            var data = e.Result;
            _fileSaver.SaveAudioFile(_active.Text, $"{_active.To}", data);
            DownloadPictures();
        }
        

        private static void DownloadPictures() {
            _c.DownloadStringAsync(new Uri($"https://www.bing.com/images/search?q={_active.Text}"));
//            _c.DownloadStringAsync(new Uri($"https://www.google.com/search?q={_active.Text}&source=lnms&tbm=isch&sa=X&ved=" +
//                                           $"0ahUKEwiLltmqto3dghWObFAKHTo7B84Q_AUICigB&biw=2752&bih=1466&dpr=1.4"));
            
//            Console.WriteLine($"downloading pic page -> https://www.google.com/search?q={_active.Text}&source=lnms&tbm=isch&sa=X&ved=" +
//                              $"0ahUKEwiLltmqto3dghWObFAKHTo7B84Q_AUICigB&biw=2752&bih=1466&dpr=1.4");
            _c.DownloadStringCompleted += DownloadImagesPageCompleted;
        }

        private static void DownloadImagesPageCompleted(object sender, DownloadStringCompletedEventArgs e) {
            var str = e.Result;
            var html = new HtmlDocument();
            html.LoadHtml(str);

            var images = html.DocumentNode.SelectNodes("//img");

//            var images = html.DocumentNode.Descendants().Where(n => n.HasClass("rg_ic rg_i")).ToArray();
            
            for (var i = 7; i < 10; i++) {
                var image = images[i].Attributes[@"src"].Value;
                DownloadSpecificImage(image);
            }
        }

        private static void DownloadSpecificImage(string src) {
            var client = new WebClient();
            client.DownloadDataAsync(new Uri(src));
            client.DownloadDataCompleted += DownloadImageCompleted;
        }

        private static void DownloadImageCompleted(object sender, DownloadDataCompletedEventArgs e) {
            var data = e.Result;
            
            _downloadedImgs++;
            
            _fileSaver.SaveImageFile(data, _active.Text, _downloadedImgs);

            WindowController.ChangeImage(_active.Text, _downloadedImgs);
            
            


            if (_downloadedImgs != 3) return;
            _downloadedImgs = 0;
            _c = new WebClient();
            Act();
        }
    }
}