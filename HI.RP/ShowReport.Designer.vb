<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ShowReport
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
        Me.PDFShowViewer = New DevExpress.XtraPdfViewer.PdfViewer()
        Me.PdfBarController1 = New DevExpress.XtraPdfViewer.Bars.PdfBarController()
        Me.RibbonControl1 = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.PdfFileRibbonPageGroup1 = New DevExpress.XtraPdfViewer.Bars.PdfFileRibbonPageGroup()
        Me.PdfRibbonPage1 = New DevExpress.XtraPdfViewer.Bars.PdfRibbonPage()
        Me.PdfFileOpenBarItem1 = New DevExpress.XtraPdfViewer.Bars.PdfFileOpenBarItem()
        Me.PdfFileSaveAsBarItem1 = New DevExpress.XtraPdfViewer.Bars.PdfFileSaveAsBarItem()
        Me.PdfFilePrintBarItem1 = New DevExpress.XtraPdfViewer.Bars.PdfFilePrintBarItem()
        Me.PdfNavigationRibbonPageGroup1 = New DevExpress.XtraPdfViewer.Bars.PdfNavigationRibbonPageGroup()
        Me.PdfPreviousPageBarItem1 = New DevExpress.XtraPdfViewer.Bars.PdfPreviousPageBarItem()
        Me.PdfNextPageBarItem1 = New DevExpress.XtraPdfViewer.Bars.PdfNextPageBarItem()
        Me.PdfFindTextBarItem1 = New DevExpress.XtraPdfViewer.Bars.PdfFindTextBarItem()
        Me.PdfZoomRibbonPageGroup1 = New DevExpress.XtraPdfViewer.Bars.PdfZoomRibbonPageGroup()
        Me.PdfZoomOutBarItem1 = New DevExpress.XtraPdfViewer.Bars.PdfZoomOutBarItem()
        Me.PdfZoomInBarItem1 = New DevExpress.XtraPdfViewer.Bars.PdfZoomInBarItem()
        Me.PdfExactZoomListBarSubItem1 = New DevExpress.XtraPdfViewer.Bars.PdfExactZoomListBarSubItem()
        Me.PdfZoom10CheckItem1 = New DevExpress.XtraPdfViewer.Bars.PdfZoom10CheckItem()
        Me.PdfZoom25CheckItem1 = New DevExpress.XtraPdfViewer.Bars.PdfZoom25CheckItem()
        Me.PdfZoom50CheckItem1 = New DevExpress.XtraPdfViewer.Bars.PdfZoom50CheckItem()
        Me.PdfZoom75CheckItem1 = New DevExpress.XtraPdfViewer.Bars.PdfZoom75CheckItem()
        Me.PdfZoom100CheckItem1 = New DevExpress.XtraPdfViewer.Bars.PdfZoom100CheckItem()
        Me.PdfZoom125CheckItem1 = New DevExpress.XtraPdfViewer.Bars.PdfZoom125CheckItem()
        Me.PdfZoom150CheckItem1 = New DevExpress.XtraPdfViewer.Bars.PdfZoom150CheckItem()
        Me.PdfZoom200CheckItem1 = New DevExpress.XtraPdfViewer.Bars.PdfZoom200CheckItem()
        Me.PdfZoom400CheckItem1 = New DevExpress.XtraPdfViewer.Bars.PdfZoom400CheckItem()
        Me.PdfZoom500CheckItem1 = New DevExpress.XtraPdfViewer.Bars.PdfZoom500CheckItem()
        Me.PdfSetActualSizeZoomModeCheckItem1 = New DevExpress.XtraPdfViewer.Bars.PdfSetActualSizeZoomModeCheckItem()
        Me.PdfSetPageLevelZoomModeCheckItem1 = New DevExpress.XtraPdfViewer.Bars.PdfSetPageLevelZoomModeCheckItem()
        Me.PdfSetFitWidthZoomModeCheckItem1 = New DevExpress.XtraPdfViewer.Bars.PdfSetFitWidthZoomModeCheckItem()
        Me.PdfSetFitVisibleZoomModeCheckItem1 = New DevExpress.XtraPdfViewer.Bars.PdfSetFitVisibleZoomModeCheckItem()
        Me.PdfFormDataBarPageGroup1 = New DevExpress.XtraPdfViewer.Bars.PdfFormDataBarPageGroup()
        Me.PdfFormDataRibbonPage1 = New DevExpress.XtraPdfViewer.Bars.PdfFormDataRibbonPage()
        Me.PdfExportFormDataBarItem1 = New DevExpress.XtraPdfViewer.Bars.PdfExportFormDataBarItem()
        Me.PdfImportFormDataBarItem1 = New DevExpress.XtraPdfViewer.Bars.PdfImportFormDataBarItem()
        CType(Me.PdfBarController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PDFShowViewer
        '
        Me.PDFShowViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PDFShowViewer.DocumentFilePath = "C:\Users\NUM\Downloads\BOM Automation Factory Kickoff 1.1.pdf"
        Me.PDFShowViewer.Location = New System.Drawing.Point(0, 177)
        Me.PDFShowViewer.Name = "PDFShowViewer"
        Me.PDFShowViewer.Size = New System.Drawing.Size(1245, 411)
        Me.PDFShowViewer.TabIndex = 0
        '
        'PdfBarController1
        '
        Me.PdfBarController1.BarItems.Add(Me.PdfFileOpenBarItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfFileSaveAsBarItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfFilePrintBarItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfPreviousPageBarItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfNextPageBarItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfFindTextBarItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfZoomOutBarItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfZoomInBarItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfExactZoomListBarSubItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfZoom10CheckItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfZoom25CheckItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfZoom50CheckItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfZoom75CheckItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfZoom100CheckItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfZoom125CheckItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfZoom150CheckItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfZoom200CheckItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfZoom400CheckItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfZoom500CheckItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfSetActualSizeZoomModeCheckItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfSetPageLevelZoomModeCheckItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfSetFitWidthZoomModeCheckItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfSetFitVisibleZoomModeCheckItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfExportFormDataBarItem1)
        Me.PdfBarController1.BarItems.Add(Me.PdfImportFormDataBarItem1)
        Me.PdfBarController1.Control = Me.PDFShowViewer
        '
        'RibbonControl1
        '
        Me.RibbonControl1.ExpandCollapseItem.Id = 0
        Me.RibbonControl1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl1.ExpandCollapseItem, Me.PdfFileOpenBarItem1, Me.PdfFileSaveAsBarItem1, Me.PdfFilePrintBarItem1, Me.PdfPreviousPageBarItem1, Me.PdfNextPageBarItem1, Me.PdfFindTextBarItem1, Me.PdfZoomOutBarItem1, Me.PdfZoomInBarItem1, Me.PdfExactZoomListBarSubItem1, Me.PdfZoom10CheckItem1, Me.PdfZoom25CheckItem1, Me.PdfZoom50CheckItem1, Me.PdfZoom75CheckItem1, Me.PdfZoom100CheckItem1, Me.PdfZoom125CheckItem1, Me.PdfZoom150CheckItem1, Me.PdfZoom200CheckItem1, Me.PdfZoom400CheckItem1, Me.PdfZoom500CheckItem1, Me.PdfSetActualSizeZoomModeCheckItem1, Me.PdfSetPageLevelZoomModeCheckItem1, Me.PdfSetFitWidthZoomModeCheckItem1, Me.PdfSetFitVisibleZoomModeCheckItem1, Me.PdfExportFormDataBarItem1, Me.PdfImportFormDataBarItem1})
        Me.RibbonControl1.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl1.MaxItemId = 26
        Me.RibbonControl1.Name = "RibbonControl1"
        Me.RibbonControl1.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.PdfRibbonPage1, Me.PdfFormDataRibbonPage1})
        Me.RibbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013
        Me.RibbonControl1.Size = New System.Drawing.Size(1245, 177)
        Me.RibbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Above
        '
        'PdfFileRibbonPageGroup1
        '
        Me.PdfFileRibbonPageGroup1.ItemLinks.Add(Me.PdfFileOpenBarItem1)
        Me.PdfFileRibbonPageGroup1.ItemLinks.Add(Me.PdfFileSaveAsBarItem1)
        Me.PdfFileRibbonPageGroup1.ItemLinks.Add(Me.PdfFilePrintBarItem1)
        Me.PdfFileRibbonPageGroup1.Name = "PdfFileRibbonPageGroup1"
        '
        'PdfRibbonPage1
        '
        Me.PdfRibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.PdfFileRibbonPageGroup1, Me.PdfNavigationRibbonPageGroup1, Me.PdfZoomRibbonPageGroup1})
        Me.PdfRibbonPage1.Name = "PdfRibbonPage1"
        '
        'PdfFileOpenBarItem1
        '
        Me.PdfFileOpenBarItem1.Id = 1
        Me.PdfFileOpenBarItem1.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O))
        Me.PdfFileOpenBarItem1.Name = "PdfFileOpenBarItem1"
        '
        'PdfFileSaveAsBarItem1
        '
        Me.PdfFileSaveAsBarItem1.Id = 2
        Me.PdfFileSaveAsBarItem1.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
        Me.PdfFileSaveAsBarItem1.Name = "PdfFileSaveAsBarItem1"
        '
        'PdfFilePrintBarItem1
        '
        Me.PdfFilePrintBarItem1.Id = 3
        Me.PdfFilePrintBarItem1.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
        Me.PdfFilePrintBarItem1.Name = "PdfFilePrintBarItem1"
        '
        'PdfNavigationRibbonPageGroup1
        '
        Me.PdfNavigationRibbonPageGroup1.ItemLinks.Add(Me.PdfPreviousPageBarItem1)
        Me.PdfNavigationRibbonPageGroup1.ItemLinks.Add(Me.PdfNextPageBarItem1)
        Me.PdfNavigationRibbonPageGroup1.ItemLinks.Add(Me.PdfFindTextBarItem1)
        Me.PdfNavigationRibbonPageGroup1.Name = "PdfNavigationRibbonPageGroup1"
        '
        'PdfPreviousPageBarItem1
        '
        Me.PdfPreviousPageBarItem1.Id = 4
        Me.PdfPreviousPageBarItem1.Name = "PdfPreviousPageBarItem1"
        '
        'PdfNextPageBarItem1
        '
        Me.PdfNextPageBarItem1.Id = 5
        Me.PdfNextPageBarItem1.Name = "PdfNextPageBarItem1"
        '
        'PdfFindTextBarItem1
        '
        Me.PdfFindTextBarItem1.Id = 6
        Me.PdfFindTextBarItem1.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F))
        Me.PdfFindTextBarItem1.Name = "PdfFindTextBarItem1"
        '
        'PdfZoomRibbonPageGroup1
        '
        Me.PdfZoomRibbonPageGroup1.ItemLinks.Add(Me.PdfZoomOutBarItem1)
        Me.PdfZoomRibbonPageGroup1.ItemLinks.Add(Me.PdfZoomInBarItem1)
        Me.PdfZoomRibbonPageGroup1.ItemLinks.Add(Me.PdfExactZoomListBarSubItem1)
        Me.PdfZoomRibbonPageGroup1.Name = "PdfZoomRibbonPageGroup1"
        '
        'PdfZoomOutBarItem1
        '
        Me.PdfZoomOutBarItem1.Id = 7
        Me.PdfZoomOutBarItem1.Name = "PdfZoomOutBarItem1"
        '
        'PdfZoomInBarItem1
        '
        Me.PdfZoomInBarItem1.Id = 8
        Me.PdfZoomInBarItem1.Name = "PdfZoomInBarItem1"
        '
        'PdfExactZoomListBarSubItem1
        '
        Me.PdfExactZoomListBarSubItem1.Id = 9
        Me.PdfExactZoomListBarSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PdfZoom10CheckItem1, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PdfZoom25CheckItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PdfZoom50CheckItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PdfZoom75CheckItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PdfZoom100CheckItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PdfZoom125CheckItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PdfZoom150CheckItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PdfZoom200CheckItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PdfZoom400CheckItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PdfZoom500CheckItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PdfSetActualSizeZoomModeCheckItem1, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PdfSetPageLevelZoomModeCheckItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PdfSetFitWidthZoomModeCheckItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PdfSetFitVisibleZoomModeCheckItem1)})
        Me.PdfExactZoomListBarSubItem1.Name = "PdfExactZoomListBarSubItem1"
        Me.PdfExactZoomListBarSubItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
        '
        'PdfZoom10CheckItem1
        '
        Me.PdfZoom10CheckItem1.Id = 10
        Me.PdfZoom10CheckItem1.Name = "PdfZoom10CheckItem1"
        '
        'PdfZoom25CheckItem1
        '
        Me.PdfZoom25CheckItem1.Id = 11
        Me.PdfZoom25CheckItem1.Name = "PdfZoom25CheckItem1"
        '
        'PdfZoom50CheckItem1
        '
        Me.PdfZoom50CheckItem1.Id = 12
        Me.PdfZoom50CheckItem1.Name = "PdfZoom50CheckItem1"
        '
        'PdfZoom75CheckItem1
        '
        Me.PdfZoom75CheckItem1.Id = 13
        Me.PdfZoom75CheckItem1.Name = "PdfZoom75CheckItem1"
        '
        'PdfZoom100CheckItem1
        '
        Me.PdfZoom100CheckItem1.Id = 14
        Me.PdfZoom100CheckItem1.Name = "PdfZoom100CheckItem1"
        '
        'PdfZoom125CheckItem1
        '
        Me.PdfZoom125CheckItem1.Id = 15
        Me.PdfZoom125CheckItem1.Name = "PdfZoom125CheckItem1"
        '
        'PdfZoom150CheckItem1
        '
        Me.PdfZoom150CheckItem1.Id = 16
        Me.PdfZoom150CheckItem1.Name = "PdfZoom150CheckItem1"
        '
        'PdfZoom200CheckItem1
        '
        Me.PdfZoom200CheckItem1.Id = 17
        Me.PdfZoom200CheckItem1.Name = "PdfZoom200CheckItem1"
        '
        'PdfZoom400CheckItem1
        '
        Me.PdfZoom400CheckItem1.Id = 18
        Me.PdfZoom400CheckItem1.Name = "PdfZoom400CheckItem1"
        '
        'PdfZoom500CheckItem1
        '
        Me.PdfZoom500CheckItem1.Id = 19
        Me.PdfZoom500CheckItem1.Name = "PdfZoom500CheckItem1"
        '
        'PdfSetActualSizeZoomModeCheckItem1
        '
        Me.PdfSetActualSizeZoomModeCheckItem1.Id = 20
        Me.PdfSetActualSizeZoomModeCheckItem1.Name = "PdfSetActualSizeZoomModeCheckItem1"
        '
        'PdfSetPageLevelZoomModeCheckItem1
        '
        Me.PdfSetPageLevelZoomModeCheckItem1.Id = 21
        Me.PdfSetPageLevelZoomModeCheckItem1.Name = "PdfSetPageLevelZoomModeCheckItem1"
        '
        'PdfSetFitWidthZoomModeCheckItem1
        '
        Me.PdfSetFitWidthZoomModeCheckItem1.Id = 22
        Me.PdfSetFitWidthZoomModeCheckItem1.Name = "PdfSetFitWidthZoomModeCheckItem1"
        '
        'PdfSetFitVisibleZoomModeCheckItem1
        '
        Me.PdfSetFitVisibleZoomModeCheckItem1.Id = 23
        Me.PdfSetFitVisibleZoomModeCheckItem1.Name = "PdfSetFitVisibleZoomModeCheckItem1"
        '
        'PdfFormDataBarPageGroup1
        '
        Me.PdfFormDataBarPageGroup1.ItemLinks.Add(Me.PdfExportFormDataBarItem1)
        Me.PdfFormDataBarPageGroup1.ItemLinks.Add(Me.PdfImportFormDataBarItem1)
        Me.PdfFormDataBarPageGroup1.Name = "PdfFormDataBarPageGroup1"
        '
        'PdfFormDataRibbonPage1
        '
        Me.PdfFormDataRibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.PdfFormDataBarPageGroup1})
        Me.PdfFormDataRibbonPage1.Name = "PdfFormDataRibbonPage1"
        Me.PdfFormDataRibbonPage1.Visible = False
        '
        'PdfExportFormDataBarItem1
        '
        Me.PdfExportFormDataBarItem1.Id = 24
        Me.PdfExportFormDataBarItem1.Name = "PdfExportFormDataBarItem1"
        '
        'PdfImportFormDataBarItem1
        '
        Me.PdfImportFormDataBarItem1.Id = 25
        Me.PdfImportFormDataBarItem1.Name = "PdfImportFormDataBarItem1"
        '
        'ShowReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1245, 588)
        Me.Controls.Add(Me.PDFShowViewer)
        Me.Controls.Add(Me.RibbonControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ShowReport"
        Me.Text = "ShowReport"
        CType(Me.PdfBarController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PDFShowViewer As DevExpress.XtraPdfViewer.PdfViewer
    Friend WithEvents PdfBarController1 As DevExpress.XtraPdfViewer.Bars.PdfBarController
    Friend WithEvents PdfFileOpenBarItem1 As DevExpress.XtraPdfViewer.Bars.PdfFileOpenBarItem
    Friend WithEvents PdfFileSaveAsBarItem1 As DevExpress.XtraPdfViewer.Bars.PdfFileSaveAsBarItem
    Friend WithEvents PdfFilePrintBarItem1 As DevExpress.XtraPdfViewer.Bars.PdfFilePrintBarItem
    Friend WithEvents PdfPreviousPageBarItem1 As DevExpress.XtraPdfViewer.Bars.PdfPreviousPageBarItem
    Friend WithEvents PdfNextPageBarItem1 As DevExpress.XtraPdfViewer.Bars.PdfNextPageBarItem
    Friend WithEvents PdfFindTextBarItem1 As DevExpress.XtraPdfViewer.Bars.PdfFindTextBarItem
    Friend WithEvents PdfZoomOutBarItem1 As DevExpress.XtraPdfViewer.Bars.PdfZoomOutBarItem
    Friend WithEvents PdfZoomInBarItem1 As DevExpress.XtraPdfViewer.Bars.PdfZoomInBarItem
    Friend WithEvents PdfExactZoomListBarSubItem1 As DevExpress.XtraPdfViewer.Bars.PdfExactZoomListBarSubItem
    Friend WithEvents PdfZoom10CheckItem1 As DevExpress.XtraPdfViewer.Bars.PdfZoom10CheckItem
    Friend WithEvents PdfZoom25CheckItem1 As DevExpress.XtraPdfViewer.Bars.PdfZoom25CheckItem
    Friend WithEvents PdfZoom50CheckItem1 As DevExpress.XtraPdfViewer.Bars.PdfZoom50CheckItem
    Friend WithEvents PdfZoom75CheckItem1 As DevExpress.XtraPdfViewer.Bars.PdfZoom75CheckItem
    Friend WithEvents PdfZoom100CheckItem1 As DevExpress.XtraPdfViewer.Bars.PdfZoom100CheckItem
    Friend WithEvents PdfZoom125CheckItem1 As DevExpress.XtraPdfViewer.Bars.PdfZoom125CheckItem
    Friend WithEvents PdfZoom150CheckItem1 As DevExpress.XtraPdfViewer.Bars.PdfZoom150CheckItem
    Friend WithEvents PdfZoom200CheckItem1 As DevExpress.XtraPdfViewer.Bars.PdfZoom200CheckItem
    Friend WithEvents PdfZoom400CheckItem1 As DevExpress.XtraPdfViewer.Bars.PdfZoom400CheckItem
    Friend WithEvents PdfZoom500CheckItem1 As DevExpress.XtraPdfViewer.Bars.PdfZoom500CheckItem
    Friend WithEvents PdfSetActualSizeZoomModeCheckItem1 As DevExpress.XtraPdfViewer.Bars.PdfSetActualSizeZoomModeCheckItem
    Friend WithEvents PdfSetPageLevelZoomModeCheckItem1 As DevExpress.XtraPdfViewer.Bars.PdfSetPageLevelZoomModeCheckItem
    Friend WithEvents PdfSetFitWidthZoomModeCheckItem1 As DevExpress.XtraPdfViewer.Bars.PdfSetFitWidthZoomModeCheckItem
    Friend WithEvents PdfSetFitVisibleZoomModeCheckItem1 As DevExpress.XtraPdfViewer.Bars.PdfSetFitVisibleZoomModeCheckItem
    Friend WithEvents PdfExportFormDataBarItem1 As DevExpress.XtraPdfViewer.Bars.PdfExportFormDataBarItem
    Friend WithEvents PdfImportFormDataBarItem1 As DevExpress.XtraPdfViewer.Bars.PdfImportFormDataBarItem
    Friend WithEvents RibbonControl1 As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents PdfRibbonPage1 As DevExpress.XtraPdfViewer.Bars.PdfRibbonPage
    Friend WithEvents PdfFileRibbonPageGroup1 As DevExpress.XtraPdfViewer.Bars.PdfFileRibbonPageGroup
    Friend WithEvents PdfNavigationRibbonPageGroup1 As DevExpress.XtraPdfViewer.Bars.PdfNavigationRibbonPageGroup
    Friend WithEvents PdfZoomRibbonPageGroup1 As DevExpress.XtraPdfViewer.Bars.PdfZoomRibbonPageGroup
    Friend WithEvents PdfFormDataRibbonPage1 As DevExpress.XtraPdfViewer.Bars.PdfFormDataRibbonPage
    Friend WithEvents PdfFormDataBarPageGroup1 As DevExpress.XtraPdfViewer.Bars.PdfFormDataBarPageGroup
End Class
