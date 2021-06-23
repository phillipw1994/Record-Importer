using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shell;
using Race.Windows.Ns.JsonSettings.Managers;
using Race.Windows.Wpf.Base;
using RecordImporter.JsonSettings;
using RecordImporter.Wpf.FileSystem.EventDelegates;
using RecordImporter.Wpf.FileSystem.Pages.ViewModels.Interfaces;

namespace RecordImporter.Wpf.FileSystem.Pages.ViewModels
{
    public class MainPageVm : BaseViewModel, INavigationVm
    {
        public event Delegates.NavigateEventArgs Navigate;
        public event Delegates.TaskbarIconUpdateEventArgs TaskbarIconUpdate;

        public ICommand SyncButtonCommand { get; }
        public ICommand StopSyncingButtonCommand { get; }

        public string FilesFromPdaSynced { get; set; }
        public string FilesToPdaSynced { get; set; }

        private MobileDeviceDataSyncRemovableDisk MobileDeviceDataSyncRemovableDisk { get; }

        public bool SyncRunning { get; set; }

        public MainPageVm(ISettingsManager<ProgramSettingsFileSystem> settingsManager)
        {
            var settings = settingsManager.Load();
            FilesFromPdaSynced = "0 files synced";
            FilesToPdaSynced = "0 files synced";
            MobileDeviceDataSyncRemovableDisk = new MobileDeviceDataSyncRemovableDisk(settings);
            MobileDeviceDataSyncRemovableDisk.FromPdaSyncCount += MobileDeviceDataSyncRemovableDisk_FromPdaSyncCount;
            MobileDeviceDataSyncRemovableDisk.ToPdaSyncCount += MobileDeviceDataSyncRemovableDisk_ToPdaSyncCount; ;
            SyncButtonCommand = new RelayCommand(param =>
            {
                TaskbarIconUpdate?.Invoke(this, TaskbarItemProgressState.Indeterminate, 100);
                FilesFromPdaSynced = "0 files synced";
                FilesToPdaSynced = "0 files synced";
                SyncRunning = true;
                MobileDeviceDataSyncRemovableDisk.StartSync();
            }, param => !SyncRunning);
            StopSyncingButtonCommand = new RelayCommand(param =>
                {
                    TaskbarIconUpdate?.Invoke(this, TaskbarItemProgressState.Error, 100);
                    MobileDeviceDataSyncRemovableDisk.Shutdown();
                    SyncRunning = false;
                }, param => SyncRunning);
        }

        private void MobileDeviceDataSyncRemovableDisk_ToPdaSyncCount(string fileCount)
        {
            FilesToPdaSynced = $"{fileCount} files synced";
        }

        private void MobileDeviceDataSyncRemovableDisk_FromPdaSyncCount(string fileCount)
        {
            FilesFromPdaSynced = $"{fileCount} files synced";
        }

        public Task LoadPageAsync()
        {
            return Task.CompletedTask;
        }
    }
}
