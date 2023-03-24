Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wImportBooking
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
        Me.ReposFDShipDateTo = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.RepositoryItemFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemrFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.FTFilePath_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTFilePath = New DevExpress.XtraEditors.ButtonEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmReadExcel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTInvoiceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTInvoiceRefNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTBookingNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTNikePOLineItem = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ReposFDShipDateTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFDShipDateTo.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemrFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReposFDShipDateTo
        '
        Me.ReposFDShipDateTo.AutoHeight = False
        Me.ReposFDShipDateTo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFDShipDateTo.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFDShipDateTo.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.ReposFDShipDateTo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ReposFDShipDateTo.EditFormat.FormatString = "dd/MM/yyyy"
        Me.ReposFDShipDateTo.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ReposFDShipDateTo.Name = "ReposFDShipDateTo"
        '
        'RepositoryItemFTSelect
        '
        Me.RepositoryItemFTSelect.AutoHeight = False
        Me.RepositoryItemFTSelect.Name = "RepositoryItemFTSelect"
        Me.RepositoryItemFTSelect.ValueChecked = "1"
        Me.RepositoryItemFTSelect.ValueUnchecked = "0"
        '
        'RepositoryItemrFTSelect
        '
        Me.RepositoryItemrFTSelect.AutoHeight = False
        Me.RepositoryItemrFTSelect.Name = "RepositoryItemrFTSelect"
        Me.RepositoryItemrFTSelect.ValueChecked = "1"
        Me.RepositoryItemrFTSelect.ValueUnchecked = "0"
        '
        'ocmdoc
        '
        Me.ocmdoc.Form = Me
        Me.ocmdoc.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.FTFilePath_lbl)
        Me.GroupControl2.Controls.Add(Me.FTFilePath)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1026, 10)
        Me.GroupControl2.TabIndex = 2
        Me.GroupControl2.Text = "PO SUMMARY"
        Me.GroupControl2.Visible = False
        '
        'FTFilePath_lbl
        '
        Me.FTFilePath_lbl.Appearance.Options.UseTextOptions = True
        Me.FTFilePath_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTFilePath_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTFilePath_lbl.Location = New System.Drawing.Point(3, 35)
        Me.FTFilePath_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTFilePath_lbl.Name = "FTFilePath_lbl"
        Me.FTFilePath_lbl.Size = New System.Drawing.Size(192, 16)
        Me.FTFilePath_lbl.TabIndex = 398
        Me.FTFilePath_lbl.Text = "Excel file Path :"
        '
        'FTFilePath
        '
        Me.FTFilePath.Location = New System.Drawing.Point(203, 31)
        Me.FTFilePath.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTFilePath.Name = "FTFilePath"
        Me.FTFilePath.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTFilePath.Properties.Appearance.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTFilePath.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTFilePath.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTFilePath.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTFilePath.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTFilePath.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTFilePath.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTFilePath.Properties.ReadOnly = True
        Me.FTFilePath.Size = New System.Drawing.Size(641, 23)
        Me.FTFilePath.TabIndex = 397
        Me.FTFilePath.Tag = "2|"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.ogbmainprocbutton)
        Me.GroupControl1.Controls.Add(Me.ogc)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 10)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1026, 832)
        Me.GroupControl1.TabIndex = 3
        Me.GroupControl1.Text = "PACKAGE DETAIL"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmReadExcel)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(86, 440)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(888, 64)
        Me.ogbmainprocbutton.TabIndex = 394
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(16, 5)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(111, 31)
        Me.ocmsave.TabIndex = 100
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'ocmReadExcel
        '
        Me.ocmReadExcel.Location = New System.Drawing.Point(154, 5)
        Me.ocmReadExcel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmReadExcel.Name = "ocmReadExcel"
        Me.ocmReadExcel.Size = New System.Drawing.Size(111, 31)
        Me.ocmReadExcel.TabIndex = 99
        Me.ocmReadExcel.TabStop = False
        Me.ocmReadExcel.Tag = "2|"
        Me.ocmReadExcel.Text = "READ EXCEL FILE"
        '
        'ocmclear
        '
        Me.ocmclear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmclear.Location = New System.Drawing.Point(569, 9)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(111, 31)
        Me.ocmclear.TabIndex = 96
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "Clear"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(703, 9)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ogc
        '
        Me.ogc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogc.Location = New System.Drawing.Point(2, 25)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Name = "ogc"
        Me.ogc.Size = New System.Drawing.Size(1022, 805)
        Me.ogc.TabIndex = 0
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTInvoiceNo, Me.cFTInvoiceRefNo, Me.cFTBookingNo, Me.cFTPORef, Me.cFTNikePOLineItem})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupedColumns = True
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'cFTInvoiceNo
        '
        Me.cFTInvoiceNo.Caption = "FTInvoiceNo"
        Me.cFTInvoiceNo.FieldName = "FTInvoiceNo"
        Me.cFTInvoiceNo.Name = "cFTInvoiceNo"
        Me.cFTInvoiceNo.OptionsColumn.AllowEdit = False
        Me.cFTInvoiceNo.Visible = True
        Me.cFTInvoiceNo.VisibleIndex = 0
        Me.cFTInvoiceNo.Width = 102
        '
        'cFTInvoiceRefNo
        '
        Me.cFTInvoiceRefNo.Caption = "FTInvoiceRefNo"
        Me.cFTInvoiceRefNo.FieldName = "FTInvoiceRefNo"
        Me.cFTInvoiceRefNo.Name = "cFTInvoiceRefNo"
        Me.cFTInvoiceRefNo.OptionsColumn.AllowEdit = False
        Me.cFTInvoiceRefNo.Width = 132
        '
        'cFTBookingNo
        '
        Me.cFTBookingNo.Caption = "FTBookingNo"
        Me.cFTBookingNo.FieldName = "FTBookingNo"
        Me.cFTBookingNo.Name = "cFTBookingNo"
        Me.cFTBookingNo.OptionsColumn.AllowEdit = False
        Me.cFTBookingNo.Visible = True
        Me.cFTBookingNo.VisibleIndex = 3
        Me.cFTBookingNo.Width = 130
        '
        'cFTPORef
        '
        Me.cFTPORef.Caption = "FTPORef"
        Me.cFTPORef.FieldName = "FTPORef"
        Me.cFTPORef.Name = "cFTPORef"
        Me.cFTPORef.OptionsColumn.AllowEdit = False
        Me.cFTPORef.Visible = True
        Me.cFTPORef.VisibleIndex = 1
        Me.cFTPORef.Width = 205
        '
        'cFTNikePOLineItem
        '
        Me.cFTNikePOLineItem.Caption = "FTNikePOLineItem"
        Me.cFTNikePOLineItem.FieldName = "FTNikePOLineItem"
        Me.cFTNikePOLineItem.Name = "cFTNikePOLineItem"
        Me.cFTNikePOLineItem.OptionsColumn.AllowEdit = False
        Me.cFTNikePOLineItem.Visible = True
        Me.cFTNikePOLineItem.VisibleIndex = 2
        Me.cFTNikePOLineItem.Width = 85
        '
        'wImportBooking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1026, 842)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.GroupControl2)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wImportBooking"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import File Booking"
        CType(Me.ReposFDShipDateTo.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFDShipDateTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemrFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmdoc As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ReposFDShipDateTo As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents RepositoryItemFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemrFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTFilePath_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTFilePath As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents cFTInvoiceNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTInvoiceRefNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTBookingNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmReadExcel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
End Class
