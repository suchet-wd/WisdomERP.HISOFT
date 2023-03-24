<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wAccountGroupPopup
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.FTAccountGroupNameEN = New DevExpress.XtraEditors.TextEdit()
        Me.FTAccountGroupNameTH = New DevExpress.XtraEditors.TextEdit()
        Me.FTAccountGroupCode = New DevExpress.XtraEditors.TextEdit()
        Me.FTAccountNameTH_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTAccountNameEN_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTAccountCode_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.btnExit = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.FTStateActive = New DevExpress.XtraEditors.CheckEdit()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.FTAccountGroupNameEN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTAccountGroupNameTH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTAccountGroupCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateActive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl3
        '
        Me.GroupControl3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl3.Controls.Add(Me.FTAccountGroupNameEN)
        Me.GroupControl3.Controls.Add(Me.FTAccountGroupNameTH)
        Me.GroupControl3.Controls.Add(Me.FTAccountGroupCode)
        Me.GroupControl3.Controls.Add(Me.FTAccountNameTH_lbl)
        Me.GroupControl3.Controls.Add(Me.FTAccountNameEN_lbl)
        Me.GroupControl3.Controls.Add(Me.FTAccountCode_lbl)
        Me.GroupControl3.Controls.Add(Me.btnExit)
        Me.GroupControl3.Controls.Add(Me.btnDelete)
        Me.GroupControl3.Controls.Add(Me.FTStateActive)
        Me.GroupControl3.Controls.Add(Me.btnAdd)
        Me.GroupControl3.Controls.Add(Me.btnSave)
        Me.GroupControl3.Location = New System.Drawing.Point(1, 1)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupControl3.MinimumSize = New System.Drawing.Size(632, 235)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(903, 385)
        Me.GroupControl3.TabIndex = 4
        Me.GroupControl3.Text = "Account"
        '
        'FTAccountGroupNameEN
        '
        Me.FTAccountGroupNameEN.Location = New System.Drawing.Point(173, 128)
        Me.FTAccountGroupNameEN.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.FTAccountGroupNameEN.Name = "FTAccountGroupNameEN"
        Me.FTAccountGroupNameEN.Size = New System.Drawing.Size(403, 23)
        Me.FTAccountGroupNameEN.TabIndex = 539
        Me.FTAccountGroupNameEN.Tag = "2|"
        '
        'FTAccountGroupNameTH
        '
        Me.FTAccountGroupNameTH.Location = New System.Drawing.Point(173, 99)
        Me.FTAccountGroupNameTH.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.FTAccountGroupNameTH.Name = "FTAccountGroupNameTH"
        Me.FTAccountGroupNameTH.Size = New System.Drawing.Size(403, 23)
        Me.FTAccountGroupNameTH.TabIndex = 538
        Me.FTAccountGroupNameTH.Tag = "2|"
        '
        'FTAccountGroupCode
        '
        Me.FTAccountGroupCode.Location = New System.Drawing.Point(173, 68)
        Me.FTAccountGroupCode.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.FTAccountGroupCode.Name = "FTAccountGroupCode"
        Me.FTAccountGroupCode.Size = New System.Drawing.Size(178, 23)
        Me.FTAccountGroupCode.TabIndex = 537
        Me.FTAccountGroupCode.Tag = "2|"
        '
        'FTAccountNameTH_lbl
        '
        Me.FTAccountNameTH_lbl.Appearance.Options.UseTextOptions = True
        Me.FTAccountNameTH_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTAccountNameTH_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTAccountNameTH_lbl.Location = New System.Drawing.Point(21, 97)
        Me.FTAccountNameTH_lbl.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.FTAccountNameTH_lbl.Name = "FTAccountNameTH_lbl"
        Me.FTAccountNameTH_lbl.Size = New System.Drawing.Size(146, 26)
        Me.FTAccountNameTH_lbl.TabIndex = 532
        Me.FTAccountNameTH_lbl.Tag = "2|"
        Me.FTAccountNameTH_lbl.Text = "FTAccountNameTH :"
        '
        'FTAccountNameEN_lbl
        '
        Me.FTAccountNameEN_lbl.Appearance.Options.UseTextOptions = True
        Me.FTAccountNameEN_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTAccountNameEN_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTAccountNameEN_lbl.Location = New System.Drawing.Point(21, 124)
        Me.FTAccountNameEN_lbl.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.FTAccountNameEN_lbl.Name = "FTAccountNameEN_lbl"
        Me.FTAccountNameEN_lbl.Size = New System.Drawing.Size(146, 26)
        Me.FTAccountNameEN_lbl.TabIndex = 530
        Me.FTAccountNameEN_lbl.Tag = "2|"
        Me.FTAccountNameEN_lbl.Text = "FTAccountNameEN :"
        '
        'FTAccountCode_lbl
        '
        Me.FTAccountCode_lbl.Appearance.Options.UseTextOptions = True
        Me.FTAccountCode_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTAccountCode_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTAccountCode_lbl.Location = New System.Drawing.Point(21, 66)
        Me.FTAccountCode_lbl.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.FTAccountCode_lbl.Name = "FTAccountCode_lbl"
        Me.FTAccountCode_lbl.Size = New System.Drawing.Size(146, 26)
        Me.FTAccountCode_lbl.TabIndex = 528
        Me.FTAccountCode_lbl.Tag = "2|"
        Me.FTAccountCode_lbl.Text = "FTAccountCode :"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(259, 186)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(87, 36)
        Me.btnExit.TabIndex = 488
        Me.btnExit.Text = "Exit"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(145, 186)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(87, 36)
        Me.btnDelete.TabIndex = 489
        Me.btnDelete.Text = "Delete"
        '
        'FTStateActive
        '
        Me.FTStateActive.EditValue = "0"
        Me.FTStateActive.Location = New System.Drawing.Point(417, 69)
        Me.FTStateActive.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.FTStateActive.Name = "FTStateActive"
        Me.FTStateActive.Properties.Caption = "Status"
        Me.FTStateActive.Properties.ValueChecked = "1"
        Me.FTStateActive.Properties.ValueUnchecked = "0"
        Me.FTStateActive.Size = New System.Drawing.Size(159, 20)
        Me.FTStateActive.TabIndex = 512
        Me.FTStateActive.Tag = "2|"
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Location = New System.Drawing.Point(52, 186)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(87, 36)
        Me.btnAdd.TabIndex = 487
        Me.btnAdd.Text = "Add"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(52, 186)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(87, 36)
        Me.btnSave.TabIndex = 486
        Me.btnSave.Text = "Save"
        '
        'wAccountGroupPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(916, 400)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupControl3)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wAccountGroupPopup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wAccountGroupPopup"
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.FTAccountGroupNameEN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTAccountGroupNameTH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTAccountGroupCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateActive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTAccountCode_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTAccountNameTH_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTAccountNameEN_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTAccountGroupNameEN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTAccountGroupNameTH As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTAccountGroupCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTStateActive As DevExpress.XtraEditors.CheckEdit
End Class
