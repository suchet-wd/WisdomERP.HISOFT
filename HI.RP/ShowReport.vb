Public Class ShowReport
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        PDFShowViewer.LookAndFeel.UseDefaultLookAndFeel = False
        PDFShowViewer.LookAndFeel.SkinName = "Office 2016 Colorful"

    End Sub
End Class