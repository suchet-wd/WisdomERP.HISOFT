<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wScanCartonPopUp
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
        Me.SBtnOK = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbpackpercarton = New DevExpress.XtraEditors.GroupControl()
        Me.ogcppercarton = New DevExpress.XtraGrid.GridControl()
        Me.ogvppercarton = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.G6FTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G6FTSubOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G6FTNikePOLineItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G6FTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogdscanqty = New DevExpress.XtraEditors.GroupControl()
        Me.ogcScan = New DevExpress.XtraGrid.GridControl()
        Me.ogvScan = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cSFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cSFTSubOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cSFTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cSFTNikePOLineItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cSTotal = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogbpackpercarton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbpackpercarton.SuspendLayout()
        CType(Me.ogcppercarton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvppercarton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogdscanqty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogdscanqty.SuspendLayout()
        CType(Me.ogcScan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvScan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SBtnOK
        '
        Me.SBtnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SBtnOK.Location = New System.Drawing.Point(953, 388)
        Me.SBtnOK.Margin = New System.Windows.Forms.Padding(4)
        Me.SBtnOK.Name = "SBtnOK"
        Me.SBtnOK.Size = New System.Drawing.Size(203, 33)
        Me.SBtnOK.TabIndex = 3
        Me.SBtnOK.Text = "OK"
        '
        'ogbpackpercarton
        '
        Me.ogbpackpercarton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbpackpercarton.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ogbpackpercarton.Appearance.Options.UseBackColor = True
        Me.ogbpackpercarton.Controls.Add(Me.ogcppercarton)
        Me.ogbpackpercarton.Location = New System.Drawing.Point(4, 5)
        Me.ogbpackpercarton.Margin = New System.Windows.Forms.Padding(4)
        Me.ogbpackpercarton.Name = "ogbpackpercarton"
        Me.ogbpackpercarton.Size = New System.Drawing.Size(1164, 181)
        Me.ogbpackpercarton.TabIndex = 308
        Me.ogbpackpercarton.Text = "จำนวนแพ็ค/กล่อง"
        '
        'ogcppercarton
        '
        Me.ogcppercarton.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcppercarton.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.ogcppercarton.Location = New System.Drawing.Point(5, 37)
        Me.ogcppercarton.MainView = Me.ogvppercarton
        Me.ogcppercarton.Margin = New System.Windows.Forms.Padding(4)
        Me.ogcppercarton.Name = "ogcppercarton"
        Me.ogcppercarton.Size = New System.Drawing.Size(1156, 138)
        Me.ogcppercarton.TabIndex = 305
        Me.ogcppercarton.TabStop = False
        Me.ogcppercarton.Tag = "3|"
        Me.ogcppercarton.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvppercarton})
        '
        'ogvppercarton
        '
        Me.ogvppercarton.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.G6FTOrderNo, Me.G6FTSubOrderNo, Me.G6FTNikePOLineItem, Me.G6FTColorway})
        Me.ogvppercarton.GridControl = Me.ogcppercarton
        Me.ogvppercarton.Name = "ogvppercarton"
        Me.ogvppercarton.OptionsCustomization.AllowGroup = False
        Me.ogvppercarton.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvppercarton.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        Me.ogvppercarton.OptionsView.ColumnAutoWidth = False
        Me.ogvppercarton.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvppercarton.OptionsView.ShowFooter = True
        Me.ogvppercarton.OptionsView.ShowGroupPanel = False
        Me.ogvppercarton.Tag = "2|"
        '
        'G6FTOrderNo
        '
        Me.G6FTOrderNo.Caption = "FTOrderNo"
        Me.G6FTOrderNo.FieldName = "FTOrderNo"
        Me.G6FTOrderNo.Name = "G6FTOrderNo"
        '
        'G6FTSubOrderNo
        '
        Me.G6FTSubOrderNo.Caption = "FTSubOrderNo"
        Me.G6FTSubOrderNo.FieldName = "FTSubOrderNo"
        Me.G6FTSubOrderNo.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.G6FTSubOrderNo.Name = "G6FTSubOrderNo"
        Me.G6FTSubOrderNo.OptionsColumn.AllowEdit = False
        Me.G6FTSubOrderNo.OptionsColumn.AllowMove = False
        Me.G6FTSubOrderNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.G6FTSubOrderNo.OptionsColumn.ReadOnly = True
        Me.G6FTSubOrderNo.Visible = True
        Me.G6FTSubOrderNo.VisibleIndex = 0
        Me.G6FTSubOrderNo.Width = 120
        '
        'G6FTNikePOLineItem
        '
        Me.G6FTNikePOLineItem.Caption = "FTNikePOLineItem"
        Me.G6FTNikePOLineItem.FieldName = "FTNikePOLineItem"
        Me.G6FTNikePOLineItem.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.G6FTNikePOLineItem.Name = "G6FTNikePOLineItem"
        Me.G6FTNikePOLineItem.OptionsColumn.AllowEdit = False
        Me.G6FTNikePOLineItem.OptionsColumn.AllowMove = False
        Me.G6FTNikePOLineItem.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.G6FTNikePOLineItem.OptionsFilter.AllowAutoFilter = False
        Me.G6FTNikePOLineItem.OptionsFilter.AllowFilter = False
        Me.G6FTNikePOLineItem.Visible = True
        Me.G6FTNikePOLineItem.VisibleIndex = 2
        '
        'G6FTColorway
        '
        Me.G6FTColorway.Caption = "FTColorway"
        Me.G6FTColorway.FieldName = "FTColorway"
        Me.G6FTColorway.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.G6FTColorway.Name = "G6FTColorway"
        Me.G6FTColorway.OptionsColumn.AllowEdit = False
        Me.G6FTColorway.OptionsColumn.AllowMove = False
        Me.G6FTColorway.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.G6FTColorway.OptionsFilter.AllowAutoFilter = False
        Me.G6FTColorway.OptionsFilter.AllowFilter = False
        Me.G6FTColorway.Visible = True
        Me.G6FTColorway.VisibleIndex = 1
        Me.G6FTColorway.Width = 82
        '
        'ogdscanqty
        '
        Me.ogdscanqty.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogdscanqty.Controls.Add(Me.ogcScan)
        Me.ogdscanqty.Location = New System.Drawing.Point(4, 187)
        Me.ogdscanqty.Margin = New System.Windows.Forms.Padding(4)
        Me.ogdscanqty.Name = "ogdscanqty"
        Me.ogdscanqty.Size = New System.Drawing.Size(1164, 193)
        Me.ogdscanqty.TabIndex = 309
        Me.ogdscanqty.Text = "SCAN Qty"
        '
        'ogcScan
        '
        Me.ogcScan.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcScan.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.ogcScan.Location = New System.Drawing.Point(5, 37)
        Me.ogcScan.MainView = Me.ogvScan
        Me.ogcScan.Margin = New System.Windows.Forms.Padding(4)
        Me.ogcScan.Name = "ogcScan"
        Me.ogcScan.Size = New System.Drawing.Size(1156, 150)
        Me.ogcScan.TabIndex = 305
        Me.ogcScan.TabStop = False
        Me.ogcScan.Tag = "3|"
        Me.ogcScan.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvScan})
        '
        'ogvScan
        '
        Me.ogvScan.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cSFTOrderNo, Me.cSFTSubOrderNo, Me.cSFTColorway, Me.cSFTNikePOLineItem, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.cSTotal})
        Me.ogvScan.GridControl = Me.ogcScan
        Me.ogvScan.Name = "ogvScan"
        Me.ogvScan.OptionsCustomization.AllowGroup = False
        Me.ogvScan.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvScan.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        Me.ogvScan.OptionsView.ColumnAutoWidth = False
        Me.ogvScan.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvScan.OptionsView.ShowFooter = True
        Me.ogvScan.OptionsView.ShowGroupPanel = False
        Me.ogvScan.Tag = "2|"
        '
        'cSFTOrderNo
        '
        Me.cSFTOrderNo.Caption = "FTOrderNo"
        Me.cSFTOrderNo.FieldName = "FTOrderNo"
        Me.cSFTOrderNo.Name = "cSFTOrderNo"
        '
        'cSFTSubOrderNo
        '
        Me.cSFTSubOrderNo.Caption = "FTSubOrderNo"
        Me.cSFTSubOrderNo.FieldName = "FTSubOrderNo"
        Me.cSFTSubOrderNo.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.cSFTSubOrderNo.Name = "cSFTSubOrderNo"
        Me.cSFTSubOrderNo.OptionsColumn.AllowEdit = False
        Me.cSFTSubOrderNo.OptionsColumn.AllowMove = False
        Me.cSFTSubOrderNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.cSFTSubOrderNo.OptionsColumn.ReadOnly = True
        Me.cSFTSubOrderNo.Visible = True
        Me.cSFTSubOrderNo.VisibleIndex = 0
        Me.cSFTSubOrderNo.Width = 120
        '
        'cSFTColorway
        '
        Me.cSFTColorway.Caption = "FTColorway"
        Me.cSFTColorway.FieldName = "FTColorway"
        Me.cSFTColorway.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.cSFTColorway.Name = "cSFTColorway"
        Me.cSFTColorway.OptionsColumn.AllowEdit = False
        Me.cSFTColorway.Visible = True
        Me.cSFTColorway.VisibleIndex = 1
        Me.cSFTColorway.Width = 85
        '
        'cSFTNikePOLineItem
        '
        Me.cSFTNikePOLineItem.Caption = "FTNikePOLineItem"
        Me.cSFTNikePOLineItem.FieldName = "FTNikePOLineItem"
        Me.cSFTNikePOLineItem.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.cSFTNikePOLineItem.Name = "cSFTNikePOLineItem"
        Me.cSFTNikePOLineItem.OptionsColumn.AllowEdit = False
        Me.cSFTNikePOLineItem.OptionsColumn.AllowMove = False
        Me.cSFTNikePOLineItem.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.cSFTNikePOLineItem.OptionsFilter.AllowAutoFilter = False
        Me.cSFTNikePOLineItem.OptionsFilter.AllowFilter = False
        Me.cSFTNikePOLineItem.Visible = True
        Me.cSFTNikePOLineItem.VisibleIndex = 2
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "GridColumn2"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 3
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "GridColumn3"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 4
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "GridColumn4"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 5
        '
        'cSTotal
        '
        Me.cSTotal.Caption = "Total"
        Me.cSTotal.Name = "cSTotal"
        Me.cSTotal.Visible = True
        Me.cSTotal.VisibleIndex = 6
        '
        'wScanCartonPopUp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1172, 427)
        Me.Controls.Add(Me.ogdscanqty)
        Me.Controls.Add(Me.ogbpackpercarton)
        Me.Controls.Add(Me.SBtnOK)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wScanCartonPopUp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wScanCartonPopUp"
        CType(Me.ogbpackpercarton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbpackpercarton.ResumeLayout(False)
        CType(Me.ogcppercarton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvppercarton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogdscanqty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogdscanqty.ResumeLayout(False)
        CType(Me.ogcScan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvScan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SBtnOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbpackpercarton As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcppercarton As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvppercarton As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents G6FTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G6FTSubOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G6FTNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G6FTColorway As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogdscanqty As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcScan As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvScan As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cSFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cSFTSubOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cSFTNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cSTotal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cSFTColorway As DevExpress.XtraGrid.Columns.GridColumn
End Class
