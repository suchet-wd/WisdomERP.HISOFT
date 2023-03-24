<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wGenerateMRP
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
        Me.components = New System.ComponentModel.Container()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbStyleHeader = New DevExpress.XtraEditors.GroupControl()
        Me.GridOrderList = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEditFNSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDOrderDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysCustId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCustCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCustName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysStyleId_lbl2 = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTUpdTime = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.FDUpdDate = New DevExpress.XtraEditors.TextEdit()
        Me.FTUpdUser = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysBuyId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysBuyId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysBuyId = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbStyleDetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmclearclsr = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmrefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcalc = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpmatcode = New DevExpress.XtraTab.XtraTabPage()
        Me.ogcmatcode = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNHSysStyleId2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDecimal0 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.FTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTOrderNo1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSubOrderNo1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysMerMatId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMerMatSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMainMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTMainMatCode = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.FNHSysMerMatId_None = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysUnitId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNConSmp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDecimal4 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.FNConSmpPlus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysMatColorId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMatColorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysMatSizeId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysRawMatColorId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysRawMatSizeId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatSizeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantityExtra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CalcMPR = New DevExpress.XtraTab.XtraTabPage()
        Me.GridCalculated = New DevExpress.XtraGrid.GridControl()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNStateSourcing = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FNHSysStyleId2_1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSeq2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEditNumberN2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.FTStyleCode2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTOrderNo2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSubOrderNo2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysMerMatId2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMerMatSeq2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMainMatCode2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysMerMatId_None2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysUnitId2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCode2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysSuplId2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSuplCode2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNConSmp2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNConSmpPlus2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysMatColorId2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMatColorName2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysMatSizeId2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMatSizeCode2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysRawMatColorId2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorName2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysRawMatSizeId2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatSizeName2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemButtonEditNumberN0 = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.FNQuantityExtra2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNUsedQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNUsedPlusQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysCurId2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCurCode2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPRQuantity2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPRTotalPrice2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.viewContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.deleteSelection = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.ogbStyleHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbStyleHeader.SuspendLayout()
        CType(Me.GridOrderList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEditFNSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTUpdTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDUpdDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTUpdUser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysBuyId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysBuyId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbStyleDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbStyleDetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpmatcode.SuspendLayout()
        CType(Me.ogcmatcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDecimal0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTMainMatCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDecimal4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CalcMPR.SuspendLayout()
        CType(Me.GridCalculated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEditNumberN2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonEditNumberN0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.viewContextMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbStyleHeader
        '
        Me.ogbStyleHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbStyleHeader.Controls.Add(Me.GridOrderList)
        Me.ogbStyleHeader.Controls.Add(Me.FNHSysStyleId_None)
        Me.ogbStyleHeader.Controls.Add(Me.FNHSysStyleId_lbl2)
        Me.ogbStyleHeader.Controls.Add(Me.FNHSysStyleId)
        Me.ogbStyleHeader.Controls.Add(Me.FTUpdTime)
        Me.ogbStyleHeader.Controls.Add(Me.LabelControl2)
        Me.ogbStyleHeader.Controls.Add(Me.LabelControl1)
        Me.ogbStyleHeader.Controls.Add(Me.FDUpdDate)
        Me.ogbStyleHeader.Controls.Add(Me.FTUpdUser)
        Me.ogbStyleHeader.Controls.Add(Me.FNHSysBuyId_None)
        Me.ogbStyleHeader.Controls.Add(Me.FNHSysBuyId_lbl)
        Me.ogbStyleHeader.Controls.Add(Me.FNHSysBuyId)
        Me.ogbStyleHeader.Location = New System.Drawing.Point(1, 1)
        Me.ogbStyleHeader.Name = "ogbStyleHeader"
        Me.ogbStyleHeader.Size = New System.Drawing.Size(1062, 110)
        Me.ogbStyleHeader.TabIndex = 1
        Me.ogbStyleHeader.Text = "Style Info"
        '
        'GridOrderList
        '
        Me.GridOrderList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridOrderList.Location = New System.Drawing.Point(623, 0)
        Me.GridOrderList.MainView = Me.GridView2
        Me.GridOrderList.Name = "GridOrderList"
        Me.GridOrderList.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEditFNSelect})
        Me.GridOrderList.Size = New System.Drawing.Size(432, 110)
        Me.GridOrderList.TabIndex = 7
        Me.GridOrderList.TabStop = False
        Me.GridOrderList.Tag = "2|"
        Me.GridOrderList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView2.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.GridView2.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView2.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.GridView2.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.GridView2.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.GridView2.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.GridView2.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridView2.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(154, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.GridView2.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridView2.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.GridView2.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.GridView2.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.GridView2.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.GridView2.Appearance.Empty.BackColor = System.Drawing.Color.White
        Me.GridView2.Appearance.Empty.Options.UseBackColor = True
        Me.GridView2.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView2.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.EvenRow.Options.UseBackColor = True
        Me.GridView2.Appearance.EvenRow.Options.UseForeColor = True
        Me.GridView2.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView2.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.GridView2.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView2.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.GridView2.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.GridView2.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.GridView2.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.GridView2.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.GridView2.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
        Me.GridView2.Appearance.FilterPanel.Options.UseBackColor = True
        Me.GridView2.Appearance.FilterPanel.Options.UseForeColor = True
        Me.GridView2.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.GridView2.Appearance.FixedLine.Options.UseBackColor = True
        Me.GridView2.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.GridView2.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.FocusedCell.Options.UseBackColor = True
        Me.GridView2.Appearance.FocusedCell.Options.UseForeColor = True
        Me.GridView2.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.GridView2.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.GridView2.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView2.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView2.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.GridView2.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView2.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.GridView2.Appearance.FooterPanel.Options.UseBackColor = True
        Me.GridView2.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.GridView2.Appearance.FooterPanel.Options.UseForeColor = True
        Me.GridView2.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.GridView2.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.GridView2.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.GroupButton.Options.UseBackColor = True
        Me.GridView2.Appearance.GroupButton.Options.UseBorderColor = True
        Me.GridView2.Appearance.GroupButton.Options.UseForeColor = True
        Me.GridView2.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.GridView2.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.GridView2.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.GroupFooter.Options.UseBackColor = True
        Me.GridView2.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.GridView2.Appearance.GroupFooter.Options.UseForeColor = True
        Me.GridView2.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.GridView2.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView2.Appearance.GroupPanel.Options.UseBackColor = True
        Me.GridView2.Appearance.GroupPanel.Options.UseForeColor = True
        Me.GridView2.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.GridView2.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.GridView2.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.GroupRow.Options.UseBackColor = True
        Me.GridView2.Appearance.GroupRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.GroupRow.Options.UseFont = True
        Me.GridView2.Appearance.GroupRow.Options.UseForeColor = True
        Me.GridView2.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView2.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.GridView2.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView2.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.GridView2.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.GridView2.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.GridView2.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.GridView2.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(106, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.GridView2.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.GridView2.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.GridView2.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.GridView2.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(99, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(196, Byte), Integer))
        Me.GridView2.Appearance.HorzLine.Options.UseBackColor = True
        Me.GridView2.Appearance.OddRow.BackColor = System.Drawing.Color.White
        Me.GridView2.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.OddRow.Options.UseBackColor = True
        Me.GridView2.Appearance.OddRow.Options.UseForeColor = True
        Me.GridView2.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridView2.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.GridView2.Appearance.Preview.Options.UseBackColor = True
        Me.GridView2.Appearance.Preview.Options.UseForeColor = True
        Me.GridView2.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.GridView2.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.Row.Options.UseBackColor = True
        Me.GridView2.Appearance.Row.Options.UseForeColor = True
        Me.GridView2.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
        Me.GridView2.Appearance.RowSeparator.Options.UseBackColor = True
        Me.GridView2.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.GridView2.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.GridView2.Appearance.SelectedRow.Options.UseBackColor = True
        Me.GridView2.Appearance.SelectedRow.Options.UseForeColor = True
        Me.GridView2.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(99, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(196, Byte), Integer))
        Me.GridView2.Appearance.VertLine.Options.UseBackColor = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNSelect, Me.FTOrderNo, Me.FDOrderDate, Me.FDShipDate, Me.FNHSysCustId, Me.FTCustCode, Me.FTCustName})
        Me.GridView2.CustomizationFormBounds = New System.Drawing.Rectangle(758, 512, 216, 178)
        Me.GridView2.GridControl = Me.GridOrderList
        Me.GridView2.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsBehavior.AutoExpandAllGroups = True
        Me.GridView2.OptionsCustomization.AllowQuickHideColumns = False
        Me.GridView2.OptionsNavigation.EnterMoveNextColumn = True
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.GridView2.Tag = "2|"
        '
        'FNSelect
        '
        Me.FNSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FNSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNSelect.Caption = "Select"
        Me.FNSelect.ColumnEdit = Me.RepositoryItemCheckEditFNSelect
        Me.FNSelect.FieldName = "FNSelect"
        Me.FNSelect.Name = "FNSelect"
        Me.FNSelect.Visible = True
        Me.FNSelect.VisibleIndex = 0
        '
        'RepositoryItemCheckEditFNSelect
        '
        Me.RepositoryItemCheckEditFNSelect.AutoHeight = False
        Me.RepositoryItemCheckEditFNSelect.Name = "RepositoryItemCheckEditFNSelect"
        Me.RepositoryItemCheckEditFNSelect.ValueChecked = "1"
        Me.RepositoryItemCheckEditFNSelect.ValueUnchecked = "0"
        '
        'FTOrderNo
        '
        Me.FTOrderNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTOrderNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTOrderNo.Caption = "FTOrderNo"
        Me.FTOrderNo.FieldName = "FTOrderNo"
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.OptionsColumn.AllowEdit = False
        Me.FTOrderNo.Visible = True
        Me.FTOrderNo.VisibleIndex = 1
        '
        'FDOrderDate
        '
        Me.FDOrderDate.AppearanceHeader.Options.UseTextOptions = True
        Me.FDOrderDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDOrderDate.Caption = "FDOrderDate"
        Me.FDOrderDate.FieldName = "FDOrderDate"
        Me.FDOrderDate.Name = "FDOrderDate"
        Me.FDOrderDate.OptionsColumn.AllowEdit = False
        Me.FDOrderDate.Visible = True
        Me.FDOrderDate.VisibleIndex = 2
        '
        'FDShipDate
        '
        Me.FDShipDate.AppearanceHeader.Options.UseTextOptions = True
        Me.FDShipDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDShipDate.Caption = "FDShipDate"
        Me.FDShipDate.FieldName = "FDShipDate"
        Me.FDShipDate.Name = "FDShipDate"
        Me.FDShipDate.OptionsColumn.AllowEdit = False
        Me.FDShipDate.Visible = True
        Me.FDShipDate.VisibleIndex = 3
        '
        'FNHSysCustId
        '
        Me.FNHSysCustId.Caption = "FNHSysCustId"
        Me.FNHSysCustId.FieldName = "FNHSysCustId"
        Me.FNHSysCustId.Name = "FNHSysCustId"
        '
        'FTCustCode
        '
        Me.FTCustCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTCustCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTCustCode.Caption = "FTCustCode"
        Me.FTCustCode.FieldName = "FTCustCode"
        Me.FTCustCode.Name = "FTCustCode"
        '
        'FTCustName
        '
        Me.FTCustName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTCustName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTCustName.Caption = "FTCustName"
        Me.FTCustName.FieldName = "FTCustName"
        Me.FTCustName.Name = "FTCustName"
        Me.FTCustName.OptionsColumn.AllowEdit = False
        Me.FTCustName.Visible = True
        Me.FTCustName.VisibleIndex = 4
        Me.FTCustName.Width = 110
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(315, 48)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(302, 20)
        Me.FNHSysStyleId_None.TabIndex = 3
        Me.FNHSysStyleId_None.Tag = "2|"
        '
        'FNHSysStyleId_lbl2
        '
        Me.FNHSysStyleId_lbl2.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId_lbl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl2.Location = New System.Drawing.Point(5, 48)
        Me.FNHSysStyleId_lbl2.Name = "FNHSysStyleId_lbl2"
        Me.FNHSysStyleId_lbl2.Size = New System.Drawing.Size(168, 20)
        Me.FNHSysStyleId_lbl2.TabIndex = 287
        Me.FNHSysStyleId_lbl2.Tag = "2|"
        Me.FNHSysStyleId_lbl2.Text = "Style No :"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(179, 48)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "89", Nothing, True)})
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysStyleId.TabIndex = 2
        Me.FNHSysStyleId.Tag = "2|"
        '
        'FTUpdTime
        '
        Me.FTUpdTime.Location = New System.Drawing.Point(455, 72)
        Me.FTUpdTime.Name = "FTUpdTime"
        Me.FTUpdTime.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTUpdTime.Properties.Appearance.Options.UseBackColor = True
        Me.FTUpdTime.Properties.ReadOnly = True
        Me.FTUpdTime.Size = New System.Drawing.Size(63, 20)
        Me.FTUpdTime.TabIndex = 6
        Me.FTUpdTime.Tag = "2|"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Location = New System.Drawing.Point(315, 74)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(60, 16)
        Me.LabelControl2.TabIndex = 284
        Me.LabelControl2.Tag = "2|"
        Me.LabelControl2.Text = "Date :"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(66, 73)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(107, 17)
        Me.LabelControl1.TabIndex = 283
        Me.LabelControl1.Tag = "2|"
        Me.LabelControl1.Text = "Last Update By :"
        '
        'FDUpdDate
        '
        Me.FDUpdDate.Location = New System.Drawing.Point(381, 72)
        Me.FDUpdDate.Name = "FDUpdDate"
        Me.FDUpdDate.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FDUpdDate.Properties.Appearance.Options.UseBackColor = True
        Me.FDUpdDate.Properties.ReadOnly = True
        Me.FDUpdDate.Size = New System.Drawing.Size(68, 20)
        Me.FDUpdDate.TabIndex = 5
        Me.FDUpdDate.Tag = "2|"
        '
        'FTUpdUser
        '
        Me.FTUpdUser.Location = New System.Drawing.Point(179, 72)
        Me.FTUpdUser.Name = "FTUpdUser"
        Me.FTUpdUser.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTUpdUser.Properties.Appearance.Options.UseBackColor = True
        Me.FTUpdUser.Properties.ReadOnly = True
        Me.FTUpdUser.Size = New System.Drawing.Size(130, 20)
        Me.FTUpdUser.TabIndex = 4
        Me.FTUpdUser.Tag = "2|"
        '
        'FNHSysBuyId_None
        '
        Me.FNHSysBuyId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysBuyId_None.Location = New System.Drawing.Point(315, 24)
        Me.FNHSysBuyId_None.Name = "FNHSysBuyId_None"
        Me.FNHSysBuyId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysBuyId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysBuyId_None.Properties.ReadOnly = True
        Me.FNHSysBuyId_None.Size = New System.Drawing.Size(302, 20)
        Me.FNHSysBuyId_None.TabIndex = 1
        Me.FNHSysBuyId_None.Tag = "2|"
        '
        'FNHSysBuyId_lbl
        '
        Me.FNHSysBuyId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysBuyId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysBuyId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysBuyId_lbl.Location = New System.Drawing.Point(5, 24)
        Me.FNHSysBuyId_lbl.Name = "FNHSysBuyId_lbl"
        Me.FNHSysBuyId_lbl.Size = New System.Drawing.Size(168, 20)
        Me.FNHSysBuyId_lbl.TabIndex = 279
        Me.FNHSysBuyId_lbl.Tag = "2|"
        Me.FNHSysBuyId_lbl.Text = "Buy :"
        '
        'FNHSysBuyId
        '
        Me.FNHSysBuyId.Location = New System.Drawing.Point(179, 24)
        Me.FNHSysBuyId.Name = "FNHSysBuyId"
        Me.FNHSysBuyId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "113", Nothing, True)})
        Me.FNHSysBuyId.Properties.Tag = ""
        Me.FNHSysBuyId.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysBuyId.TabIndex = 0
        Me.FNHSysBuyId.Tag = "2|"
        '
        'ogbStyleDetail
        '
        Me.ogbStyleDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbStyleDetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbStyleDetail.Controls.Add(Me.otb)
        Me.ogbStyleDetail.Location = New System.Drawing.Point(0, 117)
        Me.ogbStyleDetail.Name = "ogbStyleDetail"
        Me.ogbStyleDetail.ShowCaption = False
        Me.ogbStyleDetail.Size = New System.Drawing.Size(1064, 492)
        Me.ogbStyleDetail.TabIndex = 3
        Me.ogbStyleDetail.Text = "Style Detail"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclearclsr)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmrefresh)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdelete)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmcalc)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(28, 154)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(983, 52)
        Me.ogbmainprocbutton.TabIndex = 137
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmclearclsr
        '
        Me.ocmclearclsr.Location = New System.Drawing.Point(213, 11)
        Me.ocmclearclsr.Name = "ocmclearclsr"
        Me.ocmclearclsr.Size = New System.Drawing.Size(95, 25)
        Me.ocmclearclsr.TabIndex = 109
        Me.ocmclearclsr.TabStop = False
        Me.ocmclearclsr.Tag = "2|"
        Me.ocmclearclsr.Text = "CLEAR"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(314, 11)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(95, 25)
        Me.ocmpreview.TabIndex = 108
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PREVIEW"
        '
        'ocmrefresh
        '
        Me.ocmrefresh.Location = New System.Drawing.Point(415, 11)
        Me.ocmrefresh.Name = "ocmrefresh"
        Me.ocmrefresh.Size = New System.Drawing.Size(95, 25)
        Me.ocmrefresh.TabIndex = 107
        Me.ocmrefresh.TabStop = False
        Me.ocmrefresh.Tag = "2|"
        Me.ocmrefresh.Text = "REFRESH"
        '
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(111, 11)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(95, 25)
        Me.ocmdelete.TabIndex = 106
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "DELETE"
        '
        'ocmcalc
        '
        Me.ocmcalc.Location = New System.Drawing.Point(10, 11)
        Me.ocmcalc.Name = "ocmcalc"
        Me.ocmcalc.Size = New System.Drawing.Size(95, 25)
        Me.ocmcalc.TabIndex = 105
        Me.ocmcalc.TabStop = False
        Me.ocmcalc.Tag = "2|"
        Me.ocmcalc.Text = "CALC MRP"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(908, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(70, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'otb
        '
        Me.otb.AppearancePage.Header.Options.UseTextOptions = True
        Me.otb.AppearancePage.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.otb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otb.Location = New System.Drawing.Point(2, 2)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpmatcode
        Me.otb.Size = New System.Drawing.Size(1060, 488)
        Me.otb.TabIndex = 8
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpmatcode, Me.CalcMPR})
        Me.otb.TabPageWidth = 150
        '
        'otpmatcode
        '
        Me.otpmatcode.AutoScroll = True
        Me.otpmatcode.Controls.Add(Me.ogcmatcode)
        Me.otpmatcode.Name = "otpmatcode"
        Me.otpmatcode.Size = New System.Drawing.Size(1054, 460)
        Me.otpmatcode.Text = "Bom Information"
        '
        'ogcmatcode
        '
        Me.ogcmatcode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcmatcode.Location = New System.Drawing.Point(0, 0)
        Me.ogcmatcode.MainView = Me.GridView1
        Me.ogcmatcode.Name = "ogcmatcode"
        Me.ogcmatcode.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTMainMatCode, Me.RepositoryItemDecimal0, Me.RepositoryItemDecimal4})
        Me.ogcmatcode.Size = New System.Drawing.Size(1054, 460)
        Me.ogcmatcode.TabIndex = 3
        Me.ogcmatcode.TabStop = False
        Me.ogcmatcode.Tag = "2|"
        Me.ogcmatcode.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNHSysStyleId2, Me.FNSeq, Me.FTStyleCode, Me.FTOrderNo1, Me.FTSubOrderNo1, Me.FNHSysMerMatId, Me.FNMerMatSeq, Me.FTMainMatCode, Me.FNHSysMerMatId_None, Me.FNHSysUnitId, Me.FTUnitCode, Me.FNConSmp, Me.FNConSmpPlus, Me.FNHSysMatColorId, Me.FTMatColorName, Me.FNHSysMatSizeId, Me.FTMatSizeCode, Me.FNHSysRawMatColorId, Me.FTRawMatColorName, Me.FNHSysRawMatSizeId, Me.FTRawMatSizeName, Me.FNQuantity, Me.FNQuantityExtra})
        Me.GridView1.CustomizationFormBounds = New System.Drawing.Rectangle(758, 512, 216, 178)
        Me.GridView1.GridControl = Me.ogcmatcode
        Me.GridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.AutoExpandAllGroups = True
        Me.GridView1.OptionsCustomization.AllowQuickHideColumns = False
        Me.GridView1.OptionsNavigation.EnterMoveNextColumn = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.Tag = "2|"
        '
        'FNHSysStyleId2
        '
        Me.FNHSysStyleId2.Caption = "FNHSysStyleId"
        Me.FNHSysStyleId2.FieldName = "FNHSysStyleId"
        Me.FNHSysStyleId2.Name = "FNHSysStyleId2"
        Me.FNHSysStyleId2.OptionsColumn.AllowEdit = False
        Me.FNHSysStyleId2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.FNHSysStyleId2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        '
        'FNSeq
        '
        Me.FNSeq.AppearanceHeader.Options.UseTextOptions = True
        Me.FNSeq.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNSeq.Caption = "Seq"
        Me.FNSeq.ColumnEdit = Me.RepositoryItemDecimal0
        Me.FNSeq.FieldName = "FNSeq"
        Me.FNSeq.Name = "FNSeq"
        Me.FNSeq.OptionsColumn.AllowEdit = False
        Me.FNSeq.OptionsColumn.AllowMove = False
        Me.FNSeq.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSeq.Width = 40
        '
        'RepositoryItemDecimal0
        '
        Me.RepositoryItemDecimal0.AutoHeight = False
        Me.RepositoryItemDecimal0.DisplayFormat.FormatString = "N0"
        Me.RepositoryItemDecimal0.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemDecimal0.EditFormat.FormatString = "N0"
        Me.RepositoryItemDecimal0.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemDecimal0.Name = "RepositoryItemDecimal0"
        '
        'FTStyleCode
        '
        Me.FTStyleCode.Caption = "FTStyleCode"
        Me.FTStyleCode.FieldName = "FTStyleCode"
        Me.FTStyleCode.Name = "FTStyleCode"
        Me.FTStyleCode.OptionsColumn.AllowEdit = False
        Me.FTStyleCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTStyleCode.Visible = True
        Me.FTStyleCode.VisibleIndex = 0
        '
        'FTOrderNo1
        '
        Me.FTOrderNo1.Caption = "FTOrderNo"
        Me.FTOrderNo1.FieldName = "FTOrderNo"
        Me.FTOrderNo1.Name = "FTOrderNo1"
        Me.FTOrderNo1.OptionsColumn.AllowEdit = False
        Me.FTOrderNo1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTOrderNo1.Visible = True
        Me.FTOrderNo1.VisibleIndex = 1
        '
        'FTSubOrderNo1
        '
        Me.FTSubOrderNo1.Caption = "FTSubOrderNo"
        Me.FTSubOrderNo1.FieldName = "FTSubOrderNo"
        Me.FTSubOrderNo1.Name = "FTSubOrderNo1"
        Me.FTSubOrderNo1.OptionsColumn.AllowEdit = False
        Me.FTSubOrderNo1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTSubOrderNo1.Visible = True
        Me.FTSubOrderNo1.VisibleIndex = 2
        '
        'FNHSysMerMatId
        '
        Me.FNHSysMerMatId.FieldName = "FNHSysMerMatId"
        Me.FNHSysMerMatId.Name = "FNHSysMerMatId"
        Me.FNHSysMerMatId.OptionsColumn.AllowEdit = False
        Me.FNHSysMerMatId.OptionsColumn.AllowMove = False
        '
        'FNMerMatSeq
        '
        Me.FNMerMatSeq.Caption = "FNMerMatSeq"
        Me.FNMerMatSeq.FieldName = "FNMerMatSeq"
        Me.FNMerMatSeq.Name = "FNMerMatSeq"
        Me.FNMerMatSeq.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.FNMerMatSeq.Width = 40
        '
        'FTMainMatCode
        '
        Me.FTMainMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTMainMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTMainMatCode.Caption = "Material Code"
        Me.FTMainMatCode.ColumnEdit = Me.RepositoryFTMainMatCode
        Me.FTMainMatCode.FieldName = "FTMainMatCode"
        Me.FTMainMatCode.Name = "FTMainMatCode"
        Me.FTMainMatCode.OptionsColumn.AllowEdit = False
        Me.FTMainMatCode.OptionsColumn.AllowMove = False
        Me.FTMainMatCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTMainMatCode.Visible = True
        Me.FTMainMatCode.VisibleIndex = 3
        Me.FTMainMatCode.Width = 105
        '
        'RepositoryFTMainMatCode
        '
        Me.RepositoryFTMainMatCode.AutoHeight = False
        Me.RepositoryFTMainMatCode.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "108", Nothing, True)})
        Me.RepositoryFTMainMatCode.Name = "RepositoryFTMainMatCode"
        Me.RepositoryFTMainMatCode.Tag = ""
        '
        'FNHSysMerMatId_None
        '
        Me.FNHSysMerMatId_None.Caption = "FTMainMatName"
        Me.FNHSysMerMatId_None.FieldName = "FNHSysMerMatId_None"
        Me.FNHSysMerMatId_None.Name = "FNHSysMerMatId_None"
        Me.FNHSysMerMatId_None.OptionsColumn.AllowEdit = False
        Me.FNHSysMerMatId_None.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FNHSysMerMatId_None.Visible = True
        Me.FNHSysMerMatId_None.VisibleIndex = 4
        Me.FNHSysMerMatId_None.Width = 120
        '
        'FNHSysUnitId
        '
        Me.FNHSysUnitId.Caption = "FNHSysUnitId"
        Me.FNHSysUnitId.FieldName = "FNHSysUnitId"
        Me.FNHSysUnitId.Name = "FNHSysUnitId"
        '
        'FTUnitCode
        '
        Me.FTUnitCode.Caption = "FTUnitCode"
        Me.FTUnitCode.FieldName = "FTUnitCode"
        Me.FTUnitCode.Name = "FTUnitCode"
        Me.FTUnitCode.OptionsColumn.AllowEdit = False
        Me.FTUnitCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTUnitCode.Visible = True
        Me.FTUnitCode.VisibleIndex = 5
        '
        'FNConSmp
        '
        Me.FNConSmp.AppearanceHeader.Options.UseTextOptions = True
        Me.FNConSmp.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNConSmp.Caption = "Consumption"
        Me.FNConSmp.ColumnEdit = Me.RepositoryItemDecimal4
        Me.FNConSmp.DisplayFormat.FormatString = "N2"
        Me.FNConSmp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNConSmp.FieldName = "FNConSmp"
        Me.FNConSmp.Name = "FNConSmp"
        Me.FNConSmp.OptionsColumn.AllowEdit = False
        Me.FNConSmp.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FNConSmp.Visible = True
        Me.FNConSmp.VisibleIndex = 6
        '
        'RepositoryItemDecimal4
        '
        Me.RepositoryItemDecimal4.AutoHeight = False
        Me.RepositoryItemDecimal4.DisplayFormat.FormatString = "N4"
        Me.RepositoryItemDecimal4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemDecimal4.EditFormat.FormatString = "N4"
        Me.RepositoryItemDecimal4.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemDecimal4.Name = "RepositoryItemDecimal4"
        '
        'FNConSmpPlus
        '
        Me.FNConSmpPlus.Caption = "ConSomption Plus"
        Me.FNConSmpPlus.ColumnEdit = Me.RepositoryItemDecimal0
        Me.FNConSmpPlus.DisplayFormat.FormatString = "N2"
        Me.FNConSmpPlus.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNConSmpPlus.FieldName = "FNConSmpPlus"
        Me.FNConSmpPlus.Name = "FNConSmpPlus"
        Me.FNConSmpPlus.OptionsColumn.AllowEdit = False
        Me.FNConSmpPlus.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FNConSmpPlus.Visible = True
        Me.FNConSmpPlus.VisibleIndex = 7
        '
        'FNHSysMatColorId
        '
        Me.FNHSysMatColorId.Caption = "FNHSysMatColorId"
        Me.FNHSysMatColorId.FieldName = "FNHSysMatColorId"
        Me.FNHSysMatColorId.Name = "FNHSysMatColorId"
        '
        'FTMatColorName
        '
        Me.FTMatColorName.Caption = "FTMatColorName"
        Me.FTMatColorName.FieldName = "FTMatColorNameEN"
        Me.FTMatColorName.Name = "FTMatColorName"
        Me.FTMatColorName.OptionsColumn.AllowEdit = False
        Me.FTMatColorName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTMatColorName.Visible = True
        Me.FTMatColorName.VisibleIndex = 10
        '
        'FNHSysMatSizeId
        '
        Me.FNHSysMatSizeId.Caption = "FNHSysMatSizeId"
        Me.FNHSysMatSizeId.FieldName = "FNHSysMatSizeId"
        Me.FNHSysMatSizeId.Name = "FNHSysMatSizeId"
        '
        'FTMatSizeCode
        '
        Me.FTMatSizeCode.Caption = "FTMatSizeCode"
        Me.FTMatSizeCode.FieldName = "FTMatSizeCode"
        Me.FTMatSizeCode.Name = "FTMatSizeCode"
        Me.FTMatSizeCode.OptionsColumn.AllowEdit = False
        Me.FTMatSizeCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTMatSizeCode.Visible = True
        Me.FTMatSizeCode.VisibleIndex = 11
        '
        'FNHSysRawMatColorId
        '
        Me.FNHSysRawMatColorId.Caption = "FNHSysRawMatColorId"
        Me.FNHSysRawMatColorId.FieldName = "FNHSysRawMatColorId"
        Me.FNHSysRawMatColorId.Name = "FNHSysRawMatColorId"
        '
        'FTRawMatColorName
        '
        Me.FTRawMatColorName.Caption = "FTRawMatColorName"
        Me.FTRawMatColorName.FieldName = "FTRawMatColorNameEN"
        Me.FTRawMatColorName.Name = "FTRawMatColorName"
        Me.FTRawMatColorName.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTRawMatColorName.Visible = True
        Me.FTRawMatColorName.VisibleIndex = 8
        '
        'FNHSysRawMatSizeId
        '
        Me.FNHSysRawMatSizeId.Caption = "FNHSysRawMatSizeId"
        Me.FNHSysRawMatSizeId.FieldName = "FNHSysRawMatSizeId"
        Me.FNHSysRawMatSizeId.Name = "FNHSysRawMatSizeId"
        '
        'FTRawMatSizeName
        '
        Me.FTRawMatSizeName.Caption = "FTRawMatSizeName"
        Me.FTRawMatSizeName.FieldName = "FTRawMatSizeNameEN"
        Me.FTRawMatSizeName.Name = "FTRawMatSizeName"
        Me.FTRawMatSizeName.OptionsColumn.AllowEdit = False
        Me.FTRawMatSizeName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTRawMatSizeName.Visible = True
        Me.FTRawMatSizeName.VisibleIndex = 9
        '
        'FNQuantity
        '
        Me.FNQuantity.Caption = "FNQuantity"
        Me.FNQuantity.ColumnEdit = Me.RepositoryItemDecimal0
        Me.FNQuantity.FieldName = "FNQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FNQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", "{0:n0}")})
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 12
        '
        'FNQuantityExtra
        '
        Me.FNQuantityExtra.Caption = "FNQuantityExtra"
        Me.FNQuantityExtra.ColumnEdit = Me.RepositoryItemDecimal0
        Me.FNQuantityExtra.FieldName = "FNQuantityExtra"
        Me.FNQuantityExtra.Name = "FNQuantityExtra"
        Me.FNQuantityExtra.OptionsColumn.AllowEdit = False
        Me.FNQuantityExtra.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FNQuantityExtra.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantityExtra", "{0:n0}")})
        Me.FNQuantityExtra.Visible = True
        Me.FNQuantityExtra.VisibleIndex = 13
        '
        'CalcMPR
        '
        Me.CalcMPR.Controls.Add(Me.GridCalculated)
        Me.CalcMPR.Name = "CalcMPR"
        Me.CalcMPR.Size = New System.Drawing.Size(1054, 460)
        Me.CalcMPR.Text = "Calculated MPR"
        '
        'GridCalculated
        '
        Me.GridCalculated.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridCalculated.Location = New System.Drawing.Point(0, 0)
        Me.GridCalculated.MainView = Me.GridView3
        Me.GridCalculated.Name = "GridCalculated"
        Me.GridCalculated.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemButtonEditNumberN0, Me.RepositoryItemTextEditNumberN2, Me.RepositoryItemCheckEdit1})
        Me.GridCalculated.Size = New System.Drawing.Size(1054, 460)
        Me.GridCalculated.TabIndex = 4
        Me.GridCalculated.TabStop = False
        Me.GridCalculated.Tag = "2|"
        Me.GridCalculated.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView3})
        '
        'GridView3
        '
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNStateSourcing, Me.FNHSysStyleId2_1, Me.FNSeq2, Me.FTStyleCode2, Me.FTOrderNo2, Me.FTSubOrderNo2, Me.FNHSysMerMatId2, Me.FNMerMatSeq2, Me.FTMainMatCode2, Me.FNHSysMerMatId_None2, Me.FNHSysUnitId2, Me.FTUnitCode2, Me.FNHSysSuplId2, Me.FTSuplCode2, Me.FNConSmp2, Me.FNConSmpPlus2, Me.FNHSysMatColorId2, Me.FTMatColorName2, Me.FNHSysMatSizeId2, Me.FTMatSizeCode2, Me.FNHSysRawMatColorId2, Me.FTRawMatColorName2, Me.FNHSysRawMatSizeId2, Me.FTRawMatSizeName2, Me.FNQuantity2, Me.FNQuantityExtra2, Me.FNUsedQuantity, Me.FNUsedPlusQuantity, Me.FNPrice, Me.FNHSysCurId2, Me.FTCurCode2, Me.FNPRQuantity2, Me.FNPRTotalPrice2})
        Me.GridView3.CustomizationFormBounds = New System.Drawing.Rectangle(758, 512, 216, 178)
        Me.GridView3.GridControl = Me.GridCalculated
        Me.GridView3.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GridView3.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", Me.FNQuantity2, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantityExtra", Me.FNQuantityExtra2, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNUsedQuantity", Me.FNUsedQuantity, "{0:n4}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNUsedPlusQuantity", Me.FNUsedPlusQuantity, "{0:n4}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNPRQuantity", Me.FNPRQuantity2, "{0:n4}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNPRTotalPrice", Me.FNPRTotalPrice2, "{0:n4}")})
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsBehavior.AutoExpandAllGroups = True
        Me.GridView3.OptionsCustomization.AllowQuickHideColumns = False
        Me.GridView3.OptionsNavigation.EnterMoveNextColumn = True
        Me.GridView3.OptionsSelection.MultiSelect = True
        Me.GridView3.OptionsView.ColumnAutoWidth = False
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GridView3.OptionsView.ShowFooter = True
        Me.GridView3.OptionsView.ShowGroupedColumns = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        Me.GridView3.Tag = "2|"
        '
        'FNStateSourcing
        '
        Me.FNStateSourcing.Caption = "FNStateSourcing"
        Me.FNStateSourcing.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.FNStateSourcing.FieldName = "FNStateSourcing"
        Me.FNStateSourcing.Name = "FNStateSourcing"
        Me.FNStateSourcing.OptionsColumn.AllowEdit = False
        Me.FNStateSourcing.Visible = True
        Me.FNStateSourcing.VisibleIndex = 0
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'FNHSysStyleId2_1
        '
        Me.FNHSysStyleId2_1.Caption = "FNHSysStyleId"
        Me.FNHSysStyleId2_1.FieldName = "FNHSysStyleId"
        Me.FNHSysStyleId2_1.Name = "FNHSysStyleId2_1"
        Me.FNHSysStyleId2_1.OptionsColumn.AllowEdit = False
        Me.FNHSysStyleId2_1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.FNHSysStyleId2_1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        '
        'FNSeq2
        '
        Me.FNSeq2.AppearanceHeader.Options.UseTextOptions = True
        Me.FNSeq2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNSeq2.Caption = "Seq"
        Me.FNSeq2.ColumnEdit = Me.RepositoryItemTextEditNumberN2
        Me.FNSeq2.FieldName = "FNSeq"
        Me.FNSeq2.Name = "FNSeq2"
        Me.FNSeq2.OptionsColumn.AllowEdit = False
        Me.FNSeq2.OptionsColumn.AllowMove = False
        Me.FNSeq2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSeq2.Width = 40
        '
        'RepositoryItemTextEditNumberN2
        '
        Me.RepositoryItemTextEditNumberN2.AutoHeight = False
        Me.RepositoryItemTextEditNumberN2.DisplayFormat.FormatString = "N4"
        Me.RepositoryItemTextEditNumberN2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemTextEditNumberN2.EditFormat.FormatString = "N4"
        Me.RepositoryItemTextEditNumberN2.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemTextEditNumberN2.Name = "RepositoryItemTextEditNumberN2"
        '
        'FTStyleCode2
        '
        Me.FTStyleCode2.Caption = "FTStyleCode"
        Me.FTStyleCode2.FieldName = "FTStyleCode"
        Me.FTStyleCode2.Name = "FTStyleCode2"
        Me.FTStyleCode2.OptionsColumn.AllowEdit = False
        Me.FTStyleCode2.Visible = True
        Me.FTStyleCode2.VisibleIndex = 1
        '
        'FTOrderNo2
        '
        Me.FTOrderNo2.Caption = "FTOrderNo"
        Me.FTOrderNo2.FieldName = "FTOrderNo"
        Me.FTOrderNo2.Name = "FTOrderNo2"
        Me.FTOrderNo2.OptionsColumn.AllowEdit = False
        Me.FTOrderNo2.Visible = True
        Me.FTOrderNo2.VisibleIndex = 2
        '
        'FTSubOrderNo2
        '
        Me.FTSubOrderNo2.Caption = "FTSubOrderNo"
        Me.FTSubOrderNo2.FieldName = "FTSubOrderNo"
        Me.FTSubOrderNo2.Name = "FTSubOrderNo2"
        Me.FTSubOrderNo2.OptionsColumn.AllowEdit = False
        Me.FTSubOrderNo2.Visible = True
        Me.FTSubOrderNo2.VisibleIndex = 3
        '
        'FNHSysMerMatId2
        '
        Me.FNHSysMerMatId2.FieldName = "FNHSysMerMatId"
        Me.FNHSysMerMatId2.Name = "FNHSysMerMatId2"
        Me.FNHSysMerMatId2.OptionsColumn.AllowEdit = False
        Me.FNHSysMerMatId2.OptionsColumn.AllowMove = False
        '
        'FNMerMatSeq2
        '
        Me.FNMerMatSeq2.Caption = "FNMerMatSeq"
        Me.FNMerMatSeq2.FieldName = "FNMerMatSeq"
        Me.FNMerMatSeq2.Name = "FNMerMatSeq2"
        Me.FNMerMatSeq2.OptionsColumn.AllowEdit = False
        Me.FNMerMatSeq2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.FNMerMatSeq2.Width = 40
        '
        'FTMainMatCode2
        '
        Me.FTMainMatCode2.AppearanceHeader.Options.UseTextOptions = True
        Me.FTMainMatCode2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTMainMatCode2.Caption = "Material Code"
        Me.FTMainMatCode2.FieldName = "FTMainMatCode"
        Me.FTMainMatCode2.Name = "FTMainMatCode2"
        Me.FTMainMatCode2.OptionsColumn.AllowEdit = False
        Me.FTMainMatCode2.OptionsColumn.AllowMove = False
        Me.FTMainMatCode2.Visible = True
        Me.FTMainMatCode2.VisibleIndex = 4
        Me.FTMainMatCode2.Width = 105
        '
        'FNHSysMerMatId_None2
        '
        Me.FNHSysMerMatId_None2.Caption = "FTMainMatName"
        Me.FNHSysMerMatId_None2.FieldName = "FNHSysMerMatId_None"
        Me.FNHSysMerMatId_None2.Name = "FNHSysMerMatId_None2"
        Me.FNHSysMerMatId_None2.OptionsColumn.AllowEdit = False
        Me.FNHSysMerMatId_None2.Visible = True
        Me.FNHSysMerMatId_None2.VisibleIndex = 5
        Me.FNHSysMerMatId_None2.Width = 120
        '
        'FNHSysUnitId2
        '
        Me.FNHSysUnitId2.Caption = "FNHSysUnitId"
        Me.FNHSysUnitId2.FieldName = "FNHSysUnitId"
        Me.FNHSysUnitId2.Name = "FNHSysUnitId2"
        Me.FNHSysUnitId2.OptionsColumn.AllowEdit = False
        '
        'FTUnitCode2
        '
        Me.FTUnitCode2.Caption = "FTUnitCode"
        Me.FTUnitCode2.FieldName = "FTUnitCode"
        Me.FTUnitCode2.Name = "FTUnitCode2"
        Me.FTUnitCode2.OptionsColumn.AllowEdit = False
        Me.FTUnitCode2.Visible = True
        Me.FTUnitCode2.VisibleIndex = 6
        '
        'FNHSysSuplId2
        '
        Me.FNHSysSuplId2.Caption = "FNHSysSuplId"
        Me.FNHSysSuplId2.FieldName = "FNHSysSuplId"
        Me.FNHSysSuplId2.Name = "FNHSysSuplId2"
        Me.FNHSysSuplId2.OptionsColumn.AllowEdit = False
        '
        'FTSuplCode2
        '
        Me.FTSuplCode2.Caption = "FTSuplCode"
        Me.FTSuplCode2.FieldName = "FTSuplCode"
        Me.FTSuplCode2.Name = "FTSuplCode2"
        Me.FTSuplCode2.OptionsColumn.AllowEdit = False
        Me.FTSuplCode2.Visible = True
        Me.FTSuplCode2.VisibleIndex = 7
        '
        'FNConSmp2
        '
        Me.FNConSmp2.AppearanceHeader.Options.UseTextOptions = True
        Me.FNConSmp2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNConSmp2.Caption = "Consumption"
        Me.FNConSmp2.ColumnEdit = Me.RepositoryItemTextEditNumberN2
        Me.FNConSmp2.DisplayFormat.FormatString = "N2"
        Me.FNConSmp2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNConSmp2.FieldName = "FNConSmp"
        Me.FNConSmp2.Name = "FNConSmp2"
        Me.FNConSmp2.OptionsColumn.AllowEdit = False
        Me.FNConSmp2.Visible = True
        Me.FNConSmp2.VisibleIndex = 8
        '
        'FNConSmpPlus2
        '
        Me.FNConSmpPlus2.Caption = "ConSomption Plus"
        Me.FNConSmpPlus2.ColumnEdit = Me.RepositoryItemTextEditNumberN2
        Me.FNConSmpPlus2.DisplayFormat.FormatString = "N2"
        Me.FNConSmpPlus2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNConSmpPlus2.FieldName = "FNConSmpPlus"
        Me.FNConSmpPlus2.Name = "FNConSmpPlus2"
        Me.FNConSmpPlus2.OptionsColumn.AllowEdit = False
        Me.FNConSmpPlus2.Visible = True
        Me.FNConSmpPlus2.VisibleIndex = 9
        '
        'FNHSysMatColorId2
        '
        Me.FNHSysMatColorId2.Caption = "FNHSysMatColorId"
        Me.FNHSysMatColorId2.FieldName = "FNHSysMatColorId"
        Me.FNHSysMatColorId2.Name = "FNHSysMatColorId2"
        Me.FNHSysMatColorId2.OptionsColumn.AllowEdit = False
        '
        'FTMatColorName2
        '
        Me.FTMatColorName2.Caption = "FTMatColorName"
        Me.FTMatColorName2.FieldName = "FTMatColorNameEN"
        Me.FTMatColorName2.Name = "FTMatColorName2"
        Me.FTMatColorName2.OptionsColumn.AllowEdit = False
        Me.FTMatColorName2.Visible = True
        Me.FTMatColorName2.VisibleIndex = 10
        '
        'FNHSysMatSizeId2
        '
        Me.FNHSysMatSizeId2.Caption = "FNHSysMatSizeId"
        Me.FNHSysMatSizeId2.FieldName = "FNHSysMatSizeId"
        Me.FNHSysMatSizeId2.Name = "FNHSysMatSizeId2"
        Me.FNHSysMatSizeId2.OptionsColumn.AllowEdit = False
        '
        'FTMatSizeCode2
        '
        Me.FTMatSizeCode2.Caption = "FTMatSizeCode"
        Me.FTMatSizeCode2.FieldName = "FTMatSizeCode"
        Me.FTMatSizeCode2.Name = "FTMatSizeCode2"
        Me.FTMatSizeCode2.OptionsColumn.AllowEdit = False
        Me.FTMatSizeCode2.Visible = True
        Me.FTMatSizeCode2.VisibleIndex = 11
        '
        'FNHSysRawMatColorId2
        '
        Me.FNHSysRawMatColorId2.Caption = "FNHSysRawMatColorId"
        Me.FNHSysRawMatColorId2.FieldName = "FNHSysRawMatColorId"
        Me.FNHSysRawMatColorId2.Name = "FNHSysRawMatColorId2"
        Me.FNHSysRawMatColorId2.OptionsColumn.AllowEdit = False
        '
        'FTRawMatColorName2
        '
        Me.FTRawMatColorName2.Caption = "FTRawMatColorName"
        Me.FTRawMatColorName2.FieldName = "FTRawMatColorNameEN"
        Me.FTRawMatColorName2.Name = "FTRawMatColorName2"
        Me.FTRawMatColorName2.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorName2.Visible = True
        Me.FTRawMatColorName2.VisibleIndex = 12
        '
        'FNHSysRawMatSizeId2
        '
        Me.FNHSysRawMatSizeId2.Caption = "FNHSysRawMatSizeId"
        Me.FNHSysRawMatSizeId2.FieldName = "FNHSysRawMatSizeId"
        Me.FNHSysRawMatSizeId2.Name = "FNHSysRawMatSizeId2"
        Me.FNHSysRawMatSizeId2.OptionsColumn.AllowEdit = False
        '
        'FTRawMatSizeName2
        '
        Me.FTRawMatSizeName2.Caption = "FTRawMatSizeName"
        Me.FTRawMatSizeName2.FieldName = "FTRawMatSizeNameEN"
        Me.FTRawMatSizeName2.Name = "FTRawMatSizeName2"
        Me.FTRawMatSizeName2.OptionsColumn.AllowEdit = False
        Me.FTRawMatSizeName2.Visible = True
        Me.FTRawMatSizeName2.VisibleIndex = 13
        '
        'FNQuantity2
        '
        Me.FNQuantity2.Caption = "FNQuantity"
        Me.FNQuantity2.ColumnEdit = Me.RepositoryItemButtonEditNumberN0
        Me.FNQuantity2.FieldName = "FNQuantity"
        Me.FNQuantity2.Name = "FNQuantity2"
        Me.FNQuantity2.OptionsColumn.AllowEdit = False
        Me.FNQuantity2.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", "{0:n0}")})
        Me.FNQuantity2.Visible = True
        Me.FNQuantity2.VisibleIndex = 14
        '
        'RepositoryItemButtonEditNumberN0
        '
        Me.RepositoryItemButtonEditNumberN0.AutoHeight = False
        Me.RepositoryItemButtonEditNumberN0.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "108", Nothing, True)})
        Me.RepositoryItemButtonEditNumberN0.DisplayFormat.FormatString = "N0"
        Me.RepositoryItemButtonEditNumberN0.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemButtonEditNumberN0.EditFormat.FormatString = "N0"
        Me.RepositoryItemButtonEditNumberN0.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemButtonEditNumberN0.Name = "RepositoryItemButtonEditNumberN0"
        Me.RepositoryItemButtonEditNumberN0.Tag = ""
        '
        'FNQuantityExtra2
        '
        Me.FNQuantityExtra2.Caption = "FNQuantityExtra"
        Me.FNQuantityExtra2.ColumnEdit = Me.RepositoryItemButtonEditNumberN0
        Me.FNQuantityExtra2.FieldName = "FNQuantityExtra"
        Me.FNQuantityExtra2.Name = "FNQuantityExtra2"
        Me.FNQuantityExtra2.OptionsColumn.AllowEdit = False
        Me.FNQuantityExtra2.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantityExtra", "{0:n0}")})
        Me.FNQuantityExtra2.Visible = True
        Me.FNQuantityExtra2.VisibleIndex = 15
        '
        'FNUsedQuantity
        '
        Me.FNUsedQuantity.Caption = "FNUsedQuantity"
        Me.FNUsedQuantity.ColumnEdit = Me.RepositoryItemTextEditNumberN2
        Me.FNUsedQuantity.FieldName = "FNUsedQuantity"
        Me.FNUsedQuantity.Name = "FNUsedQuantity"
        Me.FNUsedQuantity.OptionsColumn.AllowEdit = False
        Me.FNUsedQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNUsedQuantity", "{0:n4}")})
        Me.FNUsedQuantity.Visible = True
        Me.FNUsedQuantity.VisibleIndex = 16
        '
        'FNUsedPlusQuantity
        '
        Me.FNUsedPlusQuantity.Caption = "FNUsedPlusQuantity"
        Me.FNUsedPlusQuantity.ColumnEdit = Me.RepositoryItemTextEditNumberN2
        Me.FNUsedPlusQuantity.FieldName = "FNUsedPlusQuantity"
        Me.FNUsedPlusQuantity.Name = "FNUsedPlusQuantity"
        Me.FNUsedPlusQuantity.OptionsColumn.AllowEdit = False
        Me.FNUsedPlusQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNUsedPlusQuantity", "{0:n4}")})
        Me.FNUsedPlusQuantity.Visible = True
        Me.FNUsedPlusQuantity.VisibleIndex = 17
        '
        'FNPrice
        '
        Me.FNPrice.Caption = "FNPrice"
        Me.FNPrice.ColumnEdit = Me.RepositoryItemTextEditNumberN2
        Me.FNPrice.FieldName = "FNPrice"
        Me.FNPrice.Name = "FNPrice"
        Me.FNPrice.OptionsColumn.AllowEdit = False
        Me.FNPrice.Visible = True
        Me.FNPrice.VisibleIndex = 18
        '
        'FNHSysCurId2
        '
        Me.FNHSysCurId2.Caption = "FNHSysCurId"
        Me.FNHSysCurId2.FieldName = "FNHSysCurId"
        Me.FNHSysCurId2.Name = "FNHSysCurId2"
        Me.FNHSysCurId2.OptionsColumn.AllowEdit = False
        '
        'FTCurCode2
        '
        Me.FTCurCode2.Caption = "FTCurCode"
        Me.FTCurCode2.FieldName = "FTCurCode"
        Me.FTCurCode2.Name = "FTCurCode2"
        Me.FTCurCode2.OptionsColumn.AllowEdit = False
        Me.FTCurCode2.Visible = True
        Me.FTCurCode2.VisibleIndex = 19
        '
        'FNPRQuantity2
        '
        Me.FNPRQuantity2.Caption = "FNPRQuantity2"
        Me.FNPRQuantity2.ColumnEdit = Me.RepositoryItemTextEditNumberN2
        Me.FNPRQuantity2.FieldName = "FNPRQuantity"
        Me.FNPRQuantity2.Name = "FNPRQuantity2"
        Me.FNPRQuantity2.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNPRQuantity", "{0:n4}")})
        Me.FNPRQuantity2.Visible = True
        Me.FNPRQuantity2.VisibleIndex = 20
        '
        'FNPRTotalPrice2
        '
        Me.FNPRTotalPrice2.Caption = "FNPRTotalPrice2"
        Me.FNPRTotalPrice2.ColumnEdit = Me.RepositoryItemTextEditNumberN2
        Me.FNPRTotalPrice2.FieldName = "FNPRTotalPrice"
        Me.FNPRTotalPrice2.Name = "FNPRTotalPrice2"
        Me.FNPRTotalPrice2.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNPRTotalPrice", "{0:n4}")})
        Me.FNPRTotalPrice2.Visible = True
        Me.FNPRTotalPrice2.VisibleIndex = 21
        '
        'viewContextMenuStrip
        '
        Me.viewContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.deleteSelection})
        Me.viewContextMenuStrip.Name = "detailViewContextMenuStrip"
        Me.viewContextMenuStrip.Size = New System.Drawing.Size(108, 26)
        '
        'deleteSelection
        '
        Me.deleteSelection.Name = "deleteSelection"
        Me.deleteSelection.Size = New System.Drawing.Size(107, 22)
        Me.deleteSelection.Text = "Delete"
        '
        'wGenerateMRP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1064, 609)
        Me.Controls.Add(Me.ogbStyleDetail)
        Me.Controls.Add(Me.ogbStyleHeader)
        Me.Name = "wGenerateMRP"
        Me.Text = "Generate MPR"
        CType(Me.ogbStyleHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbStyleHeader.ResumeLayout(False)
        CType(Me.GridOrderList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEditFNSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTUpdTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDUpdDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTUpdUser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysBuyId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysBuyId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbStyleDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbStyleDetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpmatcode.ResumeLayout(False)
        CType(Me.ogcmatcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDecimal0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTMainMatCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDecimal4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CalcMPR.ResumeLayout(False)
        CType(Me.GridCalculated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEditNumberN2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonEditNumberN0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.viewContextMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbStyleHeader As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNHSysBuyId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysBuyId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysBuyId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogbStyleDetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpmatcode As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents FTUpdTime As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDUpdDate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTUpdUser As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcalc As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcmatcode As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysMerMatId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMainMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNConSmp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTMainMatCode As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents FNConSmpPlus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemDecimal0 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents FNHSysStyleId2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMerMatSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysMerMatId_None As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysStyleId_lbl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTOrderNo1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSubOrderNo1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysMatColorId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMatColorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysMatSizeId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatSizeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridOrderList As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDOrderDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEditFNSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FNHSysCustId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCustName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCustCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantityExtra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysUnitId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysRawMatColorId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysRawMatSizeId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CalcMPR As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GridCalculated As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FNHSysStyleId2_1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSeq2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextEditNumberN2 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents FTStyleCode2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTOrderNo2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSubOrderNo2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysMerMatId2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMerMatSeq2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMainMatCode2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemButtonEditNumberN0 As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents FNHSysMerMatId_None2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysUnitId2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitCode2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNConSmp2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNConSmpPlus2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysMatColorId2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMatColorName2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysMatSizeId2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMatSizeCode2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysRawMatColorId2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorName2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysRawMatSizeId2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatSizeName2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantityExtra2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNUsedQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNUsedPlusQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSuplCode2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysSuplId2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysCurId2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCurCode2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPRQuantity2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPRTotalPrice2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmclearclsr As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNStateSourcing As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Private WithEvents viewContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Private WithEvents deleteSelection As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RepositoryItemDecimal4 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit

End Class
