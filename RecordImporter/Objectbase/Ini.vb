Option Strict Off
Option Explicit On
Namespace Objectbase
    Public Class Ini
        Public INIFileName As String
        Public INIFileFullName As String
        Public sSectionName As String

        Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
        Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer

        Public Function ReadINI(ByRef Section As Object, ByRef KeyName As Object, ByRef Filename As String) As String
            Try
                Dim sRet As String = ""
                sRet = New String(Chr(0), 255)
                'Laura 2-05-2017 VB6 to .net conversion have changed this as the left function did not work in this case, all that got returned was an empty string
                'what the code below does is strip the vbnullchar out of the string so you are left with the bin number
                Dim i As Integer = GetPrivateProfileString(Section, KeyName, "", sRet, Len(sRet), Filename) 'Old code that can be removed once we are happy with the way in which its working
                ReadINI = sRet.Replace(vbNullChar, "")
            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString())
                'Throw ex
                Return ""
            End Try
        End Function

        Public Function WriteINI(ByRef sSection As String, ByRef sKeyName As String, ByRef sNewString As String, ByRef sFilename As Object) As Short
            Try
                Dim r As Object
                r = WritePrivateProfileString(sSection, sKeyName, sNewString, sFilename)
            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString())
            End Try

        End Function


    End Class
End NameSpace