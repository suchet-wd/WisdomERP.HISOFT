<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wCostSheetExportJSon
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
        Me.ogbOptRptOrderNo = New DevExpress.XtraEditors.GroupControl()
        Me.ogbActionBtn = New DevExpress.XtraEditors.GroupControl()
        Me.ocmCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmOK = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateExportUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDStateExportDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateExportTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSendType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSendStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSendStatusDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogbOptRptOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbOptRptOrderNo.SuspendLayout()
        CType(Me.ogbActionBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbActionBtn.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbOptRptOrderNo
        '
        Me.ogbOptRptOrderNo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbOptRptOrderNo.Controls.Add(Me.ogcdetail)
        Me.ogbOptRptOrderNo.Location = New System.Drawing.Point(0, 12)
        Me.ogbOptRptOrderNo.Name = "ogbOptRptOrderNo"
        Me.ogbOptRptOrderNo.Size = New System.Drawing.Size(910, 240)
        Me.ogbOptRptOrderNo.TabIndex = 6
        Me.ogbOptRptOrderNo.Text = "Option"
        '
        'ogbActionBtn
        '
        Me.ogbActionBtn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbActionBtn.Controls.Add(Me.ocmCancel)
        Me.ogbActionBtn.Controls.Add(Me.ocmOK)
        Me.ogbActionBtn.Location = New System.Drawing.Point(2, 258)
        Me.ogbActionBtn.Name = "ogbActionBtn"
        Me.ogbActionBtn.ShowCaption = False
        Me.ogbActionBtn.Size = New System.Drawing.Size(908, 42)
        Me.ogbActionBtn.TabIndex = 7
        Me.ogbActionBtn.Text = "GroupControl1"
        '
        'ocmCancel
        '
        Me.ocmCancel.Location = New System.Drawing.Point(533, 5)
        Me.ocmCancel.Name = "ocmCancel"
        Me.ocmCancel.Size = New System.Drawing.Size(120, 25)
        Me.ocmCancel.TabIndex = 1
        Me.ocmCancel.Text = "CANCEL"
        '
        'ocmOK
        '
        Me.ocmOK.Location = New System.Drawing.Point(250, 7)
        Me.ocmOK.Name = "ocmOK"
        Me.ocmOK.Size = New System.Drawing.Size(120, 25)
        Me.ocmOK.TabIndex = 0
        Me.ocmOK.Text = "O.K."
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.Location = New System.Drawing.Point(2, 23)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect})
        Me.ogcdetail.Size = New System.Drawing.Size(906, 215)
        Me.ogcdetail.TabIndex = 4
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FNSeq, Me.FTStateExportUser, Me.FDStateExportDate, Me.FTStateExportTime, Me.FTSendType, Me.FTSendStatus, Me.FTSendStatusDescription})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'RepositoryFTSelect
        '
        Me.RepositoryFTSelect.AutoHeight = False
        Me.RepositoryFTSelect.Caption = "Check"
        Me.RepositoryFTSelect.Name = "RepositoryFTSelect"
        Me.RepositoryFTSelect.ValueChecked = "1"
        Me.RepositoryFTSelect.ValueUnchecked = "0"
        '
        'FTSelect
        '
        Me.FTSelect.Caption = "Select"
        Me.FTSelect.ColumnEdit = Me.RepositoryFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 52
        '
        'FNSeq
        '
        Me.FNSeq.Caption = "FNSeq"
        Me.FNSeq.FieldName = "FNSeq"
        Me.FNSeq.Name = "FNSeq"
        Me.FNSeq.OptionsColumn.AllowEdit = False
        Me.FNSeq.OptionsColumn.ReadOnly = True
        '
        'FTStateExportUser
        '
        Me.FTStateExportUser.Caption = "Last Export By"
        Me.FTStateExportUser.FieldName = "FTStateExportUser"
        Me.FTStateExportUser.Name = "FTStateExportUser"
        Me.FTStateExportUser.OptionsColumn.AllowEdit = False
        Me.FTStateExportUser.OptionsColumn.ReadOnly = True
        Me.FTStateExportUser.Visible = True
        Me.FTStateExportUser.VisibleIndex = 1
        Me.FTStateExportUser.Width = 112
        '
        'FDStateExportDate
        '
        Me.FDStateExportDate.Caption = "Last Export Date"
        Me.FDStateExportDate.FieldName = "FDStateExportDate"
        Me.FDStateExportDate.Name = "FDStateExportDate"
        Me.FDStateExportDate.OptionsColumn.AllowEdit = False
        Me.FDStateExportDate.OptionsColumn.ReadOnly = True
        Me.FDStateExportDate.Visible = True
        Me.FDStateExportDate.VisibleIndex = 2
        Me.FDStateExportDate.Width = 99
        '
        'FTStateExportTime
        '
        Me.FTStateExportTime.Caption = "Last Export Time"
        Me.FTStateExportTime.FieldName = "FTStateExportTime"
        Me.FTStateExportTime.Name = "FTStateExportTime"
        Me.FTStateExportTime.OptionsColumn.AllowEdit = False
        Me.FTStateExportTime.OptionsColumn.ReadOnly = True
        Me.FTStateExportTime.Visible = True
        Me.FTStateExportTime.VisibleIndex = 3
        Me.FTStateExportTime.Width = 112
        '
        'FTSendType
        '
        Me.FTSendType.Caption = "Send Type"
        Me.FTSendType.FieldName = "FTSendType"
        Me.FTSendType.Name = "FTSendType"
        Me.FTSendType.OptionsColumn.AllowEdit = False
        Me.FTSendType.OptionsColumn.ReadOnly = True
        Me.FTSendType.Visible = True
        Me.FTSendType.VisibleIndex = 4
        Me.FTSendType.Width = 82
        '
        'FTSendStatus
        '
        Me.FTSendStatus.Caption = "Send Status"
        Me.FTSendStatus.FieldName = "FTSendStatus"
        Me.FTSendStatus.Name = "FTSendStatus"
        Me.FTSendStatus.OptionsColumn.AllowEdit = False
        Me.FTSendStatus.OptionsColumn.ReadOnly = True
        Me.FTSendStatus.Visible = True
        Me.FTSendStatus.VisibleIndex = 5
        Me.FTSendStatus.Width = 78
        '
        'FTSendStatusDescription
        '
        Me.FTSendStatusDescription.Caption = "Send Status"
        Me.FTSendStatusDescription.FieldName = "FTSendStatusDescription"
        Me.FTSendStatusDescription.Name = "FTSendStatusDescription"
        Me.FTSendStatusDescription.OptionsColumn.AllowEdit = False
        Me.FTSendStatusDescription.OptionsColumn.ReadOnly = True
        Me.FTSendStatusDescription.Visible = True
        Me.FTSendStatusDescription.VisibleIndex = 6
        Me.FTSendStatusDescription.Width = 318
        '
        'wCostSheetExportJSon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(910, 302)
        Me.Controls.Add(Me.ogbActionBtn)
        Me.Controls.Add(Me.ogbOptRptOrderNo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wCostSheetExportJSon"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Send Json File"
        CType(Me.ogbOptRptOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbOptRptOrderNo.ResumeLayout(False)
        CType(Me.ogbActionBtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbActionBtn.ResumeLayout(False)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbOptRptOrderNo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbActionBtn As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateExportUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDStateExportDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateExportTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSendType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSendStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSendStatusDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class
