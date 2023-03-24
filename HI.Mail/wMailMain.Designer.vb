<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wMailMain
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wMailMain))
        Me.RepositoryItemPictureEdit5 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.RepositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.RepositoryItemPictureEdit6 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.RepositoryItemPictureEdit7 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.RepositoryItemPictureEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmUndoMail = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmCancelMail = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdeletemail = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmForwordMail = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmReplyMail = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmrefreshmail = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmaddnewMail = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmmail = New DevExpress.XtraEditors.SimpleButton()
        Me.ilColumns = New System.Windows.Forms.ImageList(Me.components)
        Me.icEditors = New DevExpress.Utils.ImageCollection(Me.components)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PclShowMail = New DevExpress.XtraEditors.PanelControl()
        Me.PicMail_2 = New DevExpress.XtraEditors.PictureEdit()
        Me.ocmexit2 = New DevExpress.XtraEditors.SimpleButton()
        Me.FTMailText29 = New DevExpress.XtraRichEdit.RichEditControl()
        Me.ogcTMAILFileAttach9 = New DevExpress.XtraGrid.GridControl()
        Me.ogvTMAILFileAttach9 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMailFileAttach_lbl9 = New DevExpress.XtraEditors.LabelControl()
        Me.SbntAttachFile9 = New DevExpress.XtraEditors.SimpleButton()
        Me.FNMailStatePriority_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNMailStatePriority9 = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FTMailSubject9 = New DevExpress.XtraEditors.TextEdit()
        Me.FTMailTo9 = New DevExpress.XtraEditors.TextEdit()
        Me.FTMailSubject_Lbl9 = New DevExpress.XtraEditors.LabelControl()
        Me.FTMailTo_Lbl9 = New DevExpress.XtraEditors.LabelControl()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.OtmCheckLanguage = New System.Windows.Forms.Timer(Me.components)
        Me.SpcShow = New DevExpress.XtraEditors.SplitContainerControl()
        Me.FTChkStateSelect = New DevExpress.XtraEditors.CheckEdit()
        Me.ogcTMAILMessages = New DevExpress.XtraGrid.GridControl()
        Me.ogvTMAILMessages = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ColFTMailId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTMailDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTMailFrom = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTMailTo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTMailSubject = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTMailText = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTMailStateOpen = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFDMailOpenDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTMailOpenTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNMailStateAttach = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNMailStatePriority = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNMailStateSend = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNMailStateJobStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColTImageStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColImageOpen = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColImagePriority = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColImageAttach = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNMailFileAttach = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTInsTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTMailFromTemp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNMailStateDelete = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNMailStateType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMailTypeInfo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMailInfoRef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCallMnuName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCallMethodName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me._Lst = New DevExpress.XtraTreeList.TreeList()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.olbFTCallMethodName = New DevExpress.XtraEditors.LabelControl()
        Me.olbFTCallMnuName = New DevExpress.XtraEditors.LabelControl()
        Me.olblink = New DevExpress.XtraEditors.LabelControl()
        Me.TFrom_Lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTMailTo_Lbl_2 = New DevExpress.XtraEditors.LabelControl()
        Me.FTMailDate_Lbl_2 = New DevExpress.XtraEditors.LabelControl()
        Me.FTMailSubject_Lbl_2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.ogcTMAILFileAttach = New DevExpress.XtraGrid.GridControl()
        Me.ogvTMAILFileAttach = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PicMail = New DevExpress.XtraEditors.PictureEdit()
        Me.recMessage = New DevExpress.XtraRichEdit.RichEditControl()
        Me.FTMailTo_Lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTMailDate_Lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTMailSubject_Lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.RepositoryItemPictureEdit5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.icEditors, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PclShowMail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PclShowMail.SuspendLayout()
        CType(Me.PicMail_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcTMAILFileAttach9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvTMAILFileAttach9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNMailStatePriority9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTMailSubject9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTMailTo9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpcShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SpcShow.SuspendLayout()
        CType(Me.FTChkStateSelect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcTMAILMessages, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvTMAILMessages, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._Lst, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.ogcTMAILFileAttach, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvTMAILFileAttach, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicMail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RepositoryItemPictureEdit5
        '
        Me.RepositoryItemPictureEdit5.Name = "RepositoryItemPictureEdit5"
        Me.RepositoryItemPictureEdit5.ZoomAccelerationFactor = 1.0R
        '
        'RepositoryItemPictureEdit1
        '
        Me.RepositoryItemPictureEdit1.Name = "RepositoryItemPictureEdit1"
        Me.RepositoryItemPictureEdit1.ZoomAccelerationFactor = 1.0R
        '
        'RepositoryItemPictureEdit6
        '
        Me.RepositoryItemPictureEdit6.Name = "RepositoryItemPictureEdit6"
        Me.RepositoryItemPictureEdit6.ZoomAccelerationFactor = 1.0R
        '
        'RepositoryItemPictureEdit7
        '
        Me.RepositoryItemPictureEdit7.Name = "RepositoryItemPictureEdit7"
        Me.RepositoryItemPictureEdit7.ZoomAccelerationFactor = 1.0R
        '
        'RepositoryItemPictureEdit4
        '
        Me.RepositoryItemPictureEdit4.Name = "RepositoryItemPictureEdit4"
        Me.RepositoryItemPictureEdit4.ZoomAccelerationFactor = 1.0R
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmUndoMail)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmCancelMail)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdeletemail)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmForwordMail)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmReplyMail)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmrefreshmail)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmaddnewMail)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmmail)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(40, 151)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(565, 119)
        Me.ogbmainprocbutton.TabIndex = 141
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmUndoMail
        '
        Me.ocmUndoMail.Location = New System.Drawing.Point(373, 12)
        Me.ocmUndoMail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmUndoMail.Name = "ocmUndoMail"
        Me.ocmUndoMail.Size = New System.Drawing.Size(111, 31)
        Me.ocmUndoMail.TabIndex = 115
        Me.ocmUndoMail.TabStop = False
        Me.ocmUndoMail.Tag = "2|"
        Me.ocmUndoMail.Text = "Undo"
        '
        'ocmCancelMail
        '
        Me.ocmCancelMail.Location = New System.Drawing.Point(371, 55)
        Me.ocmCancelMail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmCancelMail.Name = "ocmCancelMail"
        Me.ocmCancelMail.Size = New System.Drawing.Size(111, 31)
        Me.ocmCancelMail.TabIndex = 114
        Me.ocmCancelMail.TabStop = False
        Me.ocmCancelMail.Tag = "2|"
        Me.ocmCancelMail.Text = "Cancel"
        '
        'ocmdeletemail
        '
        Me.ocmdeletemail.Location = New System.Drawing.Point(131, 12)
        Me.ocmdeletemail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdeletemail.Name = "ocmdeletemail"
        Me.ocmdeletemail.Size = New System.Drawing.Size(111, 31)
        Me.ocmdeletemail.TabIndex = 113
        Me.ocmdeletemail.TabStop = False
        Me.ocmdeletemail.Tag = "2|"
        Me.ocmdeletemail.Text = "Delete"
        '
        'ocmForwordMail
        '
        Me.ocmForwordMail.Location = New System.Drawing.Point(255, 12)
        Me.ocmForwordMail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmForwordMail.Name = "ocmForwordMail"
        Me.ocmForwordMail.Size = New System.Drawing.Size(111, 31)
        Me.ocmForwordMail.TabIndex = 112
        Me.ocmForwordMail.TabStop = False
        Me.ocmForwordMail.Tag = "2|"
        Me.ocmForwordMail.Text = "Forword"
        '
        'ocmReplyMail
        '
        Me.ocmReplyMail.Location = New System.Drawing.Point(253, 55)
        Me.ocmReplyMail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmReplyMail.Name = "ocmReplyMail"
        Me.ocmReplyMail.Size = New System.Drawing.Size(111, 31)
        Me.ocmReplyMail.TabIndex = 111
        Me.ocmReplyMail.TabStop = False
        Me.ocmReplyMail.Tag = "2|"
        Me.ocmReplyMail.Text = "Reply"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(433, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmrefreshmail
        '
        Me.ocmrefreshmail.Location = New System.Drawing.Point(10, 12)
        Me.ocmrefreshmail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmrefreshmail.Name = "ocmrefreshmail"
        Me.ocmrefreshmail.Size = New System.Drawing.Size(111, 31)
        Me.ocmrefreshmail.TabIndex = 95
        Me.ocmrefreshmail.TabStop = False
        Me.ocmrefreshmail.Tag = "2|"
        Me.ocmrefreshmail.Text = "Refresh"
        '
        'ocmaddnewMail
        '
        Me.ocmaddnewMail.Location = New System.Drawing.Point(128, 55)
        Me.ocmaddnewMail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmaddnewMail.Name = "ocmaddnewMail"
        Me.ocmaddnewMail.Size = New System.Drawing.Size(111, 31)
        Me.ocmaddnewMail.TabIndex = 94
        Me.ocmaddnewMail.TabStop = False
        Me.ocmaddnewMail.Tag = "2|"
        Me.ocmaddnewMail.Text = "New Mail"
        '
        'ocmmail
        '
        Me.ocmmail.Location = New System.Drawing.Point(10, 55)
        Me.ocmmail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmmail.Name = "ocmmail"
        Me.ocmmail.Size = New System.Drawing.Size(111, 31)
        Me.ocmmail.TabIndex = 93
        Me.ocmmail.TabStop = False
        Me.ocmmail.Tag = "2|"
        Me.ocmmail.Text = "Sent Mail"
        '
        'ilColumns
        '
        Me.ilColumns.ImageStream = CType(resources.GetObject("ilColumns.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilColumns.TransparentColor = System.Drawing.Color.Transparent
        Me.ilColumns.Images.SetKeyName(0, "Prioritized_32x32.png")
        Me.ilColumns.Images.SetKeyName(1, "MemoStyle.png")
        Me.ilColumns.Images.SetKeyName(2, "AttachmentObject_16x16.png")
        Me.ilColumns.Images.SetKeyName(3, "reading.png")
        '
        'icEditors
        '
        Me.icEditors.ImageStream = CType(resources.GetObject("icEditors.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.icEditors.Images.SetKeyName(0, "Low16x16.png")
        Me.icEditors.Images.SetKeyName(1, "High16x16.png")
        Me.icEditors.Images.SetKeyName(2, "reading.png")
        Me.icEditors.Images.SetKeyName(3, "Mail_16x16.png")
        Me.icEditors.Images.SetKeyName(4, "AttachmentObject_16x16.png")
        Me.icEditors.Images.SetKeyName(5, "Completed_16x16.png")
        Me.icEditors.Images.SetKeyName(6, "Delete_16x16.png")
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'Timer1
        '
        Me.Timer1.Interval = 60000
        '
        'PclShowMail
        '
        Me.PclShowMail.Controls.Add(Me.PicMail_2)
        Me.PclShowMail.Controls.Add(Me.ocmexit2)
        Me.PclShowMail.Controls.Add(Me.FTMailText29)
        Me.PclShowMail.Controls.Add(Me.ogcTMAILFileAttach9)
        Me.PclShowMail.Controls.Add(Me.FNMailFileAttach_lbl9)
        Me.PclShowMail.Controls.Add(Me.SbntAttachFile9)
        Me.PclShowMail.Controls.Add(Me.FNMailStatePriority_lbl)
        Me.PclShowMail.Controls.Add(Me.FNMailStatePriority9)
        Me.PclShowMail.Controls.Add(Me.FTMailSubject9)
        Me.PclShowMail.Controls.Add(Me.FTMailTo9)
        Me.PclShowMail.Controls.Add(Me.FTMailSubject_Lbl9)
        Me.PclShowMail.Controls.Add(Me.FTMailTo_Lbl9)
        Me.PclShowMail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PclShowMail.Location = New System.Drawing.Point(0, 0)
        Me.PclShowMail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PclShowMail.Name = "PclShowMail"
        Me.PclShowMail.Size = New System.Drawing.Size(1231, 815)
        Me.PclShowMail.TabIndex = 144
        '
        'PicMail_2
        '
        Me.PicMail_2.Location = New System.Drawing.Point(13, 7)
        Me.PicMail_2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PicMail_2.Name = "PicMail_2"
        Me.PicMail_2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        Me.PicMail_2.Properties.ZoomAccelerationFactor = 1.0R
        Me.PicMail_2.Size = New System.Drawing.Size(101, 94)
        Me.PicMail_2.TabIndex = 165
        '
        'ocmexit2
        '
        Me.ocmexit2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit2.Image = CType(resources.GetObject("ocmexit2.Image"), System.Drawing.Image)
        Me.ocmexit2.Location = New System.Drawing.Point(1137, 15)
        Me.ocmexit2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit2.Name = "ocmexit2"
        Me.ocmexit2.Size = New System.Drawing.Size(84, 43)
        Me.ocmexit2.TabIndex = 115
        Me.ocmexit2.TabStop = False
        Me.ocmexit2.Tag = "2|"
        Me.ocmexit2.Text = "EXIT"
        '
        'FTMailText29
        '
        Me.FTMailText29.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple
        Me.FTMailText29.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTMailText29.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.FTMailText29.Location = New System.Drawing.Point(14, 106)
        Me.FTMailText29.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTMailText29.Name = "FTMailText29"
        Me.FTMailText29.Size = New System.Drawing.Size(1207, 703)
        Me.FTMailText29.TabIndex = 159
        Me.FTMailText29.Tag = "2|"
        '
        'ogcTMAILFileAttach9
        '
        Me.ogcTMAILFileAttach9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcTMAILFileAttach9.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcTMAILFileAttach9.Location = New System.Drawing.Point(871, 15)
        Me.ogcTMAILFileAttach9.MainView = Me.ogvTMAILFileAttach9
        Me.ogcTMAILFileAttach9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcTMAILFileAttach9.Name = "ogcTMAILFileAttach9"
        Me.ogcTMAILFileAttach9.Size = New System.Drawing.Size(252, 89)
        Me.ogcTMAILFileAttach9.TabIndex = 163
        Me.ogcTMAILFileAttach9.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvTMAILFileAttach9})
        '
        'ogvTMAILFileAttach9
        '
        Me.ogvTMAILFileAttach9.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.ogvTMAILFileAttach9.GridControl = Me.ogcTMAILFileAttach9
        Me.ogvTMAILFileAttach9.Name = "ogvTMAILFileAttach9"
        Me.ogvTMAILFileAttach9.OptionsView.ShowColumnHeaders = False
        Me.ogvTMAILFileAttach9.OptionsView.ShowGroupPanel = False
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "GridColumn1"
        Me.GridColumn3.FieldName = "FTMailId"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.ShowCaption = False
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Attach File  "
        Me.GridColumn4.FieldName = "FNMailFileAttach"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.ReadOnly = True
        Me.GridColumn4.OptionsColumn.ShowCaption = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'FNMailFileAttach_lbl9
        '
        Me.FNMailFileAttach_lbl9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNMailFileAttach_lbl9.Location = New System.Drawing.Point(747, 18)
        Me.FNMailFileAttach_lbl9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNMailFileAttach_lbl9.Name = "FNMailFileAttach_lbl9"
        Me.FNMailFileAttach_lbl9.Size = New System.Drawing.Size(77, 16)
        Me.FNMailFileAttach_lbl9.TabIndex = 162
        Me.FNMailFileAttach_lbl9.Text = "Attach File  : "
        '
        'SbntAttachFile9
        '
        Me.SbntAttachFile9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SbntAttachFile9.Image = CType(resources.GetObject("SbntAttachFile9.Image"), System.Drawing.Image)
        Me.SbntAttachFile9.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.SbntAttachFile9.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.SbntAttachFile9.Location = New System.Drawing.Point(824, 11)
        Me.SbntAttachFile9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SbntAttachFile9.Name = "SbntAttachFile9"
        Me.SbntAttachFile9.Size = New System.Drawing.Size(31, 31)
        Me.SbntAttachFile9.TabIndex = 161
        '
        'FNMailStatePriority_lbl
        '
        Me.FNMailStatePriority_lbl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNMailStatePriority_lbl.Location = New System.Drawing.Point(719, 55)
        Me.FNMailStatePriority_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNMailStatePriority_lbl.Name = "FNMailStatePriority_lbl"
        Me.FNMailStatePriority_lbl.Size = New System.Drawing.Size(53, 16)
        Me.FNMailStatePriority_lbl.TabIndex = 160
        Me.FNMailStatePriority_lbl.Text = "Priority  :"
        '
        'FNMailStatePriority9
        '
        Me.FNMailStatePriority9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNMailStatePriority9.Location = New System.Drawing.Point(796, 54)
        Me.FNMailStatePriority9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNMailStatePriority9.Name = "FNMailStatePriority9"
        Me.FNMailStatePriority9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNMailStatePriority9.Properties.Tag = "Priority"
        Me.FNMailStatePriority9.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNMailStatePriority9.Size = New System.Drawing.Size(59, 22)
        Me.FNMailStatePriority9.TabIndex = 160
        Me.FNMailStatePriority9.Tag = "2|"
        '
        'FTMailSubject9
        '
        Me.FTMailSubject9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTMailSubject9.Location = New System.Drawing.Point(182, 62)
        Me.FTMailSubject9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTMailSubject9.Name = "FTMailSubject9"
        Me.FTMailSubject9.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTMailSubject9.Properties.Appearance.Options.UseBackColor = True
        Me.FTMailSubject9.Properties.MaxLength = 500
        Me.FTMailSubject9.Size = New System.Drawing.Size(509, 22)
        Me.FTMailSubject9.TabIndex = 158
        Me.FTMailSubject9.Tag = "2|"
        '
        'FTMailTo9
        '
        Me.FTMailTo9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTMailTo9.Location = New System.Drawing.Point(182, 21)
        Me.FTMailTo9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTMailTo9.Name = "FTMailTo9"
        Me.FTMailTo9.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTMailTo9.Properties.Appearance.Options.UseBackColor = True
        Me.FTMailTo9.Properties.MaxLength = 500
        Me.FTMailTo9.Size = New System.Drawing.Size(509, 22)
        Me.FTMailTo9.TabIndex = 157
        Me.FTMailTo9.Tag = "2|"
        '
        'FTMailSubject_Lbl9
        '
        Me.FTMailSubject_Lbl9.Location = New System.Drawing.Point(119, 63)
        Me.FTMailSubject_Lbl9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTMailSubject_Lbl9.Name = "FTMailSubject_Lbl9"
        Me.FTMailSubject_Lbl9.Size = New System.Drawing.Size(52, 16)
        Me.FTMailSubject_Lbl9.TabIndex = 156
        Me.FTMailSubject_Lbl9.Text = "Subject :"
        '
        'FTMailTo_Lbl9
        '
        Me.FTMailTo_Lbl9.Location = New System.Drawing.Point(147, 23)
        Me.FTMailTo_Lbl9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTMailTo_Lbl9.Name = "FTMailTo_Lbl9"
        Me.FTMailTo_Lbl9.Size = New System.Drawing.Size(30, 16)
        Me.FTMailTo_Lbl9.TabIndex = 155
        Me.FTMailTo_Lbl9.Text = "TO : "
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog1"
        '
        'OtmCheckLanguage
        '
        Me.OtmCheckLanguage.Interval = 1000
        '
        'SpcShow
        '
        Me.SpcShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SpcShow.Location = New System.Drawing.Point(0, 0)
        Me.SpcShow.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SpcShow.Name = "SpcShow"
        Me.SpcShow.Panel1.Controls.Add(Me.FTChkStateSelect)
        Me.SpcShow.Panel1.Controls.Add(Me.ogcTMAILMessages)
        Me.SpcShow.Panel1.Controls.Add(Me._Lst)
        Me.SpcShow.Panel1.Text = "Panel1"
        Me.SpcShow.Panel2.Controls.Add(Me.PanelControl1)
        Me.SpcShow.Panel2.Text = "Panel2"
        Me.SpcShow.Size = New System.Drawing.Size(1231, 815)
        Me.SpcShow.SplitterPosition = 706
        Me.SpcShow.TabIndex = 146
        Me.SpcShow.Text = "SplitContainerControl1"
        '
        'FTChkStateSelect
        '
        Me.FTChkStateSelect.EditValue = "0"
        Me.FTChkStateSelect.Location = New System.Drawing.Point(149, 4)
        Me.FTChkStateSelect.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTChkStateSelect.Name = "FTChkStateSelect"
        Me.FTChkStateSelect.Properties.Caption = ""
        Me.FTChkStateSelect.Properties.ValueChecked = "1"
        Me.FTChkStateSelect.Properties.ValueUnchecked = "0"
        Me.FTChkStateSelect.Size = New System.Drawing.Size(20, 19)
        Me.FTChkStateSelect.TabIndex = 272
        Me.FTChkStateSelect.Tag = "2|"
        '
        'ogcTMAILMessages
        '
        Me.ogcTMAILMessages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcTMAILMessages.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcTMAILMessages.Location = New System.Drawing.Point(140, 0)
        Me.ogcTMAILMessages.MainView = Me.ogvTMAILMessages
        Me.ogcTMAILMessages.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcTMAILMessages.Name = "ogcTMAILMessages"
        Me.ogcTMAILMessages.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFTSelect})
        Me.ogcTMAILMessages.Size = New System.Drawing.Size(566, 815)
        Me.ogcTMAILMessages.TabIndex = 143
        Me.ogcTMAILMessages.Tag = ""
        Me.ogcTMAILMessages.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvTMAILMessages})
        '
        'ogvTMAILMessages
        '
        Me.ogvTMAILMessages.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTSelect, Me.ColFTMailId, Me.ColFTMailDate, Me.ColFTMailFrom, Me.ColFTMailTo, Me.ColFTMailSubject, Me.ColFTMailText, Me.ColFTMailStateOpen, Me.ColFDMailOpenDate, Me.ColFTMailOpenTime, Me.ColFNMailStateAttach, Me.ColFNMailStatePriority, Me.ColFNMailStateSend, Me.ColFNMailStateJobStatus, Me.ColTImageStatus, Me.ColImageOpen, Me.ColImagePriority, Me.ColImageAttach, Me.ColFNMailFileAttach, Me.ColFTInsTime, Me.ColFTMailFromTemp, Me.ColFNMailStateDelete, Me.ColFNMailStateType, Me.FNMailTypeInfo, Me.FTMailInfoRef, Me.FTCallMnuName, Me.FTCallMethodName})
        Me.ogvTMAILMessages.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus
        Me.ogvTMAILMessages.GridControl = Me.ogcTMAILMessages
        Me.ogvTMAILMessages.Images = Me.ilColumns
        Me.ogvTMAILMessages.Name = "ogvTMAILMessages"
        Me.ogvTMAILMessages.OptionsView.ColumnAutoWidth = False
        Me.ogvTMAILMessages.OptionsView.ShowAutoFilterRow = True
        Me.ogvTMAILMessages.OptionsView.ShowGroupPanel = False
        '
        'CFTSelect
        '
        Me.CFTSelect.Caption = " "
        Me.CFTSelect.ColumnEdit = Me.ReposFTSelect
        Me.CFTSelect.FieldName = "FTSelect"
        Me.CFTSelect.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.CFTSelect.Name = "CFTSelect"
        Me.CFTSelect.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSelect.OptionsColumn.AllowMove = False
        Me.CFTSelect.OptionsColumn.AllowShowHide = False
        Me.CFTSelect.OptionsColumn.ShowCaption = False
        Me.CFTSelect.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTSelect.Visible = True
        Me.CFTSelect.VisibleIndex = 0
        Me.CFTSelect.Width = 69
        '
        'ReposFTSelect
        '
        Me.ReposFTSelect.AutoHeight = False
        Me.ReposFTSelect.Caption = "Check"
        Me.ReposFTSelect.Name = "ReposFTSelect"
        Me.ReposFTSelect.ValueChecked = "1"
        Me.ReposFTSelect.ValueUnchecked = "0"
        '
        'ColFTMailId
        '
        Me.ColFTMailId.Caption = "Mail Id"
        Me.ColFTMailId.FieldName = "FTMailId"
        Me.ColFTMailId.Name = "ColFTMailId"
        Me.ColFTMailId.OptionsColumn.AllowEdit = False
        Me.ColFTMailId.OptionsColumn.ReadOnly = True
        Me.ColFTMailId.Width = 70
        '
        'ColFTMailDate
        '
        Me.ColFTMailDate.Caption = "Date"
        Me.ColFTMailDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.ColFTMailDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ColFTMailDate.FieldName = "FTMailDate"
        Me.ColFTMailDate.Name = "ColFTMailDate"
        Me.ColFTMailDate.OptionsColumn.AllowEdit = False
        Me.ColFTMailDate.OptionsColumn.ReadOnly = True
        Me.ColFTMailDate.Visible = True
        Me.ColFTMailDate.VisibleIndex = 6
        Me.ColFTMailDate.Width = 40
        '
        'ColFTMailFrom
        '
        Me.ColFTMailFrom.Caption = "From"
        Me.ColFTMailFrom.FieldName = "FTMailFrom"
        Me.ColFTMailFrom.Name = "ColFTMailFrom"
        Me.ColFTMailFrom.OptionsColumn.AllowEdit = False
        Me.ColFTMailFrom.OptionsColumn.AllowFocus = False
        Me.ColFTMailFrom.OptionsColumn.ReadOnly = True
        Me.ColFTMailFrom.OptionsColumn.ShowCaption = False
        Me.ColFTMailFrom.Visible = True
        Me.ColFTMailFrom.VisibleIndex = 4
        Me.ColFTMailFrom.Width = 66
        '
        'ColFTMailTo
        '
        Me.ColFTMailTo.Caption = "To"
        Me.ColFTMailTo.FieldName = "FTMailTo"
        Me.ColFTMailTo.Name = "ColFTMailTo"
        Me.ColFTMailTo.OptionsColumn.AllowEdit = False
        Me.ColFTMailTo.OptionsColumn.ReadOnly = True
        Me.ColFTMailTo.Width = 50
        '
        'ColFTMailSubject
        '
        Me.ColFTMailSubject.Caption = "Subject"
        Me.ColFTMailSubject.FieldName = "FTMailSubject"
        Me.ColFTMailSubject.Name = "ColFTMailSubject"
        Me.ColFTMailSubject.OptionsColumn.AllowEdit = False
        Me.ColFTMailSubject.OptionsColumn.ReadOnly = True
        Me.ColFTMailSubject.Visible = True
        Me.ColFTMailSubject.VisibleIndex = 5
        Me.ColFTMailSubject.Width = 350
        '
        'ColFTMailText
        '
        Me.ColFTMailText.Caption = "Text"
        Me.ColFTMailText.FieldName = "FTMailText"
        Me.ColFTMailText.Name = "ColFTMailText"
        Me.ColFTMailText.OptionsColumn.AllowEdit = False
        Me.ColFTMailText.OptionsColumn.ReadOnly = True
        Me.ColFTMailText.Width = 50
        '
        'ColFTMailStateOpen
        '
        Me.ColFTMailStateOpen.Caption = "Read"
        Me.ColFTMailStateOpen.FieldName = "FTMailStateOpen"
        Me.ColFTMailStateOpen.ImageIndex = 1
        Me.ColFTMailStateOpen.Name = "ColFTMailStateOpen"
        Me.ColFTMailStateOpen.OptionsColumn.AllowEdit = False
        Me.ColFTMailStateOpen.OptionsColumn.AllowFocus = False
        Me.ColFTMailStateOpen.OptionsColumn.AllowSize = False
        Me.ColFTMailStateOpen.OptionsColumn.ReadOnly = True
        Me.ColFTMailStateOpen.OptionsColumn.ShowCaption = False
        Me.ColFTMailStateOpen.Width = 70
        '
        'ColFDMailOpenDate
        '
        Me.ColFDMailOpenDate.Caption = "OpenDate"
        Me.ColFDMailOpenDate.FieldName = "FDMailOpenDate"
        Me.ColFDMailOpenDate.Name = "ColFDMailOpenDate"
        Me.ColFDMailOpenDate.OptionsColumn.AllowEdit = False
        Me.ColFDMailOpenDate.OptionsColumn.ReadOnly = True
        Me.ColFDMailOpenDate.Width = 60
        '
        'ColFTMailOpenTime
        '
        Me.ColFTMailOpenTime.Caption = "OpenTime"
        Me.ColFTMailOpenTime.FieldName = "FTMailOpenTime"
        Me.ColFTMailOpenTime.Name = "ColFTMailOpenTime"
        Me.ColFTMailOpenTime.OptionsColumn.AllowEdit = False
        Me.ColFTMailOpenTime.OptionsColumn.ReadOnly = True
        Me.ColFTMailOpenTime.Width = 70
        '
        'ColFNMailStateAttach
        '
        Me.ColFNMailStateAttach.Caption = "Attach"
        Me.ColFNMailStateAttach.FieldName = "FNMailStateAttach"
        Me.ColFNMailStateAttach.ImageIndex = 2
        Me.ColFNMailStateAttach.Name = "ColFNMailStateAttach"
        Me.ColFNMailStateAttach.OptionsColumn.AllowEdit = False
        Me.ColFNMailStateAttach.OptionsColumn.AllowFocus = False
        Me.ColFNMailStateAttach.OptionsColumn.AllowSize = False
        Me.ColFNMailStateAttach.OptionsColumn.ReadOnly = True
        Me.ColFNMailStateAttach.OptionsColumn.ShowCaption = False
        Me.ColFNMailStateAttach.Width = 70
        '
        'ColFNMailStatePriority
        '
        Me.ColFNMailStatePriority.Caption = "Priority"
        Me.ColFNMailStatePriority.FieldName = "FNMailStatePriority"
        Me.ColFNMailStatePriority.ImageIndex = 0
        Me.ColFNMailStatePriority.Name = "ColFNMailStatePriority"
        Me.ColFNMailStatePriority.OptionsColumn.AllowEdit = False
        Me.ColFNMailStatePriority.OptionsColumn.AllowFocus = False
        Me.ColFNMailStatePriority.OptionsColumn.AllowSize = False
        Me.ColFNMailStatePriority.OptionsColumn.FixedWidth = True
        Me.ColFNMailStatePriority.OptionsColumn.ReadOnly = True
        Me.ColFNMailStatePriority.OptionsColumn.ShowCaption = False
        Me.ColFNMailStatePriority.Width = 70
        '
        'ColFNMailStateSend
        '
        Me.ColFNMailStateSend.Caption = "StateSend"
        Me.ColFNMailStateSend.ColumnEdit = Me.RepositoryItemPictureEdit5
        Me.ColFNMailStateSend.FieldName = "FNMailStateSend"
        Me.ColFNMailStateSend.Name = "ColFNMailStateSend"
        Me.ColFNMailStateSend.OptionsColumn.AllowEdit = False
        Me.ColFNMailStateSend.OptionsColumn.ReadOnly = True
        Me.ColFNMailStateSend.Width = 60
        '
        'ColFNMailStateJobStatus
        '
        Me.ColFNMailStateJobStatus.Caption = "JobStatus"
        Me.ColFNMailStateJobStatus.FieldName = "FNMailStateJobStatus"
        Me.ColFNMailStateJobStatus.Name = "ColFNMailStateJobStatus"
        Me.ColFNMailStateJobStatus.OptionsColumn.AllowEdit = False
        Me.ColFNMailStateJobStatus.OptionsColumn.ReadOnly = True
        Me.ColFNMailStateJobStatus.Width = 87
        '
        'ColTImageStatus
        '
        Me.ColTImageStatus.Caption = "GridColumn1"
        Me.ColTImageStatus.ColumnEdit = Me.RepositoryItemPictureEdit1
        Me.ColTImageStatus.FieldName = "FTImageStatus"
        Me.ColTImageStatus.ImageAlignment = System.Drawing.StringAlignment.Center
        Me.ColTImageStatus.Name = "ColTImageStatus"
        Me.ColTImageStatus.OptionsColumn.AllowEdit = False
        Me.ColTImageStatus.OptionsColumn.ReadOnly = True
        Me.ColTImageStatus.OptionsColumn.ShowCaption = False
        Me.ColTImageStatus.Width = 30
        '
        'ColImageOpen
        '
        Me.ColImageOpen.Caption = "GridColumn1"
        Me.ColImageOpen.ColumnEdit = Me.RepositoryItemPictureEdit6
        Me.ColImageOpen.FieldName = "FTImageOpen"
        Me.ColImageOpen.ImageAlignment = System.Drawing.StringAlignment.Center
        Me.ColImageOpen.ImageIndex = 3
        Me.ColImageOpen.Name = "ColImageOpen"
        Me.ColImageOpen.OptionsColumn.AllowEdit = False
        Me.ColImageOpen.OptionsColumn.ReadOnly = True
        Me.ColImageOpen.OptionsColumn.ShowCaption = False
        Me.ColImageOpen.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText
        Me.ColImageOpen.Visible = True
        Me.ColImageOpen.VisibleIndex = 2
        Me.ColImageOpen.Width = 30
        '
        'ColImagePriority
        '
        Me.ColImagePriority.Caption = "GridColumn1"
        Me.ColImagePriority.ColumnEdit = Me.RepositoryItemPictureEdit7
        Me.ColImagePriority.FieldName = "FTImagePriority"
        Me.ColImagePriority.ImageAlignment = System.Drawing.StringAlignment.Center
        Me.ColImagePriority.ImageIndex = 0
        Me.ColImagePriority.Name = "ColImagePriority"
        Me.ColImagePriority.OptionsColumn.AllowEdit = False
        Me.ColImagePriority.OptionsColumn.ReadOnly = True
        Me.ColImagePriority.OptionsColumn.ShowCaption = False
        Me.ColImagePriority.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText
        Me.ColImagePriority.Visible = True
        Me.ColImagePriority.VisibleIndex = 1
        Me.ColImagePriority.Width = 29
        '
        'ColImageAttach
        '
        Me.ColImageAttach.Caption = "GridColumn1"
        Me.ColImageAttach.ColumnEdit = Me.RepositoryItemPictureEdit4
        Me.ColImageAttach.FieldName = "FTImageAttach"
        Me.ColImageAttach.ImageAlignment = System.Drawing.StringAlignment.Center
        Me.ColImageAttach.ImageIndex = 2
        Me.ColImageAttach.Name = "ColImageAttach"
        Me.ColImageAttach.OptionsColumn.AllowEdit = False
        Me.ColImageAttach.OptionsColumn.ReadOnly = True
        Me.ColImageAttach.OptionsColumn.ShowCaption = False
        Me.ColImageAttach.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText
        Me.ColImageAttach.Visible = True
        Me.ColImageAttach.VisibleIndex = 3
        Me.ColImageAttach.Width = 30
        '
        'ColFNMailFileAttach
        '
        Me.ColFNMailFileAttach.Caption = "GridColumn1"
        Me.ColFNMailFileAttach.FieldName = "FNMailFileAttach"
        Me.ColFNMailFileAttach.Name = "ColFNMailFileAttach"
        Me.ColFNMailFileAttach.OptionsColumn.AllowEdit = False
        Me.ColFNMailFileAttach.OptionsColumn.ReadOnly = True
        '
        'ColFTInsTime
        '
        Me.ColFTInsTime.Caption = "เวลา"
        Me.ColFTInsTime.FieldName = "FTInsTime"
        Me.ColFTInsTime.Name = "ColFTInsTime"
        Me.ColFTInsTime.OptionsColumn.AllowEdit = False
        Me.ColFTInsTime.OptionsColumn.ReadOnly = True
        Me.ColFTInsTime.Visible = True
        Me.ColFTInsTime.VisibleIndex = 7
        Me.ColFTInsTime.Width = 60
        '
        'ColFTMailFromTemp
        '
        Me.ColFTMailFromTemp.Caption = "FTMailFromTemp"
        Me.ColFTMailFromTemp.FieldName = "FTMailFromTemp"
        Me.ColFTMailFromTemp.Name = "ColFTMailFromTemp"
        Me.ColFTMailFromTemp.OptionsColumn.AllowEdit = False
        Me.ColFTMailFromTemp.OptionsColumn.ReadOnly = True
        '
        'ColFNMailStateDelete
        '
        Me.ColFNMailStateDelete.Caption = "FNMailStateDelete"
        Me.ColFNMailStateDelete.FieldName = "FNMailStateDelete"
        Me.ColFNMailStateDelete.Name = "ColFNMailStateDelete"
        Me.ColFNMailStateDelete.OptionsColumn.AllowEdit = False
        Me.ColFNMailStateDelete.OptionsColumn.ReadOnly = True
        '
        'ColFNMailStateType
        '
        Me.ColFNMailStateType.Caption = "FNMailStateType"
        Me.ColFNMailStateType.FieldName = "FNMailStateType"
        Me.ColFNMailStateType.Name = "ColFNMailStateType"
        Me.ColFNMailStateType.OptionsColumn.AllowEdit = False
        Me.ColFNMailStateType.OptionsColumn.ReadOnly = True
        '
        'FNMailTypeInfo
        '
        Me.FNMailTypeInfo.Caption = "FNMailTypeInfo"
        Me.FNMailTypeInfo.FieldName = "FNMailTypeInfo"
        Me.FNMailTypeInfo.Name = "FNMailTypeInfo"
        '
        'FTMailInfoRef
        '
        Me.FTMailInfoRef.Caption = "FTMailInfoRef"
        Me.FTMailInfoRef.FieldName = "FTMailInfoRef"
        Me.FTMailInfoRef.Name = "FTMailInfoRef"
        '
        'FTCallMnuName
        '
        Me.FTCallMnuName.Caption = "FTCallMnuName"
        Me.FTCallMnuName.FieldName = "FTCallMnuName"
        Me.FTCallMnuName.Name = "FTCallMnuName"
        '
        'FTCallMethodName
        '
        Me.FTCallMethodName.Caption = "FTCallMethodName"
        Me.FTCallMethodName.FieldName = "FTCallMethodName"
        Me.FTCallMethodName.Name = "FTCallMethodName"
        '
        '_Lst
        '
        Me._Lst.Dock = System.Windows.Forms.DockStyle.Left
        Me._Lst.Location = New System.Drawing.Point(0, 0)
        Me._Lst.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me._Lst.Name = "_Lst"
        Me._Lst.Size = New System.Drawing.Size(140, 815)
        Me._Lst.TabIndex = 1
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.olbFTCallMethodName)
        Me.PanelControl1.Controls.Add(Me.olbFTCallMnuName)
        Me.PanelControl1.Controls.Add(Me.olblink)
        Me.PanelControl1.Controls.Add(Me.TFrom_Lbl)
        Me.PanelControl1.Controls.Add(Me.FTMailTo_Lbl_2)
        Me.PanelControl1.Controls.Add(Me.FTMailDate_Lbl_2)
        Me.PanelControl1.Controls.Add(Me.FTMailSubject_Lbl_2)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Controls.Add(Me.ogcTMAILFileAttach)
        Me.PanelControl1.Controls.Add(Me.PicMail)
        Me.PanelControl1.Controls.Add(Me.recMessage)
        Me.PanelControl1.Controls.Add(Me.FTMailTo_Lbl)
        Me.PanelControl1.Controls.Add(Me.FTMailDate_Lbl)
        Me.PanelControl1.Controls.Add(Me.FTMailSubject_Lbl)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(519, 815)
        Me.PanelControl1.TabIndex = 146
        '
        'olbFTCallMethodName
        '
        Me.olbFTCallMethodName.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.olbFTCallMethodName.Appearance.Options.UseFont = True
        Me.olbFTCallMethodName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbFTCallMethodName.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.olbFTCallMethodName.Location = New System.Drawing.Point(121, 185)
        Me.olbFTCallMethodName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.olbFTCallMethodName.Name = "olbFTCallMethodName"
        Me.olbFTCallMethodName.Size = New System.Drawing.Size(89, 20)
        Me.olbFTCallMethodName.TabIndex = 161
        Me.olbFTCallMethodName.Text = "FTCallMnuName"
        Me.olbFTCallMethodName.Visible = False
        '
        'olbFTCallMnuName
        '
        Me.olbFTCallMnuName.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.olbFTCallMnuName.Appearance.Options.UseFont = True
        Me.olbFTCallMnuName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbFTCallMnuName.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.olbFTCallMnuName.Location = New System.Drawing.Point(13, 185)
        Me.olbFTCallMnuName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.olbFTCallMnuName.Name = "olbFTCallMnuName"
        Me.olbFTCallMnuName.Size = New System.Drawing.Size(89, 20)
        Me.olbFTCallMnuName.TabIndex = 160
        Me.olbFTCallMnuName.Text = "FTCallMnuName"
        Me.olbFTCallMnuName.Visible = False
        '
        'olblink
        '
        Me.olblink.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.olblink.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.olblink.Appearance.Options.UseFont = True
        Me.olblink.Appearance.Options.UseForeColor = True
        Me.olblink.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.olblink.Cursor = System.Windows.Forms.Cursors.Hand
        Me.olblink.Location = New System.Drawing.Point(16, 212)
        Me.olblink.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.olblink.Name = "olblink"
        Me.olblink.Size = New System.Drawing.Size(0, 19)
        Me.olblink.TabIndex = 159
        Me.olblink.Visible = False
        '
        'TFrom_Lbl
        '
        Me.TFrom_Lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TFrom_Lbl.Appearance.Options.UseFont = True
        Me.TFrom_Lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.TFrom_Lbl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.TFrom_Lbl.Location = New System.Drawing.Point(29, 158)
        Me.TFrom_Lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TFrom_Lbl.Name = "TFrom_Lbl"
        Me.TFrom_Lbl.Size = New System.Drawing.Size(44, 19)
        Me.TFrom_Lbl.TabIndex = 158
        Me.TFrom_Lbl.Text = "From"
        Me.TFrom_Lbl.Visible = False
        '
        'FTMailTo_Lbl_2
        '
        Me.FTMailTo_Lbl_2.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FTMailTo_Lbl_2.Appearance.Options.UseFont = True
        Me.FTMailTo_Lbl_2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.FTMailTo_Lbl_2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.FTMailTo_Lbl_2.Location = New System.Drawing.Point(77, 130)
        Me.FTMailTo_Lbl_2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTMailTo_Lbl_2.Name = "FTMailTo_Lbl_2"
        Me.FTMailTo_Lbl_2.Size = New System.Drawing.Size(178, 19)
        Me.FTMailTo_Lbl_2.TabIndex = 157
        Me.FTMailTo_Lbl_2.Text = "xxxx"
        '
        'FTMailDate_Lbl_2
        '
        Me.FTMailDate_Lbl_2.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FTMailDate_Lbl_2.Appearance.Options.UseFont = True
        Me.FTMailDate_Lbl_2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.FTMailDate_Lbl_2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.FTMailDate_Lbl_2.Location = New System.Drawing.Point(76, 103)
        Me.FTMailDate_Lbl_2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTMailDate_Lbl_2.Name = "FTMailDate_Lbl_2"
        Me.FTMailDate_Lbl_2.Size = New System.Drawing.Size(177, 19)
        Me.FTMailDate_Lbl_2.TabIndex = 156
        Me.FTMailDate_Lbl_2.Text = "DD/MM/YYYY"
        '
        'FTMailSubject_Lbl_2
        '
        Me.FTMailSubject_Lbl_2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTMailSubject_Lbl_2.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FTMailSubject_Lbl_2.Appearance.Options.UseFont = True
        Me.FTMailSubject_Lbl_2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.FTMailSubject_Lbl_2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.FTMailSubject_Lbl_2.Location = New System.Drawing.Point(213, 15)
        Me.FTMailSubject_Lbl_2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTMailSubject_Lbl_2.Name = "FTMailSubject_Lbl_2"
        Me.FTMailSubject_Lbl_2.Size = New System.Drawing.Size(260, 57)
        Me.FTMailSubject_Lbl_2.TabIndex = 155
        Me.FTMailSubject_Lbl_2.Text = "xxxx" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "xxxx" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "xxxx"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.LabelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.LabelControl1.Location = New System.Drawing.Point(261, 82)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(89, 38)
        Me.LabelControl1.TabIndex = 154
        Me.LabelControl1.Text = "Attach File  :"
        '
        'ogcTMAILFileAttach
        '
        Me.ogcTMAILFileAttach.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcTMAILFileAttach.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcTMAILFileAttach.Location = New System.Drawing.Point(259, 108)
        Me.ogcTMAILFileAttach.MainView = Me.ogvTMAILFileAttach
        Me.ogcTMAILFileAttach.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcTMAILFileAttach.Name = "ogcTMAILFileAttach"
        Me.ogcTMAILFileAttach.Size = New System.Drawing.Size(216, 129)
        Me.ogcTMAILFileAttach.TabIndex = 153
        Me.ogcTMAILFileAttach.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvTMAILFileAttach, Me.GridView1})
        '
        'ogvTMAILFileAttach
        '
        Me.ogvTMAILFileAttach.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.ogvTMAILFileAttach.GridControl = Me.ogcTMAILFileAttach
        Me.ogvTMAILFileAttach.Name = "ogvTMAILFileAttach"
        Me.ogvTMAILFileAttach.OptionsView.ShowColumnHeaders = False
        Me.ogvTMAILFileAttach.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "GridColumn1"
        Me.GridColumn1.FieldName = "FTMailId"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Attach File  "
        Me.GridColumn2.FieldName = "FNMailFileAttach"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.ShowCaption = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.ogcTMAILFileAttach
        Me.GridView1.Name = "GridView1"
        '
        'PicMail
        '
        Me.PicMail.Location = New System.Drawing.Point(13, 5)
        Me.PicMail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PicMail.Name = "PicMail"
        Me.PicMail.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.PicMail.Properties.ZoomAccelerationFactor = 1.0R
        Me.PicMail.Size = New System.Drawing.Size(96, 91)
        Me.PicMail.TabIndex = 152
        '
        'recMessage
        '
        Me.recMessage.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple
        Me.recMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.recMessage.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.recMessage.Location = New System.Drawing.Point(5, 249)
        Me.recMessage.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.recMessage.Name = "recMessage"
        Me.recMessage.Size = New System.Drawing.Size(470, 519)
        Me.recMessage.TabIndex = 150
        '
        'FTMailTo_Lbl
        '
        Me.FTMailTo_Lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FTMailTo_Lbl.Appearance.Options.UseFont = True
        Me.FTMailTo_Lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.FTMailTo_Lbl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.FTMailTo_Lbl.Location = New System.Drawing.Point(29, 130)
        Me.FTMailTo_Lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTMailTo_Lbl.Name = "FTMailTo_Lbl"
        Me.FTMailTo_Lbl.Size = New System.Drawing.Size(59, 19)
        Me.FTMailTo_Lbl.TabIndex = 147
        Me.FTMailTo_Lbl.Text = "To  :  "
        '
        'FTMailDate_Lbl
        '
        Me.FTMailDate_Lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FTMailDate_Lbl.Appearance.Options.UseFont = True
        Me.FTMailDate_Lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.FTMailDate_Lbl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.FTMailDate_Lbl.Location = New System.Drawing.Point(16, 103)
        Me.FTMailDate_Lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTMailDate_Lbl.Name = "FTMailDate_Lbl"
        Me.FTMailDate_Lbl.Size = New System.Drawing.Size(59, 21)
        Me.FTMailDate_Lbl.TabIndex = 146
        Me.FTMailDate_Lbl.Text = "วันที่   :"
        '
        'FTMailSubject_Lbl
        '
        Me.FTMailSubject_Lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FTMailSubject_Lbl.Appearance.Options.UseFont = True
        Me.FTMailSubject_Lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.FTMailSubject_Lbl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.FTMailSubject_Lbl.Location = New System.Drawing.Point(131, 15)
        Me.FTMailSubject_Lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTMailSubject_Lbl.Name = "FTMailSubject_Lbl"
        Me.FTMailSubject_Lbl.Size = New System.Drawing.Size(89, 21)
        Me.FTMailSubject_Lbl.TabIndex = 145
        Me.FTMailSubject_Lbl.Text = "หัวข้อเรื่อง : "
        '
        'wMailMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1231, 815)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.SpcShow)
        Me.Controls.Add(Me.PclShowMail)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wMailMain"
        Me.Text = "MailMain"
        CType(Me.RepositoryItemPictureEdit5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.icEditors, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PclShowMail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PclShowMail.ResumeLayout(False)
        Me.PclShowMail.PerformLayout()
        CType(Me.PicMail_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcTMAILFileAttach9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvTMAILFileAttach9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNMailStatePriority9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTMailSubject9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTMailTo9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpcShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SpcShow.ResumeLayout(False)
        CType(Me.FTChkStateSelect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcTMAILMessages, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvTMAILMessages, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._Lst, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.ogcTMAILFileAttach, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvTMAILFileAttach, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicMail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmrefreshmail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmaddnewMail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmmail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ilColumns As System.Windows.Forms.ImageList
    Friend WithEvents icEditors As DevExpress.Utils.ImageCollection
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ocmReplyMail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmForwordMail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdeletemail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RepositoryItemPictureEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents RepositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents RepositoryItemPictureEdit6 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents RepositoryItemPictureEdit7 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents RepositoryItemPictureEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents PclShowMail As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ogcTMAILFileAttach9 As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvTMAILFileAttach9 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMailFileAttach_lbl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SbntAttachFile9 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNMailStatePriority_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNMailStatePriority9 As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FTMailSubject9 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTMailTo9 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTMailSubject_Lbl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTMailTo_Lbl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTMailText29 As DevExpress.XtraRichEdit.RichEditControl
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ocmCancelMail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PicMail_2 As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents OtmCheckLanguage As System.Windows.Forms.Timer
    Friend WithEvents ocmUndoMail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SpcShow As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents ogcTMAILMessages As DevExpress.XtraGrid.GridControl
    Protected Friend WithEvents ogvTMAILMessages As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColFTMailId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTMailDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTMailFrom As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTMailTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTMailSubject As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTMailText As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTMailStateOpen As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFDMailOpenDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTMailOpenTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNMailStateAttach As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNMailStatePriority As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNMailStateSend As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNMailStateJobStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColTImageStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColImageOpen As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColImagePriority As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColImageAttach As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNMailFileAttach As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTInsTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTMailFromTemp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNMailStateDelete As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNMailStateType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents _Lst As DevExpress.XtraTreeList.TreeList
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents TFrom_Lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTMailTo_Lbl_2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTMailDate_Lbl_2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTMailSubject_Lbl_2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogcTMAILFileAttach As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvTMAILFileAttach As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PicMail As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents recMessage As DevExpress.XtraRichEdit.RichEditControl
    Friend WithEvents FTMailTo_Lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTMailDate_Lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTMailSubject_Lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNMailTypeInfo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMailInfoRef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCallMnuName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCallMethodName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents olbFTCallMethodName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents olbFTCallMnuName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents olblink As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTChkStateSelect As DevExpress.XtraEditors.CheckEdit
End Class
