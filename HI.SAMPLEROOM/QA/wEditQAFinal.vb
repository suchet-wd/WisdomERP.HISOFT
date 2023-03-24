Public Class wEditQAFinal


    Sub New()


        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'HI.TL.HandlerControl.AddHandlerObj(Me)

        Dim oSysLang As New ST.SysLanguage
        Dim _ModuleID As String = HI.ST.SysInfo.ModuleID
        Try
            Call oSysLang.LoadObjectLanguage(_ModuleID, Me.Name.ToString.Trim, Me)
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub Load_Grid(ByVal _UnitSectId As Integer, ByVal _QADate As String, ByVal _StyleId As Integer, ByVal _OrderNo As String)
        Try
            Dim _Cmd As String = ""
            'FTBarcodeCartonNo
            _Cmd = "Select distinct '0' AS FTSelect ,  FTBarcodeRef, FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate,Left(FNHourNo,2)+':'+Right(FNHourNo,2) AS FNHourNo , FNQAInQty, FNQAAqlQty, FNQAActualQty, Isnull(FNAndon,0) AS FNAndon"

            _Cmd &= vbCrLf & " ,isnull( (Select max(FNSeq)  as t  From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal_Detail WITH(NOLOCK)  "
            _Cmd &= vbCrLf & "  WHERE        (FNHSysUnitSectId =A.FNHSysUnitSectId)   AND (FDQADate = A.FDQADate)  AND (FNHSysStyleId = A.FNHSysStyleId) AND (FTOrderNo =A.FTOrderNo) and FNHourNo = REPLACE(A.FNHourNo,':','') "
            _Cmd &= vbCrLf & "  and FTStateReject = '1') , 0 ) AS FNDefectQty "
            _Cmd &= vbCrLf & "  ,isnull( (Select count(*) as t   "

            _Cmd &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTQAPreFinal_SubDetail AS D WITH(NOLOCK) LEFT OUTER JOIN "
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail AS Q WITH(NOLOCK) ON D.FNHSysQADetailId = Q.FNHSysQADetailId"
            _Cmd &= vbCrLf & " WHERE        (FNHSysUnitSectId =A.FNHSysUnitSectId) AND (FDQADate = A.FDQADate) AND (FNHSysStyleId = A.FNHSysStyleId) AND (FTOrderNo =A.FTOrderNo)"
            _Cmd &= vbCrLf & "  and FNHourNo = REPLACE(A.FNHourNo,':','') and Q.FTStateCtitical = '0') , 0 ) AS FNMinorQty "

            _Cmd &= vbCrLf & " ,isnull( (Select count(*) as t    "
            _Cmd &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTQAPreFinal_SubDetail AS D WITH(NOLOCK) LEFT OUTER JOIN "
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail AS Q WITH(NOLOCK) ON D.FNHSysQADetailId = Q.FNHSysQADetailId"
            _Cmd &= vbCrLf & " WHERE        (FNHSysUnitSectId =A.FNHSysUnitSectId) AND (FDQADate = A.FDQADate) AND (FNHSysStyleId = A.FNHSysStyleId) AND (FTOrderNo =A.FTOrderNo)"
            _Cmd &= vbCrLf & "  and FNHourNo = REPLACE(A.FNHourNo,':','') and Q.FTStateCtitical = '1') , 0 ) AS FNMajorQty "

            _Cmd &= vbCrLf & " ,isnull( (Select count(*) as t    "
            _Cmd &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTQAPreFinal_SubDetail AS D WITH(NOLOCK) LEFT OUTER JOIN "
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail AS Q WITH(NOLOCK) ON D.FNHSysQADetailId = Q.FNHSysQADetailId"
            _Cmd &= vbCrLf & " WHERE        (FNHSysUnitSectId =A.FNHSysUnitSectId) AND (FDQADate = A.FDQADate) AND (FNHSysStyleId = A.FNHSysStyleId) AND (FTOrderNo =A.FTOrderNo)"
            _Cmd &= vbCrLf & "  and FNHourNo = REPLACE(A.FNHourNo,':','') and Q.FTStateCtitical = '2') , 0 ) AS FNCtiticalQty "



            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal AS A WITH(NOLOCK)"
            _Cmd &= vbCrLf & "WHERE        (FNHSysUnitSectId =" & Integer.Parse(Val(_UnitSectId)) & ") "
            _Cmd &= vbCrLf & "And (FDQADate = '" & HI.UL.ULDate.ConvertEnDB(_QADate) & "')"
            _Cmd &= vbCrLf & " AND (FNHSysStyleId = " & Integer.Parse(Val(_StyleId)) & ")"
            _Cmd &= vbCrLf & " AND (FTOrderNo ='" & HI.UL.ULF.rpQuoted(_OrderNo) & "')"
            _Cmd &= vbCrLf & "ORDER BY FNHourNo"
            Me.ogcInline.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            _Cmd = "  SELECT   '0' AS FTSelect,FTBarcodeCartonNo , FTBarcodeRef,  A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate,  Left(A.FNHourNo,2)+':'+Right(A.FNHourNo,2) AS FNHourNo  , A.FNSeq, A.FTPointSubName, A.FNHSysQADetailId ,B.FTQADetailCode   "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", B.FTQADetailNameTH AS FTQADetailName , L.FTNameTH as FTStateCtitical "
            Else
                _Cmd &= vbCrLf & ", B.FTQADetailNameEN AS FTQADetailName , L.FTNameEN  as FTStateCtitical "
            End If
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal_SubDetail AS A LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail AS B ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            _Cmd &= vbCrLf & "   LEFT OUTER JOIN (  Select FTListName, FNListIndex, FTNameTH, FTNameEN From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData "
            _Cmd &= vbCrLf & "  where FTListName = 'FTStateCtitical'  ) AS L ON B.FTStateCtitical = L.FNListIndex  "
            _Cmd &= vbCrLf & "      WHERE (A.FNHSysUnitSectId = " & Integer.Parse(Val(_UnitSectId)) & ")"
            _Cmd &= vbCrLf & "AND (A.FDQADate = '" & HI.UL.ULDate.ConvertEnDB(_QADate) & "') "
            _Cmd &= vbCrLf & "AND (A.FNHSysStyleId = " & Integer.Parse(Val(_StyleId)) & ") "
            _Cmd &= vbCrLf & "AND (A.FTOrderNo = '" & HI.UL.ULF.rpQuoted(_OrderNo) & "')"


            Me.ogcDefect.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If (Me.VerrifyData) Then
                Me.Load_Grid(Me.FNHSysUnitSectId.Properties.Tag, Me.FDDate.Text, Me.FNHSysStyleId.Properties.Tag, Me.FTOrderNo.Text)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function VerrifyData() As Boolean
        Try
            If Me.FNHSysUnitSectId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysUnitSectId_lbl.Text)
                Me.FNHSysUnitSectId.Focus()
                Return False
            End If
            If Me.FDDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FDDate_lbl.Text)
                Me.FDDate.Focus()
                Return False
            End If
            If Me.FNHSysStyleId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysStyleId_lbl.Text)
                Me.FNHSysStyleId.Focus()
                Return False
            End If
            If Me.FTOrderNo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTOrderNo_lbl.Text)
                Me.FTOrderNo.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        Try
            If (Me.VerrifyData) Then
                Me.Load_Grid(Me.FNHSysUnitSectId.Properties.Tag, Me.FDDate.Text, Me.FNHSysStyleId.Properties.Tag, Me.FTOrderNo.Text)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception
        End Try
    End Sub

    Private Function SaveData() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable = CType(Me.ogcInline.DataSource, DataTable)
            Dim _oDt2 As DataTable = CType(Me.ogcDefect.DataSource, DataTable)


            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _oDt.Select("FTSelect = '1'")

                _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal "
                _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.ST.UserInfo.UserName & "'"
                _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                '_Cmd &= vbCrLf & ",FNAndon=" & CInt(R!FNAndon.ToString)
                _Cmd &= vbCrLf & ",FNMajorQty =" & CInt(R!FNMajorQty.ToString)
                _Cmd &= vbCrLf & ",FNMinorQty=" & CInt(R!FNMinorQty.ToString)
                _Cmd &= vbCrLf & " WHERE FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
                _Cmd &= vbCrLf & " AND FDQADate='" & HI.UL.ULDate.ConvertEnDB(Me.FDDate.Text) & "'"
                _Cmd &= vbCrLf & " AND FNHSysStyleId =" & CInt(Me.FNHSysStyleId.Properties.Tag)
                _Cmd &= vbCrLf & " AND FTOrderNo = '" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                _Cmd &= vbCrLf & " AND FNHourNo='" & Replace(R!FNHourNo.ToString, ":", "") & "'"
                '_Cmd &= vbCrLf & " AND FTBarcodeCartonNo='" & HI.UL.ULF.rpQuoted(R!FTBarCodeCartonNo.ToString) & "'"
                _Cmd &= vbCrLf & " AND FTBarcodeRef='" & HI.UL.ULF.rpQuoted(R!FTBarcodeRef.ToString) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
            Next


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If (VerrifyData()) Then
                If SaveData() Then
                    If (Me.VerrifyData) Then
                        Me.Load_Grid(Me.FNHSysUnitSectId.Properties.Tag, Me.FDDate.Text, Me.FNHSysStyleId.Properties.Tag, Me.FTOrderNo.Text)
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If (VerrifyData()) Then
                If DeleteData() Then
                    If (Me.VerrifyData) Then
                        Me.Load_Grid(Me.FNHSysUnitSectId.Properties.Tag, Me.FDDate.Text, Me.FNHSysStyleId.Properties.Tag, Me.FTOrderNo.Text)
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function DeleteData() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable = CType(Me.ogcInline.DataSource, DataTable)
            Dim _oDt2 As DataTable = CType(Me.ogcDefect.DataSource, DataTable)


            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            For Each R As DataRow In _oDt.Select("FTSelect = '1'")

                _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal "
                _Cmd &= vbCrLf & " WHERE FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
                _Cmd &= vbCrLf & " AND FDQADate='" & HI.UL.ULDate.ConvertEnDB(Me.FDDate.Text) & "'"
                _Cmd &= vbCrLf & " AND FNHSysStyleId =" & CInt(Me.FNHSysStyleId.Properties.Tag)
                _Cmd &= vbCrLf & " AND FTOrderNo = '" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                _Cmd &= vbCrLf & " AND FNHourNo='" & Replace(R!FNHourNo.ToString, ":", "") & "'"
                '_Cmd &= vbCrLf & " AND FTBarcodeCartonNo='" & HI.UL.ULF.rpQuoted(R!FTBarCodeCartonNo.ToString) & "'"
                _Cmd &= vbCrLf & " AND FTBarcodeRef='" & HI.UL.ULF.rpQuoted(R!FTBarcodeRef.ToString) & "'"


                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            Next

            For Each L As DataRow In _oDt2.Select("FTSelect = '1'")
                _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal_Detail"
                _Cmd &= vbCrLf & "WHERE FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
                _Cmd &= vbCrLf & "AND FDQADate='" & HI.UL.ULDate.ConvertEnDB(Me.FDDate.Text) & "'"
                _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
                _Cmd &= vbCrLf & "AND FTOrderNo = '" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                _Cmd &= vbCrLf & "AND FNHourNo='" & Replace(L!FNHourNo.ToString, ":", "") & "'"
                _Cmd &= vbCrLf & "AND FNSeq=" & CInt("0" & L!FNSeq.ToString)
                _Cmd &= vbCrLf & " AND FTBarcodeCartonNo='" & HI.UL.ULF.rpQuoted(L!FTBarCodeCartonNo.ToString) & "'"
                _Cmd &= vbCrLf & " AND FTBarcodeRef='" & HI.UL.ULF.rpQuoted(L!FTBarcodeRef.ToString) & "'"

                '_Cmd &= vbCrLf & "AND FTPointSubName='" & HI.UL.ULF.rpQuoted(L!FTPointSubName.ToString) & "'"


                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    'HI.Conn.SQLConn.Tran.Rollback()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    'Return False
                End If

                _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal_SubDetail"
                _Cmd &= vbCrLf & "WHERE FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
                _Cmd &= vbCrLf & "AND FDQADate='" & HI.UL.ULDate.ConvertEnDB(Me.FDDate.Text) & "'"
                _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
                _Cmd &= vbCrLf & "AND FTOrderNo = '" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                _Cmd &= vbCrLf & "AND FNHourNo='" & Replace(L!FNHourNo.ToString, ":", "") & "'"
                _Cmd &= vbCrLf & "AND FNSeq=" & CInt("0" & L!FNSeq.ToString)
                _Cmd &= vbCrLf & "AND FTPointSubName='" & HI.UL.ULF.rpQuoted(L!FTPointSubName.ToString) & "'"
                _Cmd &= vbCrLf & "AND FNHSysQADetailId=" & CInt("0" & L!FNHSysQADetailId.ToString)
                _Cmd &= vbCrLf & " AND FTBarcodeCartonNo='" & HI.UL.ULF.rpQuoted(L!FTBarCodeCartonNo.ToString) & "'"
                _Cmd &= vbCrLf & " AND FTBarcodeRef='" & HI.UL.ULF.rpQuoted(L!FTBarcodeRef.ToString) & "'"


                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    'HI.Conn.SQLConn.Tran.Rollback()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    'Return False
                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Sub RepositoryItemTotalDefect_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles RepositoryItemTotalDefect.MouseDown
        Try
            With New wPopupCalc
                .ShowDialog()
                If .StateEnter = True Then
                    Me.ogvDefect.SetFocusedValue(.CalcValue)
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RepositoryItemFNAndon_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles RepositoryItemFNAndon.MouseDown
        Try
            With New wPopupCalc
                .ShowDialog()
                If .StateEnter = True Then
                    Me.ogvInline.SetFocusedValue(.CalcValue)
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RepositoryItemFNMainReject_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles RepositoryItemFNMainReject.MouseDown
        Try
            With New wPopupCalc
                .ShowDialog()
                If .StateEnter = True Then
                    Me.ogvInline.SetFocusedValue(.CalcValue)
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RepositoryItemFNQCActualQty_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles RepositoryItemFNQCActualQty.MouseDown
        Try
            With New wPopupCalc
                .ShowDialog()
                If .StateEnter = True Then
                    Me.ogvInline.SetFocusedValue(.CalcValue)
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RepositoryItemFNSubReject_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles RepositoryItemFNSubReject.MouseDown
        Try
            With New wPopupCalc
                .ShowDialog()
                If .StateEnter = True Then
                    Me.ogvInline.SetFocusedValue(.CalcValue)
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub wEditQA_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            HI.TL.HandlerControl.AddHandlerGridColumnEdit(ogvDefect)
        Catch ex As Exception

        End Try
    End Sub
End Class
