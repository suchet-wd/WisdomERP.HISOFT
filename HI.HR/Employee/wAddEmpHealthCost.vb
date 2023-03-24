Public Class wAddEmpHealthCost 

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Property"

    Private _EmpSysID As Integer = 0
    Public Property EmpSysID As Integer
        Get
            Return _EmpSysID
        End Get
        Set(value As Integer)
            _EmpSysID = value
        End Set
    End Property

    Private _DateCheck As String = ""
    Public Property DateCheck As String
        Get
            Return _DateCheck
        End Get
        Set(value As String)
            _DateCheck = value
        End Set
    End Property

    Private _HealthSeq As Integer = 0
    Public Property HealthSeq As Integer
        Get
            Return _HealthSeq
        End Get
        Set(value As Integer)
            _HealthSeq = value
        End Set
    End Property

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(value As Boolean)
            _ProcComplete = value
        End Set
    End Property

#End Region

#Region "Procedure"

    Private Sub LoadDataEdit()

        Dim _StrWhere As String = ""
        Dim _value As String = ""
        Dim _Qry As String = ""

        _Qry = " SELECT        TOP 1     H.FTBillNo, H.FDTreatment, H.FCMedical, H.FCSocial, H.FCDisburse, H.FNPatients, H.FTNote, R.FTHospitalCode AS FNHSysHospitalId"
        _Qry &= vbCrLf & " FROM            THRTEmployeeMedicalExpenses AS H WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHospital AS R WITH (NOLOCK) ON H.FNHSysHospitalId = R.FNHSysHospitalId"
        _Qry &= vbCrLf & "  WHERE  (H.FNHSysEmpID =" & Val(Me.EmpSysID) & ")"
        _Qry &= vbCrLf & "  AND (H.FNSeqNo =" & Val(Me.HealthSeq) & ")"


        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        Dim _FieldName As String = ""
        For Each R As DataRow In _dt.Rows
            For Each Col As DataColumn In _dt.Columns
                _FieldName = Col.ColumnName.ToString

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case Obj.GetType.FullName.ToString.ToUpper
                        Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)

                                .Text = R.Item(Col).ToString

                            End With
                        Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Value = Val(R.Item(Col).ToString)
                            End With
                        Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                Try
                                    If "" & .Properties.Tag.ToString <> "" Then
                                        .SelectedIndex = HI.TL.CboList.GetIndexByValue("" & .Properties.Tag.ToString, R.Item(Col).ToString)
                                    Else
                                        .SelectedIndex = Val(R.Item(Col).ToString)
                                    End If

                                Catch ex As Exception
                                    .SelectedIndex = -1
                                End Try
                            End With
                        Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .EditValue = (Integer.Parse(Val(R.Item(Col).ToString))).ToString
                            End With
                        Case "DevExpress.XtraEditors.MemoEdit".ToUpper, "DevExpress.XtraEditors.TextEdit".ToUpper
                            Obj.Text = R.Item(Col).ToString
                        Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                Try
                                    .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString)
                                Catch ex As Exception
                                    .Image = Nothing
                                End Try
                            End With
                        Case "DevExpress.XtraEditors.DateEdit".ToUpper
                            Try
                                With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                    If .Properties.DisplayFormat.FormatString = "dd/MM/yyyy" Or .Properties.DisplayFormat.FormatString = "d" Then
                                        .DateTime = CDate(HI.UL.ULDate.ConvertEnDB(R.Item(Col).ToString))
                                    Else
                                        .Text = R.Item(Col).ToString
                                    End If
                                End With
                            Catch ex As Exception
                            End Try
                        Case Else
                            Obj.Text = R.Item(Col).ToString
                    End Select
                Next
            Next

            Exit For
        Next
        Me.ocmexit.Focus()
    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FDTreatment.Text <> "" Then
            If Me.FNHSysHospitalId.Text <> "" And Me.FNHSysHospitalId.Properties.Tag.ToString <> "" Then
                _Pass = True
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysHospitalId_lbl.Text)
                FNHSysHospitalId.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDTreatment_lbl.Text)
            FDTreatment.Focus()
        End If

        Return _Pass
    End Function

    Private Function SaveData() As Boolean
        Dim _Qry As String = ""
        Try

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try

                _Qry = "UPDATE   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeMedicalExpenses SET "
                _Qry &= vbCrLf & " FTBillNo=N'" & HI.UL.ULF.rpQuoted(FTBillNo.Text) & "',FDTreatment='" & HI.UL.ULDate.ConvertEnDB(FDTreatment.Text) & "'"
                _Qry &= vbCrLf & " ,FNHSysHospitalId=" & Val(FNHSysHospitalId.Properties.Tag.ToString) & ",FCMedical=" & FCMedical.Value & ",FCSocial=" & FCSocial.Value & ",FCDisburse=" & FCDisburse.Value & ""
                _Qry &= vbCrLf & " ,FNPatients=" & FNPatients.SelectedIndex & ",FTNote=N'" & HI.UL.ULF.rpQuoted(FTNote.Text) & "' "
                _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(Me.EmpSysID) & " "
                _Qry &= vbCrLf & "  AND FNSeqNo=" & Val(Me.HealthSeq) & "  "

                Dim tSeqNo As Integer

                If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    _Qry = "SELECT MAX(FNSeqNo) AS FNSeqNo FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeMedicalExpenses WHERE  FNHSysEmpID=" & Val(Me.EmpSysID) & " "

                    tSeqNo = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                    tSeqNo = Val(tSeqNo) + 1

                    _Qry = "INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeMedicalExpenses(FNHSysEmpID, FNSeqNo, FTBillNo, FDTreatment,  FNHSysHospitalId, FCMedical, FCSocial, FCDisburse, FNPatients, FTNote"
                    _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                    _Qry &= vbCrLf & " SELECT " & Val(Me.EmpSysID) & "," & tSeqNo & ""
                    _Qry &= vbCrLf & " ,N'" & HI.UL.ULF.rpQuoted(FTBillNo.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FDTreatment.Text) & "'"
                    _Qry &= vbCrLf & " ," & Val(FNHSysHospitalId.Properties.Tag.ToString) & "," & FCMedical.Value & "," & FCSocial.Value & "," & FCDisburse.Value & ""
                    _Qry &= vbCrLf & " ," & FNPatients.SelectedIndex & ",N'" & HI.UL.ULF.rpQuoted(FTNote.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

                _Qry = "UPDATE  THRTEmployeeMedicalExpenses SET FNSeqNo=FNNo"
                _Qry &= vbCrLf & " FROM  THRTEmployeeMedicalExpenses INNER JOIN "
                _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FDInsDate, FTInsTime) AS FNNo, FNSeqNo,FNHSysEmpID"
                _Qry &= vbCrLf & " FROM  THRTEmployeeMedicalExpenses"
                _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(Me.EmpSysID) & ""
                _Qry &= vbCrLf & ") T1 ON  THRTEmployeeMedicalExpenses.FNSeqNo=T1.FNSeqNo "
                _Qry &= vbCrLf & " AND  THRTEmployeeMedicalExpenses.FNHSysEmpID=T1.FNHSysEmpID "
                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

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

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "General"

    Private Sub wAddEmpHealthCost_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Me.HealthSeq > 0 Then
                Call LoadDataEdit()
            End If
            HI.ST.Lang.SP_SETxLanguage(Me)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.ProcComplete = False
        Me.Close()
    End Sub

    Private Sub ocmsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click
        If Me.VerifyData() Then
            If Me.SaveData Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Me.ProcComplete = True
                Me.Close()

            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

#End Region

End Class