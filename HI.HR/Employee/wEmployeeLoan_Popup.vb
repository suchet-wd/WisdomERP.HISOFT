
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls


Public Class wEmployeeLoan_Popup

#Region " Procedure "

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub wEmployeeLoan_Popup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call LoadCon()
    End Sub


    Private Sub LoadCon()

        ogc.DataSource = _dt


    End Sub
#End Region

#Region "MAIN PROC"


    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub



    Private Sub ReposFCFinAmt112_EditValueChanged(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFCFinAmt112.EditValueChanging

        Try
            'If Val(e.NewValue.ToString) < 0 Then
            '    e.Cancel = True
            'Else
            '    e.Cancel = False

            Dim _Qry As String
            Dim _NewValue As String

            _NewValue = e.NewValue.ToString()
            With Me.ogv

                Dim _FNHSysEmpID As Integer = Val("" & .GetFocusedRowCellValue("FNHSysEmpID").ToString)
                Dim FNCalType As Integer = Val("" & .GetFocusedRowCellValue("FNCalType").ToString)

                If FNCalType = "0" Then
                    _NewValue = _NewValue / 2

                End If

                _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeFin SET FTFinAmt='" & _NewValue & "' "
                _Qry &= vbCrLf & " ,FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Val(_FNHSysEmpID) & " AND  FTFinCode='112' "
                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then

                    _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeFin (FTInsUser, FDInsDate, FTInsTime,  FNHSysEmpID ,FTFinCode,FTFinAmt)"
                    _Qry &= vbCrLf & " SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & "," & Val(_FNHSysEmpID) & ",'112', '" & _NewValue & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                End If
            End With

            'Call _refresh_VerifyImportData_More()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReposFCFinAmt113_EditValueChanged(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFCFinAmt113.EditValueChanging

        Try
            'If Val(e.NewValue.ToString) < 0 Then
            '    e.Cancel = True
            'Else
            '    e.Cancel = False

            Dim _Qry As String
            Dim _NewValue As String

            _NewValue = e.NewValue.ToString()
            With Me.ogv

                Dim _FNHSysEmpID As Integer = Val("" & .GetFocusedRowCellValue("FNHSysEmpID").ToString)
                Dim FNCalType As Integer = Val("" & .GetFocusedRowCellValue("FNCalType").ToString)

                If FNCalType = "0" Then
                    _NewValue = _NewValue / 2

                End If
                _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeFin SET FTFinAmt='" & _NewValue & "' "
                _Qry &= vbCrLf & " ,FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Val(_FNHSysEmpID) & " AND  FTFinCode='113' "
                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then

                    _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeFin (FTInsUser, FDInsDate, FTInsTime,  FNHSysEmpID ,FTFinCode,FTFinAmt)"
                    _Qry &= vbCrLf & " SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & "," & Val(_FNHSysEmpID) & ",'113', '" & _NewValue & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                End If

            End With

            ' Call _refresh_VerifyImportData_More()

        Catch ex As Exception

        End Try
    End Sub

    'Private Sub _refresh_VerifyImportData_More()
    '    Dim dt As DataTable
    '    ' dt = HI.Conn.SQLConn.GetDataTable(_qry_more, Conn.DB.DataBaseName.DB_HR)
    '    With Me.ogc
    '        .DataSource = HI.Conn.SQLConn.GetDataTable(_qry_more, Conn.DB.DataBaseName.DB_HR)
    '        ogv.RefreshData()
    '    End With

    'End Sub

#End Region

#Region "General"

    Private _dt As DataTable
    Public Property DT As DataTable
        Get
            Return _dt
        End Get
        Set(value As DataTable)
            _dt = value
        End Set
    End Property


    Private _qry_more As String
    Public Property Qry_more As String
        Get
            Return _qry_more
        End Get
        Set(value As String)
            _qry_more = value
        End Set

    End Property

#End Region

End Class