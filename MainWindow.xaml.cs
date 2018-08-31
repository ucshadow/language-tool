using System.Windows;
using TranslateDownloader.Loaders;

namespace TranslateDownloader {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            new Controller(this);
        }
    }
}
