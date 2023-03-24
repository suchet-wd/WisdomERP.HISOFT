<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAddEditPIDes
    Inherits DevExpress.XtraEditors.XtraForm

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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmedit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmaddnew = New DevExpress.XtraEditors.SimpleButton()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNHSysMainMatId_No = New DevExpress.XtraEditors.LabelControl()
        Me.FTRemark_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRemark = New DevExpress.XtraEditors.MemoEdit()
        Me.FNHSysMainMatId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysMainMatId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTMainMatNameEN = New DevExpress.XtraEditors.TextEdit()
        Me.FTMainMatNameEN_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTMainMatNameTH = New DevExpress.XtraEditors.TextEdit()
        Me.FTMainMatNameTH_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStateActive = New DevExpress.XtraEditors.CheckEdit()
        Me.FNHSysMainMatId = New DevExpress.XtraEditors.ButtonEdit()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbutton.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysMainMatId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTMainMatNameEN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTMainMatNameTH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateActive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysMainMatId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbbutton
        '
        Me.ogbbutton.Controls.Add(Me.ocmedit)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Controls.Add(Me.ocmclear)
        Me.ogbbutton.Controls.Add(Me.ocmdelete)
        Me.ogbbutton.Controls.Add(Me.ocmaddnew)
        Me.ogbbutton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ogbbutton.Location = New System.Drawing.Point(0, 202)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.Size = New System.Drawing.Size(540, 42)
        Me.ogbbutton.TabIndex = 4
        '
        'ocmedit
        '
        Me.ocmedit.Location = New System.Drawing.Point(12, -11)
        Me.ocmedit.Name = "ocmedit"
        Me.ocmedit.Size = New System.Drawing.Size(95, 25)
        Me.ocmedit.TabIndex = 101
        Me.ocmedit.Tag = "2|"
        Me.ocmedit.Text = "EDIT"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(433, 10)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 100
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(108, 11)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(95, 25)
        Me.ocmclear.TabIndex = 99
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(204, 11)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(95, 25)
        Me.ocmdelete.TabIndex = 98
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "DELETE"
        '
        'ocmaddnew
        '
        Me.ocmaddnew.Location = New System.Drawing.Point(12, 10)
        Me.ocmaddnew.Name = "ocmaddnew"
        Me.ocmaddnew.Size = New System.Drawing.Size(95, 25)
        Me.ocmaddnew.TabIndex = 97
        Me.ocmaddnew.TabStop = False
        Me.ocmaddnew.Tag = "2|"
        Me.ocmaddnew.Text = "NEW"
        '
        'FTSelect
        '
        Me.FTSelect.AppearanceCell.BackColor = System.Drawing.Color.LightCyan
        Me.FTSelect.AppearanceCell.Options.UseBackColor = True
        Me.FTSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSelect.Caption = "FTSelect"
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowMove = False
        Me.FTSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        '
        'GridView1
        '
        Me.GridView1.Name = "GridView1"
        '
        'FNHSysMainMatId_No
        '
        Me.FNHSysMainMatId_No.Appearance.Options.UseTextOptions = True
        Me.FNHSysMainMatId_No.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysMainMatId_No.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysMainMatId_No.Location = New System.Drawing.Point(9, 47)
        Me.FNHSysMainMatId_No.Name = "FNHSysMainMatId_No"
        Me.FNHSysMainMatId_No.Size = New System.Drawing.Size(162, 19)
        Me.FNHSysMainMatId_No.TabIndex = 426
        Me.FNHSysMainMatId_No.Text = "FNHSysMainMatIdNone :"
        '
        'FTRemark_lbl
        '
        Me.FTRemark_lbl.Appearance.Options.UseTextOptions = True
        Me.FTRemark_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRemark_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRemark_lbl.Location = New System.Drawing.Point(9, 114)
        Me.FTRemark_lbl.Name = "FTRemark_lbl"
        Me.FTRemark_lbl.Size = New System.Drawing.Size(162, 19)
        Me.FTRemark_lbl.TabIndex = 425
        Me.FTRemark_lbl.Text = "FTRemark :"
        '
        'FTRemark
        '
        Me.FTRemark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FTRemark.Location = New System.Drawing.Point(173, 116)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Size = New System.Drawing.Size(355, 45)
        Me.FTRemark.TabIndex = 424
        Me.FTRemark.Tag = "2|"
        '
        'FNHSysMainMatId_None
        '
        Me.FNHSysMainMatId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysMainMatId_None.Location = New System.Drawing.Point(173, 47)
        Me.FNHSysMainMatId_None.Name = "FNHSysMainMatId_None"
        Me.FNHSysMainMatId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysMainMatId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysMainMatId_None.Properties.AutoHeight = False
        Me.FNHSysMainMatId_None.Properties.ReadOnly = True
        Me.FNHSysMainMatId_None.Size = New System.Drawing.Size(355, 20)
        Me.FNHSysMainMatId_None.TabIndex = 423
        Me.FNHSysMainMatId_None.Tag = "2|"
        '
        'FNHSysMainMatId_lbl
        '
        Me.FNHSysMainMatId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMainMatId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysMainMatId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysMainMatId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysMainMatId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysMainMatId_lbl.Location = New System.Drawing.Point(9, 23)
        Me.FNHSysMainMatId_lbl.Name = "FNHSysMainMatId_lbl"
        Me.FNHSysMainMatId_lbl.Size = New System.Drawing.Size(162, 19)
        Me.FNHSysMainMatId_lbl.TabIndex = 422
        Me.FNHSysMainMatId_lbl.Tag = "2|"
        Me.FNHSysMainMatId_lbl.Text = "FNHSysMainMatId :"
        '
        'FTMainMatNameEN
        '
        Me.FTMainMatNameEN.Location = New System.Drawing.Point(173, 92)
        Me.FTMainMatNameEN.Name = "FTMainMatNameEN"
        Me.FTMainMatNameEN.Properties.AutoHeight = False
        Me.FTMainMatNameEN.Properties.MaxLength = 200
        Me.FTMainMatNameEN.Size = New System.Drawing.Size(355, 20)
        Me.FTMainMatNameEN.TabIndex = 419
        Me.FTMainMatNameEN.Tag = "2|"
        '
        'FTMainMatNameEN_lbl
        '
        Me.FTMainMatNameEN_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTMainMatNameEN_lbl.Appearance.Options.UseForeColor = True
        Me.FTMainMatNameEN_lbl.Appearance.Options.UseTextOptions = True
        Me.FTMainMatNameEN_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTMainMatNameEN_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTMainMatNameEN_lbl.Location = New System.Drawing.Point(9, 92)
        Me.FTMainMatNameEN_lbl.Name = "FTMainMatNameEN_lbl"
        Me.FTMainMatNameEN_lbl.Size = New System.Drawing.Size(162, 19)
        Me.FTMainMatNameEN_lbl.TabIndex = 421
        Me.FTMainMatNameEN_lbl.Text = "FTMainMatNameEN :"
        '
        'FTMainMatNameTH
        '
        Me.FTMainMatNameTH.Location = New System.Drawing.Point(173, 70)
        Me.FTMainMatNameTH.Name = "FTMainMatNameTH"
        Me.FTMainMatNameTH.Properties.AutoHeight = False
        Me.FTMainMatNameTH.Properties.MaxLength = 200
        Me.FTMainMatNameTH.Size = New System.Drawing.Size(355, 20)
        Me.FTMainMatNameTH.TabIndex = 418
        Me.FTMainMatNameTH.Tag = "2|"
        '
        'FTMainMatNameTH_lbl
        '
        Me.FTMainMatNameTH_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTMainMatNameTH_lbl.Appearance.Options.UseForeColor = True
        Me.FTMainMatNameTH_lbl.Appearance.Options.UseTextOptions = True
        Me.FTMainMatNameTH_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTMainMatNameTH_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTMainMatNameTH_lbl.Location = New System.Drawing.Point(9, 70)
        Me.FTMainMatNameTH_lbl.Name = "FTMainMatNameTH_lbl"
        Me.FTMainMatNameTH_lbl.Size = New System.Drawing.Size(162, 19)
        Me.FTMainMatNameTH_lbl.TabIndex = 420
        Me.FTMainMatNameTH_lbl.Tag = "2|"
        Me.FTMainMatNameTH_lbl.Text = "FTMainMatNameTH :"
        '
        'FTStateActive
        '
        Me.FTStateActive.Location = New System.Drawing.Point(380, 20)
        Me.FTStateActive.Name = "FTStateActive"
        Me.FTStateActive.Properties.AutoHeight = False
        Me.FTStateActive.Properties.Caption = "FTStateActive"
        Me.FTStateActive.Properties.ValueChecked = "1"
        Me.FTStateActive.Properties.ValueUnchecked = "0"
        Me.FTStateActive.Size = New System.Drawing.Size(142, 19)
        Me.FTStateActive.TabIndex = 417
        Me.FTStateActive.Tag = "2|"
        '
        'FNHSysMainMatId
        '
        Me.FNHSysMainMatId.Location = New System.Drawing.Point(173, 23)
        Me.FNHSysMainMatId.Name = "FNHSysMainMatId"
        Me.FNHSysMainMatId.Properties.AutoHeight = False
        Me.FNHSysMainMatId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "274", Nothing, True)})
        Me.FNHSysMainMatId.Size = New System.Drawing.Size(186, 20)
        Me.FNHSysMainMatId.TabIndex = 416
        Me.FNHSysMainMatId.Tag = "2|"
        '
        'wAddEditPIDes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.ClientSize = New System.Drawing.Size(540, 244)
        Me.ControlBox = False
        Me.Controls.Add(Me.FNHSysMainMatId_No)
        Me.Controls.Add(Me.FTRemark_lbl)
        Me.Controls.Add(Me.FTRemark)
        Me.Controls.Add(Me.FNHSysMainMatId_None)
        Me.Controls.Add(Me.FNHSysMainMatId_lbl)
        Me.Controls.Add(Me.FTMainMatNameEN)
        Me.Controls.Add(Me.FTMainMatNameEN_lbl)
        Me.Controls.Add(Me.FTMainMatNameTH)
        Me.Controls.Add(Me.FTMainMatNameTH_lbl)
        Me.Controls.Add(Me.FTStateActive)
        Me.Controls.Add(Me.FNHSysMainMatId)
        Me.Controls.Add(Me.ogbbutton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wAddEditPIDes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "wAddEditPIDes"
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbbutton.ResumeLayout(False)
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysMainMatId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTMainMatNameEN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTMainMatNameTH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateActive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysMainMatId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmaddnew As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmedit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FNHSysMainMatId_No As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRemark_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRemark As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FNHSysMainMatId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysMainMatId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTMainMatNameEN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTMainMatNameEN_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTMainMatNameTH As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTMainMatNameTH_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStateActive As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FNHSysMainMatId As DevExpress.XtraEditors.ButtonEdit
End Class
