using System;
using System.IO;
using System.Net;
using System.Windows.Controls;
using TranslateDownloader.Interfaces;
using TranslateDownloader.Util;
using static TranslateDownloader.Util.ConsoleProvider;
using static TranslateDownloader.Util.WindowController;

namespace TranslateDownloader.Workers {
    public class BingFileDownloader {

        private readonly string _ig; // may have to be generated for each request?
        
        
        public BingFileDownloader(MainWindow window) {
            _ig = Helpers.Generate32LengthString();
        }
        
        public void DownloadTargetLanguage() {
            var list = GetListOfDownloadLines();
            foreach (var text in list) {
                if (text.Length <= 1) continue;
                if(Helpers.DataExists($"{GetToLanguage()}", text)) {
                    Print($"Data already exists {GetToLanguage()}, {text}");
                }
                else {      
                    // TODO: The text should be hangul not latin!
                    var audioUrl = $"https://www.bing.com/tspeak?&format=audio%2Fmp3&language={GetToLanguage()}&IG={_ig}" +
                            $"=translator.5034.4&options=female&text={text}";
                    
//                    var translateUrl = $"https://www.bing.com/translator/api/Dictionary/Lookup?" +
//                                       $"from={GetFromLanguage()}&to={GetToLanguage()}&text={text}";

//                    var translateUrl =
//                        $"https://www.bing.com/ttranslate?&category=&IG={_ig}&IID=translator.5033.1&text={text}" +
//                        $"&from={GetFromLanguage()}&to={GetToLanguage()}";
                    
                    var translateUrl =
                        $"https://www.bing.com/ttranslate?";

                    var reqparm = new System.Collections.Specialized.NameValueCollection {
                        {"category", ""},
                        {"IG", _ig},
                        {"IID", "translator.5033.1"},
                        {"text", text},
                        {"from", GetFromLanguage()},
                        {"to", GetToLanguage()}
                    };

                    var postParams = $"&category=&IG={_ig}&IID=translator.5033.1&text={text}" +
                                     $"&from={GetFromLanguage()}&to={GetToLanguage()}";

                    WebWorker.AddToDownloadQueue(audioUrl, translateUrl, text, GetToLanguage(), reqparm, _ig);
                }
            }
            WebWorker.Act();
        }
    }
}