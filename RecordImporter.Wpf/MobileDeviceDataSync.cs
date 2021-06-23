using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OpenNETCF.Desktop.Communication;
using Race.Windows.Ns.Poller;
using RecordImporter.Model;
using RecordImporter.Wpf.Workers;

namespace RecordImporter.Wpf
{
    public class MobileDeviceDataSync
    {
        public event DeviceConnectedEventArgs DeviceConnected;

        public delegate void DeviceConnectedEventArgs();

        private RAPI MyRapi { get; }
        private List<Location> Locations { get; }
        private IWorkerAsync ExportWorker { get; }
        private IWorkerAsync ImportWorker { get; }
        public ManualResetEvent ShutdownEvent { get; }

        private DateTime ExportWorkerSyncLastRan { get; set; }

        //private DateTime DatabaseTidyUpLastRan { get; set; }
        //private DateTime WorkOrderSyncLastRan { get; set; }
        private RAPI Rapi { get; }

        private string strPathOnPda2;
        private string strFolderName;
        private string INIFileName = "settings.ini";
        private string INIFileFullName;
        private string IniPath;
        private int iExportCount;

        public MobileDeviceDataSync(List<Location> locations)
        {
            Locations = locations;
            ShutdownEvent = new ManualResetEvent(false);
            //Rapi = new RAPI();
            ExportWorker = new ExportWorker();
            Sync().Wait();
        }

        private Task RapiConnect()
        {
            try
            {
                Rapi.ConnectNew(5, 5);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.CompletedTask;
            }
        }

        public async Task Sync()
        {
            try
            {
                //await RapiConnect();
                //await ExportWorker.DoWorkAsync();
                //await ImportWorker.DoWorkAsync();

                while (!ShutdownEvent.WaitOne(1000)) // check every second to see if there is any work tasks to start
                {
                    try
                    {
                        if (ExportWorkerSyncLastRan.AddMinutes(TimeSpan.FromMinutes(1).TotalMinutes) < DateTime.Now)
                        {
                            await ExportWorker.DoWorkAsync();
                            ExportWorkerSyncLastRan = DateTime.Now;
                        }
                    }
                    catch (Exception ex)
                    {
                        ExportWorkerSyncLastRan = DateTime.Now;
                        //AppService.Container.Resolve<IExceptionManager>().Log(nameof(PalletLabelSyncWorker), nameof(PalletLabelSyncWorker), ex);
                    }

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
                }
            }
            catch (Exception ex)
            {

            }
        }

        public Task Shutdown()
        {
            ShutdownEvent.Set();
            return Task.CompletedTask;
        }

        private bool DevicePresent()
        {
            return MyRapi.DevicePresent;
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
