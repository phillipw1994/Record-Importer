using System.IO;
using System.Threading.Tasks;
using Race.Windows.Ns.Poller;

namespace RecordImporter.Wpf.FileSystem.Workers
{
    public class ExportWorker : IWorkerAsync
    {
        public string SettingsPathOnPdaExport { get; }
        public string SettingsPathOnDesktopExport { get; }

        public event MobileDeviceDataSyncRemovableDisk.SyncFilesDelegate SyncCountUpdate;

        public string WorkerName { get; set; }
        public event Delegates.WorkerRanEventArgs WorkerRan;

        public ExportWorker(string settingsPathOnPdaExport, string settingsPathOnDesktopExport)
        {
            SettingsPathOnPdaExport = settingsPathOnPdaExport;
            SettingsPathOnDesktopExport = settingsPathOnDesktopExport;
        }

        public async Task DoWorkAsync()
        {
            await MoveFiles();
        }

        private Task MoveFiles()
        {
            if (!Directory.Exists(SettingsPathOnDesktopExport))
                Directory.CreateDirectory(SettingsPathOnDesktopExport);
            var di = new DirectoryInfo(SettingsPathOnPdaExport);
            var files = di.GetFiles();
            if (files.Length == 0) return Task.CompletedTask;
            foreach (var file in files)
            {
                file.MoveTo(Path.Combine(SettingsPathOnDesktopExport, file.Name));
            }
            SyncCountUpdate?.Invoke(files.Length.ToString());
            return Task.CompletedTask;
        }
    }
}
