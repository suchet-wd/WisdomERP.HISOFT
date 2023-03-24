Public Class wPurchaseOrderByPR 

    Private _Popup As wPurchaseOrderByPRPoup

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Dim oSysLang As New ST.SysLanguage

        _Popup = New wPurchaseOrderByPRPoup
        HI.TL.HandlerControl.AddHandlerObj(_Popup)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _Popup.Name.ToString.Trim, _Popup)
        Catch ex As Exception
        Finally
        End Try

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub wPurchaseOrderByPR_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private _pDt As DataTable
    Private Sub LoadGridDetail()
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "SELECT  '0' AS FTSelect ,  PR.FTOrderNo, PR.FNQuantity, PR.FTRemark,PR.FNHSysRawMatId,PR.FNHSysUnitId ,PR.FTPRPurchaseNo ,PR.FNHSysUnitId  as FNHSysUnitIdPO_Hide "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , M.FTRawMatNameTH AS FTMatDesc"
            _Qry &= vbCrLf & ",C.FTRawMatColorNameTH AS  FTRawMatColorName "
        Else
            _Qry &= vbCrLf & " , M.FTRawMatNameEN AS FTMatDesc"
            _Qry &= vbCrLf & ",C.FTRawMatColorNameEN AS  FTRawMatColorName "
        End If

        _Qry &= vbCrLf & " , M.FTRawMatColorNameTH  , M.FTRawMatColorNameEN"
        _Qry &= vbCrLf & ", ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Qry &= vbCrLf & ", ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
        _Qry &= vbCrLf & ", ISNULL(PR.FTFabricFrontSize,'') AS FTFabricFrontSize"
        _Qry &= vbCrLf & ", ISNULL(U.FTUnitCode,'') AS FTUnitCode, M.FTRawMatCode , ISNULL(U.FTUnitCode,'') AS FNHSysUnitIdPO "
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(P.FDPRRequestDate)=1 THEN convert(nvarchar(10), convert(datetime,P.FDPRRequestDate),103) ELSE'' END AS FDPRRequestDate"
        _Qry &= vbCrLf & ",T.FNPrice , 0.00 as FNDisPer , '' AS  FTFabricFrontSize  , 0.00 AS FNGrandNetAmt  , 0.00 AS FNDisAmt , 0.00 AS FNNetAmt , 0.00 as FNSurchangeAmt , 0.00 as FNSurchangePerUnit  "
        _Qry &= vbCrLf & " FROM"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request_OrderNo AS PR  WITH (NOLOCK)  LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request AS P  WITH (NOLOCK) ON PR.FTPRPurchaseNo=P.FTPRPurchaseNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M  WITH (NOLOCK)  ON PR.FNHSysRawMatId = M.FNHSysRawMatId LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON M.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON M.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U  WITH (NOLOCK) ON PR.FNHSysUnitId = U.FNHSysUnitId"
        _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMainMat AS T WITH(NOLOCK)  ON M.FTRawMatCode = T.FTMainMatCode "
        _Qry &= vbCrLf & " WHERE ISNULL(P.FTStateApp,'') ='1' AND Isnull(PR.FTPurchaseRefNo,'') = ''"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)
        Me.ogcdetail.DataSource = _dt
         
         
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            Call LoadGridDetail()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub oSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles oSelectAll.CheckedChanged
        Try
            Call SelectAll()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SelectAll()
        Try
            Dim _oDt As DataTable
            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With
            _oDt.BeginInit()
            For Each r As DataRow In _oDt.Rows
                r!FTSelect = IIf(Me.oSelectAll.Checked = True, "1", "0")
            Next
            _oDt.EndInit()
            Me.ogcdetail.DataSource = _oDt
            Me.ogcdetail.Refresh()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmopenPO_Click(sender As Object, e As EventArgs) Handles ocmopenPO.Click
        Try
            Dim _oDt As DataTable
            Dim _Qry As String = ""
            Dim _OrderNo As String = "" : Dim _RawMatId As String = "" : Dim _PRNo As String = ""
            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With

            Dim _NewPO As String = ""
            If _oDt.Select("FTSelect='1'").Length > 0 Then
                For Each R As DataRow In _oDt.Select("FTSelect='1'")
                    If _OrderNo <> "" Then _OrderNo &= ","
                    _OrderNo &= "'" & R!FTOrderNo.ToString & "'"
                    If _RawMatId <> "" Then _RawMatId &= ","
                    _RawMatId &= R!FNHSysRawMatId.ToString
                    If _PRNo <> "" Then _PRNo &= ","
                    _PRNo &= "'" & R!FTPRPurchaseNo.ToString & "'"
                Next

                _Qry = "SELECT     sum(PR.FNQuantity) AS FNQuantity , max(PR.FTRemark) AS  FTRemark "
                _Qry &= vbCrLf & ",PR.FNHSysRawMatId,MAX(PR.FNHSysUnitId) AS FNHSysUnitId  ,MAX(PR.FNHSysUnitId)  as FNHSysUnitIdPO_Hide "
                _Qry &= vbCrLf & ""

                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    _Qry &= vbCrLf & ",max(M.FTRawMatNameTH) AS FTMatDesc"
                    _Qry &= vbCrLf & ",max(C.FTRawMatColorNameTH) AS  FTRawMatColorName "
                Else
                    _Qry &= vbCrLf & ",max(M.FTRawMatNameEN) AS FTMatDesc"
                    _Qry &= vbCrLf & ",max(C.FTRawMatColorNameEN) AS  FTRawMatColorName "
                End If

                _Qry &= vbCrLf & " , max(M.FTRawMatColorNameTH) AS FTRawMatColorNameTH  , max(M.FTRawMatColorNameEN) AS FTRawMatColorNameEN"
                _Qry &= vbCrLf & ", ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
                _Qry &= vbCrLf & ", ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
                _Qry &= vbCrLf & ", MAX(ISNULL(PR.FTFabricFrontSize,'')) AS FTFabricFrontSize"
                _Qry &= vbCrLf & ", MAX(ISNULL(U.FTUnitCode,'')) AS FTUnitCode, M.FTRawMatCode , MAX(ISNULL(U.FTUnitCode,'')) AS FNHSysUnitIdPO "
                _Qry &= vbCrLf & ",0.0000 AS  FNPrice , 0.00 as FNDisPer , '' AS  FTFabricFrontSize  , 0.00 AS FNGrandNetAmt  , 0.00 AS FNDisAmt , 0.00 AS FNNetAmt , 0.00 as FNSurchangeAmt , 0.00 as FNSurchangePerUnit  "
                _Qry &= vbCrLf & "FROM"
                _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request_OrderNo AS PR  WITH (NOLOCK) LEFT OUTER JOIN"
                _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request AS P  WITH (NOLOCK) ON PR.FTPRPurchaseNo=P.FTPRPurchaseNo LEFT OUTER JOIN"
                _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M  WITH (NOLOCK) ON PR.FNHSysRawMatId = M.FNHSysRawMatId LEFT OUTER JOIN"
                _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON M.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
                _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON M.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId LEFT OUTER JOIN"
                _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U  WITH (NOLOCK) ON PR.FNHSysUnitId = U.FNHSysUnitId"

                _Qry &= vbCrLf & "WHERE ISNULL(P.FTStateApp,'') ='1' AND Isnull(PR.FTPurchaseRefNo,'') = '' and PR.FTOrderNo in (" & _OrderNo & ") and PR.FNHSysRawMatId in (" & _RawMatId & ")"
                _Qry &= vbCrLf & "And PR.FTPRPurchaseNo in (" & _PRNo & ")"
                _Qry &= vbCrLf & "group by  PR.FNHSysRawMatId ,C.FTRawMatColorCode ,S.FTRawMatSizeCode , M.FTRawMatCode"
                _pDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

                With _Popup
                    .State = False
                    HI.TL.HandlerControl.ClearControl(_Popup)
                    .oDtRef = _oDt.Select("FTSelect='1'").CopyToDataTable
                    .ogcsum.DataSource = _pDt.Copy
                    .ShowDialog()
                    If (.State) Then
                        _NewPO = .FTPurchaseNo.Text
                    End If
                End With
            End If
            Call LoadGridDetail()
            'If _NewPO <> "" Then
            '    HI.MG.ShowMsg.mInfo("    ", 1511241409, Me.Text, "Purchase No.: " & _NewPO & "  ")
            'End If


        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            Dim _oDt As DataTable
            Dim _Qry As String = ""
            Dim _OrderNo As String = "" : Dim _RawMatId As String = "" : Dim _PRNo As String = ""
            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With

            Dim _NewPO As String = ""
            Dim StateDeleteData As Boolean = False
            If _oDt.Select("FTSelect='1'").Length > 0 Then


                If HI.MG.ShowMsg.mConfirmProcessDefaultNo("คุณต้องการทำการลบข้อมูลขอกเอกสารใบขอเบิกใช่หรือไม่ ?", 1607040524) = True Then
                    StateDeleteData = True
                End If
            End If



            For Each R As DataRow In _oDt.Select("FTSelect='1'")
                If _OrderNo <> "" Then _OrderNo &= ","
                _OrderNo &= "'" & R!FTOrderNo.ToString & "'"
                If _RawMatId <> "" Then _RawMatId &= ","
                _RawMatId &= R!FNHSysRawMatId.ToString
                If _PRNo <> "" Then _PRNo &= ","
                _PRNo &= "'" & R!FTPRPurchaseNo.ToString & "'"
            Next


            If StateDeleteData Then

                Dim _dt As New DataTable
                _dt.Columns.Add("FTPRPurchaseNo", GetType(String))

                For Each R As DataRow In _oDt.Select("FTSelect='1'")

                    Dim cmdstring = ""
                    cmdstring = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request_OrderNo "
                    cmdstring &= vbCrLf & " Where FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPRPurchaseNo.ToString) & "'"
                    cmdstring &= vbCrLf & " And FNHSysRawMatId=" & Val(R!FNHSysRawMatId.ToString) & " "
                    cmdstring &= vbCrLf & " And FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"

                    If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR) = True Then

                        If _dt.Select("FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPRPurchaseNo.ToString) & "'").Length <= 0 Then

                            _dt.Rows.Add(R!FTPRPurchaseNo.ToString)

                        End If

                    End If

                    HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, cmdstring)

                Next

                For Each R As DataRow In _dt.Rows

                    Dim cmdstring = ""
                    cmdstring = " DELETE   A  "
                    cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request AS A "
                    cmdstring &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request_OrderNo AS B ON A.FTPRPurchaseNo=B.FTPRPurchaseNo "
                    cmdstring &= vbCrLf & " Where A.FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPRPurchaseNo.ToString) & "' AND B.FTPRPurchaseNo IS NULL"

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)
                    HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, cmdstring)

                Next

                _dt.Dispose()

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)

            End If
            Call LoadGridDetail()


        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogbmainprocbutton_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles ogbmainprocbutton.Paint

    End Sub
End Class