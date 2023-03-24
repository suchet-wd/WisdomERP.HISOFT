<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wQADailyDetailPreFinal
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
        Me.oclose = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.FTSeriesTopName = New DevExpress.XtraEditors.LabelControl()
        Me.FTTitleTopChart = New DevExpress.XtraEditors.LabelControl()
        Me.FTSeriesDetailName = New DevExpress.XtraEditors.LabelControl()
        Me.FTTitleDetailChart = New DevExpress.XtraEditors.LabelControl()
        Me.ogrptopDefect = New DevExpress.XtraEditors.GroupControl()
        Me.ogrpTopDefectChart = New DevExpress.XtraEditors.GroupControl()
        Me.ogcTopDefect = New DevExpress.XtraGrid.GridControl()
        Me.ogvTopDefect = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTQADetailName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cDefectPer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogrpChart = New DevExpress.XtraEditors.GroupControl()
        Me.ogcDetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFNHSysStyleId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysUnitSectId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDQADate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQAInQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQAAqlQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQAActualQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNTotalDefect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNAndon = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNDefectPer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.FTUnitCode_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.ogrptopDefect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrptopDefect.SuspendLayout()
        CType(Me.ogrpTopDefectChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcTopDefect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvTopDefect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogrpChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'oclose
        '
        Me.oclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.oclose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.oclose.Image = Global.HI.QA.My.Resources.Resources.delete_16x16
        Me.oclose.Location = New System.Drawing.Point(1261, 1)
        Me.oclose.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oclose.Name = "oclose"
        Me.oclose.Size = New System.Drawing.Size(28, 28)
        Me.oclose.TabIndex = 1000000
        Me.oclose.TabStop = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelControl1.Controls.Add(Me.FTSeriesTopName)
        Me.PanelControl1.Controls.Add(Me.FTTitleTopChart)
        Me.PanelControl1.Controls.Add(Me.FTSeriesDetailName)
        Me.PanelControl1.Controls.Add(Me.FTTitleDetailChart)
        Me.PanelControl1.Controls.Add(Me.ogrptopDefect)
        Me.PanelControl1.Controls.Add(Me.ogrpChart)
        Me.PanelControl1.Controls.Add(Me.ogcDetail)
        Me.PanelControl1.Location = New System.Drawing.Point(0, 37)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1289, 757)
        Me.PanelControl1.TabIndex = 1
        '
        'FTSeriesTopName
        '
        Me.FTSeriesTopName.Location = New System.Drawing.Point(236, 118)
        Me.FTSeriesTopName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSeriesTopName.Name = "FTSeriesTopName"
        Me.FTSeriesTopName.Size = New System.Drawing.Size(78, 16)
        Me.FTSeriesTopName.TabIndex = 3
        Me.FTSeriesTopName.Text = "LabelControl1"
        Me.FTSeriesTopName.Visible = False
        '
        'FTTitleTopChart
        '
        Me.FTTitleTopChart.Location = New System.Drawing.Point(346, 118)
        Me.FTTitleTopChart.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTTitleTopChart.Name = "FTTitleTopChart"
        Me.FTTitleTopChart.Size = New System.Drawing.Size(78, 16)
        Me.FTTitleTopChart.TabIndex = 3
        Me.FTTitleTopChart.Text = "LabelControl1"
        Me.FTTitleTopChart.Visible = False
        '
        'FTSeriesDetailName
        '
        Me.FTSeriesDetailName.Location = New System.Drawing.Point(471, 118)
        Me.FTSeriesDetailName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSeriesDetailName.Name = "FTSeriesDetailName"
        Me.FTSeriesDetailName.Size = New System.Drawing.Size(78, 16)
        Me.FTSeriesDetailName.TabIndex = 3
        Me.FTSeriesDetailName.Text = "LabelControl1"
        Me.FTSeriesDetailName.Visible = False
        '
        'FTTitleDetailChart
        '
        Me.FTTitleDetailChart.Location = New System.Drawing.Point(658, 118)
        Me.FTTitleDetailChart.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTTitleDetailChart.Name = "FTTitleDetailChart"
        Me.FTTitleDetailChart.Size = New System.Drawing.Size(78, 16)
        Me.FTTitleDetailChart.TabIndex = 3
        Me.FTTitleDetailChart.Text = "LabelControl1"
        Me.FTTitleDetailChart.Visible = False
        '
        'ogrptopDefect
        '
        Me.ogrptopDefect.Controls.Add(Me.ogrpTopDefectChart)
        Me.ogrptopDefect.Controls.Add(Me.ogcTopDefect)
        Me.ogrptopDefect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrptopDefect.Location = New System.Drawing.Point(2, 442)
        Me.ogrptopDefect.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrptopDefect.Name = "ogrptopDefect"
        Me.ogrptopDefect.ShowCaption = False
        Me.ogrptopDefect.Size = New System.Drawing.Size(1285, 313)
        Me.ogrptopDefect.TabIndex = 2
        Me.ogrptopDefect.Text = "GroupControl2"
        '
        'ogrpTopDefectChart
        '
        Me.ogrpTopDefectChart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpTopDefectChart.Location = New System.Drawing.Point(439, 2)
        Me.ogrpTopDefectChart.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpTopDefectChart.Name = "ogrpTopDefectChart"
        Me.ogrpTopDefectChart.ShowCaption = False
        Me.ogrpTopDefectChart.Size = New System.Drawing.Size(844, 309)
        Me.ogrpTopDefectChart.TabIndex = 1
        Me.ogrpTopDefectChart.Text = "GroupControl1"
        '
        'ogcTopDefect
        '
        Me.ogcTopDefect.Dock = System.Windows.Forms.DockStyle.Left
        Me.ogcTopDefect.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcTopDefect.Location = New System.Drawing.Point(2, 2)
        Me.ogcTopDefect.MainView = Me.ogvTopDefect
        Me.ogcTopDefect.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcTopDefect.Name = "ogcTopDefect"
        Me.ogcTopDefect.Size = New System.Drawing.Size(437, 309)
        Me.ogcTopDefect.TabIndex = 0
        Me.ogcTopDefect.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvTopDefect})
        '
        'ogvTopDefect
        '
        Me.ogvTopDefect.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTQADetailName, Me.cQty, Me.cDefectPer})
        Me.ogvTopDefect.GridControl = Me.ogcTopDefect
        Me.ogvTopDefect.Name = "ogvTopDefect"
        '
        'cFTQADetailName
        '
        Me.cFTQADetailName.Caption = "FTQADetailName"
        Me.cFTQADetailName.FieldName = "FTQADetailName"
        Me.cFTQADetailName.Name = "cFTQADetailName"
        Me.cFTQADetailName.OptionsColumn.AllowEdit = False
        Me.cFTQADetailName.Visible = True
        Me.cFTQADetailName.VisibleIndex = 0
        Me.cFTQADetailName.Width = 540
        '
        'cQty
        '
        Me.cQty.Caption = "Qty"
        Me.cQty.FieldName = "Qty"
        Me.cQty.Name = "cQty"
        Me.cQty.OptionsColumn.AllowEdit = False
        Me.cQty.Visible = True
        Me.cQty.VisibleIndex = 1
        Me.cQty.Width = 372
        '
        'cDefectPer
        '
        Me.cDefectPer.Caption = "DefectPer"
        Me.cDefectPer.FieldName = "DefectPer"
        Me.cDefectPer.Name = "cDefectPer"
        Me.cDefectPer.OptionsColumn.AllowEdit = False
        Me.cDefectPer.Visible = True
        Me.cDefectPer.VisibleIndex = 2
        Me.cDefectPer.Width = 392
        '
        'ogrpChart
        '
        Me.ogrpChart.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogrpChart.Location = New System.Drawing.Point(2, 203)
        Me.ogrpChart.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpChart.Name = "ogrpChart"
        Me.ogrpChart.ShowCaption = False
        Me.ogrpChart.Size = New System.Drawing.Size(1285, 239)
        Me.ogrpChart.TabIndex = 1
        Me.ogrpChart.Text = "GroupControl1"
        '
        'ogcDetail
        '
        Me.ogcDetail.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogcDetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDetail.Location = New System.Drawing.Point(2, 2)
        Me.ogcDetail.MainView = Me.ogvDetail
        Me.ogcDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDetail.Name = "ogcDetail"
        Me.ogcDetail.Size = New System.Drawing.Size(1285, 201)
        Me.ogcDetail.TabIndex = 0
        Me.ogcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDetail})
        '
        'ogvDetail
        '
        Me.ogvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFNHSysStyleId, Me.cFNHSysUnitSectId, Me.cFTOrderNo, Me.cFDQADate, Me.cFNQAInQty, Me.cFNQAAqlQty, Me.cFNQAActualQty, Me.cFNTotalDefect, Me.cFTPORef, Me.cFNAndon, Me.cFNDefectPer, Me.cFTStyleCode})
        Me.ogvDetail.GridControl = Me.ogcDetail
        Me.ogvDetail.Name = "ogvDetail"
        Me.ogvDetail.OptionsView.ShowFooter = True
        Me.ogvDetail.OptionsView.ShowGroupPanel = False
        '
        'cFNHSysStyleId
        '
        Me.cFNHSysStyleId.Caption = "FNHSysStyleId"
        Me.cFNHSysStyleId.FieldName = "FNHSysStyleId"
        Me.cFNHSysStyleId.Name = "cFNHSysStyleId"
        Me.cFNHSysStyleId.OptionsColumn.AllowEdit = False
        '
        'cFNHSysUnitSectId
        '
        Me.cFNHSysUnitSectId.Caption = "FNHSysUnitSectId"
        Me.cFNHSysUnitSectId.FieldName = "FNHSysUnitSectId"
        Me.cFNHSysUnitSectId.Name = "cFNHSysUnitSectId"
        Me.cFNHSysUnitSectId.OptionsColumn.AllowEdit = False
        '
        'cFTOrderNo
        '
        Me.cFTOrderNo.Caption = "FTOrderNo"
        Me.cFTOrderNo.FieldName = "FTOrderNo"
        Me.cFTOrderNo.Name = "cFTOrderNo"
        Me.cFTOrderNo.OptionsColumn.AllowEdit = False
        Me.cFTOrderNo.Visible = True
        Me.cFTOrderNo.VisibleIndex = 2
        Me.cFTOrderNo.Width = 132
        '
        'cFDQADate
        '
        Me.cFDQADate.Caption = "FDQADate"
        Me.cFDQADate.FieldName = "FDQADate"
        Me.cFDQADate.Name = "cFDQADate"
        Me.cFDQADate.OptionsColumn.AllowEdit = False
        Me.cFDQADate.Visible = True
        Me.cFDQADate.VisibleIndex = 0
        Me.cFDQADate.Width = 108
        '
        'cFNQAInQty
        '
        Me.cFNQAInQty.Caption = "FNQAInQty"
        Me.cFNQAInQty.FieldName = "FNQAInQty"
        Me.cFNQAInQty.Name = "cFNQAInQty"
        Me.cFNQAInQty.OptionsColumn.AllowEdit = False
        Me.cFNQAInQty.Visible = True
        Me.cFNQAInQty.VisibleIndex = 4
        Me.cFNQAInQty.Width = 139
        '
        'cFNQAAqlQty
        '
        Me.cFNQAAqlQty.Caption = "FNQAAqlQty"
        Me.cFNQAAqlQty.FieldName = "FNQAAqlQty"
        Me.cFNQAAqlQty.Name = "cFNQAAqlQty"
        Me.cFNQAAqlQty.OptionsColumn.AllowEdit = False
        Me.cFNQAAqlQty.Visible = True
        Me.cFNQAAqlQty.VisibleIndex = 5
        Me.cFNQAAqlQty.Width = 127
        '
        'cFNQAActualQty
        '
        Me.cFNQAActualQty.Caption = "FNQAActualQty"
        Me.cFNQAActualQty.FieldName = "FNQAActualQty"
        Me.cFNQAActualQty.Name = "cFNQAActualQty"
        Me.cFNQAActualQty.OptionsColumn.AllowEdit = False
        Me.cFNQAActualQty.Visible = True
        Me.cFNQAActualQty.VisibleIndex = 6
        Me.cFNQAActualQty.Width = 130
        '
        'cFNTotalDefect
        '
        Me.cFNTotalDefect.Caption = "FNTotalDefect"
        Me.cFNTotalDefect.FieldName = "FNTotalDefect"
        Me.cFNTotalDefect.Name = "cFNTotalDefect"
        Me.cFNTotalDefect.OptionsColumn.AllowEdit = False
        Me.cFNTotalDefect.Visible = True
        Me.cFNTotalDefect.VisibleIndex = 7
        Me.cFNTotalDefect.Width = 123
        '
        'cFTPORef
        '
        Me.cFTPORef.Caption = "FTPORef"
        Me.cFTPORef.FieldName = "FTPORef"
        Me.cFTPORef.Name = "cFTPORef"
        Me.cFTPORef.OptionsColumn.AllowEdit = False
        Me.cFTPORef.Visible = True
        Me.cFTPORef.VisibleIndex = 3
        Me.cFTPORef.Width = 154
        '
        'cFNAndon
        '
        Me.cFNAndon.Caption = "FNAndon"
        Me.cFNAndon.FieldName = "FNAndon"
        Me.cFNAndon.Name = "cFNAndon"
        Me.cFNAndon.OptionsColumn.AllowEdit = False
        Me.cFNAndon.Width = 130
        '
        'cFNDefectPer
        '
        Me.cFNDefectPer.Caption = "FNDefectPer"
        Me.cFNDefectPer.DisplayFormat.FormatString = "{n:n2}"
        Me.cFNDefectPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNDefectPer.FieldName = "FNDefectPer"
        Me.cFNDefectPer.Name = "cFNDefectPer"
        Me.cFNDefectPer.OptionsColumn.AllowEdit = False
        Me.cFNDefectPer.Visible = True
        Me.cFNDefectPer.VisibleIndex = 8
        Me.cFNDefectPer.Width = 121
        '
        'cFTStyleCode
        '
        Me.cFTStyleCode.Caption = "FTStyleCode"
        Me.cFTStyleCode.FieldName = "FTStyleCode"
        Me.cFTStyleCode.Name = "cFTStyleCode"
        Me.cFTStyleCode.OptionsColumn.AllowEdit = False
        Me.cFTStyleCode.Visible = True
        Me.cFTStyleCode.VisibleIndex = 1
        Me.cFTStyleCode.Width = 140
        '
        'ocmpreview
        '
        Me.ocmpreview.Appearance.Options.UseTextOptions = True
        Me.ocmpreview.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.ocmpreview.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.ocmpreview.Image = Global.HI.QA.My.Resources.Resources.preview_16x16
        Me.ocmpreview.Location = New System.Drawing.Point(5, 1)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(184, 31)
        Me.ocmpreview.TabIndex = 1000001
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Text = "Preview"
        '
        'FTUnitCode_lbl
        '
        Me.FTUnitCode_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.FTUnitCode_lbl.Appearance.Options.UseFont = True
        Me.FTUnitCode_lbl.Appearance.Options.UseTextOptions = True
        Me.FTUnitCode_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitCode_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTUnitCode_lbl.Location = New System.Drawing.Point(472, 4)
        Me.FTUnitCode_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTUnitCode_lbl.Name = "FTUnitCode_lbl"
        Me.FTUnitCode_lbl.Size = New System.Drawing.Size(323, 31)
        Me.FTUnitCode_lbl.TabIndex = 1000002
        Me.FTUnitCode_lbl.Text = "SWA01"
        '
        'wQADailyDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1290, 795)
        Me.Controls.Add(Me.FTUnitCode_lbl)
        Me.Controls.Add(Me.ocmpreview)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.oclose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wQADailyDetailPreFinal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.ogrptopDefect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrptopDefect.ResumeLayout(False)
        CType(Me.ogrpTopDefectChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcTopDefect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvTopDefect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogrpChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents oclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ogrptopDefect As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogrpChart As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFNHSysStyleId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysUnitSectId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDQADate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQAInQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQAAqlQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQAActualQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNTotalDefect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNAndon As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNDefectPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogcTopDefect As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvTopDefect As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTQADetailName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cDefectPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogrpTopDefectChart As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTUnitCode_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSeriesTopName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTTitleTopChart As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSeriesDetailName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTTitleDetailChart As DevExpress.XtraEditors.LabelControl
End Class
