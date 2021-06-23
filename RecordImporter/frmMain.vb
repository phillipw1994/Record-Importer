Imports System.IO
Imports System.Threading
Imports System.IO.IsolatedStorage


Public Class frmMain

    Private myRapi As New OpenNETCF.Desktop.Communication.RAPI()
    Private strPathOnPda1 As String
    Private strPathOnPda2 As String
    Private strPathOnPc1 As String
    Private strFolderName As String
    Private INIFileName As String = "settings.ini"
    Private INIFileFullName As String
    Private IniPath As String
    Private iExportCount As Integer = 0
    Private iImportCount As Integer = 0

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            tmrCheck.Interval = 3000
            tmrCheck.Enabled = True
            lblNotification.Text = "Checking connectivity ..."
            Me.BackColor = RaceGUIObjectsLib.Util.raceBlue

            INIFileFullName = My.Application.Info.DirectoryPath & "\" & INIFileName

            'the path to copy the settings.ini file to
            'decided to put this into a race folder on the c drive due to not being able to modifiy the settings.ini from the
            'program files
            IniPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\R.A.C.E Services Ltd\RecordImporter\settings.ini"

            If Not Directory.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\R.A.C.E Services Ltd\RecordImporter\") Then
                Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\R.A.C.E Services Ltd\RecordImporter\")
            End If

            If File.Exists(IniPath) = False Then
                File.Copy(INIFileFullName, IniPath)
            End If
            'MessageBox.Show()
            strPathOnPda1 = ReadINI("ROOT", "PathOnPDAImport1", IniPath)
            strPathOnPc1 = ReadINI("ROOT", "PathOnDesktop1", IniPath)
            strPathOnPda2 = ReadINI("ROOT", "PathOnPDAImport2", IniPath)
        Catch ex As Exception
            MessageBox.Show("Problem loading program!")
        End Try
    End Sub

    Private Sub tmrCheck_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrCheck.Tick
        'check connetion to device
        If myRapi.DevicePresent = False Then
            lblNotification.Text = "Please dock your PDA."
            iImportCount = 0
        Else
            myRapi.Connect()
            If myRapi.DeviceFileExists(strPathOnPda1) = False Then
                lblNotification.Text = "Device could not be found! Please Redock PDA."
                myRapi.Disconnect()
            Else
                If iImportCount = 0 Then
                    lblNotification.Text = "No Files to Import"
                End If
                Dim _test As OpenNETCF.Desktop.Communication.FileList
                Dim strFullPath As String
                Dim strFullPathWithcsv As String
                strFullPath = ReadINI("ROOT", "PathOnPDAExport", IniPath)
                strFullPathWithcsv = strFullPath & "*csv"
                _test = myRapi.EnumFiles(strFullPathWithcsv)
                If _test.Count <> 0 Then
                    tmrCheck.Enabled = False
                    lblNotification.Text = "Importing..."
                    Dim t = New Thread(AddressOf Import)
                    t.IsBackground = True
                    t.Start()
                End If
                lblExportInfo.Text = "Exporting..."
                Export()
            End If
        End If
    End Sub

    Private Sub MyBackgroundThread()

    End Sub

    Private Function Import() As Boolean
        iImportCount = 0
        Thread.Sleep(20000)
        Try
            Dim _test As OpenNETCF.Desktop.Communication.FileList
            Dim strFullPath As String
            Dim strFullPathWithcsv As String
            Dim INIFileName As String = "settings.ini"

            'Get the settings from the ini file
            strFullPath = ReadINI("ROOT", "PathOnPDAExport", IniPath)
            strFullPathWithcsv = strFullPath & "*csv"
            Dim strImportedPathOnPC As String
            strImportedPathOnPC = ReadINI("ROOT", "PathOnDesktopExport", IniPath)
            _test = myRapi.EnumFiles(strFullPathWithcsv)

            'loop through the file information to get the file names
            Dim _fileName As String
            If _test.Count <> 0 Then
                For i As Integer = 0 To _test.Count
                    Try
                        _fileName = _test.Item(i).FileName.ToString()
                        myRapi.Connect()
                        myRapi.CopyFileFromDevice(strImportedPathOnPC & _fileName, strFullPath & _fileName, True)
                        myRapi.DeleteDeviceFile(strFullPath & _fileName)
                        myRapi.Disconnect()
                        'lblNotification.Text = "Imported Successfully!"
                        iImportCount += 1
                        If (iImportCount <> 0) Then
                            UpdateLabel("Imported " & iImportCount.ToString() & " files successfully from device!")
                        Else
                            UpdateLabel(lblNotification.Text = "No Files To Import!")
                        End If
                    Catch ex As Exception
                    End Try
                Next
            Else
                'UpdateLabel(lblNotification.Text = "Imported " & iImportCount.ToString() & " files successfully from device!")
            End If
            EnableTimer(True)
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try

    End Function
    Private Function Export() As Boolean
        Try
            'this exports the file from PC to scanner :)
            'check that we have a path set for both
            'the first file
            Dim strPathOnDesktop1 As String = ReadINI("ROOT", "PathOnDesktop1", IniPath)
            'check that theres an \ at the end of the string otherwise it cannot find the correct folder
            If strPathOnDesktop1.EndsWith("\") = False Then
                strPathOnDesktop1 = strPathOnDesktop1 & "\"
            End If

            'the Second file
            Dim strPathOnDesktop2 = ReadINI("ROOT", "PathOnDesktop2", IniPath)


            If (strPathOnDesktop1 <> "") Then
                DoExport(strPathOnDesktop1, strPathOnPda1)
            End If

            If (strPathOnDesktop2 <> "") Then
                'check that theres an \ at the end of the string otherwise it cannot find the correct folder
                If strPathOnDesktop2.EndsWith("\") = False Then
                    strPathOnDesktop2 = strPathOnDesktop2 & "\"
                End If
                'MessageBox.Show(strPathOnDesktop2)
                DoExport(strPathOnDesktop2, strPathOnPda2)
            End If
        Catch ex As Exception
            MessageBox.Show("Problem exporting file." & ex.Message.ToString())
            'need to disable the timer if we run into a problem
            EnableTimer(False)
        End Try
    End Function

    Private Sub DoExport(pathOnDesktop As String, pathOnPDA As String)
        Try
            Dim di As New DirectoryInfo(pathOnDesktop)
            Dim fiArr As FileInfo() = di.GetFiles()
            Dim fri As FileInfo
            Dim bHaveFileToExport As Boolean = False
            If fiArr.Length = 0 Then
                lblExportInfo.Text = "No Files to export"
            Else
                For Each fri In fiArr
                    If fri.Extension.Equals(".csv") Then
                        'copy file to device
                        myRapi.CopyFileToDevice(fri.FullName, pathOnPDA & fri.Name)
                        bHaveFileToExport = True
                        'delete the file once exported file to PDA
                        File.Delete(fri.FullName)
                        iExportCount += 1
                    End If
                Next fri
            End If
            If bHaveFileToExport Then
                'we had files to export...
                lblExportInfo.Text = "Exported files successfully to device!"
            Else
                If (iExportCount <> 0) Then
                    lblExportInfo.Text = "Exported " & iExportCount.ToString() & " files successfully to device!"
                Else
                    lblExportInfo.Text = "No Files To Export!"
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) 
        'Export()
    End Sub

    Private Sub pbxLogo_DoubleClick(sender As Object, e As EventArgs) Handles pbxLogo.DoubleClick
        Dim _frmSetting As New frmSettings()
        _frmSetting.ShowDialog()
        _frmSetting.Close()
        _frmSetting = Nothing
    End Sub

    Private Sub UpdateLabel(ByVal text As String)
        If Me.InvokeRequired Then
            Dim args() As String = {text}
            Me.Invoke(New Action(Of String)(AddressOf UpdateLabel), args)
            Return
        End If
        lblNotification.Text = text
    End Sub

    Private Sub EnableTimer(ByVal value As Boolean)
        If Me.InvokeRequired Then
            Dim args() As String = {value}
            Me.Invoke(New Action(Of String)(AddressOf EnableTimer), args)
            Return
        End If
        tmrCheck.Enabled = value
    End Sub

End Class
