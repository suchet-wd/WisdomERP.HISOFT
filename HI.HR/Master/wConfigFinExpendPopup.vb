Public Class wConfigFinExpendPopup

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub WConfigFinExpendPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'RemoveHandler btnOrganize.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
            'RemoveHandler btnOrganize.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicButtone_ButtonClick
            'RemoveHandler btnOrganize.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly

            If FNHSysFinExpendID <> "" Then
                btnAdd.Visible = False
                btnSave.Visible = True
                btnDelete.Visible = True
            Else
                btnAdd.Visible = True
                btnSave.Visible = False
                btnDelete.Visible = True
                ClearScreen()
            End If


        Catch ex As Exception
        End Try
    End Sub

    Private _FNHSysFinExpendID_Popup As String
    Public Property FNHSysFinExpendID As String
        Get
            Return _FNHSysFinExpendID_Popup
        End Get
        Set(value As String)
            _FNHSysFinExpendID_Popup = value
        End Set
    End Property

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        FNHSysFinExpendID = ""
        Me.Close()
    End Sub

    Private Sub ClearScreen()

        FNHSysFinExpendID = ""
        FNHSysEmpID.Properties.Tag = ""
        FNHSysEmpID.Text = ""
        FNHSysEmpTypeId.Text = ""
        FTEmpName.Text = ""
        FTFinCode.Properties.Tag = ""
        FTFinCode.Text = ""
        FTFinDesc.Text = ""
        FTPayYearBegin.Properties.Tag = ""
        FTPayYearBegin.Text = ""
        FTPayTermBegin.Properties.Tag = ""
        FTPayTermBegin.Text = ""
        FTPayYearEnd.Properties.Tag = ""
        FTPayYearEnd.Text = ""
        FTPayTermEnd.Properties.Tag = ""
        FTPayTermEnd.Text = ""
        FCFinAmt.Text = "0.00"
        FCFinAmtTotal.Text = "0.00"
        FCFinAmtTotalSysExpend.Text = "0.00"
        FTStaCompletedPayment.Checked = False
    End Sub


    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If FNHSysEmpID.Text <> "" Then
            If FTPayYearBegin.Text <> "" Then
                If FTPayTermBegin.Text <> "" Then
                    If FTPayYearEnd.Text <> "" Then
                        If FTPayTermEnd.Text <> "" Then
                            'Check

                            Dim _Begin As Integer
                            Dim _End As Integer

                            _Begin = Val(FTPayYearBegin.Text + FTPayTermBegin.Text)
                            _End = Val(FTPayYearEnd.Text + FTPayTermEnd.Text)
                            If (_Begin <= _End) Then


                                'check date overlap
                                If ValidateDateOverlap() Then

                                    If FTFinCode.Text <> "" Then
                                        If FCFinAmt.Text <> "" Then

                                            If Me.AddData() Then
                                                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                                'btnSave.Visible = False
                                            Else
                                                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                            End If
                                        Else
                                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lbl_FCFinAmt.Text)
                                        End If
                                    Else
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lbl_FTFinCode.Text)
                                    End If

                                Else
                                    HI.MG.ShowMsg.mProcessError(201907310000, "", Me.Text)
                                End If
                            Else
                                    HI.MG.ShowMsg.mProcessError(201907240000, "", Me.Text)
                            End If
                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lbl_FTPayTermEnd.Text)
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lbl_FTPayYearEnd.Text)
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lbl_FTPayTermBegin.Text)
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lbl_FTPayYearBegin.Text)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lbl_FNHSysEmpID.Text)
        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If FNHSysEmpID.Text <> "" Then
            If FTPayYearBegin.Text <> "" Then
                If FTPayTermBegin.Text <> "" Then
                    If FTPayYearEnd.Text <> "" Then
                        If FTPayTermEnd.Text <> "" Then
                            'Check

                            Dim _Begin As Integer
                            Dim _End As Integer

                            _Begin = Val(FTPayYearBegin.Text + FTPayTermBegin.Text)
                            _End = Val(FTPayYearEnd.Text + FTPayTermEnd.Text)
                            If (_Begin <= _End) Then

                                'check date overlap
                                If ValidateDateOverlap() Then
                                    If FTFinCode.Text <> "" Then
                                        If FCFinAmt.Text <> "" Then
                                            If Me.SaveData() Then
                                                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                                'btnSave.Visible = False
                                            Else
                                                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                            End If
                                        Else
                                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lbl_FCFinAmt.Text)
                                        End If
                                    Else
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lbl_FTFinCode.Text)
                                    End If
                                Else
                                    HI.MG.ShowMsg.mProcessError(201907310000, "", Me.Text)
                                End If
                            Else
                                    HI.MG.ShowMsg.mProcessError(201907240000, "", Me.Text)
                            End If
                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lbl_FTPayTermEnd.Text)
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lbl_FTPayYearEnd.Text)
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lbl_FTPayTermBegin.Text)
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lbl_FTPayYearBegin.Text)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lbl_FNHSysEmpID.Text)
        End If
    End Sub

    Private Function ValidateDateOverlap() As Boolean
        Dim _Qry As String
        Dim _Dt As DataTable
        Dim _Begin As Integer
        Dim _End As Integer

        Dim _OldPayBegin As Integer
        Dim _OldPayEnd As Integer

        _Begin = Val(FTPayYearBegin.Text + FTPayTermBegin.Text)
        _End = Val(FTPayYearEnd.Text + FTPayTermEnd.Text)

        _Qry = "SELECT FNHSysFinExpendID, FNHSysEmpID
                    , FTFinCode
                    , FTPayYearBegin, FTPayTermBegin
                    , FTPayYearEnd, FTPayTermEnd
                    , FCFinAmt
                    , FCFinAmtTotal, FTStaCompletedPayment 
                   	, [PayBegin],[PayEnd]
FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.V_Finance_Expend 
WHERE FNHSysEmpID =" & Val(FNHSysEmpID.Properties.Tag.ToString) & " AND FTFinCode = '" & FTFinCode.Properties.Tag.ToString & "'"
        If FNHSysFinExpendID <> "" Then
            _Qry += " AND FNHSysFinExpendID <> '" & FNHSysFinExpendID & "'"
        End If
        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        If _Dt.Rows.Count > 0 Then
            For Each R As DataRow In _Dt.Rows
                _OldPayBegin = R!PayBegin.ToString
                _OldPayEnd = R!PayEnd.ToString

                If (_OldPayBegin > _Begin And _OldPayBegin < _End) Or (_OldPayEnd > _Begin And _OldPayEnd < _End) Then
                    Return False
                End If
            Next
            Return True
        Else
            Return True
        End If

    End Function


    Private Function AddData() As Boolean

        Dim _Qry As String

        Dim _ChkActive As Integer
        _ChkActive = 0
        If FTStaCompletedPayment.Checked = True Then
            _ChkActive = 1
        End If


        Dim nFNHSysFinExpendID As Integer
        nFNHSysFinExpendID = Val(HI.TL.RunID.GetRunNoID("THRMFinance_Expend", "FNHSysFinExpendID", Conn.DB.DataBaseName.DB_HR).ToString())

        Try

            _Qry = "INSERT INTO  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMFinance_Expend (FTInsUser, FDInsDate, FTInsTime
                    , FNHSysFinExpendID, FNHSysEmpID
                    , FTFinCode
                    , FTPayYearBegin, FTPayTermBegin
                    , FTPayYearEnd, FTPayTermEnd
                    , FCFinAmt
                    , FCFinAmtTotal, FTStaCompletedPayment)
          VALUES(" & "N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & "," & nFNHSysFinExpendID & "," & Val(FNHSysEmpID.Properties.Tag.ToString)
            _Qry &= vbCrLf & ",'" & FTFinCode.Properties.Tag.ToString & "'"
            _Qry &= vbCrLf & "," & FTPayYearBegin.Properties.Tag.ToString & ",'" & FTPayTermBegin.Text & "'"
            _Qry &= vbCrLf & "," & FTPayYearEnd.Properties.Tag.ToString & ",'" & FTPayTermEnd.Text & "'"
            _Qry &= vbCrLf & "," & FCFinAmt.Value
            _Qry &= vbCrLf & "," & FCFinAmtTotal.Value
            _Qry &= vbCrLf & "," & _ChkActive &  ")"
          

            If (HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception

            Return False

        End Try
    End Function


    Private Function SaveData() As Boolean

        Dim _Qry As String
        Dim _ChkActive As Integer
        _ChkActive = 0
        If FTStaCompletedPayment.Checked = True Then
            _ChkActive = 1
        End If

        Try

            _Qry = " UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMFinance_Expend SET  
                            FTUpdUser = " & "N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'" &
                        ", FDUpdDate  = " & HI.UL.ULDate.FormatDateDB &
                        ", FTUpdTime  = " & HI.UL.ULDate.FormatTimeDB &
                        ", FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString) &
                        ", FTFinCode = '" & FTFinCode.Properties.Tag.ToString & "'" &
                        ", FTPayYearBegin = '" & FTPayYearBegin.Properties.Tag.ToString & "'" &
                        ", FTPayTermBegin = '" & FTPayTermBegin.Properties.Tag.ToString & "'" &
                        ", FTPayYearEnd = '" & FTPayYearEnd.Properties.Tag.ToString & "'" &
                        ", FTPayTermEnd = '" & FTPayTermEnd.Properties.Tag.ToString & "'" &
                        ", FCFinAmt = " & FCFinAmt.Value &
                         ", FCFinAmtTotal = " & FCFinAmtTotal.Value &
                           ", FTStaCompletedPayment = " & _ChkActive &
                        " WHERE FNHSysFinExpendID = " & FNHSysFinExpendID

            If (HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

            Return False

            End Try
    End Function


    Private Sub FNHSysEmpID_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysEmpID.EditValueChanged
        FNHSysEmpTypeId.Text = "'"
        FTPayYearBegin.Properties.Tag = ""
        FTPayYearBegin.Text = ""
        FTPayTermBegin.Properties.Tag = ""
        FTPayTermBegin.Text = ""
        FTPayYearEnd.Properties.Tag = ""
        FTPayYearEnd.Text = ""
        FTPayTermEnd.Properties.Tag = ""
        FTPayTermEnd.Text = ""
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Dim _N As Integer
            _N = 0
            'Check Already used

            Dim _Qry As String
            _Qry = "SELECT count(*) AS n
                  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".[dbo].[THRTPayRollFin]
                  WHERE FNHSysEmpID =" & Val(FNHSysEmpID.Properties.Tag.ToString) & "
                  AND FTFinCode='106' "
            _N = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")
            If _N > 0 Then
                If HI.MG.ShowMsg.mConfirmProcess(HI.MG.ShowMsg.ProcessType.mDelete, "Already used this condition. Please confrim for delete!") = True Then
                    DeleteData()
                Else

                End If
            Else
                If HI.MG.ShowMsg.mConfirmProcess(HI.MG.ShowMsg.ProcessType.mDelete, "Please confrim for delete!") = True Then
                    DeleteData()
                Else

                End If
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Function DeleteData() As Boolean
        Try
            Dim _Qry As String
            _Qry = "DELETE  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".[dbo].[THRMFinance_Expend]
                  WHERE FNHSysFinExpendID = " & FNHSysFinExpendID
            If (HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

        End Try
    End Function
End Class