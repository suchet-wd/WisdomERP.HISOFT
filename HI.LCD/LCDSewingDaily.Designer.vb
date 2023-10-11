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
        Me.ottime = New System.Windows.Forms.Timer(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.lbljobBalQty = New DevExpress.XtraEditors.LabelControl()
        Me.lbljobBal = New DevExpress.XtraEditors.LabelControl()
        Me.lbljobDoneQty = New DevExpress.XtraEditors.LabelControl()
        Me.lbljobDone = New DevExpress.XtraEditors.LabelControl()
        Me.lbljoballQty = New DevExpress.XtraEditors.LabelControl()
        Me.lbljoball = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.lblsewBalQty = New DevExpress.XtraEditors.LabelControl()
        Me.lblsewBal = New DevExpress.XtraEditors.LabelControl()
        Me.lblsewDoneQty = New DevExpress.XtraEditors.LabelControl()
        Me.lblsewDone = New DevExpress.XtraEditors.LabelControl()
        Me.lblsewAllQty = New DevExpress.XtraEditors.LabelControl()
        Me.lblsewAll = New DevExpress.XtraEditors.LabelControl()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.BandedGridView2 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        Me.GridBand2 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FDShipDate = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gridBand11 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.ConfirmDate = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gridBand10 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FTOrderNo = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gridBand9 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FTStyleCode = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gridBand8 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.Item = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gridBand7 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNGrandQuantity = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gridBand3 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNWIPQuantity = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gridBand6 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNTarget = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gridBand4 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNFinishQuantity = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gridBand5 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNTotalFinishQuantity = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gridBand12 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.PerSuccess = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.BandedGridView1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        Me.GridBand1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BandedGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BandedGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ottime
        '
        Me.ottime.Interval = 120000
        '
        'Timer1
        '
        Me.Timer1.Interval = 10000
        '
        'PanelControl1
        '
        Me.PanelControl1.Appearance.BackColor = System.Drawing.Color.Black
        Me.PanelControl1.Appearance.Options.UseBackColor = True
        Me.PanelControl1.Controls.Add(Me.lbljobBalQty)
        Me.PanelControl1.Controls.Add(Me.lbljobBal)
        Me.PanelControl1.Controls.Add(Me.lbljobDoneQty)
        Me.PanelControl1.Controls.Add(Me.lbljobDone)
        Me.PanelControl1.Controls.Add(Me.lbljoballQty)
        Me.PanelControl1.Controls.Add(Me.lbljoball)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.PanelControl1.LookAndFeel.TouchUIMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.PanelControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1447, 104)
        Me.PanelControl1.TabIndex = 1
        '
        'lbljobBalQty
        '
        Me.lbljobBalQty.Appearance.Font = New System.Drawing.Font("Tahoma", 45.0!, System.Drawing.FontStyle.Bold)
        Me.lbljobBalQty.Appearance.ForeColor = System.Drawing.Color.White
        Me.lbljobBalQty.Appearance.Options.UseFont = True
        Me.lbljobBalQty.Appearance.Options.UseForeColor = True
        Me.lbljobBalQty.Appearance.Options.UseTextOptions = True
        Me.lbljobBalQty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lbljobBalQty.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lbljobBalQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lbljobBalQty.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbljobBalQty.Location = New System.Drawing.Point(1150, 3)
        Me.lbljobBalQty.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lbljobBalQty.Name = "lbljobBalQty"
        Me.lbljobBalQty.Size = New System.Drawing.Size(294, 98)
        Me.lbljobBalQty.TabIndex = 6
        Me.lbljobBalQty.Text = "-"
        '
        'lbljobBal
        '
        Me.lbljobBal.Appearance.Font = New System.Drawing.Font("Tahoma", 25.0!, System.Drawing.FontStyle.Bold)
        Me.lbljobBal.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lbljobBal.Appearance.Options.UseFont = True
        Me.lbljobBal.Appearance.Options.UseForeColor = True
        Me.lbljobBal.Appearance.Options.UseTextOptions = True
        Me.lbljobBal.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lbljobBal.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lbljobBal.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lbljobBal.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbljobBal.Location = New System.Drawing.Point(919, 3)
        Me.lbljobBal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lbljobBal.Name = "lbljobBal"
        Me.lbljobBal.Size = New System.Drawing.Size(231, 98)
        Me.lbljobBal.TabIndex = 5
        Me.lbljobBal.Text = "จำนวนงานคงเหลือ"
        '
        'lbljobDoneQty
        '
        Me.lbljobDoneQty.Appearance.Font = New System.Drawing.Font("Tahoma", 45.0!, System.Drawing.FontStyle.Bold)
        Me.lbljobDoneQty.Appearance.ForeColor = System.Drawing.Color.White
        Me.lbljobDoneQty.Appearance.Options.UseFont = True
        Me.lbljobDoneQty.Appearance.Options.UseForeColor = True
        Me.lbljobDoneQty.Appearance.Options.UseTextOptions = True
        Me.lbljobDoneQty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lbljobDoneQty.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lbljobDoneQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lbljobDoneQty.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbljobDoneQty.Location = New System.Drawing.Point(660, 3)
        Me.lbljobDoneQty.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lbljobDoneQty.Name = "lbljobDoneQty"
        Me.lbljobDoneQty.Size = New System.Drawing.Size(259, 98)
        Me.lbljobDoneQty.TabIndex = 4
        Me.lbljobDoneQty.Text = "-"
        '
        'lbljobDone
        '
        Me.lbljobDone.Appearance.Font = New System.Drawing.Font("Tahoma", 25.0!, System.Drawing.FontStyle.Bold)
        Me.lbljobDone.Appearance.ForeColor = System.Drawing.Color.Gold
        Me.lbljobDone.Appearance.Options.UseFont = True
        Me.lbljobDone.Appearance.Options.UseForeColor = True
        Me.lbljobDone.Appearance.Options.UseTextOptions = True
        Me.lbljobDone.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lbljobDone.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lbljobDone.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lbljobDone.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbljobDone.Location = New System.Drawing.Point(445, 3)
        Me.lbljobDone.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lbljobDone.Name = "lbljobDone"
        Me.lbljobDone.Size = New System.Drawing.Size(215, 98)
        Me.lbljobDone.TabIndex = 3
        Me.lbljobDone.Text = "จำนวนงานที่เสร็จ"
        '
        'lbljoballQty
        '
        Me.lbljoballQty.Appearance.Font = New System.Drawing.Font("Tahoma", 45.0!, System.Drawing.FontStyle.Bold)
        Me.lbljoballQty.Appearance.ForeColor = System.Drawing.Color.White
        Me.lbljoballQty.Appearance.Options.UseFont = True
        Me.lbljoballQty.Appearance.Options.UseForeColor = True
        Me.lbljoballQty.Appearance.Options.UseTextOptions = True
        Me.lbljoballQty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lbljoballQty.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lbljoballQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lbljoballQty.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbljoballQty.Location = New System.Drawing.Point(249, 3)
        Me.lbljoballQty.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lbljoballQty.Name = "lbljoballQty"
        Me.lbljoballQty.Size = New System.Drawing.Size(196, 98)
        Me.lbljoballQty.TabIndex = 2
        Me.lbljoballQty.Text = "-"
        '
        'lbljoball
        '
        Me.lbljoball.Appearance.Font = New System.Drawing.Font("Tahoma", 25.0!, System.Drawing.FontStyle.Bold)
        Me.lbljoball.Appearance.ForeColor = System.Drawing.Color.Lime
        Me.lbljoball.Appearance.Options.UseFont = True
        Me.lbljoball.Appearance.Options.UseForeColor = True
        Me.lbljoball.Appearance.Options.UseTextOptions = True
        Me.lbljoball.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lbljoball.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lbljoball.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lbljoball.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbljoball.Location = New System.Drawing.Point(3, 3)
        Me.lbljoball.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lbljoball.Name = "lbljoball"
        Me.lbljoball.Size = New System.Drawing.Size(246, 98)
        Me.lbljoball.TabIndex = 1
        Me.lbljoball.Text = "จำนวนงานทั้งหมด"
        '
        'PanelControl3
        '
        Me.PanelControl3.Appearance.BackColor = System.Drawing.Color.Black
        Me.PanelControl3.Appearance.Options.UseBackColor = True
        Me.PanelControl3.Controls.Add(Me.lblsewBalQty)
        Me.PanelControl3.Controls.Add(Me.lblsewBal)
        Me.PanelControl3.Controls.Add(Me.lblsewDoneQty)
        Me.PanelControl3.Controls.Add(Me.lblsewDone)
        Me.PanelControl3.Controls.Add(Me.lblsewAllQty)
        Me.PanelControl3.Controls.Add(Me.lblsewAll)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl3.Location = New System.Drawing.Point(0, 713)
        Me.PanelControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.PanelControl3.LookAndFeel.TouchUIMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.PanelControl3.LookAndFeel.UseDefaultLookAndFeel = False
        Me.PanelControl3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(1447, 63)
        Me.PanelControl3.TabIndex = 2
        '
        'lblsewBalQty
        '
        Me.lblsewBalQty.Appearance.Font = New System.Drawing.Font("Tahoma", 40.0!, System.Drawing.FontStyle.Bold)
        Me.lblsewBalQty.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblsewBalQty.Appearance.Options.UseFont = True
        Me.lblsewBalQty.Appearance.Options.UseForeColor = True
        Me.lblsewBalQty.Appearance.Options.UseTextOptions = True
        Me.lblsewBalQty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblsewBalQty.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblsewBalQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblsewBalQty.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblsewBalQty.Location = New System.Drawing.Point(1150, 3)
        Me.lblsewBalQty.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblsewBalQty.Name = "lblsewBalQty"
        Me.lblsewBalQty.Size = New System.Drawing.Size(294, 57)
        Me.lblsewBalQty.TabIndex = 12
        Me.lblsewBalQty.Text = "-"
        '
        'lblsewBal
        '
        Me.lblsewBal.Appearance.Font = New System.Drawing.Font("Tahoma", 25.0!, System.Drawing.FontStyle.Bold)
        Me.lblsewBal.Appearance.ForeColor = System.Drawing.Color.Gold
        Me.lblsewBal.Appearance.Options.UseFont = True
        Me.lblsewBal.Appearance.Options.UseForeColor = True
        Me.lblsewBal.Appearance.Options.UseTextOptions = True
        Me.lblsewBal.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblsewBal.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblsewBal.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblsewBal.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblsewBal.Location = New System.Drawing.Point(919, 3)
        Me.lblsewBal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblsewBal.Name = "lblsewBal"
        Me.lblsewBal.Size = New System.Drawing.Size(231, 57)
        Me.lblsewBal.TabIndex = 11
        Me.lblsewBal.Text = "% ความสำเร็จ"
        '
        'lblsewDoneQty
        '
        Me.lblsewDoneQty.Appearance.Font = New System.Drawing.Font("Tahoma", 40.0!, System.Drawing.FontStyle.Bold)
        Me.lblsewDoneQty.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblsewDoneQty.Appearance.Options.UseFont = True
        Me.lblsewDoneQty.Appearance.Options.UseForeColor = True
        Me.lblsewDoneQty.Appearance.Options.UseTextOptions = True
        Me.lblsewDoneQty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblsewDoneQty.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblsewDoneQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblsewDoneQty.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblsewDoneQty.Location = New System.Drawing.Point(660, 3)
        Me.lblsewDoneQty.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblsewDoneQty.Name = "lblsewDoneQty"
        Me.lblsewDoneQty.Size = New System.Drawing.Size(259, 57)
        Me.lblsewDoneQty.TabIndex = 10
        Me.lblsewDoneQty.Text = "-"
        '
        'lblsewDone
        '
        Me.lblsewDone.Appearance.Font = New System.Drawing.Font("Tahoma", 25.0!, System.Drawing.FontStyle.Bold)
        Me.lblsewDone.Appearance.ForeColor = System.Drawing.Color.Lime
        Me.lblsewDone.Appearance.Options.UseFont = True
        Me.lblsewDone.Appearance.Options.UseForeColor = True
        Me.lblsewDone.Appearance.Options.UseTextOptions = True
        Me.lblsewDone.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblsewDone.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblsewDone.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblsewDone.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblsewDone.Location = New System.Drawing.Point(445, 3)
        Me.lblsewDone.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblsewDone.Name = "lblsewDone"
        Me.lblsewDone.Size = New System.Drawing.Size(215, 57)
        Me.lblsewDone.TabIndex = 9
        Me.lblsewDone.Text = "ทำได้จริง"
        '
        'lblsewAllQty
        '
        Me.lblsewAllQty.Appearance.Font = New System.Drawing.Font("Tahoma", 40.0!, System.Drawing.FontStyle.Bold)
        Me.lblsewAllQty.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblsewAllQty.Appearance.Options.UseFont = True
        Me.lblsewAllQty.Appearance.Options.UseForeColor = True
        Me.lblsewAllQty.Appearance.Options.UseTextOptions = True
        Me.lblsewAllQty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblsewAllQty.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblsewAllQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblsewAllQty.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblsewAllQty.Location = New System.Drawing.Point(249, 3)
        Me.lblsewAllQty.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblsewAllQty.Name = "lblsewAllQty"
        Me.lblsewAllQty.Size = New System.Drawing.Size(196, 57)
        Me.lblsewAllQty.TabIndex = 8
        Me.lblsewAllQty.Text = "-"
        '
        'lblsewAll
        '
        Me.lblsewAll.Appearance.Font = New System.Drawing.Font("Tahoma", 25.0!, System.Drawing.FontStyle.Bold)
        Me.lblsewAll.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblsewAll.Appearance.Options.UseFont = True
        Me.lblsewAll.Appearance.Options.UseForeColor = True
        Me.lblsewAll.Appearance.Options.UseTextOptions = True
        Me.lblsewAll.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblsewAll.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblsewAll.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblsewAll.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblsewAll.Location = New System.Drawing.Point(3, 3)
        Me.lblsewAll.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblsewAll.Name = "lblsewAll"
        Me.lblsewAll.Size = New System.Drawing.Size(246, 57)
        Me.lblsewAll.TabIndex = 7
        Me.lblsewAll.Text = "เป้าหมายเย็บต่อวัน"
        '
        'ogc
        '
        Me.ogc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogc.Location = New System.Drawing.Point(0, 104)
        Me.ogc.LookAndFeel.SkinMaskColor = System.Drawing.Color.Black
        Me.ogc.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Black
        Me.ogc.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.ogc.LookAndFeel.UseDefaultLookAndFeel = False
        Me.ogc.MainView = Me.BandedGridView2
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogc.Name = "ogc"
        Me.ogc.Size = New System.Drawing.Size(1447, 609)
        Me.ogc.TabIndex = 3
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.BandedGridView2, Me.BandedGridView1})
        '
        'BandedGridView2
        '
        Me.BandedGridView2.Appearance.CustomizationFormHint.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.BandedGridView2.Appearance.CustomizationFormHint.Options.UseFont = True
        Me.BandedGridView2.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Gold
        Me.BandedGridView2.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.Gold
        Me.BandedGridView2.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Gold
        Me.BandedGridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BandedGridView2.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Gold
        Me.BandedGridView2.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.BandedGridView2.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.BandedGridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.BandedGridView2.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.BandedGridView2.Appearance.Row.BackColor = System.Drawing.Color.Black
        Me.BandedGridView2.Appearance.Row.BackColor2 = System.Drawing.Color.Black
        Me.BandedGridView2.Appearance.Row.BorderColor = System.Drawing.Color.Black
        Me.BandedGridView2.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.BandedGridView2.Appearance.Row.Options.UseBackColor = True
        Me.BandedGridView2.Appearance.Row.Options.UseBorderColor = True
        Me.BandedGridView2.Appearance.Row.Options.UseFont = True
        Me.BandedGridView2.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BandedGridView2.Appearance.RowSeparator.BackColor = System.Drawing.Color.Black
        Me.BandedGridView2.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.Black
        Me.BandedGridView2.Appearance.RowSeparator.BorderColor = System.Drawing.Color.Black
        Me.BandedGridView2.Appearance.RowSeparator.Options.UseBackColor = True
        Me.BandedGridView2.Appearance.RowSeparator.Options.UseBorderColor = True
        Me.BandedGridView2.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.GridBand2, Me.gridBand11, Me.gridBand10, Me.gridBand9, Me.gridBand8, Me.gridBand7, Me.gridBand3, Me.gridBand6, Me.gridBand4, Me.gridBand5, Me.gridBand12})
        Me.BandedGridView2.ColumnPanelRowHeight = 32
        Me.BandedGridView2.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {Me.FDShipDate, Me.ConfirmDate, Me.FTOrderNo, Me.FTStyleCode, Me.Item, Me.FNGrandQuantity, Me.FNWIPQuantity, Me.FNTarget, Me.FNFinishQuantity, Me.FNTotalFinishQuantity, Me.PerSuccess})
        Me.BandedGridView2.DetailHeight = 284
        Me.BandedGridView2.GridControl = Me.ogc
        Me.BandedGridView2.Name = "BandedGridView2"
        Me.BandedGridView2.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent
        Me.BandedGridView2.OptionsView.ShowGroupPanel = False
        Me.BandedGridView2.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Indicator
        Me.BandedGridView2.RowHeight = 32
        '
        'GridBand2
        '
        Me.GridBand2.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.GridBand2.AppearanceHeader.ForeColor = System.Drawing.Color.Gold
        Me.GridBand2.AppearanceHeader.Options.UseFont = True
        Me.GridBand2.AppearanceHeader.Options.UseForeColor = True
        Me.GridBand2.AppearanceHeader.Options.UseTextOptions = True
        Me.GridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridBand2.Caption = "Original GAC"
        Me.GridBand2.Columns.Add(Me.FDShipDate)
        Me.GridBand2.Name = "GridBand2"
        Me.GridBand2.RowCount = 2
        Me.GridBand2.VisibleIndex = 0
        Me.GridBand2.Width = 75
        '
        'FDShipDate
        '
        Me.FDShipDate.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.FDShipDate.AppearanceCell.Options.UseFont = True
        Me.FDShipDate.AppearanceCell.Options.UseTextOptions = True
        Me.FDShipDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDShipDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.FDShipDate.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.FDShipDate.AppearanceHeader.Options.UseFont = True
        Me.FDShipDate.AppearanceHeader.Options.UseTextOptions = True
        Me.FDShipDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDShipDate.Caption = "วันที่ส่งมอบ"
        Me.FDShipDate.FieldName = "ConfirmDate"
        Me.FDShipDate.MinWidth = 17
        Me.FDShipDate.Name = "FDShipDate"
        Me.FDShipDate.OptionsColumn.AllowEdit = False
        Me.FDShipDate.OptionsColumn.AllowMove = False
        Me.FDShipDate.Visible = True
        '
        'gridBand11
        '
        Me.gridBand11.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.gridBand11.AppearanceHeader.ForeColor = System.Drawing.Color.Gold
        Me.gridBand11.AppearanceHeader.Options.UseFont = True
        Me.gridBand11.AppearanceHeader.Options.UseForeColor = True
        Me.gridBand11.AppearanceHeader.Options.UseTextOptions = True
        Me.gridBand11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gridBand11.Caption = "Confirm Book"
        Me.gridBand11.Columns.Add(Me.ConfirmDate)
        Me.gridBand11.Name = "gridBand11"
        Me.gridBand11.VisibleIndex = 1
        Me.gridBand11.Width = 75
        '
        'ConfirmDate
        '
        Me.ConfirmDate.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.ConfirmDate.AppearanceCell.Options.UseFont = True
        Me.ConfirmDate.AppearanceCell.Options.UseTextOptions = True
        Me.ConfirmDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ConfirmDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ConfirmDate.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.ConfirmDate.AppearanceHeader.Options.UseFont = True
        Me.ConfirmDate.AppearanceHeader.Options.UseTextOptions = True
        Me.ConfirmDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ConfirmDate.Caption = "วันส่งออกจริง"
        Me.ConfirmDate.FieldName = "FDShipDate"
        Me.ConfirmDate.MinWidth = 17
        Me.ConfirmDate.Name = "ConfirmDate"
        Me.ConfirmDate.OptionsColumn.AllowEdit = False
        Me.ConfirmDate.OptionsColumn.AllowMove = False
        Me.ConfirmDate.Visible = True
        '
        'gridBand10
        '
        Me.gridBand10.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.gridBand10.AppearanceHeader.ForeColor = System.Drawing.Color.Gold
        Me.gridBand10.AppearanceHeader.Options.UseFont = True
        Me.gridBand10.AppearanceHeader.Options.UseForeColor = True
        Me.gridBand10.AppearanceHeader.Options.UseTextOptions = True
        Me.gridBand10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gridBand10.Caption = "Order No."
        Me.gridBand10.Columns.Add(Me.FTOrderNo)
        Me.gridBand10.Name = "gridBand10"
        Me.gridBand10.VisibleIndex = 2
        Me.gridBand10.Width = 130
        '
        'FTOrderNo
        '
        Me.FTOrderNo.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.FTOrderNo.AppearanceCell.Options.UseFont = True
        Me.FTOrderNo.AppearanceCell.Options.UseTextOptions = True
        Me.FTOrderNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTOrderNo.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.FTOrderNo.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.FTOrderNo.AppearanceHeader.Options.UseFont = True
        Me.FTOrderNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTOrderNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTOrderNo.Caption = "ใบสั่งผลิต"
        Me.FTOrderNo.FieldName = "FTOrderNo"
        Me.FTOrderNo.MinWidth = 17
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.OptionsColumn.AllowEdit = False
        Me.FTOrderNo.OptionsColumn.AllowMove = False
        Me.FTOrderNo.Visible = True
        Me.FTOrderNo.Width = 130
        '
        'gridBand9
        '
        Me.gridBand9.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.gridBand9.AppearanceHeader.ForeColor = System.Drawing.Color.Gold
        Me.gridBand9.AppearanceHeader.Options.UseFont = True
        Me.gridBand9.AppearanceHeader.Options.UseForeColor = True
        Me.gridBand9.AppearanceHeader.Options.UseTextOptions = True
        Me.gridBand9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gridBand9.Caption = "Style - CW"
        Me.gridBand9.Columns.Add(Me.FTStyleCode)
        Me.gridBand9.Name = "gridBand9"
        Me.gridBand9.VisibleIndex = 3
        Me.gridBand9.Width = 130
        '
        'FTStyleCode
        '
        Me.FTStyleCode.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.FTStyleCode.AppearanceCell.Options.UseFont = True
        Me.FTStyleCode.AppearanceCell.Options.UseTextOptions = True
        Me.FTStyleCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStyleCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.FTStyleCode.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.FTStyleCode.AppearanceHeader.Options.UseFont = True
        Me.FTStyleCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStyleCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStyleCode.Caption = "สไตล์ - สี"
        Me.FTStyleCode.FieldName = "FTStyleCode"
        Me.FTStyleCode.MinWidth = 17
        Me.FTStyleCode.Name = "FTStyleCode"
        Me.FTStyleCode.OptionsColumn.AllowEdit = False
        Me.FTStyleCode.OptionsColumn.AllowMove = False
        Me.FTStyleCode.Visible = True
        Me.FTStyleCode.Width = 130
        '
        'gridBand8
        '
        Me.gridBand8.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.gridBand8.AppearanceHeader.ForeColor = System.Drawing.Color.Gold
        Me.gridBand8.AppearanceHeader.Options.UseFont = True
        Me.gridBand8.AppearanceHeader.Options.UseForeColor = True
        Me.gridBand8.AppearanceHeader.Options.UseTextOptions = True
        Me.gridBand8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gridBand8.Caption = "Item"
        Me.gridBand8.Columns.Add(Me.Item)
        Me.gridBand8.Name = "gridBand8"
        Me.gridBand8.VisibleIndex = 4
        Me.gridBand8.Width = 50
        '
        'Item
        '
        Me.Item.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.Item.AppearanceCell.Options.UseFont = True
        Me.Item.AppearanceCell.Options.UseTextOptions = True
        Me.Item.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.Item.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.Item.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.Item.AppearanceHeader.Options.UseFont = True
        Me.Item.AppearanceHeader.Options.UseTextOptions = True
        Me.Item.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.Item.Caption = "ไอเท็ม"
        Me.Item.FieldName = "Item"
        Me.Item.MinWidth = 17
        Me.Item.Name = "Item"
        Me.Item.OptionsColumn.AllowEdit = False
        Me.Item.OptionsColumn.AllowMove = False
        Me.Item.Visible = True
        Me.Item.Width = 50
        '
        'gridBand7
        '
        Me.gridBand7.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.gridBand7.AppearanceHeader.ForeColor = System.Drawing.Color.Gold
        Me.gridBand7.AppearanceHeader.Options.UseFont = True
        Me.gridBand7.AppearanceHeader.Options.UseForeColor = True
        Me.gridBand7.AppearanceHeader.Options.UseTextOptions = True
        Me.gridBand7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gridBand7.Caption = "Quantity Order"
        Me.gridBand7.Columns.Add(Me.FNGrandQuantity)
        Me.gridBand7.Name = "gridBand7"
        Me.gridBand7.VisibleIndex = 5
        Me.gridBand7.Width = 50
        '
        'FNGrandQuantity
        '
        Me.FNGrandQuantity.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.FNGrandQuantity.AppearanceCell.Options.UseFont = True
        Me.FNGrandQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNGrandQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNGrandQuantity.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.FNGrandQuantity.AppearanceHeader.Options.UseFont = True
        Me.FNGrandQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNGrandQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNGrandQuantity.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.FNGrandQuantity.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.FNGrandQuantity.Caption = "จำนวน"
        Me.FNGrandQuantity.DisplayFormat.FormatString = "N0"
        Me.FNGrandQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNGrandQuantity.FieldName = "FNGrandQuantity"
        Me.FNGrandQuantity.MinWidth = 17
        Me.FNGrandQuantity.Name = "FNGrandQuantity"
        Me.FNGrandQuantity.OptionsColumn.AllowEdit = False
        Me.FNGrandQuantity.OptionsColumn.AllowMove = False
        Me.FNGrandQuantity.Visible = True
        Me.FNGrandQuantity.Width = 50
        '
        'gridBand3
        '
        Me.gridBand3.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.gridBand3.AppearanceHeader.ForeColor = System.Drawing.Color.Gold
        Me.gridBand3.AppearanceHeader.Options.UseFont = True
        Me.gridBand3.AppearanceHeader.Options.UseForeColor = True
        Me.gridBand3.AppearanceHeader.Options.UseTextOptions = True
        Me.gridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gridBand3.Caption = "WIP Balance"
        Me.gridBand3.Columns.Add(Me.FNWIPQuantity)
        Me.gridBand3.Name = "gridBand3"
        Me.gridBand3.VisibleIndex = 6
        Me.gridBand3.Width = 50
        '
        'FNWIPQuantity
        '
        Me.FNWIPQuantity.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.FNWIPQuantity.AppearanceCell.Options.UseFont = True
        Me.FNWIPQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNWIPQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNWIPQuantity.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.FNWIPQuantity.AppearanceHeader.Options.UseFont = True
        Me.FNWIPQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNWIPQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNWIPQuantity.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.FNWIPQuantity.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.FNWIPQuantity.Caption = "คงเหลือ"
        Me.FNWIPQuantity.DisplayFormat.FormatString = "N0"
        Me.FNWIPQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNWIPQuantity.FieldName = "FNWIPQuantity"
        Me.FNWIPQuantity.MinWidth = 17
        Me.FNWIPQuantity.Name = "FNWIPQuantity"
        Me.FNWIPQuantity.OptionsColumn.AllowEdit = False
        Me.FNWIPQuantity.OptionsColumn.AllowMove = False
        Me.FNWIPQuantity.Visible = True
        Me.FNWIPQuantity.Width = 50
        '
        'gridBand6
        '
        Me.gridBand6.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.gridBand6.AppearanceHeader.ForeColor = System.Drawing.Color.Gold
        Me.gridBand6.AppearanceHeader.Options.UseFont = True
        Me.gridBand6.AppearanceHeader.Options.UseForeColor = True
        Me.gridBand6.AppearanceHeader.Options.UseTextOptions = True
        Me.gridBand6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gridBand6.Caption = "Target/Day"
        Me.gridBand6.Columns.Add(Me.FNTarget)
        Me.gridBand6.Name = "gridBand6"
        Me.gridBand6.VisibleIndex = 7
        Me.gridBand6.Width = 50
        '
        'FNTarget
        '
        Me.FNTarget.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.FNTarget.AppearanceCell.Options.UseFont = True
        Me.FNTarget.AppearanceCell.Options.UseTextOptions = True
        Me.FNTarget.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTarget.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.FNTarget.AppearanceHeader.Options.UseFont = True
        Me.FNTarget.AppearanceHeader.Options.UseTextOptions = True
        Me.FNTarget.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNTarget.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.FNTarget.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.FNTarget.Caption = "เป้าหมายเย็บ/วัน"
        Me.FNTarget.DisplayFormat.FormatString = "N0"
        Me.FNTarget.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTarget.FieldName = "FNTarget"
        Me.FNTarget.MinWidth = 17
        Me.FNTarget.Name = "FNTarget"
        Me.FNTarget.OptionsColumn.AllowEdit = False
        Me.FNTarget.OptionsColumn.AllowMove = False
        Me.FNTarget.Visible = True
        Me.FNTarget.Width = 50
        '
        'gridBand4
        '
        Me.gridBand4.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.gridBand4.AppearanceHeader.ForeColor = System.Drawing.Color.Gold
        Me.gridBand4.AppearanceHeader.Options.UseFont = True
        Me.gridBand4.AppearanceHeader.Options.UseForeColor = True
        Me.gridBand4.AppearanceHeader.Options.UseTextOptions = True
        Me.gridBand4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gridBand4.Caption = "Finish/Day"
        Me.gridBand4.Columns.Add(Me.FNFinishQuantity)
        Me.gridBand4.Name = "gridBand4"
        Me.gridBand4.VisibleIndex = 8
        Me.gridBand4.Width = 50
        '
        'FNFinishQuantity
        '
        Me.FNFinishQuantity.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.FNFinishQuantity.AppearanceCell.Options.UseFont = True
        Me.FNFinishQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNFinishQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNFinishQuantity.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.FNFinishQuantity.AppearanceHeader.Options.UseFont = True
        Me.FNFinishQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNFinishQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNFinishQuantity.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.FNFinishQuantity.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.FNFinishQuantity.Caption = "ทำได้จริง"
        Me.FNFinishQuantity.DisplayFormat.FormatString = "N0"
        Me.FNFinishQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNFinishQuantity.FieldName = "FNFinishQuantity"
        Me.FNFinishQuantity.MinWidth = 17
        Me.FNFinishQuantity.Name = "FNFinishQuantity"
        Me.FNFinishQuantity.OptionsColumn.AllowEdit = False
        Me.FNFinishQuantity.OptionsColumn.AllowMove = False
        Me.FNFinishQuantity.Visible = True
        Me.FNFinishQuantity.Width = 50
        '
        'gridBand5
        '
        Me.gridBand5.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.gridBand5.AppearanceHeader.ForeColor = System.Drawing.Color.Gold
        Me.gridBand5.AppearanceHeader.Options.UseFont = True
        Me.gridBand5.AppearanceHeader.Options.UseForeColor = True
        Me.gridBand5.AppearanceHeader.Options.UseTextOptions = True
        Me.gridBand5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gridBand5.Caption = "Total Finish"
        Me.gridBand5.Columns.Add(Me.FNTotalFinishQuantity)
        Me.gridBand5.Name = "gridBand5"
        Me.gridBand5.VisibleIndex = 9
        Me.gridBand5.Width = 50
        '
        'FNTotalFinishQuantity
        '
        Me.FNTotalFinishQuantity.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.FNTotalFinishQuantity.AppearanceCell.Options.UseFont = True
        Me.FNTotalFinishQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNTotalFinishQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTotalFinishQuantity.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.FNTotalFinishQuantity.AppearanceHeader.Options.UseFont = True
        Me.FNTotalFinishQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNTotalFinishQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNTotalFinishQuantity.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.FNTotalFinishQuantity.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.FNTotalFinishQuantity.Caption = "สะสม"
        Me.FNTotalFinishQuantity.DisplayFormat.FormatString = "N0"
        Me.FNTotalFinishQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotalFinishQuantity.FieldName = "FNTotalFinishQuantity"
        Me.FNTotalFinishQuantity.MinWidth = 17
        Me.FNTotalFinishQuantity.Name = "FNTotalFinishQuantity"
        Me.FNTotalFinishQuantity.OptionsColumn.AllowEdit = False
        Me.FNTotalFinishQuantity.OptionsColumn.AllowMove = False
        Me.FNTotalFinishQuantity.Visible = True
        Me.FNTotalFinishQuantity.Width = 50
        '
        'gridBand12
        '
        Me.gridBand12.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.gridBand12.AppearanceHeader.ForeColor = System.Drawing.Color.Gold
        Me.gridBand12.AppearanceHeader.Options.UseFont = True
        Me.gridBand12.AppearanceHeader.Options.UseForeColor = True
        Me.gridBand12.AppearanceHeader.Options.UseTextOptions = True
        Me.gridBand12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gridBand12.Caption = "% Successful"
        Me.gridBand12.Columns.Add(Me.PerSuccess)
        Me.gridBand12.Name = "gridBand12"
        Me.gridBand12.VisibleIndex = 10
        Me.gridBand12.Width = 50
        '
        'PerSuccess
        '
        Me.PerSuccess.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.PerSuccess.AppearanceCell.Options.UseFont = True
        Me.PerSuccess.AppearanceCell.Options.UseTextOptions = True
        Me.PerSuccess.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.PerSuccess.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.PerSuccess.AppearanceHeader.Options.UseFont = True
        Me.PerSuccess.AppearanceHeader.Options.UseTextOptions = True
        Me.PerSuccess.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PerSuccess.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.PerSuccess.Caption = "%ความสำเร็จ"
        Me.PerSuccess.DisplayFormat.FormatString = "N2"
        Me.PerSuccess.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PerSuccess.FieldName = "PerSuccess"
        Me.PerSuccess.MinWidth = 17
        Me.PerSuccess.Name = "PerSuccess"
        Me.PerSuccess.OptionsColumn.AllowEdit = False
        Me.PerSuccess.OptionsColumn.AllowMove = False
        Me.PerSuccess.Visible = True
        Me.PerSuccess.Width = 50
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
        'LCDSewingDaily
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1447, 776)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogc)
        Me.Controls.Add(Me.PanelControl3)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "LCDSewingDaily"
        Me.Text = "Sewing Daily"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BandedGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BandedGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ottime As System.Windows.Forms.Timer
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lbljobBalQty As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbljobBal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbljobDoneQty As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbljobDone As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbljoballQty As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbljoball As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents BandedGridView2 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents FDShipDate As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents ConfirmDate As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FTOrderNo As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FTStyleCode As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents Item As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNWIPQuantity As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNTarget As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNFinishQuantity As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents PerSuccess As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNTotalFinishQuantity As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNGrandQuantity As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents BandedGridView1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents GridBand1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents lblsewBalQty As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblsewBal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblsewDoneQty As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblsewDone As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblsewAllQty As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblsewAll As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GridBand2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gridBand11 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gridBand10 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gridBand9 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gridBand8 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gridBand7 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gridBand3 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gridBand6 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gridBand4 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gridBand5 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gridBand12 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
End Class
