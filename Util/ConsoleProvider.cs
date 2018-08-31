using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TranslateDownloader.Util {
    public static class ConsoleProvider {

        private static TextBox _log;

        public static void InitConsole(FrameworkElement window) {
            _log = (TextBox) window.FindName("consoleBox");

        }

        public static void Print(string text) {
            _log.Text += text + "\n";
            Manage();
        }

        private static void Manage() {
            var txt = _log.Text.Split('\n');
            if (txt.Length > 5) {
                _log.Text = txt.Last();
            }
        }

    }
}