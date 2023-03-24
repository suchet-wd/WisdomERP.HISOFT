<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LCDSewingDaily
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
        Me.components = New System.ComponentModel.Container()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNTarget = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sFNPer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.zFNTotalFinishQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTotalDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BandedGridView1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        Me.GridBand1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.ottime = New System.Windows.Forms.Timer(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BandedGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogc
        '
        Me.ogc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogc.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.ogc.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.ogc.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.ogc.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.ogc.EmbeddedNavigator.Buttons.First.Visible = False
        Me.ogc.EmbeddedNavigator.Buttons.Last.Visible = False
        Me.ogc.EmbeddedNavigator.Buttons.Next.Visible = False
        Me.ogc.EmbeddedNavigator.Buttons.Prev.Visible = False
        Me.ogc.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogc.Location = New System.Drawing.Point(0, 0)
        Me.ogc.LookAndFeel.SkinMaskColor = System.Drawing.Color.Black
        Me.ogc.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Black
        Me.ogc.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.ogc.LookAndFeel.UseDefaultLookAndFeel = False
        Me.ogc.MainView = Me.GridView1
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogc.Name = "ogc"
        Me.ogc.Size = New System.Drawing.Size(1447, 776)
        Me.ogc.TabIndex = 0
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1, Me.BandedGridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.CustomizationFormHint.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.GridView1.Appearance.CustomizationFormHint.Options.UseFont = True
        Me.GridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.Black
        Me.GridView1.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.Black
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.Black
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Tahoma", 18.0!)
        Me.GridView1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.FocusedRow.Options.UseTextOptions = True
        Me.GridView1.Appearance.FocusedRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridView1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Gold
        Me.GridView1.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.Gold
        Me.GridView1.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Gold
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 20.0!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Gold
        Me.GridView1.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.GridView1.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.GridView1.Appearance.Row.BackColor = System.Drawing.Color.Black
        Me.GridView1.Appearance.Row.BackColor2 = System.Drawing.Color.Black
        Me.GridView1.Appearance.Row.BorderColor = System.Drawing.Color.Black
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 18.0!)
        Me.GridView1.Appearance.Row.Options.UseBackColor = True
        Me.GridView1.Appearance.Row.Options.UseBorderColor = True
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridView1.Appearance.RowSeparator.BackColor = System.Drawing.Color.Black
        Me.GridView1.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.Black
        Me.GridView1.Appearance.RowSeparator.BorderColor = System.Drawing.Color.Black
        Me.GridView1.Appearance.RowSeparator.Options.UseBackColor = True
        Me.GridView1.Appearance.RowSeparator.Options.UseBorderColor = True
        Me.GridView1.ColumnPanelRowHeight = 32
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn6, Me.GridColumn1, Me.GridColumn4, Me.GridColumn5, Me.GridColumn7, Me.xFNTarget, Me.aFNQuantity, Me.sFNPer, Me.zFNTotalFinishQuantity, Me.GridColumn3, Me.FNTotalDay})
        Me.GridView1.DetailHeight = 284
        Me.GridView1.GridControl = Me.ogc
        Me.GridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Indicator
        Me.GridView1.RowHeight = 32
        Me.GridView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridColumn2.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.Caption = "Order No."
        Me.GridColumn2.FieldName = "FTOrderNo"
        Me.GridColumn2.MinWidth = 17
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.ReadOnly = True
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 181
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridColumn6.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn6.Caption = "Style-CW"
        Me.GridColumn6.FieldName = "FTStyleCode"
        Me.GridColumn6.MinWidth = 17
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.ReadOnly = True
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 1
        Me.GridColumn6.Width = 253
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Qty"
        Me.GridColumn1.DisplayFormat.FormatString = "{0:n0}"
        Me.GridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn1.FieldName = "FNGrandQuantity"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.ReadOnly = True
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 3
        Me.GridColumn1.Width = 88
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn4.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn4.Caption = "WIP."
        Me.GridColumn4.DisplayFormat.FormatString = "{0:n0}"
        Me.GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn4.FieldName = "FNWIPQuantity"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.ReadOnly = True
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 4
        Me.GridColumn4.Width = 78
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn5.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn5.Caption = "Gac"
        Me.GridColumn5.FieldName = "FDShipDate"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.ReadOnly = True
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        Me.GridColumn5.Width = 144
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn7.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn7.Caption = "%"
        Me.GridColumn7.DisplayFormat.FormatString = "{0:n2}"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn7.FieldName = "FNWIPPer"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.ReadOnly = True
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 5
        Me.GridColumn7.Width = 76
        '
        'xFNTarget
        '
        Me.xFNTarget.AppearanceCell.Options.UseTextOptions = True
        Me.xFNTarget.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.xFNTarget.AppearanceHeader.Options.UseTextOptions = True
        Me.xFNTarget.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFNTarget.Caption = "T./Day"
        Me.xFNTarget.DisplayFormat.FormatString = "{0:n0}"
        Me.xFNTarget.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.xFNTarget.FieldName = "FNTarget"
        Me.xFNTarget.MinWidth = 17
        Me.xFNTarget.Name = "xFNTarget"
        Me.xFNTarget.OptionsColumn.AllowEdit = False
        Me.xFNTarget.OptionsColumn.ReadOnly = True
        Me.xFNTarget.Visible = True
        Me.xFNTarget.VisibleIndex = 6
        Me.xFNTarget.Width = 123
        '
        'aFNQuantity
        '
        Me.aFNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.aFNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.aFNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.aFNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.aFNQuantity.Caption = "Finish"
        Me.aFNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.aFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.aFNQuantity.FieldName = "FNFinishQuantity"
        Me.aFNQuantity.MinWidth = 17
        Me.aFNQuantity.Name = "aFNQuantity"
        Me.aFNQuantity.OptionsColumn.AllowEdit = False
        Me.aFNQuantity.OptionsColumn.ReadOnly = True
        Me.aFNQuantity.Visible = True
        Me.aFNQuantity.VisibleIndex = 7
        Me.aFNQuantity.Width = 211
        '
        'sFNPer
        '
        Me.sFNPer.AppearanceCell.Options.UseTextOptions = True
        Me.sFNPer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.sFNPer.AppearanceHeader.Options.UseTextOptions = True
        Me.sFNPer.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.sFNPer.Caption = "%"
        Me.sFNPer.DisplayFormat.FormatString = "{0:n2}"
        Me.sFNPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.sFNPer.FieldName = "FNDailyPer"
        Me.sFNPer.MinWidth = 17
        Me.sFNPer.Name = "sFNPer"
        Me.sFNPer.OptionsColumn.AllowEdit = False
        Me.sFNPer.OptionsColumn.ReadOnly = True
        Me.sFNPer.Visible = True
        Me.sFNPer.VisibleIndex = 8
        Me.sFNPer.Width = 90
        '
        'zFNTotalFinishQuantity
        '
        Me.zFNTotalFinishQuantity.Caption = "T. Finish"
        Me.zFNTotalFinishQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.zFNTotalFinishQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.zFNTotalFinishQuantity.FieldName = "FNTotalFinishQuantity"
        Me.zFNTotalFinishQuantity.Name = "zFNTotalFinishQuantity"
        Me.zFNTotalFinishQuantity.OptionsColumn.AllowEdit = False
        Me.zFNTotalFinishQuantity.OptionsColumn.ReadOnly = True
        Me.zFNTotalFinishQuantity.Visible = True
        Me.zFNTotalFinishQuantity.VisibleIndex = 9
        Me.zFNTotalFinishQuantity.Width = 153
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = " "
        Me.GridColumn3.FieldName = "FTBlank"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.ReadOnly = True
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 10
        Me.GridColumn3.Width = 20
        '
        'FNTotalDay
        '
        Me.FNTotalDay.Caption = "FNTotalDay"
        Me.FNTotalDay.FieldName = "FNTotalDay"
        Me.FNTotalDay.Name = "FNTotalDay"
        Me.FNTotalDay.OptionsColumn.AllowEdit = False
        Me.FNTotalDay.OptionsColumn.ReadOnly = True
        '
        'BandedGridView1
        '
        Me.BandedGridView1.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.GridBand1})
        Me.BandedGridView1.DetailHeight = 284
        Me.BandedGridView1.GridControl = Me.ogc
        Me.BandedGridView1.Name = "BandedGridView1"
        Me.BandedGridView1.RowHeight = 32
        '
        'GridBand1
        '
        Me.GridBand1.Caption = "GridBand1"
        Me.GridBand1.Name = "GridBand1"
        Me.GridBand1.VisibleIndex = 0
        Me.GridBand1.Width = 60
        '
        'ottime
        '
        Me.ottime.Interval = 120000
        '
        'Timer1
        '
        Me.Timer1.Interval = 10000
        '
        'LCDSewingDaily
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1447, 776)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogc)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "LCDSewingDaily"
        Me.Text = "Sewing Daily"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BandedGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ottime As System.Windows.Forms.Timer
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents BandedGridView1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents GridBand1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNTarget As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sFNPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents zFNTotalFinishQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTotalDay As DevExpress.XtraGrid.Columns.GridColumn
End Class
