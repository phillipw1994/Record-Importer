using System.Threading.Tasks;
using System.Windows.Input;
using Race.Windows.Wpf.Base;
using RecordImporter.Wpf.EventDelegates;
using RecordImporter.Wpf.Pages.ViewModels.Interfaces;

namespace RecordImporter.Wpf.Pages.ViewModels
{
    public class MainPageVm : BaseViewModel, INavigationVm
    {
        public event Delegates.NavigateEventArgs Navigate;

        public ICommand SyncButtonCommand { get; }
        public ICommand SettingsButtonCommand { get; }

        private MobileDeviceDataSync MobileDeviceDataSync { get; }

        public MainPageVm()
        {
            //MobileDeviceDataSync = new MobileDeviceDataSync(new List<Location>());
            //SyncButtonCommand = new RelayCommand(async param => { await MobileDeviceDataSync.Sync(); }, param => true);
            //SettingsButtonCommand = new RelayCommand(param =>
            //{
            //    Navigate?.Invoke(this, AppPages.Settings);
            //}, param => true);
        }

        public Task LoadPageAsync()
        {
            return Task.CompletedTask;
        }
    }
}
