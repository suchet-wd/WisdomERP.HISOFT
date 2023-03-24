Imports System.Drawing

Public Class wJobSendSuplPrice

    Private _StateSubNew As Boolean = False
    Private _TFNMarkSpare As Double = 2.0

    Sub New()
        _StateSubNew = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _StateSubNew = False
    End Sub


#Region "Property"

#End Region

#Region "Procedure"

    Public Sub SetInfo(ByVal Key As Object)
        '...call by another form name zzz...
        FTOrderNo.Text = Key.ToString
    End Sub
#End Region

#Region "Function"

    Private Sub LoadOrderDataInfo(OrderKey As String)
        Dim _Cmd As String = ""
        Dim _dt As DataTable

        _Cmd = " Select A.FTOrderNo"
        _Cmd &= vbCrLf & "    ,A.FNHSysPartId "
        _Cmd &= vbCrLf & "  ,A.FNSendSuplType "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            _Cmd &= vbCrLf & ",X.FTNameTH AS FTSendSuplTypeName"
            _Cmd &= vbCrLf & ",PM.FTPartNameTH AS FTPartName "

        Else

            _Cmd &= vbCrLf & ",X.FTNameEN AS FTSendSuplTypeName"
            _Cmd &= vbCrLf & ",PM.FTPartNameEN AS FTPartName"

        End If

        _Cmd &= vbCrLf & " ,ISNULL(XA.FNPrice,0) AS  	FNPrice"
        _Cmd &= vbCrLf & " FROM (SELECT   A.FTOrderNo"
        _Cmd &= vbCrLf & " , B.FNHSysPartId"
        _Cmd &= vbCrLf & " , B.FNSendSuplType"
        _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS A WITH(NOLOCK) INNER JOIN"
        _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl AS B WITH(NOLOCK) ON A.FTOrderProdNo = B.FTOrderProdNo"
        _Cmd &= vbCrLf & " WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderKey) & "'"
        _Cmd &= vbCrLf & " GROUP BY A.FTOrderNo, B.FNHSysPartId, B.FNSendSuplType"
        _Cmd &= vbCrLf & " ) AS A "
        _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS PM WITH(NOLOCK) ON A.FNHSysPartId = PM.FNHSysPartId	"
        _Cmd &= vbCrLf & " INNER JOIN (SELECT FNListIndex, FTNameTH, FTNameEN"
        _Cmd &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)"
        _Cmd &= vbCrLf & "	WHERE  (FTListName = N'FNSendSuplType') "
        _Cmd &= vbCrLf & " ) AS X ON A.FNSendSuplType = X.FNListIndex"
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMSendSuplPrice AS XA WITH(NOLOCK) ON A.FTOrderNo = XA.FTOrderNo "
        _Cmd &= vbCrLf & " AND  A.FNHSysPartId = XA.FNHSysPartId"
        _Cmd &= vbCrLf & " AND  A.FNSendSuplType =XA.FNSendSuplType"

        _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
        Me.ogcdetail.DataSource = _dt

    End Sub

    Private Function SaveData(Key As String) As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Saving....   , Please wait. ")
        Dim _dt As DataTable = Nothing

        Try
            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With
        Catch ex As Exception
        End Try

        Try
            Dim _Qry As String = ""
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            _Qry = "DELETE FROM A "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMSendSuplPrice AS A"
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Key) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            If Not (_dt Is Nothing) Then

                For Each R As DataRow In _dt.Rows

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMSendSuplPrice"
                    _Qry &= vbCrLf & "( FTInsUser, FDInsDate, FTInsTime,FTOrderNo, FNHSysPartId, FNSendSuplType, FNPrice)"
                    _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Key) & "'"
                    _Qry &= vbCrLf & " ," & Val(R!FNHSysPartId.ToString) & ""
                    _Qry &= vbCrLf & " ," & Val(R!FNSendSuplType.ToString) & ""
                    _Qry &= vbCrLf & " ," & Val(R!FNPrice.ToString) & ""

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        _Spls.Close()

                        Return False

                    End If

                Next

            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return False
        End Try

        _Spls.Close()
        Return True

    End Function

    Private Function DeleteData(Key As String) As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Deleting....   , Please wait. ")

        Try

            Dim _Qry As String = ""

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Qry = "  DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMSendSuplPrice AS A"
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Key) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _Spls.Close()

            Return False

        End Try

        _Spls.Close()
        Return True

    End Function

#End Region

    Private Sub wCreateJobProduction_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    End Sub

    Private Sub wCreateJobProduction_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)

        FTOrderNo.Focus()
        FTOrderNo.SelectAll()

    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged

        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
        Else
            Call LoadOrderDataInfo(FTOrderNo.Text)
        End If

    End Sub
  
    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If FTOrderNo.Text <> "" Then
            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave) = True Then

                If Me.SaveData(FTOrderNo.Text) Then

                    Call LoadOrderDataInfo(FTOrderNo.Text.Trim)
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                Else

                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                End If
            End If
        Else
            Me.ogcdetail.DataSource = Nothing
        End If
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        If FTOrderNo.Text <> "" Then

            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete) = True Then
                If Me.DeleteData(FTOrderNo.Text) Then

                    Call LoadOrderDataInfo(FTOrderNo.Text.Trim)
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If

        Else
        Me.ogcdetail.DataSource = Nothing
        End If
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        If FTOrderNo.Text <> "" Then
            Call LoadOrderDataInfo(FTOrderNo.Text.Trim)
        End If
    End Sub
End Class