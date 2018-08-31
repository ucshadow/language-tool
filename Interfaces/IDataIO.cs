namespace TranslateDownloader.Interfaces {
    public interface IDataIO {
        void WriteData(string text, string language);
        string[] ReadData(string language);
        bool DataExists(string text, string language);
    }
}