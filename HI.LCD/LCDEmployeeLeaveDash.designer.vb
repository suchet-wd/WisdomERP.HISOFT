<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LCDEmployeeLeaveDash
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
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.opnl1 = New DevExpress.XtraEditors.PanelControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.ogcDocket = New DevExpress.XtraGrid.GridControl()
        Me.ogvDocket = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNListIndex = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTNameTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTNameEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ottime = New System.Windows.Forms.Timer()
        Me.olbhour = New DevExpress.XtraEditors.LabelControl()
        Me.Timer1 = New System.Windows.Forms.Timer()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.opnl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.opnl1.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.ogcDocket, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDocket, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        '
        'opnl1
        '
        Me.opnl1.Controls.Add(Me.PanelControl2)
        Me.opnl1.Controls.Add(Me.PanelControl1)
        Me.opnl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.opnl1.Location = New System.Drawing.Point(0, 0)
        Me.opnl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.opnl1.Name = "opnl1"
        Me.opnl1.Size = New System.Drawing.Size(1688, 955)
        Me.opnl1.TabIndex = 0
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.ogcDocket)
        Me.PanelControl2.Controls.Add(Me.PanelControl3)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(2, 122)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(1684, 831)
        Me.PanelControl2.TabIndex = 1
        '
        'ogcDocket
        '
        Me.ogcDocket.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcDocket.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode2.RelationName = "Level1"
        Me.ogcDocket.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.ogcDocket.Location = New System.Drawing.Point(2, 2)
        Me.ogcDocket.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.ogcDocket.LookAndFeel.UseDefaultLookAndFeel = False
        Me.ogcDocket.MainView = Me.ogvDocket
        Me.ogcDocket.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDocket.Name = "ogcDocket"
        Me.ogcDocket.Size = New System.Drawing.Size(1680, 778)
        Me.ogcDocket.TabIndex = 2
        Me.ogcDocket.TabStop = False
        Me.ogcDocket.Tag = "2|"
        Me.ogcDocket.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDocket})
        '
        'ogvDocket
        '
        Me.ogvDocket.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ogvDocket.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ogvDocket.Appearance.Row.BackColor2 = System.Drawing.Color.White
        Me.ogvDocket.Appearance.Row.Options.UseBackColor = True
        Me.ogvDocket.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNListIndex, Me.FTNameTH, Me.FTNameEN})
        Me.ogvDocket.GridControl = Me.ogcDocket
        Me.ogvDocket.Name = "ogvDocket"
        Me.ogvDocket.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvDocket.OptionsMenu.ShowAutoFilterRowItem = False
        Me.ogvDocket.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ogvDocket.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ogvDocket.OptionsView.ColumnAutoWidth = False
        Me.ogvDocket.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.[True]
        Me.ogvDocket.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvDocket.OptionsView.RowAutoHeight = True
        Me.ogvDocket.OptionsView.ShowGroupPanel = False
        Me.ogvDocket.Tag = "2|"
        '
        'FNListIndex
        '
        Me.FNListIndex.AppearanceCell.BackColor = System.Drawing.Color.Transparent
        Me.FNListIndex.AppearanceCell.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.FNListIndex.AppearanceCell.Options.UseBackColor = True
        Me.FNListIndex.AppearanceHeader.BackColor = System.Drawing.Color.Transparent
        Me.FNListIndex.AppearanceHeader.BackColor2 = System.Drawing.Color.Transparent
        Me.FNListIndex.AppearanceHeader.Options.UseBackColor = True
        Me.FNListIndex.Caption = "FNListIndex"
        Me.FNListIndex.FieldName = "FNListIndex"
        Me.FNListIndex.Name = "FNListIndex"
        '
        'FTNameTH
        '
        Me.FTNameTH.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.FTNameTH.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.FTNameTH.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FTNameTH.AppearanceCell.ForeColor = System.Drawing.Color.Black
        Me.FTNameTH.AppearanceCell.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.FTNameTH.AppearanceCell.Options.UseBackColor = True
        Me.FTNameTH.AppearanceCell.Options.UseFont = True
        Me.FTNameTH.AppearanceCell.Options.UseForeColor = True
        Me.FTNameTH.AppearanceHeader.BackColor = System.Drawing.Color.Black
        Me.FTNameTH.AppearanceHeader.BackColor2 = System.Drawing.Color.Black
        Me.FTNameTH.AppearanceHeader.BorderColor = System.Drawing.Color.Black
        Me.FTNameTH.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FTNameTH.AppearanceHeader.Options.UseBackColor = True
        Me.FTNameTH.AppearanceHeader.Options.UseBorderColor = True
        Me.FTNameTH.AppearanceHeader.Options.UseFont = True
        Me.FTNameTH.Caption = "FTNameTH"
        Me.FTNameTH.ColumnEdit = Me.RepositoryItemMemoEdit1
        Me.FTNameTH.FieldName = "FTNameTH"
        Me.FTNameTH.Name = "FTNameTH"
        Me.FTNameTH.OptionsColumn.AllowEdit = False
        Me.FTNameTH.OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTNameTH.OptionsColumn.ReadOnly = True
        Me.FTNameTH.OptionsFilter.AllowAutoFilter = False
        Me.FTNameTH.Visible = True
        Me.FTNameTH.VisibleIndex = 0
        Me.FTNameTH.Width = 350
        '
        'FTNameEN
        '
        Me.FTNameEN.AppearanceHeader.BackColor = System.Drawing.Color.White
        Me.FTNameEN.AppearanceHeader.BackColor2 = System.Drawing.Color.White
        Me.FTNameEN.AppearanceHeader.BorderColor = System.Drawing.Color.White
        Me.FTNameEN.AppearanceHeader.Options.UseBackColor = True
        Me.FTNameEN.AppearanceHeader.Options.UseBorderColor = True
        Me.FTNameEN.Caption = "FTNameEN"
        Me.FTNameEN.FieldName = "FTNameEN"
        Me.FTNameEN.Name = "FTNameEN"
        '
        'PanelControl3
        '
        Me.PanelControl3.Appearance.BackColor = System.Drawing.Color.Black
        Me.PanelControl3.Appearance.Options.UseBackColor = True
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl3.Location = New System.Drawing.Point(2, 780)
        Me.PanelControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.PanelControl3.LookAndFeel.TouchUIMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.PanelControl3.LookAndFeel.UseDefaultLookAndFeel = False
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(1680, 49)
        Me.PanelControl3.TabIndex = 1
        '
        'PanelControl1
        '
        Me.PanelControl1.Appearance.BackColor = System.Drawing.Color.Black
        Me.PanelControl1.Appearance.Options.UseBackColor = True
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(2, 2)
        Me.PanelControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.PanelControl1.LookAndFeel.TouchUIMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.PanelControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1684, 120)
        Me.PanelControl1.TabIndex = 0
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 25.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.Gold
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.LabelControl2.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(1678, 112)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Month Years"
        '
        'ottime
        '
        Me.ottime.Enabled = True
        Me.ottime.Interval = 120000
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
        Me.olbhour.Location = New System.Drawing.Point(8, 8)
        Me.olbhour.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.olbhour.Name = "olbhour"
        Me.olbhour.Size = New System.Drawing.Size(32, 78)
        Me.olbhour.TabIndex = 6
        Me.olbhour.Text = "08:00:00"
        Me.olbhour.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 10000
        '
        'LCDEmployeeLeaveDash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1688, 955)
        Me.ControlBox = False
        Me.Controls.Add(Me.opnl1)
        Me.Controls.Add(Me.olbhour)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "LCDEmployeeLeaveDash"
        Me.Text = "LCDEmployeeLeaveDash "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.opnl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.opnl1.ResumeLayout(False)
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.ogcDocket, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDocket, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents opnl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ottime As System.Windows.Forms.Timer
    Friend WithEvents olbhour As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ogcDocket As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDocket As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FNListIndex As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTNameTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTNameEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
End Class
