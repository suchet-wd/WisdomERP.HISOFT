<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPopUpBarcodeCarton
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
        Me.SBtnOK = New DevExpress.XtraEditors.SimpleButton()
        Me.SBtnExit = New DevExpress.XtraEditors.SimpleButton()
        Me.otlpack = New DevExpress.XtraTreeList.TreeList()
        CType(Me.otlpack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SBtnOK
        '
        Me.SBtnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SBtnOK.Location = New System.Drawing.Point(212, 609)
        Me.SBtnOK.Margin = New System.Windows.Forms.Padding(4)
        Me.SBtnOK.Name = "SBtnOK"
        Me.SBtnOK.Size = New System.Drawing.Size(96, 33)
        Me.SBtnOK.TabIndex = 3
        Me.SBtnOK.Text = "OK"
        '
        'SBtnExit
        '
        Me.SBtnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SBtnExit.Location = New System.Drawing.Point(316, 609)
        Me.SBtnExit.Margin = New System.Windows.Forms.Padding(4)
        Me.SBtnExit.Name = "SBtnExit"
        Me.SBtnExit.Size = New System.Drawing.Size(96, 33)
        Me.SBtnExit.TabIndex = 4
        Me.SBtnExit.Text = "ALL"
        '
        'otlpack
        '
        Me.otlpack.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.otlpack.Location = New System.Drawing.Point(3, -1)
        Me.otlpack.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otlpack.Name = "otlpack"
        Me.otlpack.Size = New System.Drawing.Size(421, 604)
        Me.otlpack.TabIndex = 5
        '
        'wPopUpBarcodeCarton
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(425, 652)
        Me.Controls.Add(Me.otlpack)
        Me.Controls.Add(Me.SBtnExit)
        Me.Controls.Add(Me.SBtnOK)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wPopUpBarcodeCarton"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wPopUpBarcodeCarton"
        CType(Me.otlpack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SBtnOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SBtnExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otlpack As DevExpress.XtraTreeList.TreeList
End Class
