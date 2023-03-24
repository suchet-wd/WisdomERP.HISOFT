Public Class wInComeDeductSetting

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.Trim, Me)
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub FNFinType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNFinType.SelectedIndexChanged
        Try
            If FNFinType.SelectedIndex < 0 Then Exit Sub
            Dim _Str As String = ""
            _Str = " SELECT  B.FTFinCode"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Str &= vbCrLf & ",FTFinDescTH  AS FTFinDesc"
            Else
                _Str &= vbCrLf & ",FTFinDescEN AS FTFinDesc"
            End If

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Str &= vbCrLf & ",ISNULL(FNFinSysType.FTNameTH,'') AS FTType"
                _Str &= vbCrLf & ",ISNULL(FNFinPayType.FTNameTH,'0') AS FTPayType"
                _Str &= vbCrLf & ",ISNULL(FNCalType.FTNameTH,'') AS FTCalType"


            Else
                _Str &= vbCrLf & ",ISNULL(FNFinSysType.FTNameEN,'') AS FTType"
                _Str &= vbCrLf & ",ISNULL(FNFinPayType.FTNameEN,'') AS FTPayType"
                _Str &= vbCrLf & ",ISNULL(FNCalType.FTNameEN,'') AS FTCalType"
            End If

            _Str &= vbCrLf & ",Convert(int,ISNULL(FTType,'0')) AS FTTypeInd"
            _Str &= vbCrLf & ",Convert(int,ISNULL(FTCalType,'0')) AS FTCalTypeInd"
            _Str &= vbCrLf & ",Convert(int,ISNULL(FTPayType,'0')) AS FTPayTypeInd"

            _Str &= vbCrLf & ",ISNULL(FTStaTax,'0') AS FTStaTax,ISNULL(FTStaSocial,'0') AS FTStaSocial,ISNULL(FTStaCalOT,'0') AS FTStaCalOT"
            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTStaLate,'')='1' THEN '1' ELSE '0' END AS FTStaLate"
            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTStaAbsent,'')='1' THEN '1' ELSE '0' END AS FTStaAbsent"
            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTStaLeave,'')='1' THEN '1' ELSE '0' END AS FTStaLeave"
            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTStaVacation,'')='1' THEN '1' ELSE '0' END AS FTStaVacation"
            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTStaRetire,'')='1' THEN '1' ELSE '0' END AS FTStaRetire"
            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTStaHoliday,'')='1' THEN '1' ELSE '0' END AS FTStaHoliday"
            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTStaActive,'')='1' THEN '1' ELSE '0' END AS FTStaActive"
            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTType,'')='1' THEN 0 ELSE 1 END AS FNEditable"
            _Str &= vbCrLf & ",Cast(ISNULL(FNOTTimeM,0) AS numeric(16,0)) AS FNOTTimeM"
            _Str &= vbCrLf & ",ISNULL(FTOTTime,'') AS FTOTTime"
            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTStaCheckLate,'')='1' THEN '1' ELSE '0' END AS FTStaCheckLate"
            _Str &= vbCrLf & ",ISNULL(FTLateMin,0) AS FTLateMin"
            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTStaCheckLeave,'')='1' THEN '1' ELSE '0' END AS FTStaCheckLeave"
            _Str &= vbCrLf & ",ISNULL(FTLeaveMin,0) AS FTLeaveMin"
            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTStaCheckWorkTime,'')='1' THEN '1' ELSE '0' END AS FTStaCheckWorkTime "
            _Str &= vbCrLf & ",ISNULL(FTCheckWorkTimeMin,0) AS FTCheckWorkTimeMin "
            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTStaMaternityleaveNotpay,'')='1' THEN '1' ELSE '0' END AS FTStaMaternityleaveNotpay"

            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTStaLatePerDay,'')='1' THEN '1' ELSE '0' END AS FTStaLatePerDay"
            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTStaAbsentPerDay,'')='1' THEN '1' ELSE '0' END AS FTStaAbsentPerDay"
            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTStaLeavePerDay,'')='1' THEN '1' ELSE '0' END AS FTStaLeavePerDay"
            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTStaVacationPerDay,'')='1' THEN '1' ELSE '0' END AS FTStaVacationPerDay"
            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTStaMaternityleaveNotpayPerDay,'')='1' THEN '1' ELSE '0' END AS FTStaMaternityleaveNotpayPerDay"

            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTStaLateM,'')='1' THEN '1' ELSE '0' END AS FTStaLateM "
            _Str &= vbCrLf & ",CASE WHEN ISNULL(FTStaLateA,'')='1' THEN '1' ELSE '0' END AS FTStaLateA "

            _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMFinanceSet  AS A WITH(NOLOCK)"
            _Str &= vbCrLf & " RIGHT JOIN (SELECT FTFinCode,FTFinDescTH,FTFinDescEN,FNFinSeqNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMFinance WITH(NOLOCK) WHERE FTFinType='" & HI.TL.CboList.GetListValue(FNFinType.Properties.Tag, FNFinType.SelectedIndex) & "' AND FTStaActive='1') AS B"
            _Str &= vbCrLf & " ON A.FTFinCode=B.FTFinCode"
            _Str &= vbCrLf & " LEFT JOIN (SELECT Convert(varchar(10),FNListIndex) AS FNListIndex,  FTNameTH, FTNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK) WHERE FTListName='FNFinSysType') AS FNFinSysType "
            _Str &= vbCrLf & " ON ISNULL(A.FTType,'0') =FNFinSysType.FNListIndex "
            _Str &= vbCrLf & " LEFT JOIN (SELECT Convert(varchar(10),FNListIndex) AS FNListIndex, FTNameTH, FTNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK) WHERE FTListName='FNFinPayType') AS FNFinPayType "
            _Str &= vbCrLf & " ON ISNULL(A.FTPayType,'0') =FNFinPayType.FNListIndex "
            _Str &= vbCrLf & " LEFT JOIN (SELECT Convert(varchar(10),FNListIndex) AS FNListIndex, FTNameTH, FTNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK) WHERE FTListName='FNCalType') AS FNCalType "
            _Str &= vbCrLf & " ON ISNULL(A.FTCalType,'0') =FNCalType.FNListIndex "

            _Str &= vbCrLf & " WHERE (ISNULL(A.FNHSysCmpId,0) = 0 OR ISNULL(A.FNHSysCmpId,0) = " & HI.ST.SysInfo.CmpID & " ) "
            _Str &= vbCrLf & " ORDER BY FNFinSeqNo,A.FTFinCode"

            Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_HR)
            Me.ogc.DataSource = _dt

        Catch ex As Exception
            Me.ogc.DataSource = Nothing
        End Try

        With ogv
            .Columns("FTStaCalOT").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTStaLate").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTStaAbsent").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTStaLeave").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTStaVacation").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTStaRetire").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTStaHoliday").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FNOTTimeM").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTOTTime").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTLateMin").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTStaCheckLate").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTLeaveMin").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTStaCheckLeave").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTStaCheckWorkTime").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTCheckWorkTimeMin").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTStaMaternityleaveNotpay").Visible = (FNFinType.SelectedIndex = 0)

            .Columns("FTStaLatePerDay").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTStaAbsentPerDay").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTStaLeavePerDay").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTStaVacationPerDay").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTStaMaternityleaveNotpayPerDay").Visible = (FNFinType.SelectedIndex = 0)

            .Columns("FTStaLateM").Visible = (FNFinType.SelectedIndex = 0)
            .Columns("FTStaLateA").Visible = (FNFinType.SelectedIndex = 0)

        End With

    End Sub

    Private Sub ocmsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click

        CType(ogc.DataSource, DataTable).AcceptChanges()
        Dim _dt As DataTable = CType(ogc.DataSource, DataTable)
        Dim _StrSql As String = ""

        With ogv
            .FocusedRowHandle = 0
            .FocusedColumn = .Columns.ColumnByName("FTStaSocial")
        End With

        For Each R As DataRow In _dt.Rows

            _StrSql = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMFinanceSet SET "
            _StrSql &= vbCrLf & " FTType='" & R!FTTypeInd.ToString & "'"
            _StrSql &= vbCrLf & ",FTCalType='" & R!FTCalTypeInd.ToString & "'"
            _StrSql &= vbCrLf & ",FTPayType='" & R!FTPayTypeInd.ToString & "'"
            _StrSql &= vbCrLf & ",FTStaActive='" & R!FTStaActive.ToString & "'"
            _StrSql &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _StrSql &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
            _StrSql &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
            _StrSql &= vbCrLf & ",FTStaTax='" & R!FTStaTax.ToString & "'"
            _StrSql &= vbCrLf & ",FTStaSocial='" & R!FTStaSocial.ToString & "'"
            _StrSql &= vbCrLf & ",FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ""
            'รายการได้
            If FNFinType.SelectedIndex = 0 Then

                _StrSql &= vbCrLf & ",FTStaCalOT='" & R!FTStaCalOT.ToString & "'"
                _StrSql &= vbCrLf & ",FTStaLate='" & R!FTStaLate.ToString & "'"
                _StrSql &= vbCrLf & ",FTStaAbsent='" & R!FTStaAbsent.ToString & "'"
                _StrSql &= vbCrLf & ",FTStaLeave='" & R!FTStaLeave.ToString & "'"
                _StrSql &= vbCrLf & ",FTStaVacation='" & R!FTStaVacation.ToString & "'"
                _StrSql &= vbCrLf & ",FTStaRetire='" & R!FTStaRetire.ToString & "'"
                _StrSql &= vbCrLf & ",FTStaHoliday='" & R!FTStaHoliday.ToString & "'"
                _StrSql &= vbCrLf & ",FNOTTimeM=" & Val(R!FNOTTimeM.ToString)
                _StrSql &= vbCrLf & ",FTOTTime='" & R!FTOTTime.ToString & "'"
                _StrSql &= vbCrLf & ",FTLateMin=" & Val(R!FTLateMin.ToString)
                _StrSql &= vbCrLf & ",FTStaCheckLate='" & R!FTStaCheckLate.ToString & "'"
                _StrSql &= vbCrLf & ",FTLeaveMin=" & Val(R!FTLeaveMin.ToString)
                _StrSql &= vbCrLf & ",FTStaCheckLeave='" & R!FTStaCheckLeave.ToString & "'"
                _StrSql &= vbCrLf & ",FTStaCheckWorkTime='" & R!FTStaCheckWorkTime.ToString & "'"
                _StrSql &= vbCrLf & ",FTCheckWorkTimeMin=" & Val(R!FTCheckWorkTimeMin.ToString)
                _StrSql &= vbCrLf & ",FTStaMaternityleaveNotpay='" & R!FTStaMaternityleaveNotpay.ToString & "'"

                _StrSql &= vbCrLf & ",FTStaLatePerDay='" & R!FTStaLatePerDay.ToString & "'"
                _StrSql &= vbCrLf & ",FTStaAbsentPerDay='" & R!FTStaAbsentPerDay.ToString & "'"
                _StrSql &= vbCrLf & ",FTStaLeavePerDay='" & R!FTStaLeavePerDay.ToString & "'"
                _StrSql &= vbCrLf & ",FTStaVacationPerDay='" & R!FTStaVacationPerDay.ToString & "'"
                _StrSql &= vbCrLf & ",FTStaMaternityleaveNotpayPerDay='" & R!FTStaMaternityleaveNotpayPerDay.ToString & "'"

                _StrSql &= vbCrLf & ",FTStaLateM='" & R!FTStaLateM.ToString & "'"
                _StrSql &= vbCrLf & ",FTStaLateA='" & R!FTStaLateA.ToString & "'"

            End If

            _StrSql &= vbCrLf & " WHERE  FTFinCode=N'" & HI.UL.ULF.rpQuoted(R!FTFinCode.ToString) & "' "
            _StrSql &= vbCrLf & " AND   (ISNULL(FNHSysCmpId,0) = 0 OR ISNULL(FNHSysCmpId,0) = " & HI.ST.SysInfo.CmpID & " )"


            If HI.Conn.SQLConn.ExecuteNonQuery(_StrSql, Conn.DB.DataBaseName.DB_HR) = False Then
                _StrSql = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMFinanceSet (FTFinCode,FNHSysCmpId) "
                _StrSql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(R!FTFinCode.ToString) & "'," & HI.ST.SysInfo.CmpID & ""
                _StrSql &= vbCrLf & "  UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMFinanceSet SET "
                _StrSql &= vbCrLf & " FTType='" & R!FTTypeInd.ToString & "'"
                _StrSql &= vbCrLf & ",FTCalType='" & R!FTCalTypeInd.ToString & "'"
                _StrSql &= vbCrLf & ",FTPayType='" & R!FTPayTypeInd.ToString & "'"
                _StrSql &= vbCrLf & ",FTStaActive='" & R!FTStaActive.ToString & "'"
                _StrSql &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _StrSql &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                _StrSql &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                _StrSql &= vbCrLf & ",FTStaTax='" & R!FTStaTax.ToString & "'"
                _StrSql &= vbCrLf & ",FTStaSocial='" & R!FTStaSocial.ToString & "'"
                _StrSql &= vbCrLf & ",FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ""
                'รายการได้
                If FNFinType.SelectedIndex = 0 Then

                    _StrSql &= vbCrLf & ",FTStaCalOT='" & R!FTStaCalOT.ToString & "'"
                    _StrSql &= vbCrLf & ",FTStaLate='" & R!FTStaLate.ToString & "'"
                    _StrSql &= vbCrLf & ",FTStaAbsent='" & R!FTStaAbsent.ToString & "'"
                    _StrSql &= vbCrLf & ",FTStaLeave='" & R!FTStaLeave.ToString & "'"
                    _StrSql &= vbCrLf & ",FTStaVacation='" & R!FTStaVacation.ToString & "'"
                    _StrSql &= vbCrLf & ",FTStaRetire='" & R!FTStaRetire.ToString & "'"
                    _StrSql &= vbCrLf & ",FTStaHoliday='" & R!FTStaHoliday.ToString & "'"
                    _StrSql &= vbCrLf & ",FNOTTimeM=" & Val(R!FNOTTimeM.ToString)
                    _StrSql &= vbCrLf & ",FTOTTime='" & R!FTOTTime.ToString & "'"
                    _StrSql &= vbCrLf & ",FTLateMin=" & Val(R!FTLateMin.ToString)
                    _StrSql &= vbCrLf & ",FTStaCheckLate='" & R!FTStaCheckLate.ToString & "'"
                    _StrSql &= vbCrLf & ",FTLeaveMin=" & Val(R!FTLeaveMin.ToString)
                    _StrSql &= vbCrLf & ",FTStaCheckLeave='" & R!FTStaCheckLeave.ToString & "'"
                    _StrSql &= vbCrLf & ",FTStaCheckWorkTime='" & R!FTStaCheckWorkTime.ToString & "'"
                    _StrSql &= vbCrLf & ",FTCheckWorkTimeMin=" & Val(R!FTCheckWorkTimeMin.ToString)
                    _StrSql &= vbCrLf & ",FTStaMaternityleaveNotpay='" & R!FTStaMaternityleaveNotpay.ToString & "'"

                    _StrSql &= vbCrLf & ",FTStaLatePerDay='" & R!FTStaLatePerDay.ToString & "'"
                    _StrSql &= vbCrLf & ",FTStaAbsentPerDay='" & R!FTStaAbsentPerDay.ToString & "'"
                    _StrSql &= vbCrLf & ",FTStaLeavePerDay='" & R!FTStaLeavePerDay.ToString & "'"
                    _StrSql &= vbCrLf & ",FTStaVacationPerDay='" & R!FTStaVacationPerDay.ToString & "'"
                    _StrSql &= vbCrLf & ",FTStaMaternityleaveNotpayPerDay='" & R!FTStaMaternityleaveNotpayPerDay.ToString & "'"

                    _StrSql &= vbCrLf & ",FTStaLateM='" & R!FTStaLateM.ToString & "'"
                    _StrSql &= vbCrLf & ",FTStaLateA='" & R!FTStaLateA.ToString & "'"


                End If

                _StrSql &= vbCrLf & " WHERE  FTFinCode=N'" & HI.UL.ULF.rpQuoted(R!FTFinCode.ToString) & "' "
                _StrSql &= vbCrLf & " AND   (ISNULL(FNHSysCmpId,0) = 0 OR ISNULL(FNHSysCmpId,0) = " & HI.ST.SysInfo.CmpID & " )"

                HI.Conn.SQLConn.ExecuteNonQuery(_StrSql, Conn.DB.DataBaseName.DB_HR)

            End If

        Next

    End Sub

    Private Sub wFinMasterSet_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        RepFTType.Items.AddRange(HI.TL.CboList.SetList("FNFinSysType"))
        RepFTCalType.Items.AddRange(HI.TL.CboList.SetList("FNCalType"))
        RepFTPayType.Items.AddRange(HI.TL.CboList.SetList("FNFinPayType"))

        Call FNFinType_SelectedIndexChanged(FNFinType, New System.EventArgs)

    End Sub

    Private Sub RepFTCalType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles RepFTCalType.SelectedIndexChanged
        With ogv
            .SetRowCellValue(.FocusedRowHandle, "FTCalTypeInd", TryCast(sender, DevExpress.XtraEditors.ComboBoxEdit).SelectedIndex)
        End With
    End Sub

    Private Sub RepFTPayType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles RepFTPayType.SelectedIndexChanged
        With ogv
            .SetRowCellValue(.FocusedRowHandle, "FTPayTypeInd", TryCast(sender, DevExpress.XtraEditors.ComboBoxEdit).SelectedIndex)
        End With
    End Sub

    Private Sub RepFTType_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFTType.EditValueChanging
        'Select Case True
        '    Case (HI.TL.CboList.GetIndexByText("FNFinSysType", e.NewValue.ToString) = 0) Or (HI.TL.CboList.GetIndexByText("FNFinSysType", e.OldValue.ToString) = 0)
        e.Cancel = True
        ' End Select
    End Sub

    Private Sub RepFTType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles RepFTType.SelectedIndexChanged
        With ogv
            .SetRowCellValue(.FocusedRowHandle, "FTTypeInd", TryCast(sender, DevExpress.XtraEditors.ComboBoxEdit).SelectedIndex + 1)
        End With
    End Sub

    Private Sub ocmrefresh_Click(sender As System.Object, e As System.EventArgs) Handles ocmrefresh.Click
        Call FNFinType_SelectedIndexChanged(FNFinType, New System.EventArgs)
    End Sub

    Private Sub ogc_Click(sender As Object, e As EventArgs) Handles ogc.Click

    End Sub
End Class