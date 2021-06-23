Imports GUIObjectsLib

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.NiceBtnPCPathImport1 = New NiceButton()
        Me.txtPCPathImport2 = New System.Windows.Forms.TextBox()
        Me.txtPathonDevice2 = New System.Windows.Forms.TextBox()
        Me.txtPCPathImport1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPathonDevice = New System.Windows.Forms.TextBox()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtPathOnDesktopExport = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPathOnPDAExport = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NiceBtnPCPathImport2 = New NiceButton()
        Me.NiceBtnPathOnDesktopExport = New NiceButton()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(310, 239)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Close"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(12, 239)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.NiceBtnPCPathImport2)
        Me.GroupBox1.Controls.Add(Me.NiceBtnPCPathImport1)
        Me.GroupBox1.Controls.Add(Me.txtPCPathImport2)
        Me.GroupBox1.Controls.Add(Me.txtPathonDevice2)
        Me.GroupBox1.Controls.Add(Me.txtPCPathImport1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtPathonDevice)
        Me.GroupBox1.Controls.Add(Me.lbl1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(373, 122)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Import Settings"
        '
        'NiceBtnPCPathImport1
        '
        Me.NiceBtnPCPathImport1._ButtonText = "..."
        Me.NiceBtnPCPathImport1._XOverAngle = CType(-30, Short)
        Me.NiceBtnPCPathImport1.BackColor = System.Drawing.Color.Transparent
        Me.NiceBtnPCPathImport1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NiceBtnPCPathImport1.Location = New System.Drawing.Point(343, 72)
        Me.NiceBtnPCPathImport1.Name = "NiceBtnPCPathImport1"
        Me.NiceBtnPCPathImport1.Size = New System.Drawing.Size(24, 20)
        Me.NiceBtnPCPathImport1.TabIndex = 8
        '
        'txtPCPathImport2
        '
        Me.txtPCPathImport2.Location = New System.Drawing.Point(143, 96)
        Me.txtPCPathImport2.Name = "txtPCPathImport2"
        Me.txtPCPathImport2.Size = New System.Drawing.Size(197, 20)
        Me.txtPCPathImport2.TabIndex = 10
        '
        'txtPathonDevice2
        '
        Me.txtPathonDevice2.Enabled = False
        Me.txtPathonDevice2.Location = New System.Drawing.Point(143, 46)
        Me.txtPathonDevice2.Name = "txtPathonDevice2"
        Me.txtPathonDevice2.Size = New System.Drawing.Size(197, 20)
        Me.txtPathonDevice2.TabIndex = 9
        '
        'txtPCPathImport1
        '
        Me.txtPCPathImport1.Location = New System.Drawing.Point(143, 72)
        Me.txtPCPathImport1.Name = "txtPCPathImport1"
        Me.txtPCPathImport1.Size = New System.Drawing.Size(197, 20)
        Me.txtPCPathImport1.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "PC path to store Files:"
        '
        'txtPathonDevice
        '
        Me.txtPathonDevice.Enabled = False
        Me.txtPathonDevice.Location = New System.Drawing.Point(143, 24)
        Me.txtPathonDevice.Name = "txtPathonDevice"
        Me.txtPathonDevice.Size = New System.Drawing.Size(197, 20)
        Me.txtPathonDevice.TabIndex = 6
        '
        'lbl1
        '
        Me.lbl1.AutoSize = True
        Me.lbl1.Location = New System.Drawing.Point(6, 24)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(123, 13)
        Me.lbl1.TabIndex = 5
        Me.lbl1.Text = "Csv files path on device:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.NiceBtnPathOnDesktopExport)
        Me.GroupBox2.Controls.Add(Me.txtPathOnDesktopExport)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtPathOnPDAExport)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 140)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(373, 83)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Export Settings"
        '
        'txtPathOnDesktopExport
        '
        Me.txtPathOnDesktopExport.Location = New System.Drawing.Point(143, 55)
        Me.txtPathOnDesktopExport.Name = "txtPathOnDesktopExport"
        Me.txtPathOnDesktopExport.Size = New System.Drawing.Size(197, 20)
        Me.txtPathOnDesktopExport.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "PC file path:"
        '
        'txtPathOnPDAExport
        '
        Me.txtPathOnPDAExport.Enabled = False
        Me.txtPathOnPDAExport.Location = New System.Drawing.Point(143, 21)
        Me.txtPathOnPDAExport.Name = "txtPathOnPDAExport"
        Me.txtPathOnPDAExport.Size = New System.Drawing.Size(197, 20)
        Me.txtPathOnPDAExport.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(132, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Exported file path on PDA:"
        '
        'NiceBtnPCPathImport2
        '
        Me.NiceBtnPCPathImport2._ButtonText = "..."
        Me.NiceBtnPCPathImport2._XOverAngle = CType(-30, Short)
        Me.NiceBtnPCPathImport2.BackColor = System.Drawing.Color.Transparent
        Me.NiceBtnPCPathImport2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NiceBtnPCPathImport2.Location = New System.Drawing.Point(343, 96)
        Me.NiceBtnPCPathImport2.Name = "NiceBtnPCPathImport2"
        Me.NiceBtnPCPathImport2.Size = New System.Drawing.Size(24, 20)
        Me.NiceBtnPCPathImport2.TabIndex = 11
        '
        'NiceBtnPathOnDesktopExport
        '
        Me.NiceBtnPathOnDesktopExport._ButtonText = "..."
        Me.NiceBtnPathOnDesktopExport._XOverAngle = CType(-30, Short)
        Me.NiceBtnPathOnDesktopExport.BackColor = System.Drawing.Color.Transparent
        Me.NiceBtnPathOnDesktopExport.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NiceBtnPathOnDesktopExport.Location = New System.Drawing.Point(343, 55)
        Me.NiceBtnPathOnDesktopExport.Name = "NiceBtnPathOnDesktopExport"
        Me.NiceBtnPathOnDesktopExport.Size = New System.Drawing.Size(24, 20)
        Me.NiceBtnPathOnDesktopExport.TabIndex = 12
        '
        'frmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(397, 267)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Button1)
        Me.Name = "frmSettings"
        Me.Text = "Settings"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtPCPathImport1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtPathonDevice As TextBox
    Friend WithEvents lbl1 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtPathOnDesktopExport As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtPathOnPDAExport As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtPathonDevice2 As TextBox
    Friend WithEvents txtPCPathImport2 As TextBox
    Friend WithEvents NiceBtnPCPathImport1 As NiceButton
    Friend WithEvents NiceBtnPCPathImport2 As NiceButton
    Friend WithEvents NiceBtnPathOnDesktopExport As NiceButton
End Class
