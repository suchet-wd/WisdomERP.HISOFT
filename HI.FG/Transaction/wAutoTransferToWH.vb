Public Class wAutoTransferToWH

    Private _AutoTrans As String
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        
    End Sub
    Private _SysWH As Integer = 0
    Public Property WH() As Integer
        Get
            Return _SysWH
        End Get
        Set(value As Integer)
            _SysWH = value
        End Set
    End Property
    Private _Pa As String = ""
    Public Property Pa() As String
        Get
            Return _Pa
        End Get
        Set(value As String)
            _Pa = value
        End Set
    End Property

    Private _Trans As String = ""
    Public Property Trans() As String
        Get
            Return _Trans
        End Get
        Set(value As String)
            _Trans = value
        End Set
    End Property
    Private _Li As String = ""
    Public Property Li() As String
        Get
            Return _Li
        End Get
        Set(value As String)
            _Li = value
        End Set
    End Property
    Private _Pass As Boolean = False
    Public Property Pass As Boolean
        Get
            Return _Pass
        End Get
        Set(value As Boolean)
            _Pass = value
        End Set
    End Property
    Private _DocNo As String = ""
    Public Property DocNo() As String
        Get
            Return _DocNo
        End Get
        Set(value As String)
            _DocNo = value
        End Set
    End Property
    Private _Re As String = ""
    Public Property Re() As String
        Get
            Return _Re
        End Get
        Set(value As String)
            _Re = value
        End Set
    End Property
    Private _OrderNo As String = ""
    Public Property OrderNo As String
        Get
            Return _OrderNo
        End Get
        Set(value As String)
            _OrderNo = value
        End Set
    End Property

    Private _OrderNoTo As String = ""
    Public Property OrderNoTo As String
        Get
            Return _OrderNoTo
        End Get
        Set(value As String)
            _OrderNoTo = value
        End Set
    End Property
    Private _PO As String = ""
    Public Property PO As String
        Get
            Return _PO
        End Get
        Set(value As String)
            _PO = value
        End Set
    End Property
    Private _Car As String = ""
    Public Property Car As String
        Get
            Return _Car
        End Get
        Set(value As String)
            _Car = value
        End Set
    End Property
    Private _Quan As String = ""
    Public Property Quan As String
        Get
            Return _Quan
        End Get
        Set(value As String)
            _Quan = value
        End Set
    End Property
    Private _Bar As String = ""
    Public Property Bar As String
        Get
            Return _Bar
        End Get
        Set(value As String)
            _Bar = value
        End Set
    End Property
    Private _Size As String = ""
    Public Property Size As String
        Get
            Return _Size
        End Get
        Set(value As String)
            _Size = value
        End Set
    End Property

    Private _SysWHID As Integer = 0
    Public Property WHID() As Integer
        Get
            Return _SysWHID
        End Get
        Set(value As Integer)
            _SysWHID = value
        End Set
    End Property
    Private _WHIDTo As Integer
    Public Property WHTo As Integer
        Get
            Return _WHIDTo
        End Get
        Set(value As Integer)
            _WHIDTo = value
        End Set
    End Property
    Private _ogc As DataTable
    Public Property ogc As DataTable
        Get
            Return _ogc
        End Get
        Set(value As DataTable)
            _ogc = value
        End Set
    End Property

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.Close()
    End Sub

   

    Private Sub ocmauto_Click(sender As Object, e As EventArgs) Handles ocmauto.Click

        If Me.FNHSysWHIdFGTo.Text <> "" Then

            ' If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Auto Transfer  ใช่หรือไม่ ?", 1471720001) = True Then

            If Me.FNHSysWHIdFGTo.Text <> "" Then

                ' HI.MG.ShowMsg.mInfo("Transfer Auto Complete...  ", 1410020005, Me.Text, _DocNo, System.Windows.Forms.MessageBoxIcon.Information)
                Me.Close()
                'Else
                '  HI.MG.ShowMsg.mInfo("ระบบไม่สามารถทำการ Auto Transfer ได้ เนื่องจากพบข้อผิดพลาดบางประการกรูณาทำการติดต่อ System Admin !!!", 1471720002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

            'End If
        Else
            HI.MG.ShowMsg.mInfo("กรุณาเลือก คลังสำเร็จรูป ที่ต้องการจะโอน", 1471720003, Me.FNHSysWHIdFGTo.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        'HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysWHIdFGTo.Text)
        FNHSysWHIdFGTo.Focus()

        End If

    End Sub

    Private Sub wAuto_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Call LoadData1()

        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString

    End Sub

    Private Function Auto() As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Auto Transfer To WH.. Please Wait.... ")
        Try

            Dim _Qry As String = ""

            Dim _DocNo As String = ""
            Dim _tmpDocNo As String = ""
            Dim _CmpH As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
            '_tmpDocNo = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG), "TFGTransferFG", "", False, _CmpH & "A").ToString()
            _DocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG), "TFGTransferFG", "", False, _CmpH & "A").ToString()


            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            _Qry = "INSERT INTO   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.TFGTransferFG"
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime, FTTransferFGNo, FDDateTransferFG, FTTransferFGBy, FNHSysWHIdFG, FNHSysWHIdFGTo,  FNHSysCmpId "
            _Qry &= vbCrLf & "  )"
            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DocNo) & "' "
            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & " ," & Me.WH & " "
            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Me.FNHSysWHIdFGTo.Properties.Tag.ToString)) & " "
            '  _Qry &= vbCrLf & " ,N'" & HI.UL.ULF.rpQuoted(Me.Re) & "' "
            _Qry &= vbCrLf & " ," & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & " "
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                _Spls.Close()
                Return False
            End If






            For Each R As DataRow In ogc.Select("FTSelect='1'")

                _Qry = "insert into " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "..TFGTransferFG_Detail"
                _Qry &= vbCrLf & "(  FTInsUser, FDInsDate, FTInsTime"
                _Qry &= vbCrLf & ",FTTransferFGNo,FTBarCodeCarton,FNQuantity, FTPackNo , FNCartonNo)"
                _Qry &= vbCrLf & "SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DocNo) & "','" & HI.UL.ULF.rpQuoted(R!FTBarCodeCarton.ToString) & "'," & Val(R!FNQuantityBal.ToString) & ""
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FNCartonNo.ToString) & "' "
                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    Return False
                End If

            Next



            _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "..TFGTransferFG"
            _Qry &= vbCrLf & "SET FTStateApprove='1'"
            _Qry &= vbCrLf & ",FTApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & ",FDApproveDate=" & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & ",FTApproveTime=" & HI.UL.ULDate.FormatTimeDB & ""
            _Qry &= vbCrLf & "WHERE FTTransferFGNo='" & HI.UL.ULF.rpQuoted(_DocNo) & "' AND ISNULL(FTStateApprove,'')<>'1'"
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If
            '   _Qry = "insert into " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "..TFGTransferFG_Detail"
            '_Qry &= vbCrLf & "(  FTInsUser, FDInsDate, FTInsTime"
            '_Qry &= vbCrLf & ",FTTransferFGNo,FTBarCodeCarton,FNQuantity, FTPackNo , FNCartonNo)"
            '_Qry &= vbCrLf & "SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
            '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DocNo) & "','" & HI.UL.ULF.rpQuoted(Me.Bar) & "'," & Val(Me.Quan) & ""
            '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.Pa) & "','" & HI.UL.ULF.rpQuoted(Me.Car) & "' "
            'If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            'HI.Conn.SQLConn.Tran.Rollback()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '_Spls.Close()
            'Return False
            'End If


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()

            ' HI.MG.ShowMsg.mInfo("Transfer Auto Complete...  ", 1410020005, Me.Text, _DocNo, System.Windows.Forms.MessageBoxIcon.Information)

            Return True

        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return False

        End Try

        Return True
    End Function
   

    Private Sub ogbnote_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles ogbnote.Paint

    End Sub
End Class
