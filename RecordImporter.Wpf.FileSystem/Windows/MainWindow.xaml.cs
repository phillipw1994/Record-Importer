using System;
using System.Windows;
using System.Windows.Media.Animation;
using Race.Windows.Ns.JsonSettings.Managers;
using RecordImporter.JsonSettings;
using RecordImporter.Wpf.FileSystem.Windows.ViewModels;

namespace RecordImporter.Wpf.FileSystem.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(ISettingsManager<ProgramSettingsFileSystem> settingsManager)
        {
            InitializeComponent();
            DataContext = new MainWindowVm(this, settingsManager);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Closing -= Window_Closing;
            e.Cancel = true;
            var anim = new DoubleAnimation(0, TimeSpan.FromMilliseconds(400));
            anim.Completed += (s, _) => Close();
            BeginAnimation(OpacityProperty, anim);
        }
    }
}
