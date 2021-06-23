using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortableDevices;
using RecordImporter.Model;

namespace RecordImporter.Wpf
{
    public class PortableDeviceDataSync
    {
        //public event DeviceConnectedEventArgs DeviceConnected;

        //public delegate void DeviceConnectedEventArgs();
        private PortableDeviceCollection ConnectedDevices { get; set; }

        private PortableDevice TargetDevice { get; set; }

        //private List<Location> Locations { get; }
        //private IWorkerAsync ExportWorker { get; }
        //private IWorkerAsync ImportWorker { get; }
        //public ManualResetEvent ShutdownEvent { get; }

        //private DateTime ExportWorkerSyncLastRan { get; set; }

        //private DateTime DatabaseTidyUpLastRan { get; set; }
        //private DateTime WorkOrderSyncLastRan { get; set; }

        //private string strPathOnPda2;
        //private string strFolderName;
        //private string INIFileName = "settings.ini";
        //private string INIFileFullName;
        //private string IniPath;
        //private int iExportCount;

        public PortableDeviceDataSync(List<Location> locations)
        {
            //Locations = locations;
            //ShutdownEvent = new ManualResetEvent(false);
            //ExportWorker = new ExportWorker();
            ConnectedDevices = new PortableDeviceCollection();
            RefreshDevices();
            Sync().Wait();
        }

        public PortableDeviceCollection GetDevices()
        {
            RefreshDevices();
            return ConnectedDevices;
        }

        public List<string> GetStorageFolders(string deviceName)
        {
            var storageFolders = new List<string>();
            var device = FindDevice(deviceName);
            if (device == null) return null;
            device.Connect();
            device.GetContents(deviceName);
            if (device.Root.Files == null || device.Root.Files.Count < 1)
                throw new Exception("-No Directories or files under path");
            foreach (var folder in device.Root.Files.Where(f => f is PortableDeviceFolder))
            {
                storageFolders.Add(folder.Name);

            }
            return storageFolders;
        }

        private void RefreshDevices()
        {
            ConnectedDevices.Clear();
            ConnectedDevices.Refresh();
        }

        private PortableDevice FindDevice(string name)
        {
            return TargetDevice = ConnectedDevices.FirstOrDefault(d => d.DeviceDescription == name || d.DeviceFriendlyName == name);
        }

        private void GetDeviceDetails()
        {
            TargetDevice.Connect();
            var name = TargetDevice.FriendlyName;
        }

        public bool CheckPathExists(string path)
        {
            //Remove Device Qualifier
            var newPath = path.Replace("CT60\\", "");

            TargetDevice.GetContents("CT60");

            if (TargetDevice.Root.Files == null || TargetDevice.Root.Files.Count < 1)
                throw new Exception("-No Directories or files under path");
            var folder = TargetDevice.Root;
            while (!string.IsNullOrEmpty(newPath))
            {
                var result = GetNextPart(newPath);

                var nextPart = result[0];
                newPath = result[1];
                folder = folder.Files.FirstOrDefault(f => f.Name.ToLower() == nextPart.ToLower() && f is PortableDeviceFolder) as PortableDeviceFolder;
                if (folder == null)
                    return false;
                TargetDevice.GetContents(ref folder);
            }

            return folder != null;
        }

        //var nextPart = newPath.Substring(0, newPath.IndexOf('\\', 0));
        //newPath = newPath.Remove(0, newPath.IndexOf('\\', 0));
        //newPath = newPath.TrimStart('\\');




        //result = GetNextPart(newPath);
        //nextPart = result[0];
        //newPath = result[1];

        //nextPart = newPath.Substring(0, newPath.IndexOf('\\', 0));
        //newPath = newPath.Remove(0, newPath.IndexOf('\\', 0));
        //newPath = newPath.TrimStart('\\');

        //var softwareFolder = storageInterface?.Files.FirstOrDefault(f => f.Name.ToLower() == nextPart.ToLower() && f is PortableDeviceFolder) as PortableDeviceFolder;

        //TargetDevice.GetContents(ref softwareFolder);

        //nextPart = newPath.Substring(0, newPath.IndexOf('\\', 0));
        //newPath = newPath.Remove(0, newPath.IndexOf('\\', 0));
        //newPath = newPath.TrimStart('\\');

        //result = GetNextPart(newPath);
        //nextPart = result[0];
        //newPath = result[1];

        //var companyFolder = softwareFolder?.Files.FirstOrDefault(f => f.Name.ToLower() == nextPart.ToLower() && f is PortableDeviceFolder) as PortableDeviceFolder;

        //TargetDevice.GetContents(ref companyFolder);

        //nextPart = newPath.Substring(0, newPath.IndexOf('\\', 0));
        //newPath = newPath.Remove(0, newPath.IndexOf('\\', 0));
        //newPath = newPath.TrimStart('\\');

        //result = GetNextPart(newPath);
        //nextPart = result[0];
        //newPath = result[1];

        //var importFolder = companyFolder?.Files.FirstOrDefault(f => f.Name.ToLower() == nextPart.ToLower() && f is PortableDeviceFolder) as PortableDeviceFolder;

        public string[] GetNextPart(string path)
        {
            var nextPart = path.Substring(0, path.IndexOf('\\', 0));
            path = path.Remove(0, path.IndexOf('\\', 0));
            path = path.TrimStart('\\');
            return new[] { nextPart, path };
        }

        public void PopulateView()
        {
            //Below is an example of how file structures should work

            //Gets a list of each level of folders
            //Device
            // -FlashDisk
            // -Network
            // -/

            //Expand FlashDisk
            //Device
            // -FlashDisk
            // --Race_Software
            // -Network
            // -/
        }

        public async Task Sync()
        {
            try
            {
                RefreshDevices();
                if (ConnectedDevices.Count < 1) return;
                FindDevice("CT60");
                GetDeviceDetails();
                CheckPathExists(@"CT60\SD CARD\Race_Software\AltoPackaging\Import\");

                //await RapiConnect();
                //await ExportWorker.DoWorkAsync();
                //await ImportWorker.DoWorkAsync();

                //while (!ShutdownEvent.WaitOne(1000)) // check every second to see if there is any work tasks to start
                //{
                //    try
                //    {
                //        if (ExportWorkerSyncLastRan.AddMinutes(TimeSpan.FromMinutes(1).TotalMinutes) < DateTime.Now)
                //        {
                //            await ExportWorker.DoWorkAsync();
                //            ExportWorkerSyncLastRan = DateTime.Now;
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        ExportWorkerSyncLastRan = DateTime.Now;
                //        //AppService.Container.Resolve<IExceptionManager>().Log(nameof(PalletLabelSyncWorker), nameof(PalletLabelSyncWorker), ex);
                //    }

                //try
                //{
                //    if (PalletLabelSyncLastRan.AddSeconds(TimeSpan.FromSeconds(20).TotalSeconds) < DateTime.Now)
                //    {
                //        PalletLabelWorker.DoWork();
                //        PalletLabelSyncLastRan = DateTime.Now;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    AppService.Container.Resolve<IExceptionManager>().Log(nameof(PalletLabelSyncWorker), nameof(PalletLabelSyncWorker), ex);
                //}

                //try
                //{
                //    if (DatabaseTidyUpLastRan.AddMinutes(TimeSpan.FromDays(2).TotalMinutes) < DateTime.Now)
                //    {
                //        DatabaseTidyUpWorker.DoWorkAsync();
                //        DatabaseTidyUpLastRan = DateTime.Now;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    AppService.Container.Resolve<IExceptionManager>().Log(nameof(PalletLabelSyncWorker), nameof(PalletLabelSyncWorker), ex);
                //}
                //    }
            }
            catch (Exception ex)
            {

            }
        }

        public Task Shutdown()
        {
            //ShutdownEvent.Set();
            return Task.CompletedTask;
        }


        //    private void load()
        //    {
        //        try
        //        {
        //            tmrCheck.Interval = 3000;
        //            tmrCheck.Enabled = True;
        //            lblNotification.Text = "Checking connectivity ...";

        //            INIFileFullName = My.Application.Info.DirectoryPath & @"\" & INIFileName;

        //            //the path to copy the settings.ini file to
        //            //decided to put this into a race folder on the c drive due to not being able to modifiy the settings.ini
        //            //    from the
        //            //program files
        //            IniPath =

        //                $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\R.A.C.E Services Ltd\RecordImporter\settings.ini";

        //            if (!Directory.Exists(
        //                $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\R.A.C.E Services Ltd\RecordImporter\");
        //            {
        //                Directory.CreateDirectory($@"{
        //                        Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
        //                    }\R.A.C.E Services Ltd\RecordImporter\");
        //            }

        //            if (!File.Exists(IniPath))
        //            {
        //                File.Copy(INIFileFullName, IniPath);
        //            }

        //            strPathOnPda1 = ReadINI("ROOT", "PathOnPDAImport1", IniPath);
        //            ReadINI("ROOT", "PathOnDesktop1", IniPath);
        //            strPathOnPda2 = ReadINI("ROOT", "PathOnPDAImport2", IniPath);

        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Problem loading program!");
        //        }
        //    }

        //    private void tmrCheck_Tick(object sender, EventArgs e)
        //    {
        //        //check connetion to device
        //        if (_myRapi.DevicePresent == false)
        //        {
        //            lblNotification.Text = "Please dock your PDA.";
        //            iImportCount = 0;
        //        }
        //        else
        //        {
        //            _myRapi.Connect();
        //            if (_myRapi.DeviceFileExists(strPathOnPda1) == false)
        //            {
        //                lblNotification.Text = "Device could not be found! Please Redock PDA.";
        //                _myRapi.Disconnect();
        //            }
        //            else
        //            {
        //                if (iImportCount == 0)
        //                {
        //                    lblNotification.Text = "No Files to Import"
        //                }
        //                Dim _test As OpenNETCF.Desktop.Communication.FileList
        //                    Dim strFullPath As String
        //                    Dim strFullPathWithcsv As String
        //                    strFullPath = ReadINI("ROOT", "PathOnPDAExport", IniPath)
        //                strFullPathWithcsv = strFullPath & "*csv"
        //                _test = _myRapi.EnumFiles(strFullPathWithcsv)
        //                If _test.Count <> 0 Then
        //                tmrCheck.Enabled = False
        //                lblNotification.Text = "Importing..."
        //                Dim t = New Thread(AddressOf Import)
        //                t.IsBackground = True
        //                t.Start()
        //                End If
        //                lblExportInfo.Text = "Exporting..."
        //                Export()
        //            }
        //            End If
        //        }
        //    }

        //    private void Import()
        //    {
        //        Thread.Sleep(20000);
        //        try
        //        {
        //            //Get the settings from the ini file
        //            string strFullPath = ReadINI("ROOT", "PathOnPDAExport", IniPath);
        //            var strFullPathWithCsv = $"{strFullPath}*csv";
        //            string strImportedPathOnPc = ReadINI("ROOT", "PathOnDesktopExport", IniPath);
        //            var test = _myRapi.EnumFiles(strFullPathWithcsv);

        //            //loop through the file information to get the file names
        //            if (test.Count == 0) return;
        //            for (var i = 0; i <= test.Count; i++)
        //            {
        //                try
        //                {
        //                    var fileName = test[i].FileName.ToString();
        //                    _myRapi.Connect();
        //                    _myRapi.CopyFileFromDevice($"{strImportedPathOnPc}{fileName}", $"{strFullPath}{fileName}", true);
        //                    _myRapi.DeleteDeviceFile($"{strFullPath}{fileName}");
        //                    _myRapi.Disconnect();
        //                }
        //                catch (Exception ex)
        //                {
        //                    // ignored
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message.ToString());
        //        }
        //    }
        //    private bool Export()
        //    {
        //        try
        //        {
        //            //this exports the file from PC to scanner :)
        //            //check that we have a path set for both
        //            //the first file
        //            var strPathOnDesktop1 = ReadINI("ROOT", "PathOnDesktop1", IniPath);
        //            //check that theres an \ at the end of the string otherwise it cannot find the correct folder
        //            if (strPathOnDesktop1.EndsWith(@"\") == false)
        //            {
        //                strPathOnDesktop1 = $@"{strPathOnDesktop1}\";
        //            }

        //            //the Second file
        //            var strPathOnDesktop2 = ReadINI("ROOT", "PathOnDesktop2", IniPath);


        //            if (strPathOnDesktop1 != "")
        //            {
        //                DoExport(strPathOnDesktop1, strPathOnPda1);
        //            }

        //            if (strPathOnDesktop2 != "")
        //            //check that theres an \ at the end of the string otherwise it cannot find the correct folder
        //            {
        //                if (strPathOnDesktop2.EndsWith(@"\") == false)
        //                {
        //                    strPathOnDesktop2 = $@"{strPathOnDesktop2}\";
        //                    //MessageBox.Show(strPathOnDesktop2)
        //                    DoExport(strPathOnDesktop2, strPathOnPda2);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Problem exporting file.{ex.Message.ToString()}");
        //            //need to disable the timer if we run into a problem
        //        }
        //    }

        //    private void DoExport(string pathOnDesktop, string pathOnPDA)
        //    {
        //        Try
        //            Dim di As New DirectoryInfo(pathOnDesktop)
        //        Dim fiArr As FileInfo() =
        //    di.GetFiles()
        //        Dim fri As FileInfo
        //        Dim bHaveFileToExport As Boolean = False
        //        If fiArr.Length = 0 Then
        //        lblExportInfo.Text = "No Files to export"
        //        Else
        //        For Each fri In fiArr
        //        If fri.Extension.Equals(".csv") Then
        //        'copy file to device
        //        _myRapi.CopyFileToDevice(fri.FullName, pathOnPDA & fri.Name)
        //        bHaveFileToExport = True
        //        'delete the file once exported file to PDA
        //        File.Delete(fri.FullName)
        //        iExportCount += 1
        //        End If
        //        Next fri
        //        End If
        //        If bHaveFileToExport Then
        //        'we had files to export...
        //        lblExportInfo.Text = "Exported files successfully to device!"
        //        Else
        //        If(iExportCount < > 0) Then
        //        lblExportInfo.Text = "Exported " & iExportCount.ToString() & " files successfully to device!"


        //    Else
        //        lblExportInfo.Text = "No Files To Export!"


        //    End If


        //    End If


        //    Catch ex As Exception


        //    Throw ex


        //    End Try


        //}

        //    private void Button1_Click(object sender, EventArgs e)
        //    {
        //        //'Export()
        //    }

        //    private void pbxLogo_DoubleClick(object sender, EventArgs e)
        //    {
        //        Dim _frmSetting As New frmSettings()
        //        _frmSetting.ShowDialog()
        //        _frmSetting.Close()
        //        _frmSetting = Nothing
        //    }
    }
}
