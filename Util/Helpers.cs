using System;
using System.IO;
using System.Linq;

namespace TranslateDownloader.Util {
    public static class Helpers {
        private static readonly Random Random = new Random();

        public static string Generate32LengthString() {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 32)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public static void CreateDir(string dir) {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Data\" + dir)) {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Data\" + dir);
            }
        }

//        public static void CreateFile(string fileName) {
//            if (!File.Exists(Directory.GetCurrentDirectory() + @"\Data\" + fileName)) {
//                File.Create(Directory.GetCurrentDirectory() + @"\Data\" + fileName + ".txt");
//            }
//        }

        public static bool DataExists(string language, string fileName) {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Data\" + language)) {
                Console.WriteLine(Directory.GetCurrentDirectory() + @"\Data\" + language + "does not exist");
                return false;
            }

            if (!File.Exists(Directory.GetCurrentDirectory() + @"\Data\" + language + @"\" + fileName + ".mp3")) {
                Console.WriteLine(Directory.GetCurrentDirectory() + @"\Data\" + language + @"\" + fileName + ".mp3" + " does not exist as file");
                return false;
            }

            return true;
        }

        public static void AddTranslatedLineToFile(string original, string translated, string targetLanguage) {
//            CreateDir(targetLanguage);
//            if (!File.Exists(Directory.GetCurrentDirectory() + @"\Data\" + targetLanguage + ".txt")) {
//                File.Create(Directory.GetCurrentDirectory() + @"\Data\" + targetLanguage + ".txt");
//            }

            File.AppendAllText(Directory.GetCurrentDirectory() + @"\Data\" + targetLanguage + ".txt",
                $"{original} <-> {translated}\n");
        }
    }
}