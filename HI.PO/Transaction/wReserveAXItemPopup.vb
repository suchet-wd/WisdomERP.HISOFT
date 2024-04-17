Public Class wReserveAXItemPopup

    Public ReserveAXNo As String = ""
    Public DocumentAXType As Integer = 0
    Public OrderNo As String = ""
    Public WHNo As String = ""


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        With ReposFTSelect
            AddHandler .CheckedChanged, AddressOf CheckEdit_CheckedChanged
        End With
        With ReposFNReserveQty
            .Precision = HI.ST.Config.QtyDigit
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave
            AddHandler .EditValueChanged, AddressOf CalcEdit_EditValueChanged
        End With



    End Sub

    Private Sub CheckEdit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

            If CType(sender, DevExpress.XtraEditors.CheckEdit).Checked Then
                Dim _balQty As Double = .GetFocusedRowCellValue("QtyAvaiable")
                .SetFocusedRowCellValue("FNDocQuantity", _balQty)
            Else
                .SetFocusedRowCellValue("FNDocQuantity", 0)
            End If

        End With
    End Sub

    Private Sub CalcEdit_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

            Dim _balQty As Double = .GetFocusedRowCellValue("QtyAvaiable")

            If sender.value > _balQty Then
                sender.value = _balQty
            End If

        End With
    End Sub

    Private _ProcessProc As Boolean = False
    Public Property ProcessProc As Boolean
        Get
            Return _ProcessProc
        End Get
        Set(value As Boolean)
            _ProcessProc = value
        End Set
    End Property

    Private Sub wReserveItemPopup_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    End Sub

    Private Sub ocmreceive_Click(sender As System.Object, e As System.EventArgs) Handles ocmok.Click

        If Me.ogcbarcode.DataSource Is Nothing Then Exit Sub


        With CType(Me.ogcbarcode.DataSource, DataTable)
            .AcceptChanges()
            If .Select("FTSelect='1' AND FNDocQuantity > 0").Length <= 0 Then
                Exit Sub
            End If
        End With
        Me.ProcessProc = True
        Me.Close()
    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.ProcessProc = False
        Me.Close()
    End Sub

    Private Sub ocmsearch_Click(sender As Object, e As EventArgs) Handles ocmsearch.Click

        If FTStateCheckMRP.Checked = False Then
            If FNHSysMainMatId.Text.Trim = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysMainMatId_lbl.Text)
                FNHSysMainMatId.Focus()
                Exit Sub
            End If
        End If

        Dim _Str As String = ""
        Dim spls2 As New HI.TL.SplashScreen("Loading Data Onhand Please wait....")
        Dim _dtBar As DataTable
        _Str = "  EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_RESERVESTOCKAX '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(ReserveAXNo) & "'," & DocumentAXType & ",'" & HI.UL.ULF.rpQuoted(OrderNo) & "','" & HI.UL.ULF.rpQuoted(OrderNo) & "','" & HI.UL.ULF.rpQuoted(WHNo) & "','" & HI.UL.ULF.rpQuoted(FNHSysMainMatId.Text) & "','" & FTStateCheckMRP.EditValue.ToString & "'"


        _dtBar = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)
        Me.ogcbarcode.DataSource = _dtBar.Copy
        _dtBar.Dispose()
        spls2.Close()


    End Sub
End Class