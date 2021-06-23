<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.tmrCheck = New System.Windows.Forms.Timer(Me.components)
        Me.lblNotification = New System.Windows.Forms.Label()
        Me.pbxLogo = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblExportInfo = New System.Windows.Forms.Label()
        CType(Me.pbxLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tmrCheck
        '
        '
        'lblNotification
        '
        Me.lblNotification.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotification.ForeColor = System.Drawing.Color.White
        Me.lblNotification.Location = New System.Drawing.Point(3, -1)
        Me.lblNotification.Name = "lblNotification"
        Me.lblNotification.Size = New System.Drawing.Size(472, 88)
        Me.lblNotification.TabIndex = 0
        '
        'pbxLogo
        '
        Me.pbxLogo.Image = Global.RecordImporter.My.Resources.Resources.RaceLogo_New
        Me.pbxLogo.Location = New System.Drawing.Point(313, 205)
        Me.pbxLogo.Name = "pbxLogo"
        Me.pbxLogo.Size = New System.Drawing.Size(177, 68)
        Me.pbxLogo.TabIndex = 2
        Me.pbxLogo.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 137)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 13)
        Me.Label1.TabIndex = 3
        '
        'lblExportInfo
        '
        Me.lblExportInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExportInfo.ForeColor = System.Drawing.Color.White
        Me.lblExportInfo.Location = New System.Drawing.Point(3, 87)
        Me.lblExportInfo.Name = "lblExportInfo"
        Me.lblExportInfo.Size = New System.Drawing.Size(472, 88)
        Me.lblExportInfo.TabIndex = 4
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(487, 270)
        Me.Controls.Add(Me.lblExportInfo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pbxLogo)
        Me.Controls.Add(Me.lblNotification)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Record Importer"
        CType(Me.pbxLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tmrCheck As System.Windows.Forms.Timer
    Friend WithEvents lblNotification As System.Windows.Forms.Label
    Friend WithEvents pbxLogo As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblExportInfo As Label
End Class
