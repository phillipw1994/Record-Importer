using System.IO;
using System.Threading.Tasks;
using Race.Windows.Ns.Poller;

namespace RecordImporter.Wpf.FileSystem.Workers
{
    public class ImportWorker : IWorkerAsync
    {
        public string SettingsPathOnPdaImport1 { get; }
        public string SettingsPathOnDesktop1 { get; }
        public event Delegates.WorkerRanEventArgs WorkerRan;
        public event MobileDeviceDataSyncRemovableDisk.SyncFilesDelegate SyncCountUpdate;

        public string WorkerName { get; set; }

        public ImportWorker(string settingsPathOnPdaImport1, string settingsPathOnDesktop1)
        {
            SettingsPathOnPdaImport1 = settingsPathOnPdaImport1;
            SettingsPathOnDesktop1 = settingsPathOnDesktop1;
        }

        public async Task DoWorkAsync()
        {
            await MoveFiles();
        }

        private Task MoveFiles()
        {
            if (!Directory.Exists(SettingsPathOnDesktop1))
                Directory.CreateDirectory(SettingsPathOnDesktop1);
            var di = new DirectoryInfo(SettingsPathOnDesktop1);
            var files = di.GetFiles();
            if (files.Length == 0) return Task.CompletedTask;
            foreach (var file in files)
            {
                file.MoveTo(Path.Combine(SettingsPathOnPdaImport1, file.Name));
            }
            SyncCountUpdate?.Invoke(files.Length.ToString());
            return Task.CompletedTask;
        }
    }
}