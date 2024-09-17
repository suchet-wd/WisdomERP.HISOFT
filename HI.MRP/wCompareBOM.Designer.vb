<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wCompareBOM
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
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.gcCompareBOM = New DevExpress.XtraEditors.GroupControl()
        Me.gcCompare = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTStyleDevCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStyleDevOriginalCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCompareType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCompareState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCompareCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCompareDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ocmclose = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.gcCompareBOM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcCompareBOM.SuspendLayout()
        CType(Me.gcCompare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.ocmclose)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl2.Location = New System.Drawing.Point(2, 532)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.ShowCaption = False
        Me.GroupControl2.Size = New System.Drawing.Size(1185, 45)
        Me.GroupControl2.TabIndex = 288
        Me.GroupControl2.Text = "GroupControl2"
        '
        'gcCompareBOM
        '
        Me.gcCompareBOM.Controls.Add(Me.gcCompare)
        Me.gcCompareBOM.Controls.Add(Me.GroupControl2)
        Me.gcCompareBOM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCompareBOM.Location = New System.Drawing.Point(0, 0)
        Me.gcCompareBOM.Name = "gcCompareBOM"
        Me.gcCompareBOM.Size = New System.Drawing.Size(1189, 579)
        Me.gcCompareBOM.TabIndex = 290
        Me.gcCompareBOM.Text = "Compare BOM"
        '
        'gcCompare
        '
        Me.gcCompare.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCompare.Location = New System.Drawing.Point(2, 23)
        Me.gcCompare.MainView = Me.ogvdetail
        Me.gcCompare.Name = "gcCompare"
        Me.gcCompare.Size = New System.Drawing.Size(1185, 509)
        Me.gcCompare.TabIndex = 289
        Me.gcCompare.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTStyleDevCode, Me.FTStyleDevOriginalCode, Me.FTCompareType, Me.FTCompareState, Me.FTCompareCode, Me.FTCompareDesc, Me.FTRemark})
        Me.ogvdetail.DetailHeight = 284
        Me.ogvdetail.GridControl = Me.gcCompare
        Me.ogvdetail.Name = "ogvdetail"
        '
        'FTStyleDevCode
        '
        Me.FTStyleDevCode.AppearanceCell.Options.UseTextOptions = True
        Me.FTStyleDevCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStyleDevCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStyleDevCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStyleDevCode.Caption = "FTStyleDevCode"
        Me.FTStyleDevCode.FieldName = "FTStyleDevCode"
        Me.FTStyleDevCode.Name = "FTStyleDevCode"
        Me.FTStyleDevCode.OptionsColumn.AllowEdit = False
        Me.FTStyleDevCode.Visible = True
        Me.FTStyleDevCode.VisibleIndex = 0
        Me.FTStyleDevCode.Width = 70
        '
        'FTStyleDevOriginalCode
        '
        Me.FTStyleDevOriginalCode.AppearanceCell.Options.UseTextOptions = True
        Me.FTStyleDevOriginalCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStyleDevOriginalCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStyleDevOriginalCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStyleDevOriginalCode.Caption = "FTStyleDevOriginalCode"
        Me.FTStyleDevOriginalCode.FieldName = "FTStyleDevOriginalCode"
        Me.FTStyleDevOriginalCode.Name = "FTStyleDevOriginalCode"
        Me.FTStyleDevOriginalCode.OptionsColumn.AllowEdit = False
        Me.FTStyleDevOriginalCode.Visible = True
        Me.FTStyleDevOriginalCode.VisibleIndex = 1
        Me.FTStyleDevOriginalCode.Width = 70
        '
        'FTCompareType
        '
        Me.FTCompareType.AppearanceCell.Options.UseTextOptions = True
        Me.FTCompareType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTCompareType.AppearanceHeader.Options.UseTextOptions = True
        Me.FTCompareType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTCompareType.Caption = "FTCompareType"
        Me.FTCompareType.FieldName = "FTCompareType"
        Me.FTCompareType.Name = "FTCompareType"
        Me.FTCompareType.OptionsColumn.AllowEdit = False
        Me.FTCompareType.Visible = True
        Me.FTCompareType.VisibleIndex = 2
        Me.FTCompareType.Width = 100
        '
        'FTCompareState
        '
        Me.FTCompareState.AppearanceCell.Options.UseTextOptions = True
        Me.FTCompareState.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTCompareState.AppearanceHeader.Options.UseTextOptions = True
        Me.FTCompareState.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTCompareState.Caption = "FTCompareState"
        Me.FTCompareState.FieldName = "FTCompareState"
        Me.FTCompareState.Name = "FTCompareState"
        Me.FTCompareState.OptionsColumn.AllowEdit = False
        Me.FTCompareState.Visible = True
        Me.FTCompareState.VisibleIndex = 3
        Me.FTCompareState.Width = 100
        '
        'FTCompareCode
        '
        Me.FTCompareCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTCompareCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTCompareCode.Caption = "FTCompareCode"
        Me.FTCompareCode.FieldName = "FTCompareCode"
        Me.FTCompareCode.Name = "FTCompareCode"
        Me.FTCompareCode.OptionsColumn.AllowEdit = False
        Me.FTCompareCode.Visible = True
        Me.FTCompareCode.VisibleIndex = 4
        Me.FTCompareCode.Width = 150
        '
        'FTCompareDesc
        '
        Me.FTCompareDesc.AppearanceHeader.Options.UseTextOptions = True
        Me.FTCompareDesc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTCompareDesc.Caption = "FTCompareDesc"
        Me.FTCompareDesc.FieldName = "FTCompareDesc"
        Me.FTCompareDesc.Name = "FTCompareDesc"
        Me.FTCompareDesc.OptionsColumn.AllowEdit = False
        Me.FTCompareDesc.Visible = True
        Me.FTCompareDesc.VisibleIndex = 5
        Me.FTCompareDesc.Width = 397
        '
        'FTRemark
        '
        Me.FTRemark.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRemark.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRemark.Caption = "FTRemark"
        Me.FTRemark.FieldName = "FTRemark"
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.OptionsColumn.AllowEdit = False
        Me.FTRemark.Visible = True
        Me.FTRemark.VisibleIndex = 6
        Me.FTRemark.Width = 275
        '
        'ocmclose
        '
        Me.ocmclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmclose.Location = New System.Drawing.Point(482, 10)
        Me.ocmclose.Name = "ocmclose"
        Me.ocmclose.Size = New System.Drawing.Size(145, 25)
        Me.ocmclose.TabIndex = 108
        Me.ocmclose.TabStop = False
        Me.ocmclose.Tag = "2|"
        Me.ocmclose.Text = "Close"
        '
        'wCompareBOM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1189, 579)
        Me.ControlBox = False
        Me.Controls.Add(Me.gcCompareBOM)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wCompareBOM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Show Compare BOM"
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.gcCompareBOM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcCompareBOM.ResumeLayout(False)
        CType(Me.gcCompare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gcCompareBOM As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gcCompare As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTStyleDevCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStyleDevOriginalCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCompareType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCompareState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCompareCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCompareDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmclose As DevExpress.XtraEditors.SimpleButton
End Class
