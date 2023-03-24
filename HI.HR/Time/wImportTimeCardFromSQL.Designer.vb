<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wImportTimeCardFromSQL
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmimport = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CSN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryFTApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepColFTLeavePay = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepColFTHoliday = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepFTStaCalSSO = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepFTApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepFTLeavePay = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepColFTLeavePay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepColFTHoliday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTStaCalSSO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTLeavePay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.ocmcancel)
        Me.GroupControl1.Controls.Add(Me.ocmimport)
        Me.GroupControl1.Location = New System.Drawing.Point(1, 365)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.ShowCaption = False
        Me.GroupControl1.Size = New System.Drawing.Size(803, 52)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "GroupControl1"
        '
        'ocmcancel
        '
        Me.ocmcancel.Location = New System.Drawing.Point(472, 14)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(140, 31)
        Me.ocmcancel.TabIndex = 103
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmimport
        '
        Me.ocmimport.Location = New System.Drawing.Point(168, 14)
        Me.ocmimport.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmimport.Name = "ocmimport"
        Me.ocmimport.Size = New System.Drawing.Size(140, 31)
        Me.ocmimport.TabIndex = 102
        Me.ocmimport.TabStop = False
        Me.ocmimport.Tag = "2|"
        Me.ocmimport.Text = "IMPORT TIME"
        '
        'GroupControl2
        '
        Me.GroupControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl2.Controls.Add(Me.ogc)
        Me.GroupControl2.Controls.Add(Me.FNHSysCmpId_None)
        Me.GroupControl2.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.GroupControl2.Controls.Add(Me.FNHSysCmpId)
        Me.GroupControl2.Location = New System.Drawing.Point(1, 5)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(803, 359)
        Me.GroupControl2.TabIndex = 2
        '
        'ogc
        '
        Me.ogc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Location = New System.Drawing.Point(168, 71)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect, Me.RepositoryFTApproveState, Me.RepColFTLeavePay, Me.RepColFTHoliday, Me.RepFTStaCalSSO, Me.RepFTApproveState, Me.RepFTLeavePay})
        Me.ogc.Size = New System.Drawing.Size(492, 271)
        Me.ogc.TabIndex = 432
        Me.ogc.TabStop = False
        Me.ogc.Tag = "2|"
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTDate, Me.CSN})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowGroup = False
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'CFTDate
        '
        Me.CFTDate.Caption = "วันที่"
        Me.CFTDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.CFTDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.CFTDate.FieldName = "FTDate"
        Me.CFTDate.Name = "CFTDate"
        Me.CFTDate.OptionsColumn.AllowEdit = False
        Me.CFTDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTDate.OptionsColumn.AllowMove = False
        Me.CFTDate.OptionsColumn.AllowShowHide = False
        Me.CFTDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTDate.Visible = True
        Me.CFTDate.VisibleIndex = 0
        Me.CFTDate.Width = 141
        '
        'CSN
        '
        Me.CSN.Caption = "หมายเลขเครื่อง"
        Me.CSN.FieldName = "sn"
        Me.CSN.Name = "CSN"
        Me.CSN.OptionsColumn.AllowEdit = False
        Me.CSN.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CSN.OptionsColumn.AllowMove = False
        Me.CSN.OptionsColumn.AllowShowHide = False
        Me.CSN.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CSN.Visible = True
        Me.CSN.VisibleIndex = 1
        Me.CSN.Width = 298
        '
        'RepositoryFTSelect
        '
        Me.RepositoryFTSelect.AutoHeight = False
        Me.RepositoryFTSelect.Caption = "Check"
        Me.RepositoryFTSelect.Name = "RepositoryFTSelect"
        Me.RepositoryFTSelect.ValueChecked = "1"
        Me.RepositoryFTSelect.ValueUnchecked = "0"
        '
        'RepositoryFTApproveState
        '
        Me.RepositoryFTApproveState.AutoHeight = False
        Me.RepositoryFTApproveState.Caption = "Check"
        Me.RepositoryFTApproveState.Name = "RepositoryFTApproveState"
        Me.RepositoryFTApproveState.ValueChecked = "1"
        Me.RepositoryFTApproveState.ValueUnchecked = "0"
        '
        'RepColFTLeavePay
        '
        Me.RepColFTLeavePay.AutoHeight = False
        Me.RepColFTLeavePay.Caption = "Check"
        Me.RepColFTLeavePay.Name = "RepColFTLeavePay"
        Me.RepColFTLeavePay.ValueChecked = "1"
        Me.RepColFTLeavePay.ValueUnchecked = "0"
        '
        'RepColFTHoliday
        '
        Me.RepColFTHoliday.AutoHeight = False
        Me.RepColFTHoliday.Caption = "Check"
        Me.RepColFTHoliday.Name = "RepColFTHoliday"
        Me.RepColFTHoliday.ValueChecked = "1"
        Me.RepColFTHoliday.ValueUnchecked = "0"
        '
        'RepFTStaCalSSO
        '
        Me.RepFTStaCalSSO.AutoHeight = False
        Me.RepFTStaCalSSO.Caption = "Check"
        Me.RepFTStaCalSSO.Name = "RepFTStaCalSSO"
        Me.RepFTStaCalSSO.ValueChecked = "1"
        Me.RepFTStaCalSSO.ValueUnchecked = "0"
        '
        'RepFTApproveState
        '
        Me.RepFTApproveState.AutoHeight = False
        Me.RepFTApproveState.Caption = "Check"
        Me.RepFTApproveState.Name = "RepFTApproveState"
        Me.RepFTApproveState.ValueChecked = "1"
        Me.RepFTApproveState.ValueUnchecked = "0"
        '
        'RepFTLeavePay
        '
        Me.RepFTLeavePay.AutoHeight = False
        Me.RepFTLeavePay.Caption = "Check"
        Me.RepFTLeavePay.Name = "RepFTLeavePay"
        Me.RepFTLeavePay.ValueChecked = "1"
        Me.RepFTLeavePay.ValueUnchecked = "0"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(301, 41)
        Me.FNHSysCmpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(359, 22)
        Me.FNHSysCmpId_None.TabIndex = 431
        Me.FNHSysCmpId_None.Tag = ""
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(19, 42)
        Me.FNHSysCmpId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(142, 22)
        Me.FNHSysCmpId_lbl.TabIndex = 430
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(168, 41)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.ReadOnly = True
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(132, 22)
        Me.FNHSysCmpId.TabIndex = 429
        Me.FNHSysCmpId.Tag = ""
        '
        'wImportTimeCardFromSQL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(806, 419)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wImportTimeCardFromSQL"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import Time Card"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepColFTLeavePay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepColFTHoliday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTStaCalSSO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTLeavePay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmimport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CFTDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CSN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryFTApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepColFTLeavePay As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepColFTHoliday As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepFTStaCalSSO As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepFTApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepFTLeavePay As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class
