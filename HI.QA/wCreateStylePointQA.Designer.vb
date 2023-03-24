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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
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
        Me.oGcDocument.Name = "oGcDocument"
        Me.oGcDocument.Size = New System.Drawing.Size(543, 617)
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
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(87, 122)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(349, 81)
        Me.ogbmainprocbutton.TabIndex = 447
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmcopy
        '
        Me.ocmcopy.Location = New System.Drawing.Point(200, 11)
        Me.ocmcopy.Name = "ocmcopy"
        Me.ocmcopy.Size = New System.Drawing.Size(75, 23)
        Me.ocmcopy.TabIndex = 445
        Me.ocmcopy.Text = "Copy"
        '
        'ocmAddNewImg
        '
        Me.ocmAddNewImg.Location = New System.Drawing.Point(120, 12)
        Me.ocmAddNewImg.Name = "ocmAddNewImg"
        Me.ocmAddNewImg.Size = New System.Drawing.Size(75, 23)
        Me.ocmAddNewImg.TabIndex = 445
        Me.ocmAddNewImg.Text = "New Image"
        '
        'ocmexit
        '
        Me.ocmexit.Location = New System.Drawing.Point(19, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmSavePoint
        '
        Me.ocmSavePoint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmSavePoint.Image = Global.HI.QA.My.Resources.Resources.FuncSave1
        Me.ocmSavePoint.Location = New System.Drawing.Point(5, 42)
        Me.ocmSavePoint.Name = "ocmSavePoint"
        Me.ocmSavePoint.Size = New System.Drawing.Size(118, 27)
        Me.ocmSavePoint.TabIndex = 443
        Me.ocmSavePoint.Text = "Save Point"
        Me.ocmSavePoint.ToolTip = "บันทึกข้อมูล"
        Me.ocmSavePoint.ToolTipTitle = "Save Data"
        '
        'ocmDelPoint
        '
        Me.ocmDelPoint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmDelPoint.Image = Global.HI.QA.My.Resources.Resources.FuncDelete1
        Me.ocmDelPoint.Location = New System.Drawing.Point(167, 42)
        Me.ocmDelPoint.Name = "ocmDelPoint"
        Me.ocmDelPoint.Size = New System.Drawing.Size(121, 27)
        Me.ocmDelPoint.TabIndex = 443
        Me.ocmDelPoint.Text = "Del Point"
        Me.ocmDelPoint.ToolTip = "ลบข้อมูล"
        Me.ocmDelPoint.ToolTipTitle = "Delete Data"
        '
        'ocmNext
        '
        Me.ocmNext.Image = Global.HI.QA.My.Resources.Resources.FuncNext
        Me.ocmNext.Location = New System.Drawing.Point(266, 50)
        Me.ocmNext.Name = "ocmNext"
        Me.ocmNext.Size = New System.Drawing.Size(115, 23)
        Me.ocmNext.TabIndex = 444
        Me.ocmNext.Text = "Next"
        Me.ocmNext.ToolTip = "Next Step"
        Me.ocmNext.ToolTipTitle = "Next"
        '
        'ocmPrev
        '
        Me.ocmPrev.Image = Global.HI.QA.My.Resources.Resources.FuncPrevious
        Me.ocmPrev.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight
        Me.ocmPrev.Location = New System.Drawing.Point(145, 50)
        Me.ocmPrev.Name = "ocmPrev"
        Me.ocmPrev.Size = New System.Drawing.Size(115, 23)
        Me.ocmPrev.TabIndex = 443
        Me.ocmPrev.Text = "Prev"
        Me.ocmPrev.ToolTip = "Previous "
        Me.ocmPrev.ToolTipTitle = "Previous"
        '
        'FNHSysStyleId_lbl
        '
        Me.FNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl.Location = New System.Drawing.Point(3, 25)
        Me.FNHSysStyleId_lbl.Name = "FNHSysStyleId_lbl"
        Me.FNHSysStyleId_lbl.Size = New System.Drawing.Size(127, 18)
        Me.FNHSysStyleId_lbl.TabIndex = 439
        Me.FNHSysStyleId_lbl.Tag = "2|"
        Me.FNHSysStyleId_lbl.Text = "FNHSysStyleId :"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(133, 24)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "89", Nothing, True)})
        Me.FNHSysStyleId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(131, 20)
        Me.FNHSysStyleId.TabIndex = 440
        Me.FNHSysStyleId.Tag = ""
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(267, 24)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(264, 20)
        Me.FNHSysStyleId_None.TabIndex = 441
        Me.FNHSysStyleId_None.Tag = ""
        '
        'FTImage
        '
        Me.FTImage.AllowDrop = True
        Me.FTImage.Location = New System.Drawing.Point(32, 78)
        Me.FTImage.Name = "FTImage"
        Me.FTImage.Properties.ShowMenu = False
        Me.FTImage.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.FTImage.ShowToolTips = False
        Me.FTImage.Size = New System.Drawing.Size(480, 480)
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
        Me.oGpPointCheck.Location = New System.Drawing.Point(551, 1)
        Me.oGpPointCheck.Name = "oGpPointCheck"
        Me.oGpPointCheck.Size = New System.Drawing.Size(598, 617)
        Me.oGpPointCheck.TabIndex = 1
        Me.oGpPointCheck.Text = "Point Check"
        '
        'label4
        '
        Me.label4.Location = New System.Drawing.Point(380, 122)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(0, 13)
        Me.label4.TabIndex = 446
        Me.label4.Visible = False
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(319, 122)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(0, 13)
        Me.label3.TabIndex = 445
        Me.label3.Visible = False
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(279, 58)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(0, 13)
        Me.label2.TabIndex = 444
        Me.label2.Visible = False
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(279, 39)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(0, 13)
        Me.label1.TabIndex = 444
        Me.label1.Visible = False
        '
        'ogcdetail
        '
        Me.ogcdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcdetail.Location = New System.Drawing.Point(3, 316)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.Size = New System.Drawing.Size(592, 301)
        Me.ogcdetail.TabIndex = 442
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ccFTPicName, Me.cFTPointName, Me.cRemark, Me.cFNHSysStyleId, Me.cFNPointX, Me.cFNPointY, Me.ccFTPicType, Me.cFNSeq, Me.cFNPicHeight, Me.cFNPicWidth})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'ccFTPicName
        '
        Me.ccFTPicName.Caption = "FTPicName"
        Me.ccFTPicName.FieldName = "FTPicName"
        Me.ccFTPicName.Name = "ccFTPicName"
        Me.ccFTPicName.OptionsColumn.AllowEdit = False
        Me.ccFTPicName.Width = 82
        '
        'cFTPointName
        '
        Me.cFTPointName.Caption = "FTPointName"
        Me.cFTPointName.FieldName = "FTPointName"
        Me.cFTPointName.Name = "cFTPointName"
        Me.cFTPointName.OptionsColumn.AllowEdit = False
        Me.cFTPointName.Visible = True
        Me.cFTPointName.VisibleIndex = 0
        Me.cFTPointName.Width = 136
        '
        'cRemark
        '
        Me.cRemark.Caption = "Remark"
        Me.cRemark.FieldName = "FTRemark"
        Me.cRemark.Name = "cRemark"
        Me.cRemark.OptionsColumn.AllowEdit = False
        Me.cRemark.Visible = True
        Me.cRemark.VisibleIndex = 1
        Me.cRemark.Width = 429
        '
        'cFNHSysStyleId
        '
        Me.cFNHSysStyleId.Caption = "FNHSysStyleId"
        Me.cFNHSysStyleId.FieldName = "FNHSysStyleId"
        Me.cFNHSysStyleId.Name = "cFNHSysStyleId"
        '
        'cFNPointX
        '
        Me.cFNPointX.Caption = "FNPointX"
        Me.cFNPointX.FieldName = "FNPointX"
        Me.cFNPointX.Name = "cFNPointX"
        '
        'cFNPointY
        '
        Me.cFNPointY.Caption = "FNPointY"
        Me.cFNPointY.FieldName = "FNPointY"
        Me.cFNPointY.Name = "cFNPointY"
        '
        'ccFTPicType
        '
        Me.ccFTPicType.Caption = "FTPicType"
        Me.ccFTPicType.FieldName = "FTPicType"
        Me.ccFTPicType.Name = "ccFTPicType"
        '
        'cFNSeq
        '
        Me.cFNSeq.Caption = "FNSeq"
        Me.cFNSeq.FieldName = "FNSeq"
        Me.cFNSeq.Name = "cFNSeq"
        '
        'cFNPicHeight
        '
        Me.cFNPicHeight.Caption = "FNPicHeight"
        Me.cFNPicHeight.FieldName = "FNPicHeight"
        Me.cFNPicHeight.Name = "cFNPicHeight"
        '
        'cFNPicWidth
        '
        Me.cFNPicWidth.Caption = "FNPicWidth"
        Me.cFNPicWidth.FieldName = "FNPicWidth"
        Me.cFNPicWidth.Name = "cFNPicWidth"
        '
        'FTRemark
        '
        Me.FTRemark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTRemark.Location = New System.Drawing.Point(145, 261)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Size = New System.Drawing.Size(442, 49)
        Me.FTRemark.TabIndex = 441
        Me.FTRemark.Tag = "2|"
        Me.FTRemark.ToolTip = "หมายเหตุ เพิ่มเติม"
        Me.FTRemark.ToolTipTitle = "Remark"
        Me.FTRemark.UseOptimizedRendering = True
        '
        'FTPointName
        '
        Me.FTPointName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTPointName.Location = New System.Drawing.Point(145, 230)
        Me.FTPointName.Name = "FTPointName"
        Me.FTPointName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTPointName.Properties.Appearance.Options.UseBackColor = True
        Me.FTPointName.Size = New System.Drawing.Size(442, 20)
        Me.FTPointName.TabIndex = 440
        Me.FTPointName.Tag = "2|"
        Me.FTPointName.ToolTip = "ชื่อจุดตรวจ"
        Me.FTPointName.ToolTipTitle = "Point Name"
        '
        'FTImagePoint
        '
        Me.FTImagePoint.Location = New System.Drawing.Point(7, 25)
        Me.FTImagePoint.Name = "FTImagePoint"
        Me.FTImagePoint.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.FTImagePoint.ShowToolTips = False
        Me.FTImagePoint.Size = New System.Drawing.Size(200, 200)
        Me.FTImagePoint.TabIndex = 0
        Me.FTImagePoint.Tag = "2|"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(7, 259)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(132, 18)
        Me.LabelControl1.TabIndex = 439
        Me.LabelControl1.Tag = "2|"
        Me.LabelControl1.Text = "Remark :"
        '
        'FTPointName_lbl
        '
        Me.FTPointName_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPointName_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPointName_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPointName_lbl.Location = New System.Drawing.Point(7, 228)
        Me.FTPointName_lbl.Name = "FTPointName_lbl"
        Me.FTPointName_lbl.Size = New System.Drawing.Size(132, 18)
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
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1150, 621)
        Me.Controls.Add(Me.oGpPointCheck)
        Me.Controls.Add(Me.oGcDocument)
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
