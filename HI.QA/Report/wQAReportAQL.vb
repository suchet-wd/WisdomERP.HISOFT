Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing

Public Class wQAReportAQL

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Call InitGrid()
    End Sub

    Private Sub InitGrid()
        Try
            Dim _FSumMain As String = "FNQAInQty|FNQAActualQty|FTDefectBody|FNDefect|FNQAActualQty|FNAQLQty|FNQAAqlQty|FNQAActualQty"
            With ogvDetail
                .ClearGrouping()
                .ClearDocument()
                .OptionsView.ShowFooter = True
                For Each Str As String In _FSumMain.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next
            End With
        Catch ex As Exception
        End Try
    End Sub


    Private Sub LoadData()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT     C.FTStyleCode, B.FTUnitSectCode, A.FTOrderNo,O.FTPORef ,  sum(A.FNQAInQty) AS FNQAInQty, sum(A.FNQAAqlQty) AS FNQAAqlQty, sum(A.FNQAActualQty) AS FNQAActualQty, "
            _Cmd &= vbCrLf & " SUM(A.FNAndon) AS FNAndon    , ISNULL" ', sum(case when isnull(D.FTStateReject,'') = '1' then 1 else 0 end ) as FNDefect
            _Cmd &= vbCrLf & "         ((SELECT     ISNULL(FNSampleQty, 0) AS Expr1  "


            _Cmd &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMConfigQA"
            _Cmd &= vbCrLf & "                        WHERE     (max(A.FNQAInQty) >= FNStartQty) AND (max(A.FNQAInQty) <= FNEndQty)), 0) AS FNAQLQty"

            _Cmd &= vbCrLf & " , Isnull ((  Select sum(case when isnull(D.FTStateReject,'') = '1' then 1 else 0 end ) as FNDefect     "
            _Cmd &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_Detail AS D  WITH(NOLOCK) where  D.FNHSysStyleId = A.FNHSysStyleId  and D.FNHSysUnitSectId = A.FNHSysUnitSectId   "
            _Cmd &= vbCrLf & "   and D.FTOrderNo = A.FTOrderNo and D.FDQADate = A.FDQADate),0) as FNDefect"

            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA AS A LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS B ON A.FNHSysUnitSectId = B.FNHSysUnitSectId"
            _Cmd &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS C ON A.FNHSysStyleId = C.FNHSysStyleId"
            _Cmd &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo"
            '_Cmd &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_Detail AS D  WITH(NOLOCK) ON A.FNHSysStyleId = D.FNHSysStyleId  and A.FNHSysUnitSectId = D.FNHSysUnitSectId "
            '_Cmd &= vbCrLf & "   and A.FTOrderNo = D.FTOrderNo and A.FDQADate = D.FDQADate and A.FNHourNo = D.FNHourNo "

            _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS OD ON A.FTOrderNo = OD.FTOrderNo "
            _Cmd &= vbCrLf & " WHERE OD.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & ""


            _Cmd &= vbCrLf & "  AND     (ISNULL(A.FTStateReject, N'0') = '0')"

            _Cmd &= vbCrLf & " AND A.FDQADate >='" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "' and A.FDQADate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "'"

            If Me.FNHSysUnitSectId.Text <> "" Then
                _Cmd &= vbCrLf & " and B.FTUnitSectCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "'"
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Cmd &= vbCrLf & " and B.FTUnitSectCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "'"
            End If
            If Me.FNHSysStyleId.Text <> "" Then
                _Cmd &= vbCrLf & " and C.FTStyleCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            End If
            If Me.FNHSysStyleIdTo.Text <> "" Then
                _Cmd &= vbCrLf & " and C.FTStyleCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Text) & "'"
            End If
            If Me.FTOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & " and A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " and A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If
            If Me.FNHSysPOID.Text <> "" Then
                _Cmd &= vbCrLf & " and O.FTPORef >='" & HI.UL.ULF.rpQuoted(Me.FNHSysPOID.Text) & "'"
            End If
            If Me.FNHSysPOIDTo.Text <> "" Then
                _Cmd &= vbCrLf & " and O.FTPORef <='" & HI.UL.ULF.rpQuoted(Me.FNHSysPOIDTo.Text) & "'"
            End If


            _Cmd &= vbCrLf & "GROUP BY C.FTStyleCode, B.FTUnitSectCode, A.FTOrderNo,O.FTPORef ,A.FNHSysStyleId , A.FNHSysUnitSectId ,A.FDQADate "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcDetail.DataSource = _oDt


        Catch ex As Exception

        End Try
    End Sub

    Private Sub wQAReportAQL_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvDetail)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If VerifyData() Then
                Me.LoadData()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Function VerifyData() As Boolean
        Try
            If Me.SFTDateTrans.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.SFTDateTrans_lbl.Text)
                Me.SFTDateTrans.Focus()
                Return False
            End If
            If Me.EFTDateTrans.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.EFTDateTrans_lbl.Text)
                Me.EFTDateTrans.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    

    Private Sub ogvDetail_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvDetail.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then

                Dim _ActualQty As Integer = View.GetRowCellDisplayText(e.RowHandle, View.Columns("FNQAActualQty"))
                Dim _AQLQty As Integer = View.GetRowCellDisplayText(e.RowHandle, View.Columns("FNAQLQty"))
                If _ActualQty < _AQLQty Then
                    '    e.Appearance.BackColor = Color.LightSeaGreen
                    '    e.Appearance.BackColor2 = Color.SeaGreen
                    'Else
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Try
            Call Preview()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Preview()
        Dim _spls As New HI.TL.SplashScreen("Loading... Report.Please Wait.", "Preview Report")
        Try
            Dim _Fm As String = ""

            _Fm = " {V_RptAQLQA.FDQADate}  >='" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "' and {V_RptAQLQA.FDQADate} <='" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "'"

            If Me.FNHSysUnitSectId.Text <> "" Then
                _Fm &= vbCrLf & " and {V_RptAQLQA.FTUnitSectCode} >='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "'"
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Fm &= vbCrLf & " and {V_RptAQLQA.FTUnitSectCode}  <='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "'"
            End If
            If Me.FNHSysStyleId.Text <> "" Then
                _Fm &= vbCrLf & " and {V_RptAQLQA.FTStyleCode}  >='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            End If
            If Me.FNHSysStyleIdTo.Text <> "" Then
                _Fm &= vbCrLf & " and {V_RptAQLQA.FTStyleCode}  <='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Text) & "'"
            End If
            If Me.FTOrderNo.Text <> "" Then
                _Fm &= vbCrLf & " and {V_RptAQLQA.FTOrderNo} >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _Fm &= vbCrLf & " and {V_RptAQLQA.FTOrderNo} <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If
            If Me.FNHSysPOID.Text <> "" Then
                _Fm &= vbCrLf & " and {V_RptAQLQA.FTPORef} >='" & HI.UL.ULF.rpQuoted(Me.FNHSysPOID.Text) & "'"
            End If
            If Me.FNHSysPOIDTo.Text <> "" Then
                _Fm &= vbCrLf & " and {V_RptAQLQA.FTPORef} <='" & HI.UL.ULF.rpQuoted(Me.FNHSysPOIDTo.Text) & "'"
            End If



            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                .Formular = _Fm
                .AddParameter("FDSDate", HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text))
                .AddParameter("FDEDate", HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text))
                .ReportName = "ReportQAAql.rpt"
                _spls.Close()
                .Preview()
            End With

        Catch ex As Exception
            _spls.Close()
        End Try
    End Sub
End Class