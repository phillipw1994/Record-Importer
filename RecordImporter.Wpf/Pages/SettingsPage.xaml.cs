using System.Windows;
using System.Windows.Controls;
using Autofac;
using Race.Windows.Ns.JsonSettings.Managers;
using RecordImporter.JsonSettings;
using RecordImporter.Wpf.Pages.Interfaces;
using RecordImporter.Wpf.Pages.ViewModels;
using RecordImporter.Wpf.Pages.ViewModels.Interfaces;

namespace RecordImporter.Wpf.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page, IVmPage
    {
        public INavigationVm Vm { get; set; }

        public SettingsPage()
        {
            InitializeComponent();
            var settingsManager = App.Container.Resolve<ISettingsManager<ProgramSettings>>();
            DataContext = Vm = new SettingsPageVm(settingsManager);
        }

        private async void SettingsPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            await Vm.LoadPageAsync();
        }
    }
}
