using System.IO;
using TranslateDownloader.Interfaces;
using TranslateDownloader.Util;

namespace TranslateDownloader.Workers {
    public class FileSaver : IFileSaver {
        
        public void SaveAudioFile(string filename, string path, byte[] data) {
            Helpers.CreateDir(path);
            File.WriteAllBytes(Directory.GetCurrentDirectory() + @"\Data\" + path + @"\" + $"{filename}.mp3", data);
        }
        
        public void SaveImageFile(byte[] data, string name, int number) {
            Helpers.CreateDir("Images");
            File.WriteAllBytes(Directory.GetCurrentDirectory() + @"\Data\Images\" + $"{name}_{number}.png", data);
        }
    }
}