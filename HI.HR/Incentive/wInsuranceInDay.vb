Public Class wInsuranceInDay

 

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
       

        

    End Sub


#Region "Property"

    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

#End Region

#Region "MAIN PROC"

    Private Sub ProcessSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click

        If Me.VerrifyData Then


            Dim _Spls As New HI.TL.SplashScreen("Saving Data...   Please Wait   ")

            If Me.SaveData(_Spls) Then
                _Spls.Close()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)


            Else
                _Spls.Close()
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If



        End If

    End Sub

    Private Sub ProcessDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then


            CType(ogc.DataSource, DataTable).AcceptChanges()
            If CType(ogc.DataSource, DataTable).Select("FNHSysUnitSectID <> 0").Length > 0 Then
                If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text) = True Then
                    Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")

                    If Me.DeleteData(_Spls) Then

                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)

                    Else
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    End If

                End If
            Else
                HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกพนักงาน", 1304030001, Me.Text)
            End If


        Else
            HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกพนักงาน", 1304030001, Me.Text)
        End If

    End Sub

    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click

        Me.ogc.DataSource = Nothing

        HI.TL.HandlerControl.ClearControl(Me)
        Me.FTDateRequest.Focus()

    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Procedure "

    Private Function SaveData(Spls As HI.TL.SplashScreen) As Boolean

        Dim _Qry As String
        CType(ogc.DataSource, DataTable).AcceptChanges()
        Dim _Dt As DataTable = CType(ogc.DataSource, DataTable)
        Dim _tmpdt As DataTable

        Dim _ToatlRecord As Integer = _Dt.Select("FNHSysUnitSectID <> 0 ").Length
        Dim _Rec As Integer = 0
        Dim totalSum As Integer = 0

        Try

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _Dt.Rows

                _Rec = _Rec + 1

                If Not (Spls Is Nothing) Then
                    Spls.UpdateInformation("Saving....  Data" & _Rec.ToString & " Of " & _ToatlRecord.ToString & "  (" & Format((_Rec * 100.0) / _ToatlRecord, "0.00") & " % ) ")
                End If

                _Qry = " Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMInsuranceInDay "
                _Qry &= vbCrLf & "  SET  FTUpdUser='" & HI.ST.UserInfo.UserName & "' "
                _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & "  "
                _Qry &= vbCrLf & " ,FTStateInsuranceInDay='" & R!FTStateInsuranceInDay.ToString & "' "
                _Qry &= vbCrLf & " ,FTStateInsuranceInH1='" & R!FTStateInsuranceInH1.ToString & "' "
                _Qry &= vbCrLf & " ,FTStateInsuranceInH2='" & R!FTStateInsuranceInH2.ToString & "' "
                _Qry &= vbCrLf & " ,FTStateInsuranceInH3='" & R!FTStateInsuranceInH3.ToString & "' "
                _Qry &= vbCrLf & " ,FTStateInsuranceInH4='" & R!FTStateInsuranceInH4.ToString & "' "
                _Qry &= vbCrLf & " ,FTStateInsuranceInH5='" & R!FTStateInsuranceInH5.ToString & "' "
                _Qry &= vbCrLf & " ,FTStateInsuranceInH6='" & R!FTStateInsuranceInH6.ToString & "' "
                _Qry &= vbCrLf & " ,FTStateInsuranceInH7='" & R!FTStateInsuranceInH7.ToString & "' "
                _Qry &= vbCrLf & " ,FTStateInsuranceInH8='" & R!FTStateInsuranceInH8.ToString & "' "
                _Qry &= vbCrLf & " ,FTStateInsuranceInH9='" & R!FTStateInsuranceInH9.ToString & "' "
                _Qry &= vbCrLf & " ,FTStateInsuranceInH10='" & R!FTStateInsuranceInH10.ToString & "' "
                _Qry &= vbCrLf & " ,FTStateInsuranceInH11='" & R!FTStateInsuranceInH11.ToString & "' "
                _Qry &= vbCrLf & " ,FTStateInsuranceInH12='" & R!FTStateInsuranceInH12.ToString & "' "
                _Qry &= vbCrLf & " ,FTStateInsuranceInH13='" & R!FTStateInsuranceInH13.ToString & "' "
                _Qry &= vbCrLf & " WHERE FNHSysUnitSectID = " & Val(R!FNHSysUnitSectID.ToString) & ""
                _Qry &= vbCrLf & " AND FDDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Qry = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMInsuranceInDay (  FTInsUser, FDInsDate, FTInsTime "
                    _Qry &= vbCrLf & "  , FNHSysUnitSectID, FDDate"
                    _Qry &= vbCrLf & "  , FTStateInsuranceInDay, FTStateInsuranceInH1, FTStateInsuranceInH2, FTStateInsuranceInH3,FTStateInsuranceInH4"
                    _Qry &= vbCrLf & "  , FTStateInsuranceInH5, FTStateInsuranceInH6, FTStateInsuranceInH7, FTStateInsuranceInH8,FTStateInsuranceInH9"
                    _Qry &= vbCrLf & "  , FTStateInsuranceInH10, FTStateInsuranceInH11, FTStateInsuranceInH12, FTStateInsuranceInH13"
                    _Qry &= vbCrLf & " )  "
                    _Qry &= vbCrLf & " SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & "  "
                    _Qry &= vbCrLf & " ,'" & Val(R!FNHSysUnitSectID.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "' "
                    _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInDay.ToString & "' "
                    _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH1.ToString & "' "
                    _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH2.ToString & "' "
                    _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH3.ToString & "' "
                    _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH4.ToString & "' "
                    _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH5.ToString & "' "
                    _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH6.ToString & "' "
                    _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH7.ToString & "' "
                    _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH8.ToString & "' "
                    _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH9.ToString & "' "
                    _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH10.ToString & "' "
                    _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH11.ToString & "' "
                    _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH12.ToString & "' "
                    _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH13.ToString & "' "


                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        Return False

                    End If
                End If
                   

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Rec = 0

            Return True

        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return False
        End Try

    End Function

    Private Function DeleteData(Spls As HI.TL.SplashScreen) As Boolean
        Try

            CType(ogc.DataSource, DataTable).AcceptChanges()
            Dim _Dt As DataTable = CType(ogc.DataSource, DataTable)

            Dim _ToatlRecord As Integer = _Dt.Select("FNHSysUnitSectID<>0").Length
            Dim _Rec As Integer = 0

            Dim _Qry As String = ""

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _Dt.Rows

                _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMInsuranceInDay "        
                _Qry &= vbCrLf & " WHERE FNHSysUnitSectID = " & Val(R!FNHSysUnitSectID.ToString) & ""
                _Qry &= vbCrLf & " AND FDDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"

                 If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Rec = 0
           
            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Function VerrifyData() As Boolean
        Dim _Pass As Boolean = False
        If HI.UL.ULDate.CheckDate(FTDateRequest.Text) <> "" Then
            If Not (ogc.DataSource Is Nothing) Then
                CType(ogc.DataSource, DataTable).AcceptChanges()
                If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then
                    If CType(ogc.DataSource, DataTable).Select("FNHSysUnitSectId<>0").Length > 0 Then


                        _Pass = True



                    Else
                        HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกสังกัด", 1379030001, Me.Text)
                        FTDateRequest.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกสังกัด", 1379030001, Me.Text)
                    FTDateRequest.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกสังกัด", 1379030001, Me.Text)
                FTDateRequest.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateRequest_lbl.Text)
            FTDateRequest.Focus()
        End If

        Return _Pass
    End Function

    Private Sub LoadDataInfo()
        Me.ogc.DataSource = Nothing

        Dim _Dt As DataTable
        Dim _Qry As String = ""

        _Qry = "     Select A.FNHSysUnitSectID"
        _Qry &= vbCrLf & "  ,A.FTUnitSectCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " ,A.FTUnitSectNameTH As  FTUnitSectName"
        Else
            _Qry &= vbCrLf & " ,A.FTUnitSectNameEN As  FTUnitSectName"
        End If

        _Qry &= vbCrLf & " ,ISNULL(B.FTStateInsuranceInDay,'0') AS FTStateInsuranceInDay"
        _Qry &= vbCrLf & " ,ISNULL(B.FTStateInsuranceInH1,'0') AS FTStateInsuranceInH1"
        _Qry &= vbCrLf & " ,ISNULL(B.FTStateInsuranceInH2,'0') AS FTStateInsuranceInH2"
        _Qry &= vbCrLf & " ,ISNULL(B.FTStateInsuranceInH3,'0') AS FTStateInsuranceInH3"
        _Qry &= vbCrLf & " ,ISNULL(B.FTStateInsuranceInH4,'0') AS FTStateInsuranceInH4"
        _Qry &= vbCrLf & " ,ISNULL(B.FTStateInsuranceInH5,'0') AS FTStateInsuranceInH5"
        _Qry &= vbCrLf & " ,ISNULL(B.FTStateInsuranceInH6,'0') AS FTStateInsuranceInH6"
        _Qry &= vbCrLf & " ,ISNULL(B.FTStateInsuranceInH7,'0') AS FTStateInsuranceInH7"
        _Qry &= vbCrLf & " ,ISNULL(B.FTStateInsuranceInH8,'0') AS FTStateInsuranceInH8"
        _Qry &= vbCrLf & " ,ISNULL(B.FTStateInsuranceInH9,'0') AS FTStateInsuranceInH9"
        _Qry &= vbCrLf & " ,ISNULL(B.FTStateInsuranceInH10,'0') AS FTStateInsuranceInH10"
        _Qry &= vbCrLf & " ,ISNULL(B.FTStateInsuranceInH11,'0') AS FTStateInsuranceInH11"
        _Qry &= vbCrLf & " ,ISNULL(B.FTStateInsuranceInH12,'0') AS FTStateInsuranceInH12"
        _Qry &= vbCrLf & " ,ISNULL(B.FTStateInsuranceInH13,'0') AS FTStateInsuranceInH13"
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "   LEFT OUTER JOIN (SELECT *"
        _Qry &= vbCrLf & " 	FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMInsuranceInDay WITH(NOLOCK)"
        _Qry &= vbCrLf & " 	WHERE FDDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"
        _Qry &= vbCrLf & " 	) AS B ON A.FNHSysUnitSectID = B.FNHSysUnitSectID"
        _Qry &= vbCrLf & "  WHERE A.FTStateActive ='1' AND A.FTStateSew ='1'   and A.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID)
        _Qry &= vbCrLf & "  ORDER BY A.FTUnitSectCode"


        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.ogc.DataSource = _Dt
    End Sub
#End Region

#Region "General"

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If HI.UL.ULDate.CheckDate(FTDateRequest.Text) <> "" Then
            Call LoadDataInfo()

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateRequest_lbl.Text)
            FTDateRequest.Focus()
        End If
    End Sub

    Private Sub FTDateRequest_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FTDateRequest.EditValueChanged
        Me.ogc.DataSource = Nothing

        If HI.UL.ULDate.CheckDate(FTDateRequest.Text) <> "" Then
            Call LoadDataInfo()
        End If

    End Sub


    Private Sub wOTRequest_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)
    End Sub

   
#End Region


End Class