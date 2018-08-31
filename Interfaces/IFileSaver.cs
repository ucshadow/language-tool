namespace TranslateDownloader.Interfaces {
    public interface IFileSaver {
        void SaveAudioFile(string filename, string path, byte[] data);
    }
}