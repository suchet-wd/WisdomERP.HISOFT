Imports System.ComponentModel
Imports DevExpress.XtraEditors.Controls

Public Class wSMPCreatePriceMultiple


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.



    End Sub

#Region "Property"

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

#Region "Procedure"

    Public Sub LoadOrderDataInfo()

        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")

        Try

            ogcoperation.DataSource = Nothing

            Dim _Qry As String = ""
            Dim _dtprod As DataTable

            _Qry = "SELECT  P.FNSeq,P.FNEmpTeam,  P.FNStartQty , P.FNEndQty , P.FNMultiple   "


            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPriceMultiple AS P With(Nolock)"

            _Qry &= vbCrLf & "  Order By  P.FNEmpTeam  "

            _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            ogcoperation.DataSource = _dtprod.Copy


        Catch ex As Exception
        End Try

        _Spls.Close()
    End Sub



    Private Sub InitNewRow()

        With CType(Me.ogcoperation.DataSource, DataTable)
            .AcceptChanges()
            If .Select("FNEmpTeam<=0").Length <= 0 Then
                If .Select("FNStartQty<=0").Length <= 0 Then
                    If .Select("FNEndQty<=0").Length <= 0 Then
                        If .Select("FNMultiple<=0").Length <= 0 Then


                            .Rows.Add()
                            .Rows(.Rows.Count - 1)!FNSeq = .Rows.Count
                            .Rows(.Rows.Count - 1)!FNStartQty = 0
                            .Rows(.Rows.Count - 1)!FNEndQty = 0
                            .Rows(.Rows.Count - 1)!FNMultiple = 0
                            .Rows(.Rows.Count - 1)!FNEmpTeam = 1

                            .AcceptChanges()


                        End If
                    End If
                End If
            End If

        End With
    End Sub
#End Region

#Region "General"


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ogvoperation_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvoperation.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Down
                With CType(Me.ogcoperation.DataSource, DataTable)

                    .AcceptChanges()


                    Call InitNewRow()



                End With
            Case System.Windows.Forms.Keys.Delete

                If Val(ogvoperation.GetFocusedRowCellValue("FNSeq").ToString) > 1 Then

                    Me.ogvoperation.DeleteRow(Me.ogvoperation.FocusedRowHandle)
                    With CType(Me.ogcoperation.DataSource, DataTable)
                        .AcceptChanges()
                        Dim _RIndx As Integer = 0
                        For Each R In .Rows
                            _RIndx = _RIndx + 1
                            R!FNSeq = _RIndx
                        Next
                        .AcceptChanges()
                    End With
                End If



            Case System.Windows.Forms.Keys.Enter

        End Select
    End Sub


    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click

        CType(Me.ogcoperation.DataSource, DataTable).AcceptChanges()

        With CType(Me.ogcoperation.DataSource, DataTable)
                .AcceptChanges()

                Dim _FNSeq As Integer = 0
                Dim _Spls As New HI.TL.SplashScreen("Saving...Data Please Wait.", Me.Text)
            Dim _Qry As String = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPriceMultiple "

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    End If

                For Each R As DataRow In .Select("FNEmpTeam>0 AND FNStartQty>0 AND FNMultiple >0 AND FNEndQty>0", "FNSeq")


                    _FNSeq = _FNSeq + 1

                    _Qry = "Insert INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPriceMultiple"
                    _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FNSeq,  FNStartQty,FNEndQty,FNMultiple,FNEmpTeam)"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "

                    _Qry &= vbCrLf & " ," & _FNSeq & " "
                    _Qry &= vbCrLf & " ," & Val(R!FNStartQty.ToString) & " "
                    _Qry &= vbCrLf & " ," & Val(R!FNEndQty.ToString) & " "
                    _Qry &= vbCrLf & " ," & Val(R!FNMultiple.ToString) & " "
                    _Qry &= vbCrLf & " ," & Val(R!FNEmpTeam.ToString) & " "

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        _Spls.Close()

                        Exit Sub

                    End If



                Next

                HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Try
                        Call ocmrefresh_Click(ocmrefresh, New System.EventArgs)
                    Catch ex As Exception
                    End Try

                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Catch ex As Exception
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End Try

            End With

    End Sub

    Private Sub wOperationByStyle_Load(sender As Object, e As EventArgs) Handles Me.Load
        ogvoperation.OptionsView.ShowAutoFilterRow = False
        ogvoperation.OptionsSelection.MultiSelect = False
        ogvoperation.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect

        Call LoadOrderDataInfo()

    End Sub

#End Region


    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click

        Me.ogcoperation.DataSource = Nothing
        Call LoadOrderDataInfo()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        ogcoperation.DataSource = Nothing

    End Sub

    Private Sub RepQty_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepQty.EditValueChanging
        Try
            If Val(e.NewValue) < 0 Then
                e.Cancel = True
            Else

                'With ogvoperation
                '    Select Case .FocusedColumn.FieldName
                '        Case "FNEndQty"

                '        Case Then Else
                '            e.Cancel = False
                '    End Select
                'End With
                e.Cancel = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepFNMultiple_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFNMultiple.EditValueChanging
        Try
            If Val(e.NewValue) < 0 Then
                e.Cancel = True
            Else
                e.Cancel = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        InitNewRow()
    End Sub

    Private Sub ocmremove_Click(sender As Object, e As EventArgs) Handles ocmremove.Click
        Try
            Me.ogvoperation.DeleteRow(Me.ogvoperation.FocusedRowHandle)
            With CType(Me.ogcoperation.DataSource, DataTable)
                .AcceptChanges()
                Dim _RIndx As Integer = 0
                For Each R In .Rows
                    _RIndx = _RIndx + 1
                    R!FNSeq = _RIndx
                Next
                .AcceptChanges()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogcoperation_Click(sender As Object, e As EventArgs) Handles ogcoperation.Click

    End Sub

    Private Sub RepFNEmpTeam_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFNEmpTeam.EditValueChanging
        Try
            If Val(e.NewValue) <= 0 Then
                e.Cancel = True
            Else
                e.Cancel = False
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class