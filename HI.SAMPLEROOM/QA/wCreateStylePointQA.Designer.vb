<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wCreateStylePointQA
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.oGcDocument = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmcopy = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmAddNewImg = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmSavePoint = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmDelPoint = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmNext = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmPrev = New DevExpress.XtraEditors.SimpleButton()
        Me.FNHSysStyleId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTImage = New DevExpress.XtraEditors.PictureEdit()
        Me.oGpPointCheck = New DevExpress.XtraEditors.GroupControl()
        Me.label4 = New DevExpress.XtraEditors.LabelControl()
        Me.label3 = New DevExpress.XtraEditors.LabelControl()
        Me.label2 = New DevExpress.XtraEditors.LabelControl()
        Me.label1 = New DevExpress.XtraEditors.LabelControl()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ccFTPicName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTPointName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysStyleId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNPointX = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNPointY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ccFTPicType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNPicHeight = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNPicWidth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRemark = New DevExpress.XtraEditors.MemoEdit()
        Me.FTPointName = New DevExpress.XtraEditors.TextEdit()
        Me.FTImagePoint = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.FTPointName_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        CType(Me.oGcDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.oGcDocument.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTImage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oGpPointCheck, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.oGpPointCheck.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPointName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTImagePoint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'oGcDocument
        '
        Me.oGcDocument.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.oGcDocument.Controls.Add(Me.ogbmainprocbutton)
        Me.oGcDocument.Controls.Add(Me.ocmNext)
        Me.oGcDocument.Controls.Add(Me.ocmPrev)
        Me.oGcDocument.Controls.Add(Me.FNHSysStyleId_lbl)
        Me.oGcDocument.Controls.Add(Me.FNHSysStyleId)
        Me.oGcDocument.Controls.Add(Me.FNHSysStyleId_None)
        Me.oGcDocument.Controls.Add(Me.FTImage)
        Me.oGcDocument.Location = New System.Drawing.Point(2, 1)
        Me.oGcDocument.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oGcDocument.Name = "oGcDocument"
        Me.oGcDocument.Size = New System.Drawing.Size(633, 759)
        Me.oGcDocument.TabIndex = 0
        Me.oGcDocument.Text = "Document"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmcopy)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmAddNewImg)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmSavePoint)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmDelPoint)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(101, 150)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(407, 100)
        Me.ogbmainprocbutton.TabIndex = 447
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmcopy
        '
        Me.ocmcopy.Location = New System.Drawing.Point(233, 14)
        Me.ocmcopy.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcopy.Name = "ocmcopy"
        Me.ocmcopy.Size = New System.Drawing.Size(87, 28)
        Me.ocmcopy.TabIndex = 445
        Me.ocmcopy.Text = "Copy"
        '
        'ocmAddNewImg
        '
        Me.ocmAddNewImg.Location = New System.Drawing.Point(140, 15)
        Me.ocmAddNewImg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmAddNewImg.Name = "ocmAddNewImg"
        Me.ocmAddNewImg.Size = New System.Drawing.Size(87, 28)
        Me.ocmAddNewImg.TabIndex = 445
        Me.ocmAddNewImg.Text = "New Image"
        '
        'ocmexit
        '
        Me.ocmexit.Location = New System.Drawing.Point(22, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmSavePoint
        '
        Me.ocmSavePoint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmSavePoint.Location = New System.Drawing.Point(6, 52)
        Me.ocmSavePoint.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmSavePoint.Name = "ocmSavePoint"
        Me.ocmSavePoint.Size = New System.Drawing.Size(138, 33)
        Me.ocmSavePoint.TabIndex = 443
        Me.ocmSavePoint.Text = "Save Point"
        Me.ocmSavePoint.ToolTip = "บันทึกข้อมูล"
        Me.ocmSavePoint.ToolTipTitle = "Save Data"
        '
        'ocmDelPoint
        '
        Me.ocmDelPoint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmDelPoint.Location = New System.Drawing.Point(195, 52)
        Me.ocmDelPoint.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmDelPoint.Name = "ocmDelPoint"
        Me.ocmDelPoint.Size = New System.Drawing.Size(141, 33)
        Me.ocmDelPoint.TabIndex = 443
        Me.ocmDelPoint.Text = "Del Point"
        Me.ocmDelPoint.ToolTip = "ลบข้อมูล"
        Me.ocmDelPoint.ToolTipTitle = "Delete Data"
        '
        'ocmNext
        '
        Me.ocmNext.Location = New System.Drawing.Point(310, 62)
        Me.ocmNext.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmNext.Name = "ocmNext"
        Me.ocmNext.Size = New System.Drawing.Size(134, 28)
        Me.ocmNext.TabIndex = 444
        Me.ocmNext.Text = "Next"
        Me.ocmNext.ToolTip = "Next Step"
        Me.ocmNext.ToolTipTitle = "Next"
        '
        'ocmPrev
        '
        Me.ocmPrev.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight
        Me.ocmPrev.Location = New System.Drawing.Point(169, 62)
        Me.ocmPrev.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmPrev.Name = "ocmPrev"
        Me.ocmPrev.Size = New System.Drawing.Size(134, 28)
        Me.ocmPrev.TabIndex = 443
        Me.ocmPrev.Text = "Prev"
        Me.ocmPrev.ToolTip = "Previous "
        Me.ocmPrev.ToolTipTitle = "Previous"
        '
        'FNHSysStyleId_lbl
        '
        Me.FNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl.Location = New System.Drawing.Point(3, 31)
        Me.FNHSysStyleId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId_lbl.Name = "FNHSysStyleId_lbl"
        Me.FNHSysStyleId_lbl.Size = New System.Drawing.Size(148, 22)
        Me.FNHSysStyleId_lbl.TabIndex = 439
        Me.FNHSysStyleId_lbl.Tag = "2|"
        Me.FNHSysStyleId_lbl.Text = "FNHSysStyleId :"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(155, 30)
        Me.FNHSysStyleId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "89", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FNHSysStyleId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(153, 22)
        Me.FNHSysStyleId.TabIndex = 440
        Me.FNHSysStyleId.Tag = ""
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(311, 30)
        Me.FNHSysStyleId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(308, 22)
        Me.FNHSysStyleId_None.TabIndex = 441
        Me.FNHSysStyleId_None.Tag = ""
        '
        'FTImage
        '
        Me.FTImage.AllowDrop = True
        Me.FTImage.Location = New System.Drawing.Point(37, 96)
        Me.FTImage.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTImage.Name = "FTImage"
        Me.FTImage.Properties.ShowMenu = False
        Me.FTImage.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.FTImage.ShowToolTips = False
        Me.FTImage.Size = New System.Drawing.Size(560, 591)
        Me.FTImage.TabIndex = 442
        Me.FTImage.Tag = ""
        '
        'oGpPointCheck
        '
        Me.oGpPointCheck.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.oGpPointCheck.Controls.Add(Me.label4)
        Me.oGpPointCheck.Controls.Add(Me.label3)
        Me.oGpPointCheck.Controls.Add(Me.label2)
        Me.oGpPointCheck.Controls.Add(Me.label1)
        Me.oGpPointCheck.Controls.Add(Me.ogcdetail)
        Me.oGpPointCheck.Controls.Add(Me.FTRemark)
        Me.oGpPointCheck.Controls.Add(Me.FTPointName)
        Me.oGpPointCheck.Controls.Add(Me.FTImagePoint)
        Me.oGpPointCheck.Controls.Add(Me.LabelControl1)
        Me.oGpPointCheck.Controls.Add(Me.FTPointName_lbl)
        Me.oGpPointCheck.Location = New System.Drawing.Point(643, 1)
        Me.oGpPointCheck.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oGpPointCheck.Name = "oGpPointCheck"
        Me.oGpPointCheck.Size = New System.Drawing.Size(698, 759)
        Me.oGpPointCheck.TabIndex = 1
        Me.oGpPointCheck.Text = "Point Check"
        '
        'label4
        '
        Me.label4.Location = New System.Drawing.Point(443, 150)
        Me.label4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(0, 16)
        Me.label4.TabIndex = 446
        Me.label4.Visible = False
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(372, 150)
        Me.label3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(0, 16)
        Me.label3.TabIndex = 445
        Me.label3.Visible = False
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(325, 71)
        Me.label2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(0, 16)
        Me.label2.TabIndex = 444
        Me.label2.Visible = False
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(325, 48)
        Me.label1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(0, 16)
        Me.label1.TabIndex = 444
        Me.label1.Visible = False
        '
        'ogcdetail
        '
        Me.ogcdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Location = New System.Drawing.Point(3, 389)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.Size = New System.Drawing.Size(691, 370)
        Me.ogcdetail.TabIndex = 442
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ccFTPicName, Me.cFTPointName, Me.cRemark, Me.cFNHSysStyleId, Me.cFNPointX, Me.cFNPointY, Me.ccFTPicType, Me.cFNSeq, Me.cFNPicHeight, Me.cFNPicWidth})
        Me.ogvdetail.DetailHeight = 431
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'ccFTPicName
        '
        Me.ccFTPicName.Caption = "FTPicName"
        Me.ccFTPicName.FieldName = "FTPicName"
        Me.ccFTPicName.MinWidth = 23
        Me.ccFTPicName.Name = "ccFTPicName"
        Me.ccFTPicName.OptionsColumn.AllowEdit = False
        Me.ccFTPicName.Width = 96
        '
        'cFTPointName
        '
        Me.cFTPointName.Caption = "FTPointName"
        Me.cFTPointName.FieldName = "FTPointName"
        Me.cFTPointName.MinWidth = 23
        Me.cFTPointName.Name = "cFTPointName"
        Me.cFTPointName.OptionsColumn.AllowEdit = False
        Me.cFTPointName.Visible = True
        Me.cFTPointName.VisibleIndex = 0
        Me.cFTPointName.Width = 159
        '
        'cRemark
        '
        Me.cRemark.Caption = "Remark"
        Me.cRemark.FieldName = "FTRemark"
        Me.cRemark.MinWidth = 23
        Me.cRemark.Name = "cRemark"
        Me.cRemark.OptionsColumn.AllowEdit = False
        Me.cRemark.Visible = True
        Me.cRemark.VisibleIndex = 1
        Me.cRemark.Width = 500
        '
        'cFNHSysStyleId
        '
        Me.cFNHSysStyleId.Caption = "FNHSysStyleId"
        Me.cFNHSysStyleId.FieldName = "FNHSysStyleId"
        Me.cFNHSysStyleId.MinWidth = 23
        Me.cFNHSysStyleId.Name = "cFNHSysStyleId"
        Me.cFNHSysStyleId.Width = 87
        '
        'cFNPointX
        '
        Me.cFNPointX.Caption = "FNPointX"
        Me.cFNPointX.FieldName = "FNPointX"
        Me.cFNPointX.MinWidth = 23
        Me.cFNPointX.Name = "cFNPointX"
        Me.cFNPointX.Width = 87
        '
        'cFNPointY
        '
        Me.cFNPointY.Caption = "FNPointY"
        Me.cFNPointY.FieldName = "FNPointY"
        Me.cFNPointY.MinWidth = 23
        Me.cFNPointY.Name = "cFNPointY"
        Me.cFNPointY.Width = 87
        '
        'ccFTPicType
        '
        Me.ccFTPicType.Caption = "FTPicType"
        Me.ccFTPicType.FieldName = "FTPicType"
        Me.ccFTPicType.MinWidth = 23
        Me.ccFTPicType.Name = "ccFTPicType"
        Me.ccFTPicType.Width = 87
        '
        'cFNSeq
        '
        Me.cFNSeq.Caption = "FNSeq"
        Me.cFNSeq.FieldName = "FNSeq"
        Me.cFNSeq.MinWidth = 23
        Me.cFNSeq.Name = "cFNSeq"
        Me.cFNSeq.Width = 87
        '
        'cFNPicHeight
        '
        Me.cFNPicHeight.Caption = "FNPicHeight"
        Me.cFNPicHeight.FieldName = "FNPicHeight"
        Me.cFNPicHeight.MinWidth = 23
        Me.cFNPicHeight.Name = "cFNPicHeight"
        Me.cFNPicHeight.Width = 87
        '
        'cFNPicWidth
        '
        Me.cFNPicWidth.Caption = "FNPicWidth"
        Me.cFNPicWidth.FieldName = "FNPicWidth"
        Me.cFNPicWidth.MinWidth = 23
        Me.cFNPicWidth.Name = "cFNPicWidth"
        Me.cFNPicWidth.Width = 87
        '
        'FTRemark
        '
        Me.FTRemark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTRemark.Location = New System.Drawing.Point(169, 321)
        Me.FTRemark.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Size = New System.Drawing.Size(516, 60)
        Me.FTRemark.TabIndex = 441
        Me.FTRemark.Tag = "2|"
        Me.FTRemark.ToolTip = "หมายเหตุ เพิ่มเติม"
        Me.FTRemark.ToolTipTitle = "Remark"
        '
        'FTPointName
        '
        Me.FTPointName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTPointName.Location = New System.Drawing.Point(169, 283)
        Me.FTPointName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPointName.Name = "FTPointName"
        Me.FTPointName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTPointName.Properties.Appearance.Options.UseBackColor = True
        Me.FTPointName.Size = New System.Drawing.Size(516, 22)
        Me.FTPointName.TabIndex = 440
        Me.FTPointName.Tag = "2|"
        Me.FTPointName.ToolTip = "ชื่อจุดตรวจ"
        Me.FTPointName.ToolTipTitle = "Point Name"
        '
        'FTImagePoint
        '
        Me.FTImagePoint.Location = New System.Drawing.Point(8, 31)
        Me.FTImagePoint.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTImagePoint.Name = "FTImagePoint"
        Me.FTImagePoint.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.FTImagePoint.ShowToolTips = False
        Me.FTImagePoint.Size = New System.Drawing.Size(233, 246)
        Me.FTImagePoint.TabIndex = 0
        Me.FTImagePoint.Tag = "2|"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(8, 319)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(154, 22)
        Me.LabelControl1.TabIndex = 439
        Me.LabelControl1.Tag = "2|"
        Me.LabelControl1.Text = "Remark :"
        '
        'FTPointName_lbl
        '
        Me.FTPointName_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPointName_lbl.Appearance.Options.UseForeColor = True
        Me.FTPointName_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPointName_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPointName_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPointName_lbl.Location = New System.Drawing.Point(8, 281)
        Me.FTPointName_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPointName_lbl.Name = "FTPointName_lbl"
        Me.FTPointName_lbl.Size = New System.Drawing.Size(154, 22)
        Me.FTPointName_lbl.TabIndex = 439
        Me.FTPointName_lbl.Tag = "2|"
        Me.FTPointName_lbl.Text = "Point Name :"
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog1"
        '
        'wCreateStylePointQA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1342, 764)
        Me.Controls.Add(Me.oGpPointCheck)
        Me.Controls.Add(Me.oGcDocument)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wCreateStylePointQA"
        Me.Text = "wCreateStylePointQC"
        CType(Me.oGcDocument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.oGcDocument.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTImage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oGpPointCheck, System.ComponentModel.ISupportInitialize).EndInit()
        Me.oGpPointCheck.ResumeLayout(False)
        Me.oGpPointCheck.PerformLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPointName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTImagePoint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents oGcDocument As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNHSysStyleId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents oGpPointCheck As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmNext As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmPrev As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTImage As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTRemark As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FTPointName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTImagePoint As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPointName_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmDelPoint As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmSavePoint As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ccFTPicName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTPointName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents label2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents label1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents label3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents label4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cFNHSysStyleId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNPointX As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNPointY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ccFTPicType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNPicHeight As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNPicWidth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmAddNewImg As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ocmcopy As DevExpress.XtraEditors.SimpleButton
End Class
