Imports System.IO
Imports GUIObjectsLib

Public Class frmSettings
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load the settings from the settings.ini file
        Dim INIFileName As String = "settings.ini"
        INIFileFullName = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\R.A.C.E Services Ltd\RecordImporter\settings.ini"

        txtPathonDevice.Text = ReadINI("ROOT", "pathonpdaimport1", INIFileFullName)
        txtPathonDevice2.Text = ReadINI("ROOT", "pathonpdaimport2", INIFileFullName)
        txtPCPathImport1.Text = ReadINI("ROOT", "PathOnDesktop1", INIFileFullName)
        txtPCPathImport2.Text = ReadINI("ROOT", "PathOnDesktop2", INIFileFullName)
        txtPathOnDesktopExport.Text = ReadINI("ROOT", "PathOnDesktopExport", INIFileFullName)
        txtPathOnPDAExport.Text = ReadINI("ROOT", "PathOnPDAExport", INIFileFullName)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim INIFileName As String = "settings.ini"
            INIFileFullName = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\R.A.C.E Services Ltd\RecordImporter\settings.ini"
            WriteINI("ROOT", "pathonpdaimport1", txtPathonDevice.Text, INIFileFullName)
            WriteINI("ROOT", "pathonpdaimport2", txtPathonDevice2.Text, INIFileFullName)
            WriteINI("ROOT", "PathOnDesktop1", txtPCPathImport1.Text, INIFileFullName)
            WriteINI("ROOT", "PathOnDesktop2", txtPCPathImport2.Text, INIFileFullName)
            WriteINI("ROOT", "PathOnDesktopExport", txtPathOnDesktopExport.Text, INIFileFullName)
            WriteINI("ROOT", "PathOnPDAExport", txtPathOnPDAExport.Text, INIFileFullName)
            MessageBox.Show("Settings Saved Successfully!", "Saved Settings")
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Problem saving settings to settings.ini, please check that the file exists.", "Error Saving!")
        End Try

    End Sub

    Private Function SaveLocation() As String
        Dim dummyFileName As String = "Save Here"

        Dim sf As SaveFileDialog = New SaveFileDialog()
        'SaveFileDialog sf = New SaveFileDialog();
        ' Feed the dummy name to the save dialog
        sf.FileName = dummyFileName
        If sf.ShowDialog() = DialogResult.OK Then
        End If
        'If (sf.ShowDialog() == DialogResult.OK) Then
        '    {
        'Now here's our save folder
        Dim savePath As String = Path.GetDirectoryName(sf.FileName)
        Return savePath
    End Function
    Private Sub NiceBtnPCPathImport1__EvtClicked(nbButton As NiceButton) Handles NiceBtnPCPathImport1._EvtClicked
        Dim savePath As String = SaveLocation()
        If savePath <> "" Then
            txtPCPathImport1.Text = savePath
        End If
    End Sub

    Private Sub NiceBtnPCPathImport2__EvtClicked(nbButton As NiceButton) Handles NiceBtnPCPathImport2._EvtClicked
        Dim savePath As String = SaveLocation()
        If savePath <> "" Then
            txtPCPathImport2.Text = savePath
        End If
    End Sub

    Private Sub NiceBtnPathOnDesktopExport__EvtClicked(nbButton As NiceButton) Handles NiceBtnPathOnDesktopExport._EvtClicked
        Dim savePath As String = SaveLocation()
        If savePath <> "" Then
            txtPathOnDesktopExport.Text = savePath
        End If

    End Sub
End Class