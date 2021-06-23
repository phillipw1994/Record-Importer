using System.Windows.Controls;
using Race.Windows.Ns.JsonSettings.Managers;
using RecordImporter.JsonSettings;
using RecordImporter.Wpf.FileSystem.Pages.Interfaces;
using RecordImporter.Wpf.FileSystem.Pages.ViewModels;
using RecordImporter.Wpf.FileSystem.Pages.ViewModels.Interfaces;

namespace RecordImporter.Wpf.FileSystem.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page, IVmPage
    {
        public INavigationVm Vm { get; set; }

        public MainPage(ISettingsManager<ProgramSettingsFileSystem> settingsManager)
        {
            InitializeComponent();
            DataContext = Vm = new MainPageVm(settingsManager);
        }
    }
}
