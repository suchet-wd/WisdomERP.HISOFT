<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LCDCuttingDaily
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
        Me.opnl1 = New DevExpress.XtraEditors.PanelControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNTarget = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sFNPer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateToday = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNBalQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BandedGridView1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        Me.GridBand1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.lblBalQty = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.lblFinishQty = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotalQty = New DevExpress.XtraEditors.LabelControl()
        Me.lbltotalpo = New DevExpress.XtraEditors.LabelControl()
        Me.ottime = New System.Windows.Forms.Timer(Me.components)
        Me.olbhour = New DevExpress.XtraEditors.LabelControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.opnl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.opnl1.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BandedGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'opnl1
        '
        Me.opnl1.Controls.Add(Me.PanelControl2)
        Me.opnl1.Controls.Add(Me.PanelControl1)
        Me.opnl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.opnl1.Location = New System.Drawing.Point(0, 0)
        Me.opnl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.opnl1.Name = "opnl1"
        Me.opnl1.Size = New System.Drawing.Size(1447, 776)
        Me.opnl1.TabIndex = 0
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.ogc)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(2, 106)
        Me.PanelControl2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(1443, 668)
        Me.PanelControl2.TabIndex = 1
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
        Me.ogc.Location = New System.Drawing.Point(2, 2)
        Me.ogc.LookAndFeel.SkinMaskColor = System.Drawing.Color.Black
        Me.ogc.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Black
        Me.ogc.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.ogc.LookAndFeel.UseDefaultLookAndFeel = False
        Me.ogc.MainView = Me.GridView1
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogc.Name = "ogc"
        Me.ogc.Size = New System.Drawing.Size(1439, 664)
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
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn6, Me.GridColumn3, Me.xFNTarget, Me.aFNQuantity, Me.sFNPer, Me.FTStateToday, Me.FNBalQty})
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
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Plan Date"
        Me.GridColumn1.FieldName = "FDPlanDate"
        Me.GridColumn1.MinWidth = 17
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.ReadOnly = True
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 155
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridColumn2.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.Caption = "Order No."
        Me.GridColumn2.FieldName = "FTJobNo"
        Me.GridColumn2.MinWidth = 17
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.ReadOnly = True
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 231
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridColumn6.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn6.Caption = "Style No."
        Me.GridColumn6.FieldName = "FTStyleCode"
        Me.GridColumn6.MinWidth = 17
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.ReadOnly = True
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 2
        Me.GridColumn6.Width = 218
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridColumn3.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.Caption = "Colorway"
        Me.GridColumn3.FieldName = "FTColorway"
        Me.GridColumn3.MinWidth = 17
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.ReadOnly = True
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 3
        Me.GridColumn3.Width = 170
        '
        'xFNTarget
        '
        Me.xFNTarget.AppearanceCell.Options.UseTextOptions = True
        Me.xFNTarget.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.xFNTarget.AppearanceHeader.Options.UseTextOptions = True
        Me.xFNTarget.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFNTarget.Caption = "Target "
        Me.xFNTarget.DisplayFormat.FormatString = "{0:n0}"
        Me.xFNTarget.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.xFNTarget.FieldName = "FNTarget"
        Me.xFNTarget.MinWidth = 17
        Me.xFNTarget.Name = "xFNTarget"
        Me.xFNTarget.OptionsColumn.AllowEdit = False
        Me.xFNTarget.OptionsColumn.ReadOnly = True
        Me.xFNTarget.Visible = True
        Me.xFNTarget.VisibleIndex = 4
        Me.xFNTarget.Width = 179
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
        Me.aFNQuantity.FieldName = "FNQuantity"
        Me.aFNQuantity.MinWidth = 17
        Me.aFNQuantity.Name = "aFNQuantity"
        Me.aFNQuantity.OptionsColumn.AllowEdit = False
        Me.aFNQuantity.OptionsColumn.ReadOnly = True
        Me.aFNQuantity.Visible = True
        Me.aFNQuantity.VisibleIndex = 5
        Me.aFNQuantity.Width = 196
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
        Me.sFNPer.FieldName = "FNPer"
        Me.sFNPer.MinWidth = 17
        Me.sFNPer.Name = "sFNPer"
        Me.sFNPer.OptionsColumn.AllowEdit = False
        Me.sFNPer.OptionsColumn.ReadOnly = True
        Me.sFNPer.Visible = True
        Me.sFNPer.VisibleIndex = 6
        Me.sFNPer.Width = 272
        '
        'FTStateToday
        '
        Me.FTStateToday.AppearanceCell.Options.UseTextOptions = True
        Me.FTStateToday.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStateToday.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStateToday.Caption = "FTStateToday"
        Me.FTStateToday.FieldName = "FTStateToday"
        Me.FTStateToday.Name = "FTStateToday"
        Me.FTStateToday.OptionsColumn.AllowEdit = False
        Me.FTStateToday.OptionsColumn.ReadOnly = True
        '
        'FNBalQty
        '
        Me.FNBalQty.AppearanceCell.Options.UseTextOptions = True
        Me.FNBalQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNBalQty.AppearanceHeader.Options.UseTextOptions = True
        Me.FNBalQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNBalQty.Caption = "FNBalQty"
        Me.FNBalQty.FieldName = "FNBalQty"
        Me.FNBalQty.Name = "FNBalQty"
        Me.FNBalQty.OptionsColumn.AllowEdit = False
        Me.FNBalQty.OptionsColumn.ReadOnly = True
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
        'PanelControl1
        '
        Me.PanelControl1.Appearance.BackColor = System.Drawing.Color.Black
        Me.PanelControl1.Appearance.Options.UseBackColor = True
        Me.PanelControl1.Controls.Add(Me.lblBalQty)
        Me.PanelControl1.Controls.Add(Me.LabelControl4)
        Me.PanelControl1.Controls.Add(Me.lblFinishQty)
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Controls.Add(Me.lblTotalQty)
        Me.PanelControl1.Controls.Add(Me.lbltotalpo)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(2, 2)
        Me.PanelControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.PanelControl1.LookAndFeel.TouchUIMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.PanelControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1443, 104)
        Me.PanelControl1.TabIndex = 0
        '
        'lblBalQty
        '
        Me.lblBalQty.Appearance.Font = New System.Drawing.Font("Tahoma", 48.0!, System.Drawing.FontStyle.Bold)
        Me.lblBalQty.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblBalQty.Appearance.Options.UseFont = True
        Me.lblBalQty.Appearance.Options.UseForeColor = True
        Me.lblBalQty.Appearance.Options.UseTextOptions = True
        Me.lblBalQty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblBalQty.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblBalQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblBalQty.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblBalQty.Location = New System.Drawing.Point(1102, 3)
        Me.lblBalQty.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblBalQty.Name = "lblBalQty"
        Me.lblBalQty.Size = New System.Drawing.Size(338, 98)
        Me.lblBalQty.TabIndex = 6
        Me.lblBalQty.Text = "000"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 30.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl4.Appearance.ForeColor = System.Drawing.Color.Red
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Appearance.Options.UseForeColor = True
        Me.LabelControl4.Appearance.Options.UseTextOptions = True
        Me.LabelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Left
        Me.LabelControl4.Location = New System.Drawing.Point(930, 3)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(172, 98)
        Me.LabelControl4.TabIndex = 5
        Me.LabelControl4.Text = "Balance"
        '
        'lblFinishQty
        '
        Me.lblFinishQty.Appearance.Font = New System.Drawing.Font("Tahoma", 48.0!, System.Drawing.FontStyle.Bold)
        Me.lblFinishQty.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblFinishQty.Appearance.Options.UseFont = True
        Me.lblFinishQty.Appearance.Options.UseForeColor = True
        Me.lblFinishQty.Appearance.Options.UseTextOptions = True
        Me.lblFinishQty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblFinishQty.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblFinishQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblFinishQty.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblFinishQty.Location = New System.Drawing.Point(671, 3)
        Me.lblFinishQty.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblFinishQty.Name = "lblFinishQty"
        Me.lblFinishQty.Size = New System.Drawing.Size(259, 98)
        Me.lblFinishQty.TabIndex = 4
        Me.lblFinishQty.Text = "000"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 30.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.Lime
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Left
        Me.LabelControl2.Location = New System.Drawing.Point(479, 3)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(192, 98)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Finish"
        '
        'lblTotalQty
        '
        Me.lblTotalQty.Appearance.Font = New System.Drawing.Font("Tahoma", 48.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalQty.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblTotalQty.Appearance.Options.UseFont = True
        Me.lblTotalQty.Appearance.Options.UseForeColor = True
        Me.lblTotalQty.Appearance.Options.UseTextOptions = True
        Me.lblTotalQty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblTotalQty.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblTotalQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblTotalQty.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblTotalQty.Location = New System.Drawing.Point(176, 3)
        Me.lblTotalQty.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblTotalQty.Name = "lblTotalQty"
        Me.lblTotalQty.Size = New System.Drawing.Size(303, 98)
        Me.lblTotalQty.TabIndex = 2
        Me.lblTotalQty.Text = "000"
        '
        'lbltotalpo
        '
        Me.lbltotalpo.Appearance.Font = New System.Drawing.Font("Tahoma", 27.0!, System.Drawing.FontStyle.Bold)
        Me.lbltotalpo.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lbltotalpo.Appearance.Options.UseFont = True
        Me.lbltotalpo.Appearance.Options.UseForeColor = True
        Me.lbltotalpo.Appearance.Options.UseTextOptions = True
        Me.lbltotalpo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lbltotalpo.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lbltotalpo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lbltotalpo.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbltotalpo.Location = New System.Drawing.Point(3, 3)
        Me.lbltotalpo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lbltotalpo.Name = "lbltotalpo"
        Me.lbltotalpo.Size = New System.Drawing.Size(173, 98)
        Me.lbltotalpo.TabIndex = 1
        Me.lbltotalpo.Text = "Target" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Today"
        '
        'ottime
        '
        Me.ottime.Interval = 60000
        '
        'olbhour
        '
        Me.olbhour.Appearance.Font = New System.Drawing.Font("Tahoma", 35.0!, System.Drawing.FontStyle.Bold)
        Me.olbhour.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.olbhour.Appearance.Options.UseFont = True
        Me.olbhour.Appearance.Options.UseForeColor = True
        Me.olbhour.Appearance.Options.UseTextOptions = True
        Me.olbhour.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.olbhour.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.olbhour.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbhour.Location = New System.Drawing.Point(7, 6)
        Me.olbhour.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.olbhour.Name = "olbhour"
        Me.olbhour.Size = New System.Drawing.Size(27, 63)
        Me.olbhour.TabIndex = 6
        Me.olbhour.Text = "08:00:00"
        Me.olbhour.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 10000
        '
        'LCDCuttingDaily
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1447, 776)
        Me.ControlBox = False
        Me.Controls.Add(Me.opnl1)
        Me.Controls.Add(Me.olbhour)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "LCDCuttingDaily"
        Me.Text = "Cutting Daily "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.opnl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.opnl1.ResumeLayout(False)
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BandedGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents opnl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ottime As System.Windows.Forms.Timer
    Friend WithEvents olbhour As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lbltotalpo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblBalQty As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblFinishQty As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTotalQty As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents BandedGridView1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents GridBand1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNTarget As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sFNPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateToday As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNBalQty As DevExpress.XtraGrid.Columns.GridColumn
End Class
