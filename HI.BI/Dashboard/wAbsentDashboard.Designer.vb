<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAbsentDashboard
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wAbsentDashboard))
        Me.BarAndDockingController1 = New DevExpress.XtraBars.BarAndDockingController()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.ImgFont = New System.Windows.Forms.ImageList()
        Me.dashboardViewer_Renamed = New DevExpress.DashboardWin.DashboardViewer()
        Me.panelHeader = New DevExpress.XtraEditors.PanelControl()
        Me.ocmuploadloaddashboard = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmloaddashboard = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdeletedashboardname = New DevExpress.XtraEditors.SimpleButton()
        Me.pnlbuttun = New DevExpress.XtraEditors.PanelControl()
        Me.ocmmaindashboard = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmnew = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEditDashboard = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmfilterdata = New DevExpress.XtraEditors.SimpleButton()
        Me.FNDashboard = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.FNDashboard_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.panelFooter = New DevExpress.XtraEditors.PanelControl()
        Me.otmcheck = New System.Windows.Forms.Timer()
        CType(Me.BarAndDockingController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dashboardViewer_Renamed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelHeader.SuspendLayout()
        CType(Me.pnlbuttun, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlbuttun.SuspendLayout()
        CType(Me.FNDashboard.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelFooter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarAndDockingController1
        '
        Me.BarAndDockingController1.PropertiesBar.AllowLinkLighting = False
        Me.BarAndDockingController1.PropertiesBar.DefaultGlyphSize = New System.Drawing.Size(16, 16)
        Me.BarAndDockingController1.PropertiesBar.DefaultLargeGlyphSize = New System.Drawing.Size(32, 32)
        '
        'BarManager1
        '
        Me.BarManager1.Controller = Me.BarAndDockingController1
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.MaxItemId = 0
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1563, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 1045)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1563, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 1045)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1563, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 1045)
        '
        'ImgFont
        '
        Me.ImgFont.ImageStream = CType(resources.GetObject("ImgFont.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgFont.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgFont.Images.SetKeyName(0, "ImageDashboardIcon.png")
        '
        'dashboardViewer_Renamed
        '
        Me.dashboardViewer_Renamed.AllowPrintDashboardItems = True
        Me.dashboardViewer_Renamed.AutoScroll = True
        Me.dashboardViewer_Renamed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dashboardViewer_Renamed.Location = New System.Drawing.Point(0, 36)
        Me.dashboardViewer_Renamed.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dashboardViewer_Renamed.Name = "dashboardViewer_Renamed"
        Me.dashboardViewer_Renamed.Size = New System.Drawing.Size(1563, 986)
        Me.dashboardViewer_Renamed.TabIndex = 6
        '
        'panelHeader
        '
        Me.panelHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.panelHeader.Controls.Add(Me.ocmuploadloaddashboard)
        Me.panelHeader.Controls.Add(Me.ocmloaddashboard)
        Me.panelHeader.Controls.Add(Me.ocmdeletedashboardname)
        Me.panelHeader.Controls.Add(Me.pnlbuttun)
        Me.panelHeader.Controls.Add(Me.FNDashboard)
        Me.panelHeader.Controls.Add(Me.FNDashboard_lbl)
        Me.panelHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelHeader.Location = New System.Drawing.Point(0, 0)
        Me.panelHeader.Margin = New System.Windows.Forms.Padding(4)
        Me.panelHeader.Name = "panelHeader"
        Me.panelHeader.Size = New System.Drawing.Size(1563, 36)
        Me.panelHeader.TabIndex = 7
        '
        'ocmuploadloaddashboard
        '
        Me.ocmuploadloaddashboard.AppearanceDisabled.Image = CType(resources.GetObject("ocmuploadloaddashboard.AppearanceDisabled.Image"), System.Drawing.Image)
        Me.ocmuploadloaddashboard.AppearanceDisabled.Options.UseImage = True
        Me.ocmuploadloaddashboard.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.ocmuploadloaddashboard.Image = CType(resources.GetObject("ocmuploadloaddashboard.Image"), System.Drawing.Image)
        Me.ocmuploadloaddashboard.Location = New System.Drawing.Point(533, 1)
        Me.ocmuploadloaddashboard.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmuploadloaddashboard.Name = "ocmuploadloaddashboard"
        Me.ocmuploadloaddashboard.Size = New System.Drawing.Size(40, 34)
        Me.ocmuploadloaddashboard.TabIndex = 259
        Me.ocmuploadloaddashboard.Text = "X"
        Me.ocmuploadloaddashboard.ToolTip = "Upload Dashboard Template"
        Me.ocmuploadloaddashboard.Visible = False
        '
        'ocmloaddashboard
        '
        Me.ocmloaddashboard.AppearanceDisabled.Image = CType(resources.GetObject("ocmloaddashboard.AppearanceDisabled.Image"), System.Drawing.Image)
        Me.ocmloaddashboard.AppearanceDisabled.Options.UseImage = True
        Me.ocmloaddashboard.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.ocmloaddashboard.Image = CType(resources.GetObject("ocmloaddashboard.Image"), System.Drawing.Image)
        Me.ocmloaddashboard.Location = New System.Drawing.Point(490, 1)
        Me.ocmloaddashboard.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmloaddashboard.Name = "ocmloaddashboard"
        Me.ocmloaddashboard.Size = New System.Drawing.Size(40, 34)
        Me.ocmloaddashboard.TabIndex = 258
        Me.ocmloaddashboard.Text = "X"
        Me.ocmloaddashboard.ToolTip = "Load Dashboard Template"
        Me.ocmloaddashboard.Visible = False
        '
        'ocmdeletedashboardname
        '
        Me.ocmdeletedashboardname.AppearanceDisabled.Image = CType(resources.GetObject("ocmdeletedashboardname.AppearanceDisabled.Image"), System.Drawing.Image)
        Me.ocmdeletedashboardname.AppearanceDisabled.Options.UseImage = True
        Me.ocmdeletedashboardname.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.ocmdeletedashboardname.Image = CType(resources.GetObject("ocmdeletedashboardname.Image"), System.Drawing.Image)
        Me.ocmdeletedashboardname.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.ocmdeletedashboardname.Location = New System.Drawing.Point(446, 3)
        Me.ocmdeletedashboardname.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmdeletedashboardname.Name = "ocmdeletedashboardname"
        Me.ocmdeletedashboardname.Size = New System.Drawing.Size(40, 28)
        Me.ocmdeletedashboardname.TabIndex = 257
        Me.ocmdeletedashboardname.ToolTip = "Delete Dashboard Template"
        Me.ocmdeletedashboardname.Visible = False
        '
        'pnlbuttun
        '
        Me.pnlbuttun.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pnlbuttun.Controls.Add(Me.ocmmaindashboard)
        Me.pnlbuttun.Controls.Add(Me.ocmnew)
        Me.pnlbuttun.Controls.Add(Me.btnEditDashboard)
        Me.pnlbuttun.Controls.Add(Me.ocmfilterdata)
        Me.pnlbuttun.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlbuttun.Location = New System.Drawing.Point(927, 0)
        Me.pnlbuttun.Name = "pnlbuttun"
        Me.pnlbuttun.Size = New System.Drawing.Size(636, 36)
        Me.pnlbuttun.TabIndex = 256
        '
        'ocmmaindashboard
        '
        Me.ocmmaindashboard.Dock = System.Windows.Forms.DockStyle.Left
        Me.ocmmaindashboard.Location = New System.Drawing.Point(150, 0)
        Me.ocmmaindashboard.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmmaindashboard.Name = "ocmmaindashboard"
        Me.ocmmaindashboard.Size = New System.Drawing.Size(150, 36)
        Me.ocmmaindashboard.TabIndex = 4
        Me.ocmmaindashboard.Text = "Main Dashboard"
        Me.ocmmaindashboard.Visible = False
        '
        'ocmnew
        '
        Me.ocmnew.Dock = System.Windows.Forms.DockStyle.Right
        Me.ocmnew.Location = New System.Drawing.Point(336, 0)
        Me.ocmnew.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmnew.Name = "ocmnew"
        Me.ocmnew.Size = New System.Drawing.Size(150, 36)
        Me.ocmnew.TabIndex = 1
        Me.ocmnew.Text = "New"
        '
        'btnEditDashboard
        '
        Me.btnEditDashboard.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnEditDashboard.Location = New System.Drawing.Point(486, 0)
        Me.btnEditDashboard.Margin = New System.Windows.Forms.Padding(4)
        Me.btnEditDashboard.Name = "btnEditDashboard"
        Me.btnEditDashboard.Size = New System.Drawing.Size(150, 36)
        Me.btnEditDashboard.TabIndex = 0
        Me.btnEditDashboard.Text = "Edit"
        '
        'ocmfilterdata
        '
        Me.ocmfilterdata.Dock = System.Windows.Forms.DockStyle.Left
        Me.ocmfilterdata.Location = New System.Drawing.Point(0, 0)
        Me.ocmfilterdata.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmfilterdata.Name = "ocmfilterdata"
        Me.ocmfilterdata.Size = New System.Drawing.Size(150, 36)
        Me.ocmfilterdata.TabIndex = 1
        Me.ocmfilterdata.Text = "Load Data"
        Me.ocmfilterdata.Visible = False
        '
        'FNDashboard
        '
        Me.FNDashboard.Location = New System.Drawing.Point(140, 6)
        Me.FNDashboard.Margin = New System.Windows.Forms.Padding(4)
        Me.FNDashboard.Name = "FNDashboard"
        Me.FNDashboard.Properties.AllowMouseWheel = False
        Me.FNDashboard.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNDashboard.Properties.LargeImages = Me.ImgFont
        Me.FNDashboard.Size = New System.Drawing.Size(298, 22)
        Me.FNDashboard.TabIndex = 255
        '
        'FNDashboard_lbl
        '
        Me.FNDashboard_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNDashboard_lbl.Appearance.Options.UseForeColor = True
        Me.FNDashboard_lbl.Appearance.Options.UseTextOptions = True
        Me.FNDashboard_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNDashboard_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNDashboard_lbl.Location = New System.Drawing.Point(2, 2)
        Me.FNDashboard_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNDashboard_lbl.Name = "FNDashboard_lbl"
        Me.FNDashboard_lbl.Size = New System.Drawing.Size(133, 31)
        Me.FNDashboard_lbl.TabIndex = 254
        Me.FNDashboard_lbl.Tag = "2|"
        Me.FNDashboard_lbl.Text = "Dashboard :"
        '
        'panelFooter
        '
        Me.panelFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panelFooter.Location = New System.Drawing.Point(0, 1022)
        Me.panelFooter.Margin = New System.Windows.Forms.Padding(4)
        Me.panelFooter.Name = "panelFooter"
        Me.panelFooter.Size = New System.Drawing.Size(1563, 23)
        Me.panelFooter.TabIndex = 8
        Me.panelFooter.Visible = False
        '
        'otmcheck
        '
        Me.otmcheck.Interval = 60000
        '
        'wAbsentDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1563, 1045)
        Me.Controls.Add(Me.dashboardViewer_Renamed)
        Me.Controls.Add(Me.panelHeader)
        Me.Controls.Add(Me.panelFooter)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wAbsentDashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dashboard"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.BarAndDockingController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dashboardViewer_Renamed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelHeader.ResumeLayout(False)
        CType(Me.pnlbuttun, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlbuttun.ResumeLayout(False)
        CType(Me.FNDashboard.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelFooter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarAndDockingController1 As DevExpress.XtraBars.BarAndDockingController

    Private WithEvents dashboardViewer_Renamed As DevExpress.DashboardWin.DashboardViewer
    Private WithEvents panelHeader As DevExpress.XtraEditors.PanelControl
    Private WithEvents btnEditDashboard As DevExpress.XtraEditors.SimpleButton
    Protected WithEvents panelFooter As DevExpress.XtraEditors.PanelControl
    Private WithEvents ocmfilterdata As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otmcheck As System.Windows.Forms.Timer
    Friend WithEvents FNDashboard_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNDashboard As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents ImgFont As System.Windows.Forms.ImageList
    Friend WithEvents pnlbuttun As DevExpress.XtraEditors.PanelControl
    Private WithEvents ocmnew As DevExpress.XtraEditors.SimpleButton
    Private WithEvents ocmmaindashboard As DevExpress.XtraEditors.SimpleButton
    Private WithEvents ocmdeletedashboardname As DevExpress.XtraEditors.SimpleButton
    Private WithEvents ocmuploadloaddashboard As DevExpress.XtraEditors.SimpleButton
    Private WithEvents ocmloaddashboard As DevExpress.XtraEditors.SimpleButton
End Class
