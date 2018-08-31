using System.Collections.Specialized;

namespace TranslateDownloader.Util {
    public class WebData {
        public string AudioUrl;
        public string TranslateUrl;
        public string Text;
        public string To;
        public string Ig;
        public NameValueCollection Params;

        public WebData(string audioUrl, string translateUrl, string text, string to, NameValueCollection @params, string ig) {
            AudioUrl = audioUrl;
            TranslateUrl = translateUrl;
            Text = text;
            To = to;
            Params = @params;
            Ig = ig;
        }
    }
}