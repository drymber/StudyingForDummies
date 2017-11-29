using System;
using System.IO;
using System.Windows;

namespace StudyingForDummies
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new TestsViewModel();

            string baseURL = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            Uri practical_uri = new Uri($"{baseURL}\\practical_txt.html");
            Uri theory_uri = new Uri($"{baseURL}\\theory_txt.html");

            webBrowser1.Navigate(theory_uri);
            webBrowser2.Navigate(practical_uri);
        }
    }
}
