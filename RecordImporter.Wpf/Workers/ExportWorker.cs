using System.Threading.Tasks;
using OpenNETCF.Desktop.Communication;
using Race.Windows.Ns.Poller;

namespace RecordImporter.Wpf.Workers
{
    public class ExportWorker : IWorkerAsync
    {
        public event Delegates.WorkerRanEventArgs WorkerRan;

        public string WorkerName { get; set; }
        private RAPI Rapi { get; }

        public ExportWorker()
        {
            Rapi = new RAPI();
        }

        public async Task DoWorkAsync()
        {
            //Rapi.ConnectNew(5, 5);
            //if (Rapi.DevicePresent)
            //    MessageBox.Show("Device Present");
        }

       

        private void ExportData()
        {

        }
    }
}
