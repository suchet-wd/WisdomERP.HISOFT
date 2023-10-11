<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wSMPQCSendSupllist
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcopy = New DevExpress.XtraEditors.SimpleButton()
        Me.ogrpNotSendSupl = New DevExpress.XtraEditors.GroupControl()
        Me.ogcNotSendSupl = New DevExpress.XtraGrid.GridControl()
        Me.ogvNotSendSupl = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTRcvSuplNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTBarcodeSendSuplNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTBarcodeBundleNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSuplCode = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.ochkselectall = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.ogrpNotSendSupl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpNotSendSupl.SuspendLayout()
        CType(Me.ogcNotSendSupl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvNotSendSupl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSuplCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(640, 588)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(141, 31)
        Me.ocmcancel.TabIndex = 312
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmcopy
        '
        Me.ocmcopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcopy.Location = New System.Drawing.Point(496, 588)
        Me.ocmcopy.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcopy.Name = "ocmcopy"
        Me.ocmcopy.Size = New System.Drawing.Size(138, 31)
        Me.ocmcopy.TabIndex = 311
        Me.ocmcopy.TabStop = False
        Me.ocmcopy.Tag = "2|"
        Me.ocmcopy.Text = "Save"
        '
        'ogrpNotSendSupl
        '
        Me.ogrpNotSendSupl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogrpNotSendSupl.Controls.Add(Me.ogcNotSendSupl)
        Me.ogrpNotSendSupl.Controls.Add(Me.ochkselectall)
        Me.ogrpNotSendSupl.Location = New System.Drawing.Point(8, 7)
        Me.ogrpNotSendSupl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpNotSendSupl.Name = "ogrpNotSendSupl"
        Me.ogrpNotSendSupl.Size = New System.Drawing.Size(773, 572)
        Me.ogrpNotSendSupl.TabIndex = 313
        '
        'ogcNotSendSupl
        '
        Me.ogcNotSendSupl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcNotSendSupl.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcNotSendSupl.Location = New System.Drawing.Point(2, 28)
        Me.ogcNotSendSupl.MainView = Me.ogvNotSendSupl
        Me.ogcNotSendSupl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcNotSendSupl.Name = "ogcNotSendSupl"
        Me.ogcNotSendSupl.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSuplCode, Me.RepositoryItemFTSelect})
        Me.ogcNotSendSupl.Size = New System.Drawing.Size(769, 542)
        Me.ogcNotSendSupl.TabIndex = 1
        Me.ogcNotSendSupl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvNotSendSupl})
        '
        'ogvNotSendSupl
        '
        Me.ogvNotSendSupl.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.cFTRcvSuplNo, Me.cFTBarcodeSendSuplNo, Me.cFTBarcodeBundleNo, Me.cFTOrderNo, Me.cFNQuantity})
        Me.ogvNotSendSupl.GridControl = Me.ogcNotSendSupl
        Me.ogvNotSendSupl.Name = "ogvNotSendSupl"
        Me.ogvNotSendSupl.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.ogvNotSendSupl.OptionsView.ColumnAutoWidth = False
        Me.ogvNotSendSupl.OptionsView.ShowFooter = True
        Me.ogvNotSendSupl.OptionsView.ShowGroupPanel = False
        '
        'FTSelect
        '
        Me.FTSelect.Caption = "FTSelect"
        Me.FTSelect.ColumnEdit = Me.RepositoryItemFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 61
        '
        'RepositoryItemFTSelect
        '
        Me.RepositoryItemFTSelect.AutoHeight = False
        Me.RepositoryItemFTSelect.Caption = "Check"
        Me.RepositoryItemFTSelect.Name = "RepositoryItemFTSelect"
        Me.RepositoryItemFTSelect.ValueChecked = "1"
        Me.RepositoryItemFTSelect.ValueUnchecked = "0"
        '
        'cFTRcvSuplNo
        '
        Me.cFTRcvSuplNo.Caption = "FTRcvSuplNo"
        Me.cFTRcvSuplNo.FieldName = "FTRcvSuplNo"
        Me.cFTRcvSuplNo.Name = "cFTRcvSuplNo"
        Me.cFTRcvSuplNo.OptionsColumn.AllowEdit = False
        Me.cFTRcvSuplNo.Visible = True
        Me.cFTRcvSuplNo.VisibleIndex = 1
        Me.cFTRcvSuplNo.Width = 106
        '
        'cFTBarcodeSendSuplNo
        '
        Me.cFTBarcodeSendSuplNo.Caption = "FTBarcodeSendSuplNo"
        Me.cFTBarcodeSendSuplNo.FieldName = "FTBarcodeSendSuplNo"
        Me.cFTBarcodeSendSuplNo.Name = "cFTBarcodeSendSuplNo"
        Me.cFTBarcodeSendSuplNo.OptionsColumn.AllowEdit = False
        Me.cFTBarcodeSendSuplNo.Visible = True
        Me.cFTBarcodeSendSuplNo.VisibleIndex = 2
        Me.cFTBarcodeSendSuplNo.Width = 146
        '
        'cFTBarcodeBundleNo
        '
        Me.cFTBarcodeBundleNo.Caption = "FTBarcodeBundleNo"
        Me.cFTBarcodeBundleNo.FieldName = "FTBarcodeBundleNo"
        Me.cFTBarcodeBundleNo.Name = "cFTBarcodeBundleNo"
        Me.cFTBarcodeBundleNo.OptionsColumn.AllowEdit = False
        Me.cFTBarcodeBundleNo.Visible = True
        Me.cFTBarcodeBundleNo.VisibleIndex = 3
        Me.cFTBarcodeBundleNo.Width = 114
        '
        'cFTOrderNo
        '
        Me.cFTOrderNo.Caption = "FTOrderNo"
        Me.cFTOrderNo.FieldName = "FTOrderNo"
        Me.cFTOrderNo.Name = "cFTOrderNo"
        Me.cFTOrderNo.OptionsColumn.AllowEdit = False
        Me.cFTOrderNo.Visible = True
        Me.cFTOrderNo.VisibleIndex = 4
        Me.cFTOrderNo.Width = 96
        '
        'cFNQuantity
        '
        Me.cFNQuantity.Caption = "FNQuantity"
        Me.cFNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.cFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantity.FieldName = "FNQuantity"
        Me.cFNQuantity.Name = "cFNQuantity"
        Me.cFNQuantity.OptionsColumn.AllowEdit = False
        Me.cFNQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", "{0:n0}")})
        Me.cFNQuantity.Visible = True
        Me.cFNQuantity.VisibleIndex = 5
        '
        'RepositoryFTSuplCode
        '
        Me.RepositoryFTSuplCode.AutoHeight = False
        Me.RepositoryFTSuplCode.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "99", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.RepositoryFTSuplCode.Name = "RepositoryFTSuplCode"
        '
        'ochkselectall
        '
        Me.ochkselectall.Location = New System.Drawing.Point(38, 0)
        Me.ochkselectall.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ochkselectall.Name = "ochkselectall"
        Me.ochkselectall.Properties.Caption = "Select All"
        Me.ochkselectall.Size = New System.Drawing.Size(196, 24)
        Me.ochkselectall.TabIndex = 309
        '
        'wSMPQCSendSupllist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(793, 633)
        Me.Controls.Add(Me.ogrpNotSendSupl)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmcopy)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wSMPQCSendSupllist"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wSMPQCSendSupllist"
        CType(Me.ogrpNotSendSupl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpNotSendSupl.ResumeLayout(False)
        CType(Me.ogcNotSendSupl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvNotSendSupl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSuplCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcopy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogrpNotSendSupl As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcNotSendSupl As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvNotSendSupl As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTRcvSuplNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTBarcodeSendSuplNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTBarcodeBundleNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTSuplCode As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents ochkselectall As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
End Class
