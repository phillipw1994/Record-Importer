Module modConfigHandler

    Private key As Microsoft.Win32.RegistryKey

    'NO LONGER USED!!!!!!!
    Public Function GetCopyToPath() As String
        Try
            Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\VB and VBA Program Settings\RecordImporter")
            key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\VB and VBA Program Settings\RecordImporter", True)
            If key.ValueCount = 0 Then
                'key.SetValue("CopyTo", "C:\Talleys\Ashburton\Labeller\Dispatch.txt")
                key.SetValue("CopyTo", "E:\Work\Customers\Talleys\Motueka\CWL\dispatch.txt")
            End If
            Return key.GetValue("CopyTO").ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
            Return ""
        End Try
        End
    End Function
End Module
