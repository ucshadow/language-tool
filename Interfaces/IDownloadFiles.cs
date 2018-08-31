namespace TranslateDownloader.Interfaces {
    public interface IDownloadFiles {
        void DownloadOriginalLanguage();
        void DownloadTargetLanguage(string text);
    }
}