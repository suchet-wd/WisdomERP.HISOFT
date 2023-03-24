<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wPurchaseRequest
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ocmremove = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsendpoapprove = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmrefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbdocinfo = New DevExpress.XtraEditors.GroupControl()
        Me.FTStateSendApp = New DevExpress.XtraEditors.CheckEdit()
        Me.FTStateApp = New DevExpress.XtraEditors.CheckEdit()
        Me.FDPRPurchaseDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTPRPurchaseBy = New DevExpress.XtraEditors.TextEdit()
        Me.FTPRPurchaseNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTPRPurchaseBy_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDPRPurchaseDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTPRPurchaseNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbh = New DevExpress.XtraEditors.GroupControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.TextEdit()
        Me.FTRemark_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDPRRequestDate = New DevExpress.XtraEditors.DateEdit()
        Me.FDPRRequestDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRemark = New DevExpress.XtraEditors.MemoEdit()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTPRPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDPRRequestDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMatDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFabricFrontSize = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysRawMatId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysUnitId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogbdocinfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdocinfo.SuspendLayout()
        CType(Me.FTStateSendApp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateApp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDPRPurchaseDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDPRPurchaseDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPRPurchaseBy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPRPurchaseNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbh, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbh.SuspendLayout()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDPRRequestDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDPRRequestDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'ocmremove
        '
        Me.ocmremove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmremove.Location = New System.Drawing.Point(743, 20)
        Me.ocmremove.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmremove.Name = "ocmremove"
        Me.ocmremove.Size = New System.Drawing.Size(103, 25)
        Me.ocmremove.TabIndex = 94
        Me.ocmremove.TabStop = False
        Me.ocmremove.Tag = "2|"
        Me.ocmremove.Text = "Remove Item"
        '
        'ocmadd
        '
        Me.ocmadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmadd.Location = New System.Drawing.Point(657, 20)
        Me.ocmadd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(79, 25)
        Me.ocmadd.TabIndex = 93
        Me.ocmadd.TabStop = False
        Me.ocmadd.Tag = "2|"
        Me.ocmadd.Text = "Add Item"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsendpoapprove)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmrefresh)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmremove)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmadd)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdelete)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(30, 172)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1163, 105)
        Me.ogbmainprocbutton.TabIndex = 300
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmsendpoapprove
        '
        Me.ocmsendpoapprove.Location = New System.Drawing.Point(94, 53)
        Me.ocmsendpoapprove.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsendpoapprove.Name = "ocmsendpoapprove"
        Me.ocmsendpoapprove.Size = New System.Drawing.Size(133, 31)
        Me.ocmsendpoapprove.TabIndex = 103
        Me.ocmsendpoapprove.TabStop = False
        Me.ocmsendpoapprove.Tag = "2|"
        Me.ocmsendpoapprove.Text = "Send Po Approve"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(540, 37)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(83, 31)
        Me.ocmpreview.TabIndex = 102
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PREVIEW"
        '
        'ocmrefresh
        '
        Me.ocmrefresh.Location = New System.Drawing.Point(329, 52)
        Me.ocmrefresh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmrefresh.Name = "ocmrefresh"
        Me.ocmrefresh.Size = New System.Drawing.Size(87, 31)
        Me.ocmrefresh.TabIndex = 101
        Me.ocmrefresh.TabStop = False
        Me.ocmrefresh.Tag = "2|"
        Me.ocmrefresh.Text = "Refresh"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(1031, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(187, 14)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(87, 31)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(94, 14)
        Me.ocmdelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(90, 31)
        Me.ocmdelete.TabIndex = 94
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "DELETE"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(10, 14)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(78, 31)
        Me.ocmsave.TabIndex = 93
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'ogbdocinfo
        '
        Me.ogbdocinfo.Controls.Add(Me.FTStateSendApp)
        Me.ogbdocinfo.Controls.Add(Me.FTStateApp)
        Me.ogbdocinfo.Controls.Add(Me.FDPRPurchaseDate)
        Me.ogbdocinfo.Controls.Add(Me.FTPRPurchaseBy)
        Me.ogbdocinfo.Controls.Add(Me.FTPRPurchaseNo)
        Me.ogbdocinfo.Controls.Add(Me.FTPRPurchaseBy_lbl)
        Me.ogbdocinfo.Controls.Add(Me.FDPRPurchaseDate_lbl)
        Me.ogbdocinfo.Controls.Add(Me.FTPRPurchaseNo_lbl)
        Me.ogbdocinfo.Location = New System.Drawing.Point(1, 2)
        Me.ogbdocinfo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdocinfo.Name = "ogbdocinfo"
        Me.ogbdocinfo.Size = New System.Drawing.Size(394, 151)
        Me.ogbdocinfo.TabIndex = 1
        Me.ogbdocinfo.Tag = "2|"
        Me.ogbdocinfo.Text = "Doccument Info"
        '
        'FTStateSendApp
        '
        Me.FTStateSendApp.Location = New System.Drawing.Point(146, 108)
        Me.FTStateSendApp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStateSendApp.Name = "FTStateSendApp"
        Me.FTStateSendApp.Properties.Caption = "Send Approved"
        Me.FTStateSendApp.Properties.ReadOnly = True
        Me.FTStateSendApp.Properties.ValueChecked = "1"
        Me.FTStateSendApp.Properties.ValueUnchecked = "0"
        Me.FTStateSendApp.Size = New System.Drawing.Size(225, 20)
        Me.FTStateSendApp.TabIndex = 12
        Me.FTStateSendApp.Tag = "2|"
        '
        'FTStateApp
        '
        Me.FTStateApp.Location = New System.Drawing.Point(146, 129)
        Me.FTStateApp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStateApp.Name = "FTStateApp"
        Me.FTStateApp.Properties.Caption = "Supper Visor Approved"
        Me.FTStateApp.Properties.ReadOnly = True
        Me.FTStateApp.Properties.ValueChecked = "1"
        Me.FTStateApp.Properties.ValueUnchecked = "0"
        Me.FTStateApp.Size = New System.Drawing.Size(225, 20)
        Me.FTStateApp.TabIndex = 11
        Me.FTStateApp.Tag = "2|"
        '
        'FDPRPurchaseDate
        '
        Me.FDPRPurchaseDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FDPRPurchaseDate.EditValue = Nothing
        Me.FDPRPurchaseDate.EnterMoveNextControl = True
        Me.FDPRPurchaseDate.Location = New System.Drawing.Point(148, 58)
        Me.FDPRPurchaseDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDPRPurchaseDate.Name = "FDPRPurchaseDate"
        Me.FDPRPurchaseDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDPRPurchaseDate.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FDPRPurchaseDate.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FDPRPurchaseDate.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FDPRPurchaseDate.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FDPRPurchaseDate.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FDPRPurchaseDate.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FDPRPurchaseDate.Properties.AppearanceReadOnly.Options.UseTextOptions = True
        Me.FDPRPurchaseDate.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDPRPurchaseDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FDPRPurchaseDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDPRPurchaseDate.Properties.DisplayFormat.FormatString = "d"
        Me.FDPRPurchaseDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDPRPurchaseDate.Properties.EditFormat.FormatString = "d"
        Me.FDPRPurchaseDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDPRPurchaseDate.Properties.NullDate = ""
        Me.FDPRPurchaseDate.Properties.ReadOnly = True
        Me.FDPRPurchaseDate.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never
        Me.FDPRPurchaseDate.Size = New System.Drawing.Size(240, 22)
        Me.FDPRPurchaseDate.TabIndex = 1
        Me.FDPRPurchaseDate.TabStop = False
        Me.FDPRPurchaseDate.Tag = "2|"
        '
        'FTPRPurchaseBy
        '
        Me.FTPRPurchaseBy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTPRPurchaseBy.EnterMoveNextControl = True
        Me.FTPRPurchaseBy.Location = New System.Drawing.Point(148, 84)
        Me.FTPRPurchaseBy.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPRPurchaseBy.Name = "FTPRPurchaseBy"
        Me.FTPRPurchaseBy.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTPRPurchaseBy.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTPRPurchaseBy.Properties.Appearance.Options.UseBackColor = True
        Me.FTPRPurchaseBy.Properties.Appearance.Options.UseForeColor = True
        Me.FTPRPurchaseBy.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTPRPurchaseBy.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTPRPurchaseBy.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTPRPurchaseBy.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTPRPurchaseBy.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTPRPurchaseBy.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTPRPurchaseBy.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTPRPurchaseBy.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTPRPurchaseBy.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTPRPurchaseBy.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTPRPurchaseBy.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTPRPurchaseBy.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTPRPurchaseBy.Properties.ReadOnly = True
        Me.FTPRPurchaseBy.Size = New System.Drawing.Size(240, 22)
        Me.FTPRPurchaseBy.TabIndex = 2
        Me.FTPRPurchaseBy.TabStop = False
        Me.FTPRPurchaseBy.Tag = "2|"
        '
        'FTPRPurchaseNo
        '
        Me.FTPRPurchaseNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTPRPurchaseNo.EnterMoveNextControl = True
        Me.FTPRPurchaseNo.Location = New System.Drawing.Point(148, 32)
        Me.FTPRPurchaseNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPRPurchaseNo.Name = "FTPRPurchaseNo"
        Me.FTPRPurchaseNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTPRPurchaseNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTPRPurchaseNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTPRPurchaseNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTPRPurchaseNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTPRPurchaseNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTPRPurchaseNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTPRPurchaseNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTPRPurchaseNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTPRPurchaseNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTPRPurchaseNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTPRPurchaseNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTPRPurchaseNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTPRPurchaseNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        SerializableAppearanceObject2.Options.UseTextOptions = True
        SerializableAppearanceObject2.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject3.Options.UseTextOptions = True
        SerializableAppearanceObject3.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPRPurchaseNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "...", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "318", Nothing, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "New", 20, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F2), SerializableAppearanceObject3, "", "d", Nothing, True)})
        Me.FTPRPurchaseNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTPRPurchaseNo.Size = New System.Drawing.Size(240, 22)
        Me.FTPRPurchaseNo.TabIndex = 0
        Me.FTPRPurchaseNo.TabStop = False
        Me.FTPRPurchaseNo.Tag = "2|"
        '
        'FTPRPurchaseBy_lbl
        '
        Me.FTPRPurchaseBy_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPRPurchaseBy_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPRPurchaseBy_lbl.Location = New System.Drawing.Point(2, 84)
        Me.FTPRPurchaseBy_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPRPurchaseBy_lbl.Name = "FTPRPurchaseBy_lbl"
        Me.FTPRPurchaseBy_lbl.Size = New System.Drawing.Size(146, 22)
        Me.FTPRPurchaseBy_lbl.TabIndex = 5
        Me.FTPRPurchaseBy_lbl.Tag = "2|"
        Me.FTPRPurchaseBy_lbl.Text = "User :"
        '
        'FDPRPurchaseDate_lbl
        '
        Me.FDPRPurchaseDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FDPRPurchaseDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDPRPurchaseDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDPRPurchaseDate_lbl.Location = New System.Drawing.Point(6, 57)
        Me.FDPRPurchaseDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDPRPurchaseDate_lbl.Name = "FDPRPurchaseDate_lbl"
        Me.FDPRPurchaseDate_lbl.Size = New System.Drawing.Size(142, 23)
        Me.FDPRPurchaseDate_lbl.TabIndex = 3
        Me.FDPRPurchaseDate_lbl.Tag = "2|"
        Me.FDPRPurchaseDate_lbl.Text = "Document Date. :"
        '
        'FTPRPurchaseNo_lbl
        '
        Me.FTPRPurchaseNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPRPurchaseNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPRPurchaseNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPRPurchaseNo_lbl.Location = New System.Drawing.Point(-1, 31)
        Me.FTPRPurchaseNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPRPurchaseNo_lbl.Name = "FTPRPurchaseNo_lbl"
        Me.FTPRPurchaseNo_lbl.Size = New System.Drawing.Size(149, 23)
        Me.FTPRPurchaseNo_lbl.TabIndex = 1
        Me.FTPRPurchaseNo_lbl.Tag = "2|"
        Me.FTPRPurchaseNo_lbl.Text = "Document No. :"
        '
        'ogbh
        '
        Me.ogbh.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbh.Controls.Add(Me.FNHSysCmpId)
        Me.ogbh.Controls.Add(Me.FTRemark_lbl)
        Me.ogbh.Controls.Add(Me.FDPRRequestDate)
        Me.ogbh.Controls.Add(Me.FDPRRequestDate_lbl)
        Me.ogbh.Controls.Add(Me.FTRemark)
        Me.ogbh.Location = New System.Drawing.Point(398, 2)
        Me.ogbh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbh.Name = "ogbh"
        Me.ogbh.ShowCaption = False
        Me.ogbh.Size = New System.Drawing.Size(835, 151)
        Me.ogbh.TabIndex = 1
        Me.ogbh.Tag = "2|"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(6, 119)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysCmpId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysCmpId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysCmpId.Properties.MaxLength = 30
        Me.FNHSysCmpId.Size = New System.Drawing.Size(20, 22)
        Me.FNHSysCmpId.TabIndex = 286
        Me.FNHSysCmpId.Tag = "|"
        Me.FNHSysCmpId.Visible = False
        '
        'FTRemark_lbl
        '
        Me.FTRemark_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTRemark_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRemark_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRemark_lbl.Location = New System.Drawing.Point(16, 41)
        Me.FTRemark_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark_lbl.Name = "FTRemark_lbl"
        Me.FTRemark_lbl.Size = New System.Drawing.Size(107, 23)
        Me.FTRemark_lbl.TabIndex = 272
        Me.FTRemark_lbl.Tag = "2|"
        Me.FTRemark_lbl.Text = "Note :"
        '
        'FDPRRequestDate
        '
        Me.FDPRRequestDate.EditValue = Nothing
        Me.FDPRRequestDate.EnterMoveNextControl = True
        Me.FDPRRequestDate.Location = New System.Drawing.Point(125, 18)
        Me.FDPRRequestDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDPRRequestDate.Name = "FDPRRequestDate"
        Me.FDPRRequestDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDPRRequestDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDPRRequestDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDPRRequestDate.Properties.DisplayFormat.FormatString = "d"
        Me.FDPRRequestDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDPRRequestDate.Properties.EditFormat.FormatString = "d"
        Me.FDPRRequestDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDPRRequestDate.Properties.NullDate = ""
        Me.FDPRRequestDate.Size = New System.Drawing.Size(117, 22)
        Me.FDPRRequestDate.TabIndex = 268
        Me.FDPRRequestDate.Tag = "2|"
        '
        'FDPRRequestDate_lbl
        '
        Me.FDPRRequestDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FDPRRequestDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDPRRequestDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDPRRequestDate_lbl.Location = New System.Drawing.Point(7, 18)
        Me.FDPRRequestDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDPRRequestDate_lbl.Name = "FDPRRequestDate_lbl"
        Me.FDPRRequestDate_lbl.Size = New System.Drawing.Size(117, 23)
        Me.FDPRRequestDate_lbl.TabIndex = 269
        Me.FDPRRequestDate_lbl.Tag = "2|"
        Me.FDPRRequestDate_lbl.Text = " RequestDate:"
        '
        'FTRemark
        '
        Me.FTRemark.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTRemark.EditValue = ""
        Me.FTRemark.Location = New System.Drawing.Point(125, 47)
        Me.FTRemark.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Properties.MaxLength = 500
        Me.FTRemark.Size = New System.Drawing.Size(700, 98)
        Me.FTRemark.TabIndex = 2
        Me.FTRemark.Tag = "2|"
        Me.FTRemark.UseOptimizedRendering = True
        '
        'ogcdetail
        '
        Me.ogcdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcdetail.EmbeddedNavigator.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Location = New System.Drawing.Point(5, 28)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.Size = New System.Drawing.Size(1228, 609)
        Me.ogcdetail.TabIndex = 120
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTPRPurchaseNo, Me.cFDPRRequestDate, Me.FTRawMatCode, Me.FTMatDesc, Me.FTRawMatColorCode, Me.FTRawMatSizeCode, Me.cFTOrderNo, Me.FTFabricFrontSize, Me.FTUnitCode, Me.FNQuantity, Me.cFTRemark, Me.FNHSysRawMatId, Me.FNHSysUnitId})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'cFTPRPurchaseNo
        '
        Me.cFTPRPurchaseNo.AppearanceHeader.Options.UseTextOptions = True
        Me.cFTPRPurchaseNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFTPRPurchaseNo.Caption = "FTPRPurchaseNo"
        Me.cFTPRPurchaseNo.FieldName = "FTPRPurchaseNo"
        Me.cFTPRPurchaseNo.Name = "cFTPRPurchaseNo"
        Me.cFTPRPurchaseNo.OptionsColumn.AllowEdit = False
        Me.cFTPRPurchaseNo.OptionsColumn.ReadOnly = True
        Me.cFTPRPurchaseNo.Width = 132
        '
        'cFDPRRequestDate
        '
        Me.cFDPRRequestDate.AppearanceHeader.Options.UseTextOptions = True
        Me.cFDPRRequestDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFDPRRequestDate.Caption = "FDPRRequestDate"
        Me.cFDPRRequestDate.FieldName = "FDPRRequestDate"
        Me.cFDPRRequestDate.Name = "cFDPRRequestDate"
        Me.cFDPRRequestDate.OptionsColumn.AllowEdit = False
        Me.cFDPRRequestDate.OptionsColumn.ReadOnly = True
        Me.cFDPRRequestDate.Width = 99
        '
        'FTRawMatCode
        '
        Me.FTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatCode.Caption = "FTRawMatCode"
        Me.FTRawMatCode.FieldName = "FTRawMatCode"
        Me.FTRawMatCode.Name = "FTRawMatCode"
        Me.FTRawMatCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatCode.Visible = True
        Me.FTRawMatCode.VisibleIndex = 0
        Me.FTRawMatCode.Width = 125
        '
        'FTMatDesc
        '
        Me.FTMatDesc.AppearanceHeader.Options.UseTextOptions = True
        Me.FTMatDesc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTMatDesc.Caption = "FTMatDesc"
        Me.FTMatDesc.FieldName = "FTMatDesc"
        Me.FTMatDesc.Name = "FTMatDesc"
        Me.FTMatDesc.OptionsColumn.AllowEdit = False
        Me.FTMatDesc.OptionsColumn.ReadOnly = True
        Me.FTMatDesc.Visible = True
        Me.FTMatDesc.VisibleIndex = 1
        Me.FTMatDesc.Width = 203
        '
        'FTRawMatColorCode
        '
        Me.FTRawMatColorCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatColorCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatColorCode.Caption = "FTRawMatColorCode"
        Me.FTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.FTRawMatColorCode.Name = "FTRawMatColorCode"
        Me.FTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatColorCode.Visible = True
        Me.FTRawMatColorCode.VisibleIndex = 2
        Me.FTRawMatColorCode.Width = 94
        '
        'FTRawMatSizeCode
        '
        Me.FTRawMatSizeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatSizeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatSizeCode.Caption = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.FieldName = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.Name = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatSizeCode.Visible = True
        Me.FTRawMatSizeCode.VisibleIndex = 3
        Me.FTRawMatSizeCode.Width = 86
        '
        'cFTOrderNo
        '
        Me.cFTOrderNo.AppearanceHeader.Options.UseTextOptions = True
        Me.cFTOrderNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFTOrderNo.Caption = "FTOrderNo"
        Me.cFTOrderNo.FieldName = "FTOrderNo"
        Me.cFTOrderNo.Name = "cFTOrderNo"
        Me.cFTOrderNo.OptionsColumn.AllowEdit = False
        Me.cFTOrderNo.OptionsColumn.ReadOnly = True
        Me.cFTOrderNo.Visible = True
        Me.cFTOrderNo.VisibleIndex = 4
        Me.cFTOrderNo.Width = 104
        '
        'FTFabricFrontSize
        '
        Me.FTFabricFrontSize.AppearanceHeader.Options.UseTextOptions = True
        Me.FTFabricFrontSize.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTFabricFrontSize.Caption = "FTFabricFrontSize"
        Me.FTFabricFrontSize.FieldName = "FTFabricFrontSize"
        Me.FTFabricFrontSize.Name = "FTFabricFrontSize"
        Me.FTFabricFrontSize.OptionsColumn.AllowEdit = False
        Me.FTFabricFrontSize.OptionsColumn.ReadOnly = True
        Me.FTFabricFrontSize.Visible = True
        Me.FTFabricFrontSize.VisibleIndex = 5
        Me.FTFabricFrontSize.Width = 107
        '
        'FTUnitCode
        '
        Me.FTUnitCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUnitCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitCode.Caption = "FTUnitCode"
        Me.FTUnitCode.FieldName = "FTUnitCode"
        Me.FTUnitCode.Name = "FTUnitCode"
        Me.FTUnitCode.OptionsColumn.AllowEdit = False
        Me.FTUnitCode.OptionsColumn.ReadOnly = True
        Me.FTUnitCode.Visible = True
        Me.FTUnitCode.VisibleIndex = 6
        Me.FTUnitCode.Width = 90
        '
        'FNQuantity
        '
        Me.FNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQuantity.Caption = "FNQuantity"
        Me.FNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.OptionsColumn.ReadOnly = True
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 7
        Me.FNQuantity.Width = 109
        '
        'cFTRemark
        '
        Me.cFTRemark.AppearanceHeader.Options.UseTextOptions = True
        Me.cFTRemark.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFTRemark.Caption = "FTRemark"
        Me.cFTRemark.FieldName = "FTRemark"
        Me.cFTRemark.Name = "cFTRemark"
        Me.cFTRemark.OptionsColumn.AllowEdit = False
        Me.cFTRemark.OptionsColumn.ReadOnly = True
        Me.cFTRemark.Visible = True
        Me.cFTRemark.VisibleIndex = 8
        Me.cFTRemark.Width = 207
        '
        'FNHSysRawMatId
        '
        Me.FNHSysRawMatId.Caption = "FNHSysRawMatId"
        Me.FNHSysRawMatId.FieldName = "FNHSysRawMatId"
        Me.FNHSysRawMatId.Name = "FNHSysRawMatId"
        '
        'FNHSysUnitId
        '
        Me.FNHSysUnitId.Caption = "FNHSysUnitId"
        Me.FNHSysUnitId.FieldName = "FNHSysUnitId"
        Me.FNHSysUnitId.Name = "FNHSysUnitId"
        '
        'ogbdetail
        '
        Me.ogbdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ogcdetail)
        Me.ogbdetail.Location = New System.Drawing.Point(0, 158)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.Size = New System.Drawing.Size(1233, 638)
        Me.ogbdetail.TabIndex = 120
        Me.ogbdetail.Text = "Detail"
        '
        'wPurchaseRequest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1234, 793)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbdocinfo)
        Me.Controls.Add(Me.ogbh)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wPurchaseRequest"
        Me.Text = "Purchaser Order"
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogbdocinfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdocinfo.ResumeLayout(False)
        CType(Me.FTStateSendApp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateApp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDPRPurchaseDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDPRPurchaseDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPRPurchaseBy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPRPurchaseNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbh, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbh.ResumeLayout(False)
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDPRRequestDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDPRRequestDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmremove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbdocinfo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FDPRPurchaseDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTPRPurchaseBy As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTPRPurchaseNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTPRPurchaseBy_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDPRPurchaseDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPRPurchaseNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbh As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTRemark_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDPRRequestDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDPRRequestDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRemark As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTPRPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDPRRequestDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFabricFrontSize As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cFTRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysRawMatId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTMatDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysUnitId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateSendApp As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTStateApp As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ocmsendpoapprove As DevExpress.XtraEditors.SimpleButton
End Class
