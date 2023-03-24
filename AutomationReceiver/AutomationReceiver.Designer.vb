<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AutomationReceiver
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTLineNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTMachine = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTButton = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryFTApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepColFTLeavePay = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepColFTHoliday = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepFTStaCalSSO = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepFTApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepFTLeavePay = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ocmstart = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmstop = New DevExpress.XtraEditors.SimpleButton()
        Me.FNComport_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ocmRefreshport = New DevExpress.XtraEditors.SimpleButton()
        Me.Timer1 = New System.Windows.Forms.Timer()
        Me.ogcdataport = New DevExpress.XtraGrid.GridControl()
        Me.ogvdataport = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CFTComport = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit5 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit6 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit7 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepColFTLeavePay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepColFTHoliday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTStaCalSSO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTLeavePay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcdataport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdataport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.ogc)
        Me.GroupControl1.Location = New System.Drawing.Point(4, 152)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1095, 349)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "GroupControl1"
        '
        'ogc
        '
        Me.ogc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Location = New System.Drawing.Point(2, 25)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect, Me.RepositoryFTApproveState, Me.RepColFTLeavePay, Me.RepColFTHoliday, Me.RepFTStaCalSSO, Me.RepFTApproveState, Me.RepFTLeavePay})
        Me.ogc.Size = New System.Drawing.Size(1091, 322)
        Me.ogc.TabIndex = 4
        Me.ogc.TabStop = False
        Me.ogc.Tag = "2|"
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Appearance.EvenRow.BackColor = System.Drawing.Color.LemonChiffon
        Me.ogv.Appearance.EvenRow.Options.UseBackColor = True
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTLineNo, Me.CFTMachine, Me.CFTButton, Me.FDDate, Me.FTTime})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowGroup = False
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.EnableAppearanceEvenRow = True
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'FTLineNo
        '
        Me.FTLineNo.Caption = "Line No"
        Me.FTLineNo.FieldName = "FTLineNo"
        Me.FTLineNo.Name = "FTLineNo"
        Me.FTLineNo.OptionsColumn.AllowEdit = False
        Me.FTLineNo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTLineNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTLineNo.OptionsColumn.AllowMove = False
        Me.FTLineNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTLineNo.OptionsColumn.ReadOnly = True
        Me.FTLineNo.Visible = True
        Me.FTLineNo.VisibleIndex = 0
        Me.FTLineNo.Width = 122
        '
        'CFTMachine
        '
        Me.CFTMachine.Caption = "Machine"
        Me.CFTMachine.FieldName = "FTMachine"
        Me.CFTMachine.Name = "CFTMachine"
        Me.CFTMachine.OptionsColumn.AllowEdit = False
        Me.CFTMachine.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTMachine.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTMachine.OptionsColumn.AllowMove = False
        Me.CFTMachine.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTMachine.OptionsColumn.ReadOnly = True
        Me.CFTMachine.Visible = True
        Me.CFTMachine.VisibleIndex = 1
        Me.CFTMachine.Width = 151
        '
        'CFTButton
        '
        Me.CFTButton.Caption = "Status"
        Me.CFTButton.FieldName = "FTButton"
        Me.CFTButton.Name = "CFTButton"
        Me.CFTButton.OptionsColumn.AllowEdit = False
        Me.CFTButton.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTButton.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTButton.OptionsColumn.AllowMove = False
        Me.CFTButton.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTButton.OptionsColumn.ReadOnly = True
        Me.CFTButton.Visible = True
        Me.CFTButton.VisibleIndex = 2
        Me.CFTButton.Width = 159
        '
        'FDDate
        '
        Me.FDDate.Caption = "Date"
        Me.FDDate.FieldName = "FDDate"
        Me.FDDate.Name = "FDDate"
        Me.FDDate.OptionsColumn.AllowEdit = False
        Me.FDDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDDate.OptionsColumn.AllowMove = False
        Me.FDDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDDate.OptionsColumn.ReadOnly = True
        Me.FDDate.Visible = True
        Me.FDDate.VisibleIndex = 3
        Me.FDDate.Width = 138
        '
        'FTTime
        '
        Me.FTTime.Caption = "Time"
        Me.FTTime.FieldName = "FTTime"
        Me.FTTime.Name = "FTTime"
        Me.FTTime.OptionsColumn.AllowEdit = False
        Me.FTTime.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTTime.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTTime.OptionsColumn.AllowMove = False
        Me.FTTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTTime.OptionsColumn.ReadOnly = True
        Me.FTTime.Visible = True
        Me.FTTime.VisibleIndex = 4
        Me.FTTime.Width = 189
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
        'ocmstart
        '
        Me.ocmstart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmstart.Location = New System.Drawing.Point(865, 40)
        Me.ocmstart.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmstart.Name = "ocmstart"
        Me.ocmstart.Size = New System.Drawing.Size(212, 31)
        Me.ocmstart.TabIndex = 94
        Me.ocmstart.TabStop = False
        Me.ocmstart.Tag = "2|"
        Me.ocmstart.Text = "START"
        '
        'ocmstop
        '
        Me.ocmstop.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmstop.Location = New System.Drawing.Point(865, 75)
        Me.ocmstop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmstop.Name = "ocmstop"
        Me.ocmstop.Size = New System.Drawing.Size(212, 31)
        Me.ocmstop.TabIndex = 95
        Me.ocmstop.TabStop = False
        Me.ocmstop.Tag = "2|"
        Me.ocmstop.Text = "STOP"
        '
        'FNComport_lbl
        '
        Me.FNComport_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNComport_lbl.Appearance.Options.UseForeColor = True
        Me.FNComport_lbl.Appearance.Options.UseTextOptions = True
        Me.FNComport_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNComport_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNComport_lbl.Location = New System.Drawing.Point(18, 13)
        Me.FNComport_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNComport_lbl.Name = "FNComport_lbl"
        Me.FNComport_lbl.Size = New System.Drawing.Size(134, 23)
        Me.FNComport_lbl.TabIndex = 255
        Me.FNComport_lbl.Tag = "2|"
        Me.FNComport_lbl.Text = "COM PORT :"
        '
        'ocmRefreshport
        '
        Me.ocmRefreshport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmRefreshport.Location = New System.Drawing.Point(865, 5)
        Me.ocmRefreshport.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmRefreshport.Name = "ocmRefreshport"
        Me.ocmRefreshport.Size = New System.Drawing.Size(212, 31)
        Me.ocmRefreshport.TabIndex = 256
        Me.ocmRefreshport.TabStop = False
        Me.ocmRefreshport.Tag = "2|"
        Me.ocmRefreshport.Text = "REFRESH PORT"
        '
        'Timer1
        '
        Me.Timer1.Interval = 2000
        '
        'ogcdataport
        '
        Me.ogcdataport.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcdataport.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdataport.Location = New System.Drawing.Point(162, 8)
        Me.ogcdataport.MainView = Me.ogvdataport
        Me.ogcdataport.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdataport.Name = "ogcdataport"
        Me.ogcdataport.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2, Me.RepositoryItemCheckEdit3, Me.RepositoryItemCheckEdit4, Me.RepositoryItemCheckEdit5, Me.RepositoryItemCheckEdit6, Me.RepositoryItemCheckEdit7, Me.ReposSelect})
        Me.ogcdataport.Size = New System.Drawing.Size(664, 137)
        Me.ogcdataport.TabIndex = 257
        Me.ogcdataport.TabStop = False
        Me.ogcdataport.Tag = "2|"
        Me.ogcdataport.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdataport})
        '
        'ogvdataport
        '
        Me.ogvdataport.Appearance.EvenRow.BackColor = System.Drawing.Color.LemonChiffon
        Me.ogvdataport.Appearance.EvenRow.Options.UseBackColor = True
        Me.ogvdataport.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTSelect, Me.CFTComport})
        Me.ogvdataport.GridControl = Me.ogcdataport
        Me.ogvdataport.Name = "ogvdataport"
        Me.ogvdataport.OptionsCustomization.AllowGroup = False
        Me.ogvdataport.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdataport.OptionsView.ColumnAutoWidth = False
        Me.ogvdataport.OptionsView.EnableAppearanceEvenRow = True
        Me.ogvdataport.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdataport.OptionsView.ShowGroupPanel = False
        Me.ogvdataport.Tag = "2|"
        '
        'CFTSelect
        '
        Me.CFTSelect.Caption = "Select"
        Me.CFTSelect.ColumnEdit = Me.ReposSelect
        Me.CFTSelect.FieldName = "FTSelect"
        Me.CFTSelect.Name = "CFTSelect"
        Me.CFTSelect.OptionsColumn.AllowMove = False
        Me.CFTSelect.Visible = True
        Me.CFTSelect.VisibleIndex = 0
        '
        'ReposSelect
        '
        Me.ReposSelect.AutoHeight = False
        Me.ReposSelect.Caption = "Check"
        Me.ReposSelect.Name = "ReposSelect"
        Me.ReposSelect.ValueChecked = "1"
        Me.ReposSelect.ValueUnchecked = "0"
        '
        'CFTComport
        '
        Me.CFTComport.Caption = "Comport"
        Me.CFTComport.FieldName = "FTComport"
        Me.CFTComport.Name = "CFTComport"
        Me.CFTComport.OptionsColumn.AllowEdit = False
        Me.CFTComport.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTComport.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTComport.OptionsColumn.AllowMove = False
        Me.CFTComport.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTComport.OptionsColumn.ReadOnly = True
        Me.CFTComport.Visible = True
        Me.CFTComport.VisibleIndex = 1
        Me.CFTComport.Width = 245
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Caption = "Check"
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        Me.RepositoryItemCheckEdit2.ValueChecked = "1"
        Me.RepositoryItemCheckEdit2.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit3
        '
        Me.RepositoryItemCheckEdit3.AutoHeight = False
        Me.RepositoryItemCheckEdit3.Caption = "Check"
        Me.RepositoryItemCheckEdit3.Name = "RepositoryItemCheckEdit3"
        Me.RepositoryItemCheckEdit3.ValueChecked = "1"
        Me.RepositoryItemCheckEdit3.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit4
        '
        Me.RepositoryItemCheckEdit4.AutoHeight = False
        Me.RepositoryItemCheckEdit4.Caption = "Check"
        Me.RepositoryItemCheckEdit4.Name = "RepositoryItemCheckEdit4"
        Me.RepositoryItemCheckEdit4.ValueChecked = "1"
        Me.RepositoryItemCheckEdit4.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit5
        '
        Me.RepositoryItemCheckEdit5.AutoHeight = False
        Me.RepositoryItemCheckEdit5.Caption = "Check"
        Me.RepositoryItemCheckEdit5.Name = "RepositoryItemCheckEdit5"
        Me.RepositoryItemCheckEdit5.ValueChecked = "1"
        Me.RepositoryItemCheckEdit5.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit6
        '
        Me.RepositoryItemCheckEdit6.AutoHeight = False
        Me.RepositoryItemCheckEdit6.Caption = "Check"
        Me.RepositoryItemCheckEdit6.Name = "RepositoryItemCheckEdit6"
        Me.RepositoryItemCheckEdit6.ValueChecked = "1"
        Me.RepositoryItemCheckEdit6.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit7
        '
        Me.RepositoryItemCheckEdit7.AutoHeight = False
        Me.RepositoryItemCheckEdit7.Caption = "Check"
        Me.RepositoryItemCheckEdit7.Name = "RepositoryItemCheckEdit7"
        Me.RepositoryItemCheckEdit7.ValueChecked = "1"
        Me.RepositoryItemCheckEdit7.ValueUnchecked = "0"
        '
        'AutomationReceiver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1104, 513)
        Me.Controls.Add(Me.ogcdataport)
        Me.Controls.Add(Me.ocmRefreshport)
        Me.Controls.Add(Me.FNComport_lbl)
        Me.Controls.Add(Me.ocmstop)
        Me.Controls.Add(Me.ocmstart)
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "AutomationReceiver"
        Me.Text = "Form1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepColFTLeavePay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepColFTHoliday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTStaCalSSO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTLeavePay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcdataport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdataport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryFTApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepColFTLeavePay As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepColFTHoliday As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepFTStaCalSSO As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepFTApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepFTLeavePay As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ocmstart As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmstop As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNComport_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmRefreshport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents FTLineNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTMachine As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTButton As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogcdataport As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdataport As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CFTComport As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit6 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit7 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit

End Class
